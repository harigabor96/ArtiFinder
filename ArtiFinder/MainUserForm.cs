using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtiFinder
{
    internal partial class MainUserForm : Form
    {
        private protected readonly AppUsers currentUser;
        //A menüpontok közti váltogatást UserFormok cserélgetésével végeztem
        private UserSearchUser userSrc;
        private UserSearchArtifact artiSrc;
        /*Az első konstruktor csak a visual inheritance miatt kell, egyébként soha nem lehet meghívva mert a lekérdezések nem működnének 
         élesben ki lesz kommentálva*/
        internal MainUserForm()
        {
            InitializeComponent();
        }

        internal MainUserForm(AppUsers currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
            btnUser.Click += BtnUser_Click;
            btnArtifact.Click += BtnArtifact_Click;
            btnSubm.Click += BtnSubm_Click;
            this.FormClosed += MainUserForm_FormClosed;
            labCUser.Text = "User: " + currentUser.UserID;
        }

        //User alapú keresés
        private protected virtual void BtnUser_Click(object sender, EventArgs e)
        {
            ClearUserControls();
            userSrc = new UserSearchUser(currentUser)
            {
                Width = this.Width - 30,
                Top = 40
            };
            userSrc.Height = this.Height - userSrc.Top - 50;
            userSrc.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            this.Controls.Add(userSrc);
        }

        //Artifact alapú keresés
        private protected virtual void BtnArtifact_Click(object sender, EventArgs e)
        {
            ClearUserControls();
            artiSrc = new UserSearchArtifact(currentUser)
            {
                Width = this.Width - 30,
                Top = 40
            };
            artiSrc.Height = this.Height - artiSrc.Top - 50;
            artiSrc.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            this.Controls.Add(artiSrc);
        }

        //Új artifact és museum hozzáadása, ez nem látszik egyből a felületen, csak miután egy admin aprroveolta (adminoknál is szükséges)
        private void BtnSubm_Click(object sender, EventArgs e)
        {
            SubmitForm sbm = new SubmitForm(currentUser);
            sbm.ShowDialog();   
        }

        private protected virtual void ClearUserControls()
        {
            Controls.Remove(artiSrc);
            Controls.Remove(userSrc);
        }

        private void MainUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Azért mert a login form egyébként nyitva maradna
            Application.Exit();
        }
    }
}
