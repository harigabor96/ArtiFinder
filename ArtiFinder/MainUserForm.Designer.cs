namespace ArtiFinder
{
    partial class MainUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUserForm));
            this.btnUser = new System.Windows.Forms.Button();
            this.btnArtifact = new System.Windows.Forms.Button();
            this.labCUser = new System.Windows.Forms.Label();
            this.btnSubm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUser
            // 
            this.btnUser.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnUser.Location = new System.Drawing.Point(253, 12);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(185, 23);
            this.btnUser.TabIndex = 0;
            this.btnUser.Text = "Search by User";
            this.btnUser.UseVisualStyleBackColor = false;
            // 
            // btnArtifact
            // 
            this.btnArtifact.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnArtifact.Location = new System.Drawing.Point(456, 12);
            this.btnArtifact.Name = "btnArtifact";
            this.btnArtifact.Size = new System.Drawing.Size(185, 23);
            this.btnArtifact.TabIndex = 1;
            this.btnArtifact.Text = "Search by Artifact";
            this.btnArtifact.UseVisualStyleBackColor = false;
            // 
            // labCUser
            // 
            this.labCUser.AutoSize = true;
            this.labCUser.Location = new System.Drawing.Point(12, 17);
            this.labCUser.Name = "labCUser";
            this.labCUser.Size = new System.Drawing.Size(35, 13);
            this.labCUser.TabIndex = 2;
            this.labCUser.Text = "label1";
            // 
            // btnSubm
            // 
            this.btnSubm.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnSubm.Location = new System.Drawing.Point(659, 12);
            this.btnSubm.Name = "btnSubm";
            this.btnSubm.Size = new System.Drawing.Size(185, 23);
            this.btnSubm.TabIndex = 3;
            this.btnSubm.Text = "Submit New Artifact";
            this.btnSubm.UseVisualStyleBackColor = false;
            // 
            // MainUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1588, 636);
            this.Controls.Add(this.btnSubm);
            this.Controls.Add(this.labCUser);
            this.Controls.Add(this.btnArtifact);
            this.Controls.Add(this.btnUser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainUserForm";
            this.Text = "ArtiFInder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnArtifact;
        private System.Windows.Forms.Label labCUser;
        private System.Windows.Forms.Button btnSubm;
    }
}