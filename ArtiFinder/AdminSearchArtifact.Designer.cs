namespace ArtiFinder
{
    partial class AdminSearchArtifact
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
            this.btnArtiApprove = new System.Windows.Forms.Button();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnMusApprove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listApprove = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(1317, 104);
            // 
            // labDateWarn
            // 
            this.labDateWarn.Location = new System.Drawing.Point(426, 78);
            // 
            // btnArtiApprove
            // 
            this.btnArtiApprove.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnArtiApprove.Location = new System.Drawing.Point(21, 81);
            this.btnArtiApprove.Name = "btnArtiApprove";
            this.btnArtiApprove.Size = new System.Drawing.Size(208, 23);
            this.btnArtiApprove.TabIndex = 39;
            this.btnArtiApprove.Text = "Approve/Unapporve artifact";
            this.btnArtiApprove.UseVisualStyleBackColor = false;
            // 
            // btnSuspend
            // 
            this.btnSuspend.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnSuspend.Location = new System.Drawing.Point(14, 383);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(140, 23);
            this.btnSuspend.TabIndex = 40;
            this.btnSuspend.Text = "Suspend/unsuspend user";
            this.btnSuspend.UseVisualStyleBackColor = false;
            // 
            // btnAdmin
            // 
            this.btnAdmin.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnAdmin.Location = new System.Drawing.Point(14, 412);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(140, 23);
            this.btnAdmin.TabIndex = 41;
            this.btnAdmin.Text = "Make/unmake user admin";
            this.btnAdmin.UseVisualStyleBackColor = false;
            // 
            // btnMusApprove
            // 
            this.btnMusApprove.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnMusApprove.Location = new System.Drawing.Point(22, 106);
            this.btnMusApprove.Name = "btnMusApprove";
            this.btnMusApprove.Size = new System.Drawing.Size(208, 23);
            this.btnMusApprove.TabIndex = 42;
            this.btnMusApprove.Text = "Approve/Unapporve museum";
            this.btnMusApprove.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1216, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Select attifact approval status";
            // 
            // listApprove
            // 
            this.listApprove.FormattingEnabled = true;
            this.listApprove.Location = new System.Drawing.Point(1220, 48);
            this.listApprove.Name = "listApprove";
            this.listApprove.Size = new System.Drawing.Size(143, 43);
            this.listApprove.TabIndex = 44;
            // 
            // AdminSearchArtifact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.listApprove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMusApprove);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnSuspend);
            this.Controls.Add(this.btnArtiApprove);
            this.Name = "AdminSearchArtifact";
            this.Controls.SetChildIndex(this.labResults, 0);
            this.Controls.SetChildIndex(this.labSearch, 0);
            this.Controls.SetChildIndex(this.labArtiName, 0);
            this.Controls.SetChildIndex(this.labPeriodEnd, 0);
            this.Controls.SetChildIndex(this.labPeriofBegin, 0);
            this.Controls.SetChildIndex(this.labCoo, 0);
            this.Controls.SetChildIndex(this.txtAName, 0);
            this.Controls.SetChildIndex(this.txtALate, 0);
            this.Controls.SetChildIndex(this.txtACountry, 0);
            this.Controls.SetChildIndex(this.txtAEarly, 0);
            this.Controls.SetChildIndex(this.btnList, 0);
            this.Controls.SetChildIndex(this.labMName, 0);
            this.Controls.SetChildIndex(this.labMCity, 0);
            this.Controls.SetChildIndex(this.labMCountry, 0);
            this.Controls.SetChildIndex(this.txtMName, 0);
            this.Controls.SetChildIndex(this.txtMCity, 0);
            this.Controls.SetChildIndex(this.txtMCountry, 0);
            this.Controls.SetChildIndex(this.labDateWarn, 0);
            this.Controls.SetChildIndex(this.btnSubs, 0);
            this.Controls.SetChildIndex(this.btnArtiApprove, 0);
            this.Controls.SetChildIndex(this.btnSuspend, 0);
            this.Controls.SetChildIndex(this.btnAdmin, 0);
            this.Controls.SetChildIndex(this.btnMusApprove, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.listApprove, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnArtiApprove;
        private System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnMusApprove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listApprove;
    }
}
