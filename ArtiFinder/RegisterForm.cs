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
    internal partial class RegisterForm : Form
    {
        readonly ArtiFinderDBEntities context = new ArtiFinderDBEntities();
        // Jelszó minimális hossza (több helyen van használva)
        readonly int passwordMin = 6;
        // A textboxok színe ha az input megfelel a formai követelményeknek
        readonly Color validColor = Color.LightGreen;
        internal RegisterForm()
        {
            InitializeComponent();
            txtUsername.MaxLength = 50;
            txtPassword.MaxLength = 50;
            txtEmail.MaxLength = 120;
            txtFacebook.MaxLength = 50;
            txtUsername.TextChanged += TxtUsername_TextChanged;
            txtPassword.TextChanged += TxtPassword_TextChanged;
            txtConf.TextChanged += TxtConf_TextChanged;
            labPasRule.Text = "Password must be longer or equal then " + passwordMin.ToString();
            txtEmail.TextChanged += TxtEmail_TextChanged;
            txtFacebook.TextChanged += TxtFacebook_TextChanged;
            //A státuszok listboxból választhatóak
            LoadStatuses();
            btnCancel.Click += BtnCancel_Click;
            btnRegister.Click += BtnRegister_Click;
            
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {

            if (AllInputsValid())
            {
                var userIDs = (from t in context.AppUsers
                             select t.UserID).ToList();
                if (!userIDs.Contains(txtUsername.Text))
                {
                    AppUsers newUser = new AppUsers()
                    {
                        UserID = txtUsername.Text,
                        Password = LoginForm.SHA512(txtPassword.Text),
                        Email = txtEmail.Text,
                        Facebook = (String.IsNullOrEmpty(txtFacebook.Text)) ? null : txtFacebook.Text,
                        StatusFK = ((Statuses)listBoxStatus.SelectedItem).StatusSK,
                    };
                    try
                    {
                        context.AppUsers.Add(newUser);
                        context.SaveChanges();
                        MessageBox.Show("Registration was successfull!");
                        this.Close();
                        this.Dispose();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Something went wrong, please check if you made any mistakes and then try again!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Username already exists!");
                    return;
                }
            }    
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"),1,(TextBox)sender);
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^(?!\s*$).+"), passwordMin, (TextBox)sender);
        }

        private void TxtConf_TextChanged(object sender, EventArgs e)
        {
            int minLength = passwordMin;
            Regex regex = new Regex(@"^(?!\s*$).+");
            TextBox txt = (TextBox)sender;
            if (regex.IsMatch(txt.Text) && txt.Text.Length >= minLength && txt.Text == txtPassword.Text)
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

        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"(^$)|(^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$)"), 1 ,(TextBox)sender);
        }

        private void TxtFacebook_TextChanged(object sender, EventArgs e)
        {
            CheckRegex(new Regex(@"^([a-zA-Z0-9\.]+)$"), 1, (TextBox)sender);
            if (((TextBox)sender).Text.Length == 0)
            {
                ((TextBox)sender).BackColor = Color.White;
            }
        }

        private void CheckRegex(Regex regex, int minLength ,TextBox txt)
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

        private void LoadStatuses()
        {
           
            listBoxStatus.DisplayMember = "StatusName";
            listBoxStatus.DataSource = (from t in context.Statuses
                                        select t).ToList();
        }

        private bool AllInputsValid()
        {
            if (txtUsername.BackColor == validColor &&
                txtPassword.BackColor == validColor &&
                txtConf.BackColor == validColor &&
                txtEmail.BackColor == validColor &&
                (txtFacebook.BackColor == validColor || txtFacebook.Text.Length == 0))
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
