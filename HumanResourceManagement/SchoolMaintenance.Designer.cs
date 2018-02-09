namespace HumanResourceManagement
{
    partial class SchoolMaintenance
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchoolMaintenance));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEditSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.cmbBy = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.txtEmplyeeNo = new MetroFramework.Controls.MetroTextBox();
            this.infoGroup = new System.Windows.Forms.GroupBox();
            this.lblSelectedSchoolId = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.PictureBox();
            this.txtdistrict = new MetroFramework.Controls.MetroTextBox();
            this.txtSchoolname = new MetroFramework.Controls.MetroTextBox();
            this.txtSchoolId = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvSchools = new MetroFramework.Controls.MetroGrid();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFormExit = new System.Windows.Forms.Label();
            this.bgSchoolLoader = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.infoGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNew)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchools)).BeginInit();
            this.FormPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblResults);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnEditSave);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.infoGroup);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.FormPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 457);
            this.panel1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(551, 425);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(97, 24);
            this.btnRefresh.TabIndex = 37;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(449, 425);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(97, 24);
            this.btnDelete.TabIndex = 34;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Results:";
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(59, 427);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(13, 13);
            this.lblResults.TabIndex = 35;
            this.lblResults.Text = "0";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(243, 425);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 24);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEditSave
            // 
            this.btnEditSave.Enabled = false;
            this.btnEditSave.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditSave.Location = new System.Drawing.Point(346, 425);
            this.btnEditSave.Name = "btnEditSave";
            this.btnEditSave.Size = new System.Drawing.Size(97, 24);
            this.btnEditSave.TabIndex = 33;
            this.btnEditSave.Text = "Edit";
            this.btnEditSave.UseVisualStyleBackColor = true;
            this.btnEditSave.Click += new System.EventHandler(this.btnEditSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroLabel4);
            this.groupBox1.Controls.Add(this.cmbBy);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.txtEmplyeeNo);
            this.groupBox1.Location = new System.Drawing.Point(17, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(631, 40);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroLabel4.Enabled = false;
            this.metroLabel4.Location = new System.Drawing.Point(403, 13);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(27, 19);
            this.metroLabel4.TabIndex = 101;
            this.metroLabel4.Text = "BY:";
            // 
            // cmbBy
            // 
            this.cmbBy.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cmbBy.FormattingEnabled = true;
            this.cmbBy.IntegralHeight = false;
            this.cmbBy.ItemHeight = 19;
            this.cmbBy.Items.AddRange(new object[] {
            "School ID",
            "School Name",
            "District"});
            this.cmbBy.Location = new System.Drawing.Point(436, 10);
            this.cmbBy.Name = "cmbBy";
            this.cmbBy.Size = new System.Drawing.Size(181, 25);
            this.cmbBy.TabIndex = 100;
            this.cmbBy.UseSelectable = true;
            this.cmbBy.SelectedIndexChanged += new System.EventHandler(this.cmbBy_SelectedIndexChanged);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroLabel3.Enabled = false;
            this.metroLabel3.Location = new System.Drawing.Point(7, 13);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(61, 19);
            this.metroLabel3.TabIndex = 30;
            this.metroLabel3.Text = "SEARCH:";
            // 
            // txtEmplyeeNo
            // 
            // 
            // 
            // 
            this.txtEmplyeeNo.CustomButton.Image = null;
            this.txtEmplyeeNo.CustomButton.Location = new System.Drawing.Point(250, 1);
            this.txtEmplyeeNo.CustomButton.Name = "";
            this.txtEmplyeeNo.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtEmplyeeNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtEmplyeeNo.CustomButton.TabIndex = 1;
            this.txtEmplyeeNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmplyeeNo.CustomButton.UseSelectable = true;
            this.txtEmplyeeNo.CustomButton.Visible = false;
            this.txtEmplyeeNo.Lines = new string[0];
            this.txtEmplyeeNo.Location = new System.Drawing.Point(121, 10);
            this.txtEmplyeeNo.MaxLength = 30;
            this.txtEmplyeeNo.Name = "txtEmplyeeNo";
            this.txtEmplyeeNo.PasswordChar = '\0';
            this.txtEmplyeeNo.PromptText = "Press Enter To Search";
            this.txtEmplyeeNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmplyeeNo.SelectedText = "";
            this.txtEmplyeeNo.SelectionLength = 0;
            this.txtEmplyeeNo.SelectionStart = 0;
            this.txtEmplyeeNo.ShortcutsEnabled = true;
            this.txtEmplyeeNo.Size = new System.Drawing.Size(274, 25);
            this.txtEmplyeeNo.TabIndex = 29;
            this.txtEmplyeeNo.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmplyeeNo.UseSelectable = true;
            this.txtEmplyeeNo.WaterMark = "Press Enter To Search";
            this.txtEmplyeeNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEmplyeeNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtEmplyeeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmplyeeNo_KeyDown);
            // 
            // infoGroup
            // 
            this.infoGroup.Controls.Add(this.lblSelectedSchoolId);
            this.infoGroup.Controls.Add(this.btnAddNew);
            this.infoGroup.Controls.Add(this.txtdistrict);
            this.infoGroup.Controls.Add(this.txtSchoolname);
            this.infoGroup.Controls.Add(this.txtSchoolId);
            this.infoGroup.Controls.Add(this.metroLabel12);
            this.infoGroup.Controls.Add(this.metroLabel1);
            this.infoGroup.Controls.Add(this.metroLabel2);
            this.infoGroup.Location = new System.Drawing.Point(17, 70);
            this.infoGroup.Name = "infoGroup";
            this.infoGroup.Size = new System.Drawing.Size(631, 114);
            this.infoGroup.TabIndex = 31;
            this.infoGroup.TabStop = false;
            // 
            // lblSelectedSchoolId
            // 
            this.lblSelectedSchoolId.AutoSize = true;
            this.lblSelectedSchoolId.Location = new System.Drawing.Point(429, 14);
            this.lblSelectedSchoolId.Name = "lblSelectedSchoolId";
            this.lblSelectedSchoolId.Size = new System.Drawing.Size(16, 13);
            this.lblSelectedSchoolId.TabIndex = 33;
            this.lblSelectedSchoolId.Text = "   ";
            this.lblSelectedSchoolId.Visible = false;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNew.Image = global::HumanResourceManagement.Properties.Resources.add;
            this.btnAddNew.Location = new System.Drawing.Point(593, 13);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(24, 24);
            this.btnAddNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAddNew.TabIndex = 34;
            this.btnAddNew.TabStop = false;
            this.toolTip1.SetToolTip(this.btnAddNew, "Add New School");
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // txtdistrict
            // 
            this.txtdistrict.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.txtdistrict.CustomButton.Image = null;
            this.txtdistrict.CustomButton.Location = new System.Drawing.Point(474, 1);
            this.txtdistrict.CustomButton.Name = "";
            this.txtdistrict.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtdistrict.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtdistrict.CustomButton.TabIndex = 1;
            this.txtdistrict.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtdistrict.CustomButton.UseSelectable = true;
            this.txtdistrict.CustomButton.Visible = false;
            this.txtdistrict.Enabled = false;
            this.txtdistrict.Lines = new string[0];
            this.txtdistrict.Location = new System.Drawing.Point(121, 78);
            this.txtdistrict.MaxLength = 30;
            this.txtdistrict.Name = "txtdistrict";
            this.txtdistrict.PasswordChar = '\0';
            this.txtdistrict.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtdistrict.SelectedText = "";
            this.txtdistrict.SelectionLength = 0;
            this.txtdistrict.SelectionStart = 0;
            this.txtdistrict.ShortcutsEnabled = true;
            this.txtdistrict.Size = new System.Drawing.Size(496, 23);
            this.txtdistrict.TabIndex = 32;
            this.txtdistrict.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtdistrict.UseSelectable = true;
            this.txtdistrict.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtdistrict.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtSchoolname
            // 
            this.txtSchoolname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.txtSchoolname.CustomButton.Image = null;
            this.txtSchoolname.CustomButton.Location = new System.Drawing.Point(474, 1);
            this.txtSchoolname.CustomButton.Name = "";
            this.txtSchoolname.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSchoolname.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSchoolname.CustomButton.TabIndex = 1;
            this.txtSchoolname.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSchoolname.CustomButton.UseSelectable = true;
            this.txtSchoolname.CustomButton.Visible = false;
            this.txtSchoolname.Enabled = false;
            this.txtSchoolname.Lines = new string[0];
            this.txtSchoolname.Location = new System.Drawing.Point(121, 47);
            this.txtSchoolname.MaxLength = 30;
            this.txtSchoolname.Name = "txtSchoolname";
            this.txtSchoolname.PasswordChar = '\0';
            this.txtSchoolname.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSchoolname.SelectedText = "";
            this.txtSchoolname.SelectionLength = 0;
            this.txtSchoolname.SelectionStart = 0;
            this.txtSchoolname.ShortcutsEnabled = true;
            this.txtSchoolname.Size = new System.Drawing.Size(496, 23);
            this.txtSchoolname.TabIndex = 31;
            this.txtSchoolname.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSchoolname.UseSelectable = true;
            this.txtSchoolname.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSchoolname.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtSchoolId
            // 
            this.txtSchoolId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.txtSchoolId.CustomButton.Image = null;
            this.txtSchoolId.CustomButton.Location = new System.Drawing.Point(252, 1);
            this.txtSchoolId.CustomButton.Name = "";
            this.txtSchoolId.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSchoolId.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSchoolId.CustomButton.TabIndex = 1;
            this.txtSchoolId.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSchoolId.CustomButton.UseSelectable = true;
            this.txtSchoolId.CustomButton.Visible = false;
            this.txtSchoolId.Enabled = false;
            this.txtSchoolId.Lines = new string[0];
            this.txtSchoolId.Location = new System.Drawing.Point(121, 14);
            this.txtSchoolId.MaxLength = 30;
            this.txtSchoolId.Name = "txtSchoolId";
            this.txtSchoolId.PasswordChar = '\0';
            this.txtSchoolId.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSchoolId.SelectedText = "";
            this.txtSchoolId.SelectionLength = 0;
            this.txtSchoolId.SelectionStart = 0;
            this.txtSchoolId.ShortcutsEnabled = true;
            this.txtSchoolId.Size = new System.Drawing.Size(274, 23);
            this.txtSchoolId.TabIndex = 30;
            this.txtSchoolId.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSchoolId.UseSelectable = true;
            this.txtSchoolId.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSchoolId.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel12
            // 
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroLabel12.Enabled = false;
            this.metroLabel12.Location = new System.Drawing.Point(8, 18);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(81, 19);
            this.metroLabel12.TabIndex = 25;
            this.metroLabel12.Text = "SCHOOL ID:";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroLabel1.Enabled = false;
            this.metroLabel1.Location = new System.Drawing.Point(8, 51);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(107, 19);
            this.metroLabel1.TabIndex = 26;
            this.metroLabel1.Text = "SCHOOL NAME:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroLabel2.Enabled = false;
            this.metroLabel2.Location = new System.Drawing.Point(8, 82);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(65, 19);
            this.metroLabel2.TabIndex = 27;
            this.metroLabel2.Text = "DISTRICT:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.dgvSchools);
            this.panel2.Location = new System.Drawing.Point(13, 190);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(636, 230);
            this.panel2.TabIndex = 28;
            // 
            // dgvSchools
            // 
            this.dgvSchools.AllowUserToAddRows = false;
            this.dgvSchools.AllowUserToResizeRows = false;
            this.dgvSchools.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvSchools.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSchools.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvSchools.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSchools.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSchools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchools.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column2,
            this.Column1,
            this.Column11});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(202)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSchools.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSchools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchools.EnableHeadersVisualStyles = false;
            this.dgvSchools.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dgvSchools.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvSchools.Location = new System.Drawing.Point(1, 1);
            this.dgvSchools.MultiSelect = false;
            this.dgvSchools.Name = "dgvSchools";
            this.dgvSchools.ReadOnly = true;
            this.dgvSchools.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(202)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSchools.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSchools.RowHeadersVisible = false;
            this.dgvSchools.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSchools.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgvSchools.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchools.Size = new System.Drawing.Size(634, 228);
            this.dgvSchools.TabIndex = 2;
            this.dgvSchools.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvSchools_RowsAdded);
            this.dgvSchools.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvSchools_RowsRemoved);
            this.dgvSchools.SelectionChanged += new System.EventHandler(this.dgvSchools_SelectionChanged);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "id";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Visible = false;
            this.Column6.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "School ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "School";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 300;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "District";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 234;
            // 
            // FormPanel
            // 
            this.FormPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(65)))), ((int)(((byte)(136)))));
            this.FormPanel.Controls.Add(this.label1);
            this.FormPanel.Controls.Add(this.lblFormExit);
            this.FormPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FormPanel.Location = new System.Drawing.Point(0, 0);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(662, 26);
            this.FormPanel.TabIndex = 1;
            this.FormPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPanel_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Schools";
            // 
            // lblFormExit
            // 
            this.lblFormExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormExit.AutoSize = true;
            this.lblFormExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFormExit.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormExit.ForeColor = System.Drawing.Color.White;
            this.lblFormExit.Location = new System.Drawing.Point(636, 1);
            this.lblFormExit.Name = "lblFormExit";
            this.lblFormExit.Size = new System.Drawing.Size(19, 21);
            this.lblFormExit.TabIndex = 2;
            this.lblFormExit.Text = "X";
            this.lblFormExit.Click += new System.EventHandler(this.lblFormExit_Click);
            // 
            // bgSchoolLoader
            // 
            this.bgSchoolLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSchoolLoader_DoWork);
            this.bgSchoolLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgSchoolLoader_RunWorkerCompleted);
            // 
            // SchoolMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(666, 461);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SchoolMaintenance";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SchoolMaintenance";
            this.Load += new System.EventHandler(this.SchoolMaintenance_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.infoGroup.ResumeLayout(false);
            this.infoGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNew)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchools)).EndInit();
            this.FormPanel.ResumeLayout(false);
            this.FormPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel FormPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFormExit;
        private MetroFramework.Controls.MetroGrid dgvSchools;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        public MetroFramework.Controls.MetroTextBox txtEmplyeeNo;
        private System.Windows.Forms.GroupBox infoGroup;
        public MetroFramework.Controls.MetroTextBox txtdistrict;
        public MetroFramework.Controls.MetroTextBox txtSchoolname;
        public MetroFramework.Controls.MetroTextBox txtSchoolId;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        public MetroFramework.Controls.MetroComboBox cmbBy;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEditSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.ComponentModel.BackgroundWorker bgSchoolLoader;
        private System.Windows.Forms.Label lblSelectedSchoolId;
        private System.Windows.Forms.PictureBox btnAddNew;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
    }
}