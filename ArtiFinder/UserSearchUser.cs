using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtiFinder
{
    internal partial class UserSearchUser : UserControl
    {
        private protected readonly AppUsers currentUser;
        private protected readonly ArtiFinderDBEntities context = new ArtiFinderDBEntities();
        /*Az első konstruktor csak a visual inheritance miatt kell, egyébként soha nem lehet meghívva mert a lekérdezések nem működnének 
         élesben ki lesz kommentálva*/
        internal UserSearchUser()
        {
            InitializeComponent();
        }

        internal UserSearchUser(AppUsers currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
            //this.BackColor = Color.Fuchsia;
            txtUser.TextChanged += Txt_TextChanged;
            txtEmail.TextChanged += Txt_TextChanged;
            txtFacebook.TextChanged += Txt_TextChanged;
            txtStatus.TextChanged += Txt_TextChanged;
            btnList.Click += BtnList_Click;
            btnSubs.Click += BtnSubs_Click;
            this.Load += UserSearchUser_Load;
        }

        private void UserSearchUser_Load(object sender, EventArgs e)
        {
            SortSearch();
        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            SortSearch();
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            Search();
        }

        //A saját felhasznló feliratkoztatása a kiválaszotott leletre
        private void BtnSubs_Click(object sender, EventArgs e)
        {
            if (dgwResults.Rows.Count == 0) return;
            Intermediates newSubs = new Intermediates
            {
                UserFK = currentUser.UserID,
                ArtifactFK = Convert.ToInt32(dgwResults.SelectedRows[0].Cells[1].Value),
                IsUploader = false
            };

            Intermediates oldSubs = null;
            foreach (Intermediates connection in context.Intermediates)
            {
                if (connection.UserFK == newSubs.UserFK && connection.ArtifactFK == newSubs.ArtifactFK)
                {
                    oldSubs = connection;
                }
            }
            if (oldSubs != null)
            {
                if (oldSubs.IsUploader)
                {
                    MessageBox.Show("You cannot unsubscribe your own uploads!");
                    return;
                }
                else
                {
                    try
                    {
                        context.Intermediates.Remove(oldSubs);
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Something went wrong, please try again!");
                    };
                }
            }
            else
            {
                try
                {
                    context.Intermediates.Add(newSubs);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong, please try again!");
                };
            }
            ColorSubscribed();
        }

        //Azon leletek zöldre színezése amire a saját felhasználó fel van iratkozva
        private protected void ColorSubscribed()
        {
            var subsByUser = (from i in context.Intermediates
                              where i.UserFK == currentUser.UserID
                              select i.ArtifactFK).ToList();

            for (int i = 0; i < dgwResults.Rows.Count; i++)
            {
                //Resetelés miatt kell a white
                dgwResults.Rows[i].DefaultCellStyle.BackColor = Color.White;
                if (subsByUser.Contains(Convert.ToInt32(dgwResults.Rows[i].Cells[1].Value)))
                {
                    dgwResults.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }

        //Azonnali szűrés userek alapján, a suspended userek szándékosan nem jelnnek meg benne
        private protected virtual void SortSearch()
        {
            //A text null is lehet és ezt a contains() nem tudja értelmezni, ezért if-else
            if (txtFacebook.Text == "")
            {
                dgwSearch.DataSource = (from u in context.AppUsers
                                        join s in context.Statuses on u.StatusFK equals s.StatusSK
                                        where u.UserID.Contains(txtUser.Text) &&
                                              u.Email.Contains(txtEmail.Text) &&                                              
                                              s.StatusName.Contains(txtStatus.Text) &&
                                              !u.Suspended
                                        select new
                                        {
                                            Username = u.UserID,
                                            u.Email,
                                            u.Facebook,
                                            Status = s.StatusName
                                        }).ToList();
            }
            else
            {
                dgwSearch.DataSource = (from u in context.AppUsers
                                        join s in context.Statuses on u.StatusFK equals s.StatusSK
                                        where u.UserID.Contains(txtUser.Text) &&
                                              u.Email.Contains(txtEmail.Text) &&
                                              u.Facebook.Contains(txtFacebook.Text) &&
                                              s.StatusName.Contains(txtStatus.Text) &&
                                              !u.Suspended
                                        select new
                                        {
                                            Username = u.UserID,
                                            u.Email,
                                            u.Facebook,
                                            Status = s.StatusName
                                        }).ToList();
            }
        }

        //Keresés a kiválasztott userhez tartózó artifactekre, a nem approveolt artifactek nem jelennek meg
        private protected virtual void Search()
        {
            if (dgwSearch.Rows.Count == 0) return;
            try
            {
                if (!cboxOwn.Checked)
                {
                    string selectedUsrID = dgwSearch.SelectedRows[0].Cells[0].Value.ToString();
                    labResults.Text = "Artifacts of " + selectedUsrID;
                    dgwResults.DataSource = (from a in context.Artifact
                                             join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                             join ca in context.Country on a.CountryFK equals ca.CountrySK
                                             join cm in context.Country on m.CountryFK equals cm.CountrySK
                                             join i in context.Intermediates on a.ArtifactSK equals i.ArtifactFK
                                             where i.UserFK == selectedUsrID && a.Approved && m.Approved
                                             select new
                                             {
                                                 Name = a.ArtifactName,
                                                 Artifact_ID = a.ArtifactSK,
                                                 Descrition = a.ShortDescrition,
                                                 Earliest_Possible_Date = a.PeriodBegin,
                                                 Latest_Possible_Date = a.PeriodEnd,
                                                 Country_of_origin = ca.CountryName,
                                                 Place_of_origin = a.PlaceOfOrigin,
                                                 Online_source = a.Link,
                                                 Museum_Name = m.MuseumName,
                                                 Museum_Website = m.Website,
                                                 Museum_Country = cm.CountryName,
                                                 Museum_City = m.City,
                                                 Museum_Address = m.Address
                                             }).ToList();
                }
                else
                {
                    string selectedUsrID = dgwSearch.SelectedRows[0].Cells[0].Value.ToString();
                    labResults.Text = "Artifacts of " + selectedUsrID;
                    dgwResults.DataSource = (from a in context.Artifact
                                             join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                             join ca in context.Country on a.CountryFK equals ca.CountrySK
                                             join cm in context.Country on m.CountryFK equals cm.CountrySK
                                             join i in context.Intermediates on a.ArtifactSK equals i.ArtifactFK
                                             where i.UserFK == selectedUsrID && a.Approved && m.Approved && i.IsUploader
                                             select new
                                             {
                                                 Name = a.ArtifactName,
                                                 Artifact_ID = a.ArtifactSK,
                                                 Descrition = a.ShortDescrition,
                                                 Earliest_Possible_Date = a.PeriodBegin,
                                                 Latest_Possible_Date = a.PeriodEnd,
                                                 Country_of_origin = ca.CountryName,
                                                 Place_of_origin = a.PlaceOfOrigin,
                                                 Online_source = a.Link,
                                                 Museum_Name = m.MuseumName,
                                                 Museum_Website = m.Website,
                                                 Museum_Country = cm.CountryName,
                                                 Museum_City = m.City,
                                                 Museum_Address = m.Address
                                             }).ToList();
                }
                ColorSubscribed();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, please try again!");
            }       
        }
    }
}
