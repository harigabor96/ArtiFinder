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
    internal partial class UserSearchArtifact : UserControl
    {
        private protected readonly AppUsers currentUser;
        private protected readonly ArtiFinderDBEntities context = new ArtiFinderDBEntities();
        /*Az első konstruktor csak a visual inheritance miatt kell, egyébként soha nem lehet meghívva mert a lekérdezések nem működnének 
         élesben ki lesz kommentálva*/
        internal UserSearchArtifact()
        {
            InitializeComponent();
        }

        internal UserSearchArtifact(AppUsers currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
            //this.BackColor = Color.Fuchsia;
            txtAName.TextChanged += Txt_TextChanged;
            txtAEarly.TextChanged += Txt_TextChanged;
            txtALate.TextChanged += Txt_TextChanged;
            txtACountry.TextChanged += Txt_TextChanged;
            txtMName.TextChanged += Txt_TextChanged;
            txtMCountry.TextChanged += Txt_TextChanged;
            txtMCity.TextChanged += Txt_TextChanged;   
            btnList.Click += BtnList_Click;
            btnSubs.Click += BtnSubs_Click;
            dgwSearch.DataBindingComplete += DgwSearch_DataBindingComplete;
            this.Load += UserSearchArtifact_Load;
        }

        private void UserSearchArtifact_Load(object sender, EventArgs e)
        {
            SortSearch();
        }

        private void DgwSearch_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorSubscribed();
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
            if (dgwSearch.Rows.Count == 0) return;
            Intermediates newSubs = new Intermediates
            {
                UserFK = currentUser.UserID,
                ArtifactFK = Convert.ToInt32(dgwSearch.SelectedRows[0].Cells[1].Value),
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

            for (int i = 0; i < dgwSearch.Rows.Count; i++)
            {
                //Resetelés miatt kell
                dgwSearch.Rows[i].DefaultCellStyle.BackColor = Color.White;
                if (subsByUser.Contains(Convert.ToInt32(dgwSearch.Rows[i].Cells[1].Value)))
                {
                    dgwSearch.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }

        //Azonnali szűrés artifactek alapján, az unnaproved artifactek szándékosan nem jelnnek meg benne
        private protected virtual void SortSearch()
        {

            if (int.TryParse(txtAEarly.Text, out int earlyUInput) && int.TryParse(txtALate.Text, out int lateUInput))
            {
                dgwSearch.DataSource = (from a in context.Artifact
                                        join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                        join ca in context.Country on a.CountryFK equals ca.CountrySK
                                        join cm in context.Country on m.CountryFK equals cm.CountrySK
                                        where a.ArtifactName.Contains(txtAName.Text) &&
                                              ((earlyUInput <= a.PeriodBegin && a.PeriodBegin <= lateUInput) ||
                                              (earlyUInput <= a.PeriodEnd && a.PeriodEnd <= lateUInput)) &&
                                              ca.CountryName.Contains(txtACountry.Text) &&
                                              m.MuseumName.Contains(txtMName.Text) &&
                                              cm.CountryName.Contains(txtMCountry.Text) &&
                                              m.City.Contains(txtMCity.Text) &&
                                              a.Approved &&
                                              m.Approved
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
            else if (int.TryParse(txtAEarly.Text, out earlyUInput) && !int.TryParse(txtALate.Text, out lateUInput))
            {
                dgwSearch.DataSource = (from a in context.Artifact
                                        join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                        join ca in context.Country on a.CountryFK equals ca.CountrySK
                                        join cm in context.Country on m.CountryFK equals cm.CountrySK
                                        where a.ArtifactName.Contains(txtAName.Text) &&
                                              earlyUInput <= a.PeriodEnd &&
                                              ca.CountryName.Contains(txtACountry.Text) &&
                                              m.MuseumName.Contains(txtMName.Text) &&
                                              cm.CountryName.Contains(txtMCountry.Text) &&
                                              m.City.Contains(txtMCity.Text) &&
                                              a.Approved &&
                                              m.Approved
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
            else if (!int.TryParse(txtAEarly.Text, out earlyUInput) && int.TryParse(txtALate.Text, out lateUInput))
            {
                dgwSearch.DataSource = (from a in context.Artifact
                                        join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                        join ca in context.Country on a.CountryFK equals ca.CountrySK
                                        join cm in context.Country on m.CountryFK equals cm.CountrySK
                                        where a.ArtifactName.Contains(txtAName.Text) &&
                                              a.PeriodBegin <= lateUInput &&
                                              ca.CountryName.Contains(txtACountry.Text) &&
                                              m.MuseumName.Contains(txtMName.Text) &&
                                              cm.CountryName.Contains(txtMCountry.Text) &&
                                              m.City.Contains(txtMCity.Text) &&
                                              a.Approved &&
                                              m.Approved
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
                dgwSearch.DataSource = (from a in context.Artifact
                                        join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                        join ca in context.Country on a.CountryFK equals ca.CountrySK
                                        join cm in context.Country on m.CountryFK equals cm.CountrySK
                                        where a.ArtifactName.Contains(txtAName.Text) &&
                                              ca.CountryName.Contains(txtACountry.Text) &&
                                              m.MuseumName.Contains(txtMName.Text) &&
                                              cm.CountryName.Contains(txtMCountry.Text) &&
                                              m.City.Contains(txtMCity.Text) &&
                                              a.Approved &&
                                              m.Approved
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

        //Keresés a kiválasztott artifacthez tartózó userekre, a suspended userek nem jelennek meg
        private protected virtual void Search()
        {
            if (dgwSearch.Rows.Count == 0) return;
            try
            {
                labResults.Text = "Users subscribed to " + dgwSearch.SelectedRows[0].Cells[0].Value.ToString();
                string artifactID = dgwSearch.SelectedRows[0].Cells[1].Value.ToString();
                dgwResults.DataSource = (from u in context.AppUsers
                                         join s in context.Statuses on u.StatusFK equals s.StatusSK
                                         join i in context.Intermediates on u.UserID equals i.UserFK
                                         where i.ArtifactFK.ToString() == artifactID &&
                                               !u.Suspended
                                         select new
                                         {
                                             Username = u.UserID,
                                             u.Email,
                                             u.Facebook,
                                             Status = s.StatusName,
                                             Uploader = i.IsUploader
                                         }).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, please try again!");
            }         
        }
    }
}
