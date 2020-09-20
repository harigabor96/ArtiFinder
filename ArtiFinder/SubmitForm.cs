using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtiFinder
{
    internal partial class SubmitForm : Form
    {
        private readonly AppUsers currentUser;
        readonly ArtiFinderDBEntities context = new ArtiFinderDBEntities();
        // A textboxok színe ha az input megfelel a formai követelményeknek
        readonly Color validColor = Color.LightGreen;
        internal SubmitForm(AppUsers currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
            txtAName.TextChanged += TxtAName_TextChanged;
            txtAName.MaxLength = 120;
            txtADesc.TextChanged += TxtADesc_TextChanged;
            txtADesc.MaxLength = 50;
            txtAEarliest.TextChanged += TxtAEarliest_TextChanged;
            txtALatest.TextChanged += TxtALatest_TextChanged;
            txtAPlace.TextChanged += TxtAPlace_TextChanged;
            txtAPlace.MaxLength = 120;
            txtALink.TextChanged += TxtALink_TextChanged;
            txtALink.MaxLength = 2084;
            txtMName.TextChanged += TxtMName_TextChanged;
            txtMName.MaxLength = 120;
            txtMLink.TextChanged += TxtMLink_TextChanged;
            txtMLink.MaxLength = 2084;
            txtMCity.TextChanged += TxtMCity_TextChanged;
            txtMCity.MaxLength = 120;
            txtMAddress.TextChanged += TxtMAddress_TextChanged;
            txtMAddress.MaxLength = 120;
            btnSubmit.Click += BtnSubmit_Click;
            txtMName.Enabled = false;
            txtMLink.Enabled = false;
            listbMCountry.Enabled = false;
            txtMCity.Enabled = false;
            txtMAddress.Enabled = false;
            txtMName.BackColor = Color.LightGray;
            txtMLink.BackColor = Color.LightGray;
            txtMCity.BackColor = Color.LightGray;
            txtMAddress.BackColor = Color.LightGray;
            listbMCountry.BackColor = Color.LightGray;
            checkMuseum.CheckStateChanged += CheckMuseum_CheckStateChanged;
            LoadCountries();
            LoadMuseums();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (!AllInputsValid()) {
                MessageBox.Show("Please check user input!");
                return; 
            }

            try
            {
                int newMuseumSK = 0;
                if (checkMuseum.Checked)
                {
                    Museum newMuseum = new Museum
                    {
                        MuseumName = txtMName.Text,
                        Website = txtMLink.Text,
                        CountryFK = ((Country)listbMCountry.SelectedItem).CountrySK,
                        City = txtMCity.Text,
                        Address = txtMAddress.Text,
                        Approved = false
                    };
                    context.Museum.Add(newMuseum);
                    context.SaveChanges();
                    //MessageBox.Show(newMuseum.MuseumSK.ToString());
                    newMuseumSK = newMuseum.MuseumSK;
                }
                Artifact newArtifact = new Artifact
                {
                    ArtifactName = txtAName.Text,
                    ShortDescrition = (String.IsNullOrEmpty(txtADesc.Text)) ? null : txtADesc.Text,
                    PeriodBegin = int.Parse(txtAEarliest.Text),
                    PeriodEnd = int.Parse(txtALatest.Text),
                    CountryFK = ((Country)listbACountry.SelectedItem).CountrySK,
                    PlaceOfOrigin = (String.IsNullOrEmpty(txtAPlace.Text)) ? null : txtAPlace.Text,
                    Link = txtALink.Text,
                    MuseumFK = (checkMuseum.Checked) ? newMuseumSK : ((Museum)listbMSelect.SelectedItem).MuseumSK,
                    Approved = false
                };
                context.Artifact.Add(newArtifact);
                context.SaveChanges();
                //MessageBox.Show(newArtifact.ArtifactSK.ToString());
                Intermediates newConnection = new Intermediates
                {
                    UserFK = currentUser.UserID,
                    ArtifactFK = newArtifact.ArtifactSK,
                    IsUploader = true
                };
                context.Intermediates.Add(newConnection);
                context.SaveChanges();
                this.Close();
                this.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, please try again!");
            }
        }

        private void LoadMuseums()
        {
            var ownMuseums = (from m in context.Museum
                            join a in context.Artifact on m.MuseumSK equals a.MuseumFK
                            join i in context.Intermediates on a.ArtifactSK equals i.ArtifactFK
                            where !m.Approved && i.UserFK == currentUser.UserID && i.IsUploader == true
                            select m).ToList();
            var apprMuseums = (from m in context.Museum
                             where m.Approved
                             orderby m.MuseumName
                             select m).ToList();
            listbMSelect.DisplayMember = "MuseumName";
            foreach (var apprMuseum in apprMuseums)
            {
                ownMuseums.Add(apprMuseum);
            }
            listbMSelect.DataSource = ownMuseums;
        }

        private void LoadCountries()
        {
            listbACountry.DisplayMember = "CountryName";
            listbMCountry.DisplayMember = "CountryName";
            listbACountry.DataSource = (from c in context.Country
                                        orderby c.CountryName
                                        select c).ToList();
            listbMCountry.DataSource = (from c in context.Country
                                        orderby c.CountryName
                                        select c).ToList();
        }

        private void CheckMuseum_CheckStateChanged(object sender, EventArgs e)
        {
            bool check = ((CheckBox)sender).Checked;
            txtMName.Enabled = check;
            txtMLink.Enabled = check;
            listbMCountry.Enabled = check;
            txtMCity.Enabled = check;
            txtMAddress.Enabled = check;
            listbMSelect.Enabled = !check;

            if (check)
            {
                txtMName.BackColor = Color.White;
                txtMLink.BackColor = Color.White;
                txtMCity.BackColor = Color.White;
                txtMAddress.BackColor = Color.White;
                listbMCountry.BackColor = Color.White;
                listbMSelect.BackColor = Color.LightGray;
            }
            else
            {
                txtMName.Text = "";
                txtMLink.Text = "";
                txtMCity.Text = "";
                txtMAddress.Text = "";
                txtMName.BackColor = Color.LightGray;
                txtMLink.BackColor = Color.LightGray;
                txtMCity.BackColor = Color.LightGray;
                txtMAddress.BackColor = Color.LightGray;
                listbMCountry.BackColor = Color.LightGray;
                listbMSelect.BackColor = Color.White;
            }
            
        }

        private void TxtAName_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), 1, (TextBox)sender);
        }

        private void TxtADesc_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), 1, (TextBox)sender);
            if (((TextBox)sender).Text.Length == 0)
            {
                ((TextBox)sender).BackColor = Color.White;
            }
        }

        private void TxtAEarliest_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^([\-]{0,1}[0-9]+)$"), 1, (TextBox)sender);
            CheckDateConsistency();
        } 

        private void TxtALatest_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^([\-]{0,1}[0-9]+)$"), 1, (TextBox)sender);
            CheckDateConsistency(); 
        }
        private void TxtAPlace_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), 1, (TextBox)sender);
            if (((TextBox)sender).Text.Length == 0)
            {
                ((TextBox)sender).BackColor = Color.White;
            }
        }

        private void TxtALink_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), 1, (TextBox)sender);
        }
        private void TxtMName_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), 1, (TextBox)sender);
        }

        private void TxtMLink_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), 1, (TextBox)sender);
        }
        private void TxtMCity_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), 1, (TextBox)sender);
        }

        private void TxtMAddress_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), 1, (TextBox)sender);
        }

        private void CheckRegex(Regex regex, int minLength, TextBox txt)
        {
            if (regex.IsMatch(txt.Text) && txt.Text.Length >= minLength)
            {

                if (!String.IsNullOrWhiteSpace(txt.Text))
                    txt.BackColor = validColor;
                else
                    txt.BackColor = Color.White;
            }

            else
            {
                txt.BackColor = Color.PaleVioletRed;
            }
        }

        private void CheckDateConsistency()
        {
            if (int.TryParse(txtAEarliest.Text, out int early) && int.TryParse(txtALatest.Text, out int late))
            {
                if (early > late)
                {
                    txtALatest.BackColor = Color.PaleVioletRed;
                }
            }
        }

        private bool AllInputsValid()
        {
            if (txtAName.BackColor == validColor &&
                (txtADesc.BackColor == validColor || txtADesc.Text.Length == 0) &&
                txtAEarliest.BackColor == validColor &&
                txtALatest.BackColor == validColor &&
                (txtAPlace.BackColor == validColor || txtAPlace.Text.Length == 0) &&
                txtALink.BackColor == validColor &&
                (!checkMuseum.Checked || (
                txtMName.BackColor == validColor &&
                txtMLink.BackColor == validColor &&
                txtMCity.BackColor == validColor &&
                txtMAddress.BackColor == validColor
                )))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
