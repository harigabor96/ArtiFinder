using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ArtiFinder
{
    //A UserSearchUser admin verziója
    internal partial class AdminSearchUser : ArtiFinder.UserSearchUser
    {
        /*Az első konstruktor csak a visual inheritance miatt kell, egyébként soha nem lehet meghívva mert a lekérdezések nem működnének 
         élesben ki lesz kommentálva*/
        internal AdminSearchUser()
        {
            InitializeComponent();
        }

        internal AdminSearchUser(AppUsers currentUser) : base(currentUser)
        {
            InitializeComponent();
            btnSuspend.Click += BtnSuspend_Click;
            btnArtiApprove.Click += BtnArtiApprove_Click;
            btnMusApprove.Click += BtnMusApprove_Click;
            btnAdmin.Click += BtnAdmin_Click;
            listSuspend.DataSource = new List<string> { "All", "Suspended", "Non-Suspended" };
            listSuspend.SelectedIndexChanged += ListSuspend_SelectedIndexChanged;
            SortSearch();
        }

        //Szűrés suspend alapján userekre
        private void ListSuspend_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortSearch();
        }

        //A kiválasztott userből admint csinál, saját usertől nem tudja elvenni, suspended usernek nem lehet admin státuszt adni
        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            if (dgwSearch.Rows.Count == 0) return;
            string curuserID = dgwSearch.SelectedRows[0].Cells[0].Value.ToString();
            var user = (from u in context.AppUsers
                        where u.UserID == curuserID
                        select u).ToList();
            if (user[0].UserID == currentUser.UserID)
            {
                MessageBox.Show("You can't remove admin status from yourself!");
                return;
            }
            if (user[0].Suspended)
            {
                MessageBox.Show("You can't give admin status to a suspended user!");
                return;
            }
            try
            {
                user[0].Admin = !user[0].Admin;
                context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, please try again!");
            }
            SortSearch();
        }

        //Kiválasztott artifact és a hozzá tartozó museum approve státuszának megváltozatása (csak akkor lehet approveolni ha a museum már approveolva van)
        private void BtnArtiApprove_Click(object sender, EventArgs e)
        {
            if (dgwResults.Rows.Count == 0) return;
            int artifactID = Convert.ToInt32(dgwResults.SelectedRows[0].Cells[1].Value);
            var artifact = (from a in context.Artifact
                            where a.ArtifactSK == artifactID
                            select a).ToList();

            var museum = (from a in context.Artifact
                          join m in context.Museum on a.MuseumFK equals m.MuseumSK
                          where a.ArtifactSK == artifactID
                          select m).ToList();
            var uploader = (from i in context.Intermediates
                            where i.ArtifactFK == artifactID && i.IsUploader
                            select i).ToList();
            if (uploader[0].UserFK == currentUser.UserID)
            {
                MessageBox.Show("You can't approve your own uploaded artifact!");
                return;
            }
            if (!museum[0].Approved && !artifact[0].Approved)
            {
                MessageBox.Show("You must approve the museum first!");
                return;
            }
            try
            {
                artifact[0].Approved = !artifact[0].Approved;
                context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, please try again!");
            }
            Search();
        }

        //Kiválasztott museum approve státuszának megváltoztatása
        private void BtnMusApprove_Click(object sender, EventArgs e)
        {
            if (dgwResults.Rows.Count == 0) return;
            int artifactID = Convert.ToInt32(dgwResults.SelectedRows[0].Cells[1].Value);
            var artifact = (from a in context.Artifact
                            where a.ArtifactSK == artifactID
                            select a).ToList();

            var museum = (from a in context.Artifact
                          join m in context.Museum on a.MuseumFK equals m.MuseumSK
                          where a.ArtifactSK == artifactID
                          select m).ToList();
            var uploader = (from i in context.Intermediates
                            where i.ArtifactFK == artifactID && i.IsUploader
                            select i).ToList();
            if (uploader[0].UserFK == currentUser.UserID)
            {
                MessageBox.Show("You can't approve the museum of your own uploaded artifact!");
                return;
            }
            try
            {
                museum[0].Approved = !museum[0].Approved;
                context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, please try again!");
            }
            Search();
        }

        //Kiválasztott user suspend státuszának megváltoztatása (saját felhasználót nem lehet)
        private void BtnSuspend_Click(object sender, EventArgs e)
        {
            if (dgwSearch.Rows.Count == 0) return;
            string curuserID = dgwSearch.SelectedRows[0].Cells[0].Value.ToString();
            var user = (from u in context.AppUsers
                            where u.UserID == curuserID
                            select u).ToList();
            if (user[0].UserID == currentUser.UserID)
            {
                MessageBox.Show("You can't suspend yourself!");
                return;
            }
            try
            {
                user[0].Suspended = !user[0].Suspended;
                context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, please try again!");
            }
            SortSearch();
        }

        //Az adminok számára az összes user látszik suspend státusztól független (Suspended státusz is)
        private protected override void SortSearch()
        {
            //Itt ez az override miatt van az eredeti konstruktor is hívja a SortSearcher és a listSuspend akkor még nem létezik
            if (listSuspend.SelectedItem.ToString() == "All")
            {
                if (txtFacebook.Text == "")
                {
                    dgwSearch.DataSource = (from u in context.AppUsers
                                            join s in context.Statuses on u.StatusFK equals s.StatusSK
                                            where u.UserID.Contains(txtUser.Text) &&
                                                  u.Email.Contains(txtEmail.Text) &&
                                                  s.StatusName.Contains(txtStatus.Text)
                                            select new
                                            {
                                                Username = u.UserID,
                                                u.Suspended,
                                                u.Admin,
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
                                                  s.StatusName.Contains(txtStatus.Text)
                                            select new
                                            {
                                                Username = u.UserID,
                                                u.Suspended,
                                                u.Admin,
                                                u.Email,
                                                u.Facebook,
                                                Status = s.StatusName
                                            }).ToList();
                }
            }
            else if (listSuspend.SelectedItem.ToString() == "Suspended")
            {
                if (txtFacebook.Text == "")
                {
                    dgwSearch.DataSource = (from u in context.AppUsers
                                            join s in context.Statuses on u.StatusFK equals s.StatusSK
                                            where u.UserID.Contains(txtUser.Text) &&
                                                  u.Email.Contains(txtEmail.Text) &&
                                                  s.StatusName.Contains(txtStatus.Text) &&
                                                  u.Suspended
                                            select new
                                            {
                                                Username = u.UserID,
                                                u.Suspended,
                                                u.Admin,
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
                                                  u.Suspended
                                            select new
                                            {
                                                Username = u.UserID,
                                                u.Suspended,
                                                u.Admin,
                                                u.Email,
                                                u.Facebook,
                                                Status = s.StatusName
                                            }).ToList();
                }
            }
            else if (listSuspend.SelectedItem.ToString() == "Non-Suspended")
            {
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
                                                u.Suspended,
                                                u.Admin,
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
                                                u.Suspended,
                                                u.Admin,
                                                u.Email,
                                                u.Facebook,
                                                Status = s.StatusName
                                            }).ToList();
                }
            }
        }

        //Az adminok számára az összes artifact és museum látszik approved státusztól független (Approve státusz is)
        private protected override void Search()
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
                                             where i.UserFK == selectedUsrID
                                             select new
                                             {
                                                 Name = a.ArtifactName,
                                                 Artifact_ID = a.ArtifactSK,
                                                 Artifact_Approved = a.Approved,
                                                 Descrition = a.ShortDescrition,
                                                 Earliest_Possible_Date = a.PeriodBegin,
                                                 Latest_Possible_Date = a.PeriodEnd,
                                                 Country_of_origin = ca.CountryName,
                                                 Place_of_origin = a.PlaceOfOrigin,
                                                 Online_source = a.Link,
                                                 Museum_Name = m.MuseumName,
                                                 Museum_approved = m.Approved,
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
                                             where i.UserFK == selectedUsrID && i.IsUploader
                                             select new
                                             {
                                                 Name = a.ArtifactName,
                                                 Artifact_ID = a.ArtifactSK,
                                                 Artifact_Approved = a.Approved,
                                                 Descrition = a.ShortDescrition,
                                                 Earliest_Possible_Date = a.PeriodBegin,
                                                 Latest_Possible_Date = a.PeriodEnd,
                                                 Country_of_origin = ca.CountryName,
                                                 Place_of_origin = a.PlaceOfOrigin,
                                                 Online_source = a.Link,
                                                 Museum_Name = m.MuseumName,
                                                 Museum_approved = m.Approved,
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
