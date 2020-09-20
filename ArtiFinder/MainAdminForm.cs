using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArtiFinder
{
    //A MainUserForm admin verziója, ami a user controlok admin verzióját hívja meg
    internal partial class MainAdminForm : ArtiFinder.MainUserForm
    {
        private AdminSearchUser userSrc;
        private AdminSearchArtifact artiSrc;
        internal MainAdminForm(AppUsers currentUser) : base(currentUser)
        {
            InitializeComponent();
        }
        private protected override void BtnUser_Click(object sender, EventArgs e)
        {
            ClearUserControls();
            userSrc = new AdminSearchUser(currentUser)
            {
                Width = this.Width - 30,
                Top = 40
            };
            userSrc.Height = this.Height - userSrc.Top - 50;
            userSrc.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            this.Controls.Add(userSrc);
        }

        private protected override void BtnArtifact_Click(object sender, EventArgs e)
        {
            ClearUserControls();
            artiSrc = new AdminSearchArtifact(currentUser)
            {
                Width = this.Width - 30,
                Top = 40
            };
            artiSrc.Height = this.Height - artiSrc.Top - 50;
            artiSrc.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            this.Controls.Add(artiSrc);
        }

        private protected override void ClearUserControls()
        {
            Controls.Remove(artiSrc);
            Controls.Remove(userSrc);
        }
    }
}
