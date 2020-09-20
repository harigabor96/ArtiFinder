namespace ArtiFinder
{
    partial class UserSearchArtifact
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
            this.btnList = new System.Windows.Forms.Button();
            this.txtAEarly = new System.Windows.Forms.TextBox();
            this.txtACountry = new System.Windows.Forms.TextBox();
            this.txtALate = new System.Windows.Forms.TextBox();
            this.txtAName = new System.Windows.Forms.TextBox();
            this.labCoo = new System.Windows.Forms.Label();
            this.labPeriofBegin = new System.Windows.Forms.Label();
            this.labPeriodEnd = new System.Windows.Forms.Label();
            this.labArtiName = new System.Windows.Forms.Label();
            this.labSearch = new System.Windows.Forms.Label();
            this.labResults = new System.Windows.Forms.Label();
            this.dgwSearch = new System.Windows.Forms.DataGridView();
            this.dgwResults = new System.Windows.Forms.DataGridView();
            this.txtMCountry = new System.Windows.Forms.TextBox();
            this.txtMCity = new System.Windows.Forms.TextBox();
            this.txtMName = new System.Windows.Forms.TextBox();
            this.labMCountry = new System.Windows.Forms.Label();
            this.labMCity = new System.Windows.Forms.Label();
            this.labMName = new System.Windows.Forms.Label();
            this.labDateWarn = new System.Windows.Forms.Label();
            this.btnSubs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnList
            // 
            this.btnList.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnList.Location = new System.Drawing.Point(1351, 71);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(151, 23);
            this.btnList.TabIndex = 26;
            this.btnList.Text = "List users!";
            this.btnList.UseVisualStyleBackColor = false;
            // 
            // txtAEarly
            // 
            this.txtAEarly.Location = new System.Drawing.Point(272, 53);
            this.txtAEarly.Name = "txtAEarly";
            this.txtAEarly.Size = new System.Drawing.Size(151, 20);
            this.txtAEarly.TabIndex = 25;
            // 
            // txtACountry
            // 
            this.txtACountry.Location = new System.Drawing.Point(586, 53);
            this.txtACountry.Name = "txtACountry";
            this.txtACountry.Size = new System.Drawing.Size(151, 20);
            this.txtACountry.TabIndex = 24;
            // 
            // txtALate
            // 
            this.txtALate.Location = new System.Drawing.Point(429, 53);
            this.txtALate.Name = "txtALate";
            this.txtALate.Size = new System.Drawing.Size(151, 20);
            this.txtALate.TabIndex = 23;
            // 
            // txtAName
            // 
            this.txtAName.Location = new System.Drawing.Point(112, 53);
            this.txtAName.Name = "txtAName";
            this.txtAName.Size = new System.Drawing.Size(151, 20);
            this.txtAName.TabIndex = 22;
            // 
            // labCoo
            // 
            this.labCoo.AutoSize = true;
            this.labCoo.Location = new System.Drawing.Point(586, 37);
            this.labCoo.Name = "labCoo";
            this.labCoo.Size = new System.Drawing.Size(119, 13);
            this.labCoo.TabIndex = 21;
            this.labCoo.Text = "Sort by Country of origin";
            // 
            // labPeriofBegin
            // 
            this.labPeriofBegin.AutoSize = true;
            this.labPeriofBegin.Location = new System.Drawing.Point(269, 37);
            this.labPeriofBegin.Name = "labPeriofBegin";
            this.labPeriofBegin.Size = new System.Drawing.Size(146, 13);
            this.labPeriofBegin.TabIndex = 20;
            this.labPeriofBegin.Text = "Sort by Earliest possible date*";
            // 
            // labPeriodEnd
            // 
            this.labPeriodEnd.AutoSize = true;
            this.labPeriodEnd.Location = new System.Drawing.Point(429, 37);
            this.labPeriodEnd.Name = "labPeriodEnd";
            this.labPeriodEnd.Size = new System.Drawing.Size(141, 13);
            this.labPeriodEnd.TabIndex = 19;
            this.labPeriodEnd.Text = "Sort by Latest possible date*";
            // 
            // labArtiName
            // 
            this.labArtiName.AutoSize = true;
            this.labArtiName.Location = new System.Drawing.Point(113, 37);
            this.labArtiName.Name = "labArtiName";
            this.labArtiName.Size = new System.Drawing.Size(105, 13);
            this.labArtiName.TabIndex = 18;
            this.labArtiName.Text = "Sort by Artifact name";
            // 
            // labSearch
            // 
            this.labSearch.AutoSize = true;
            this.labSearch.Location = new System.Drawing.Point(236, 114);
            this.labSearch.Name = "labSearch";
            this.labSearch.Size = new System.Drawing.Size(460, 13);
            this.labSearch.TabIndex = 17;
            this.labSearch.Text = "Select an Artifact below and push \'List users!\' to view the users subscribed to t" +
    "he chosen artifact";
            // 
            // labResults
            // 
            this.labResults.AutoSize = true;
            this.labResults.Location = new System.Drawing.Point(412, 367);
            this.labResults.Name = "labResults";
            this.labResults.Size = new System.Drawing.Size(100, 13);
            this.labResults.TabIndex = 16;
            this.labResults.Text = "Users subscribed to";
            // 
            // dgwSearch
            // 
            this.dgwSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwSearch.BackgroundColor = System.Drawing.Color.Khaki;
            this.dgwSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwSearch.Location = new System.Drawing.Point(21, 135);
            this.dgwSearch.MultiSelect = false;
            this.dgwSearch.Name = "dgwSearch";
            this.dgwSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwSearch.Size = new System.Drawing.Size(1580, 229);
            this.dgwSearch.TabIndex = 15;
            // 
            // dgwResults
            // 
            this.dgwResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwResults.BackgroundColor = System.Drawing.Color.Khaki;
            this.dgwResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwResults.Location = new System.Drawing.Point(160, 383);
            this.dgwResults.MultiSelect = false;
            this.dgwResults.Name = "dgwResults";
            this.dgwResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwResults.Size = new System.Drawing.Size(1359, 162);
            this.dgwResults.TabIndex = 14;
            // 
            // txtMCountry
            // 
            this.txtMCountry.Location = new System.Drawing.Point(906, 53);
            this.txtMCountry.Name = "txtMCountry";
            this.txtMCountry.Size = new System.Drawing.Size(151, 20);
            this.txtMCountry.TabIndex = 34;
            // 
            // txtMCity
            // 
            this.txtMCity.Location = new System.Drawing.Point(1063, 53);
            this.txtMCity.Name = "txtMCity";
            this.txtMCity.Size = new System.Drawing.Size(151, 20);
            this.txtMCity.TabIndex = 32;
            // 
            // txtMName
            // 
            this.txtMName.Location = new System.Drawing.Point(746, 53);
            this.txtMName.Name = "txtMName";
            this.txtMName.Size = new System.Drawing.Size(151, 20);
            this.txtMName.TabIndex = 31;
            // 
            // labMCountry
            // 
            this.labMCountry.AutoSize = true;
            this.labMCountry.Location = new System.Drawing.Point(903, 37);
            this.labMCountry.Name = "labMCountry";
            this.labMCountry.Size = new System.Drawing.Size(133, 13);
            this.labMCountry.TabIndex = 29;
            this.labMCountry.Text = "Sort by Country of museum";
            // 
            // labMCity
            // 
            this.labMCity.AutoSize = true;
            this.labMCity.Location = new System.Drawing.Point(1063, 37);
            this.labMCity.Name = "labMCity";
            this.labMCity.Size = new System.Drawing.Size(114, 13);
            this.labMCity.TabIndex = 28;
            this.labMCity.Text = "Sort by City of museum";
            // 
            // labMName
            // 
            this.labMName.AutoSize = true;
            this.labMName.Location = new System.Drawing.Point(747, 37);
            this.labMName.Name = "labMName";
            this.labMName.Size = new System.Drawing.Size(112, 13);
            this.labMName.TabIndex = 27;
            this.labMName.Text = "Sort by Museum name";
            // 
            // labDateWarn
            // 
            this.labDateWarn.AutoSize = true;
            this.labDateWarn.Location = new System.Drawing.Point(322, 76);
            this.labDateWarn.Name = "labDateWarn";
            this.labDateWarn.Size = new System.Drawing.Size(216, 13);
            this.labDateWarn.TabIndex = 35;
            this.labDateWarn.Text = "*Please use negative numbers instead of BC";
            // 
            // btnSubs
            // 
            this.btnSubs.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnSubs.Location = new System.Drawing.Point(1014, 104);
            this.btnSubs.Name = "btnSubs";
            this.btnSubs.Size = new System.Drawing.Size(180, 23);
            this.btnSubs.TabIndex = 36;
            this.btnSubs.Text = "Unsubscribe/Subscribe to artifact";
            this.btnSubs.UseVisualStyleBackColor = false;
            // 
            // UserSearchArtifact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Controls.Add(this.btnSubs);
            this.Controls.Add(this.labDateWarn);
            this.Controls.Add(this.txtMCountry);
            this.Controls.Add(this.txtMCity);
            this.Controls.Add(this.txtMName);
            this.Controls.Add(this.labMCountry);
            this.Controls.Add(this.labMCity);
            this.Controls.Add(this.labMName);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.txtAEarly);
            this.Controls.Add(this.txtACountry);
            this.Controls.Add(this.txtALate);
            this.Controls.Add(this.txtAName);
            this.Controls.Add(this.labCoo);
            this.Controls.Add(this.labPeriofBegin);
            this.Controls.Add(this.labPeriodEnd);
            this.Controls.Add(this.labArtiName);
            this.Controls.Add(this.labSearch);
            this.Controls.Add(this.labResults);
            this.Controls.Add(this.dgwSearch);
            this.Controls.Add(this.dgwResults);
            this.Name = "UserSearchArtifact";
            this.Size = new System.Drawing.Size(1604, 548);
            ((System.ComponentModel.ISupportInitialize)(this.dgwSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnList;
        protected System.Windows.Forms.TextBox txtAEarly;
        protected System.Windows.Forms.TextBox txtACountry;
        protected System.Windows.Forms.TextBox txtALate;
        protected System.Windows.Forms.TextBox txtAName;
        protected System.Windows.Forms.Label labCoo;
        protected System.Windows.Forms.Label labPeriofBegin;
        protected System.Windows.Forms.Label labPeriodEnd;
        protected System.Windows.Forms.Label labArtiName;
        protected System.Windows.Forms.Label labSearch;
        protected System.Windows.Forms.Label labResults;
        protected System.Windows.Forms.DataGridView dgwSearch;
        protected System.Windows.Forms.DataGridView dgwResults;
        protected System.Windows.Forms.TextBox txtMCountry;
        protected System.Windows.Forms.TextBox txtMCity;
        protected System.Windows.Forms.TextBox txtMName;
        protected System.Windows.Forms.Label labMCountry;
        protected System.Windows.Forms.Label labMCity;
        protected System.Windows.Forms.Label labMName;
        protected System.Windows.Forms.Label labDateWarn;
        protected System.Windows.Forms.Button btnSubs;
    }
}
