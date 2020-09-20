using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ArtiFinder
{
    internal partial class LoginForm : Form
    {
        // Rendelkezésre áll egy testAdmin felhasználó, jelszó: 1
        // Illetve egy testUser ugyanezzel a jelszóval
        readonly ArtiFinderDBEntities context = new ArtiFinderDBEntities();
        internal LoginForm()
        {
            InitializeComponent();
            btnReg.Click += BtnReg_Click;
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var usr = (from t in context.AppUsers where t.UserID == txtUsername.Text select t).ToList();
            
            if( usr.Count == 1 && usr[0].Password == SHA512(txtPassword.Text) && usr[0].UserID == txtUsername.Text )    
            {               
                // A hide-okat Application.Exit()-el kompenzálom a Main formok FormClosed eseményinél
                // Suspended userek nem léphetnek be
                if (usr[0].Suspended)
                {
                    MessageBox.Show("You've been suspended, contact and admin for further information!");
                    return;
                }
                // Felhasználói felület megnyitása user verzióba
                if (!usr[0].Admin)
                {
                    MainUserForm mainForm = new MainUserForm(usr[0]);
                    this.Hide();
                    mainForm.ShowDialog();
                    mainForm.BringToFront();
                }
                else
                {
                    // admin verzióban
                    MainAdminForm mainForm = new MainAdminForm(usr[0]);
                    this.Hide();
                    mainForm.ShowDialog();
                    mainForm.BringToFront();
                }
            } 
            else     
            {
                MessageBox.Show("Wrong username and/or password!");
            }
        }

        private void BtnReg_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.ShowDialog();
        }

        //Jelszó titkosító metódus
        static internal string SHA512(string password)
        {
            string salt = "34weo43dfhzrzrgf4kl334";
            string HashInput = password + salt;
            byte[] hash;

            using (var sha512 = new SHA512CryptoServiceProvider())
            {
                hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(HashInput));
            }
            var sb = new StringBuilder();

            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }
    }
}
