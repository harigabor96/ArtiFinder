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
    //A UserSearchArtifact admin verziója
    internal partial class AdminSearchArtifact : ArtiFinder.UserSearchArtifact
    {
        /*Az első konstruktor csak a visual inheritance miatt kell, egyébként soha nem lehet meghívva mert a lekérdezések nem működnének 
         élesben ki lesz kommentálva*/
        internal AdminSearchArtifact()
        {
            InitializeComponent();
        }

        internal AdminSearchArtifact(AppUsers currentUser) : base(currentUser)
        {
            InitializeComponent();
            btnArtiApprove.Click += BtnArtiApprove_Click;
            btnMusApprove.Click += BtnMusApprove_Click;
            btnSuspend.Click += BtnSuspend_Click;
            btnAdmin.Click += BtnAdmin_Click;
            listApprove.DataSource = new List<string> { "All", "Approved", "Unapproved" };
            listApprove.SelectedIndexChanged += ListApprove_SelectedIndexChanged;
            SortSearch();
        }

        //Szűrés artifact approval státusz alapján
        private void ListApprove_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortSearch();
        }

        //A kiválasztott userből admint csinál, saját usertől nem tudja elvenni, suspended usernek nem lehet admin státuszt adni
        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            if (dgwResults.Rows.Count == 0) return;
            string curuserID = dgwResults.SelectedRows[0].Cells[0].Value.ToString();
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
            Search();
        }

        //Kiválasztott user suspend státuszának megváltoztatása (saját felhasználót nem lehet)
        private void BtnSuspend_Click(object sender, EventArgs e)
        {
            if (dgwResults.Rows.Count == 0) return;
            string curuserID = dgwResults.SelectedRows[0].Cells[0].Value.ToString();
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
            Search();
        }

        //Kiválasztott artifact és a hozzá tartozó museum approve státuszának megváltozatása (csak akkor lehet approveolni ha a museum már approveolva van)
        private void BtnArtiApprove_Click(object sender, EventArgs e)
        {
            if (dgwSearch.Rows.Count == 0) return;
            int artifactID = Convert.ToInt32(dgwSearch.SelectedRows[0].Cells[1].Value);
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
                MessageBox.Show("You must approve the museum fisrt!");
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
            SortSearch();
        }

        //Kiválasztott museum approve státuszának megváltoztatása
        private void BtnMusApprove_Click(object sender, EventArgs e)
        {
            if (dgwSearch.Rows.Count == 0) return;
            int artifactID = Convert.ToInt32(dgwSearch.SelectedRows[0].Cells[1].Value);
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
            SortSearch();
        }

        //Az adminok számára az összes artifact és museum látszik approved státusztól független (approved státusz is)
        private protected override void SortSearch()
        {
            if (listApprove.SelectedItem.ToString() == "All")
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
                                                  m.City.Contains(txtMCity.Text)
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
                                                  m.City.Contains(txtMCity.Text)
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
                                                  m.City.Contains(txtMCity.Text)
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
                    dgwSearch.DataSource = (from a in context.Artifact
                                            join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                            join ca in context.Country on a.CountryFK equals ca.CountrySK
                                            join cm in context.Country on m.CountryFK equals cm.CountrySK
                                            where a.ArtifactName.Contains(txtAName.Text) &&
                                                  ca.CountryName.Contains(txtACountry.Text) &&
                                                  m.MuseumName.Contains(txtMName.Text) &&
                                                  cm.CountryName.Contains(txtMCountry.Text) &&
                                                  m.City.Contains(txtMCity.Text)
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
            }
            else if (listApprove.SelectedItem.ToString() == "Approved")
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
                                                  a.Approved
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
                                                  a.Approved
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
                                                  a.Approved
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
                    dgwSearch.DataSource = (from a in context.Artifact
                                            join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                            join ca in context.Country on a.CountryFK equals ca.CountrySK
                                            join cm in context.Country on m.CountryFK equals cm.CountrySK
                                            where a.ArtifactName.Contains(txtAName.Text) &&
                                                  ca.CountryName.Contains(txtACountry.Text) &&
                                                  m.MuseumName.Contains(txtMName.Text) &&
                                                  cm.CountryName.Contains(txtMCountry.Text) &&
                                                  m.City.Contains(txtMCity.Text) &&
                                                  a.Approved
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
            }
            else if (listApprove.SelectedItem.ToString() == "Unapproved")
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
                                                  !a.Approved
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
                                                  !a.Approved
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
                                                  !a.Approved
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
                    dgwSearch.DataSource = (from a in context.Artifact
                                            join m in context.Museum on a.MuseumFK equals m.MuseumSK
                                            join ca in context.Country on a.CountryFK equals ca.CountrySK
                                            join cm in context.Country on m.CountryFK equals cm.CountrySK
                                            where a.ArtifactName.Contains(txtAName.Text) &&
                                                  ca.CountryName.Contains(txtACountry.Text) &&
                                                  m.MuseumName.Contains(txtMName.Text) &&
                                                  cm.CountryName.Contains(txtMCountry.Text) &&
                                                  m.City.Contains(txtMCity.Text) &&
                                                  !a.Approved
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
            }
            ColorSubscribed();                   
        }

        //Az adminok számára az összes user látszik suspend státusztól független (Suspended státusz is)
        private protected override void Search()
        {
            if (dgwSearch.Rows.Count == 0) return;
            try
            {
                labResults.Text = "Users subscribed to " + dgwSearch.SelectedRows[0].Cells[0].Value.ToString();
                string artifactID = dgwSearch.SelectedRows[0].Cells[1].Value.ToString();
                dgwResults.DataSource = (from u in context.AppUsers
                                         join s in context.Statuses on u.StatusFK equals s.StatusSK
                                         join i in context.Intermediates on u.UserID equals i.UserFK
                                         where i.ArtifactFK.ToString() == artifactID                                              
                                         select new
                                         {
                                             Username = u.UserID,
                                             u.Suspended,
                                             u.Admin,
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
