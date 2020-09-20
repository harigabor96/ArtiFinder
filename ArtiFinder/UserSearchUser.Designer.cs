namespace ArtiFinder
{
    partial class UserSearchUser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgwResults = new System.Windows.Forms.DataGridView();
            this.dgwSearch = new System.Windows.Forms.DataGridView();
            this.labResults = new System.Windows.Forms.Label();
            this.labSearch = new System.Windows.Forms.Label();
            this.labUser = new System.Windows.Forms.Label();
            this.labEmail = new System.Windows.Forms.Label();
            this.labFacebook = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtFacebook = new System.Windows.Forms.TextBox();
            this.btnList = new System.Windows.Forms.Button();
            this.cboxOwn = new System.Windows.Forms.CheckBox();
            this.btnSubs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwResults
            // 
            this.dgwResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwResults.BackgroundColor = System.Drawing.Color.Khaki;
            this.dgwResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwResults.Location = new System.Drawing.Point(21, 275);
            this.dgwResults.MultiSelect = false;
            this.dgwResults.Name = "dgwResults";
            this.dgwResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwResults.Size = new System.Drawing.Size(1573, 260);
            this.dgwResults.TabIndex = 0;
            // 
            // dgwSearch
            // 
            this.dgwSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwSearch.BackgroundColor = System.Drawing.Color.Khaki;
            this.dgwSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwSearch.Location = new System.Drawing.Point(210, 33);
            this.dgwSearch.MultiSelect = false;
            this.dgwSearch.Name = "dgwSearch";
            this.dgwSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwSearch.Size = new System.Drawing.Size(1384, 190);
            this.dgwSearch.TabIndex = 1;
            // 
            // labResults
            // 
            this.labResults.AutoSize = true;
            this.labResults.Location = new System.Drawing.Point(322, 246);
            this.labResults.Name = "labResults";
            this.labResults.Size = new System.Drawing.Size(60, 13);
            this.labResults.TabIndex = 2;
            this.labResults.Text = "Artifacts of ";
            // 
            // labSearch
            // 
            this.labSearch.AutoSize = true;
            this.labSearch.Location = new System.Drawing.Point(267, 17);
            this.labSearch.Name = "labSearch";
            this.labSearch.Size = new System.Drawing.Size(319, 13);
            this.labSearch.TabIndex = 3;
            this.labSearch.Text = "Select User below and push \'List artifacts!\' to view his/her artifacts";
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.Location = new System.Drawing.Point(34, 17);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(65, 13);
            this.labUser.TabIndex = 4;
            this.labUser.Text = "Sort by User";
            // 
            // labEmail
            // 
            this.labEmail.AutoSize = true;
            this.labEmail.Location = new System.Drawing.Point(34, 56);
            this.labEmail.Name = "labEmail";
            this.labEmail.Size = new System.Drawing.Size(68, 13);
            this.labEmail.TabIndex = 5;
            this.labEmail.Text = "Sort by Email";
            // 
            // labFacebook
            // 
            this.labFacebook.AutoSize = true;
            this.labFacebook.Location = new System.Drawing.Point(34, 95);
            this.labFacebook.Name = "labFacebook";
            this.labFacebook.Size = new System.Drawing.Size(91, 13);
            this.labFacebook.TabIndex = 6;
            this.labFacebook.Text = "Sort by Facebook";
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(34, 134);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(73, 13);
            this.labStatus.TabIndex = 7;
            this.labStatus.Text = "Sort by Status";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(37, 33);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(133, 20);
            this.txtUser.TabIndex = 8;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(37, 72);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(133, 20);
            this.txtEmail.TabIndex = 9;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(37, 150);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(133, 20);
            this.txtStatus.TabIndex = 10;
            // 
            // txtFacebook
            // 
            this.txtFacebook.Location = new System.Drawing.Point(37, 111);
            this.txtFacebook.Name = "txtFacebook";
            this.txtFacebook.Size = new System.Drawing.Size(133, 20);
            this.txtFacebook.TabIndex = 11;
            // 
            // btnList
            // 
            this.btnList.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnList.Location = new System.Drawing.Point(37, 200);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(133, 23);
            this.btnList.TabIndex = 12;
            this.btnList.Text = "List artifacts!";
            this.btnList.UseVisualStyleBackColor = false;
            // 
            // cboxOwn
            // 
            this.cboxOwn.AutoSize = true;
            this.cboxOwn.Location = new System.Drawing.Point(37, 176);
            this.cboxOwn.Name = "cboxOwn";
            this.cboxOwn.Size = new System.Drawing.Size(125, 17);
            this.cboxOwn.TabIndex = 13;
            this.cboxOwn.Text = "Only list own uploads";
            this.cboxOwn.UseVisualStyleBackColor = true;
            // 
            // btnSubs
            // 
            this.btnSubs.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnSubs.Location = new System.Drawing.Point(572, 241);
            this.btnSubs.Name = "btnSubs";
            this.btnSubs.Size = new System.Drawing.Size(163, 23);
            this.btnSubs.TabIndex = 14;
            this.btnSubs.Text = "Unsubscribe/Subscribe to artifact";
            this.btnSubs.UseVisualStyleBackColor = false;
            // 
            // UserSearchUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Controls.Add(this.btnSubs);
            this.Controls.Add(this.cboxOwn);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.txtFacebook);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.labFacebook);
            this.Controls.Add(this.labEmail);
            this.Controls.Add(this.labUser);
            this.Controls.Add(this.labSearch);
            this.Controls.Add(this.labResults);
            this.Controls.Add(this.dgwSearch);
            this.Controls.Add(this.dgwResults);
            this.Name = "UserSearchUser";
            this.Size = new System.Drawing.Size(1604, 548);
            ((System.ComponentModel.ISupportInitialize)(this.dgwResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.DataGridView dgwResults;
        protected System.Windows.Forms.DataGridView dgwSearch;
        protected System.Windows.Forms.Label labResults;
        protected System.Windows.Forms.Label labSearch;
        protected System.Windows.Forms.Label labUser;
        protected System.Windows.Forms.Label labEmail;
        protected System.Windows.Forms.Label labFacebook;
        protected System.Windows.Forms.Label labStatus;
        protected System.Windows.Forms.TextBox txtUser;
        protected System.Windows.Forms.TextBox txtEmail;
        protected System.Windows.Forms.TextBox txtStatus;
        protected System.Windows.Forms.TextBox txtFacebook;
        protected System.Windows.Forms.Button btnList;
        protected System.Windows.Forms.CheckBox cboxOwn;
        protected System.Windows.Forms.Button btnSubs;
    }
}
