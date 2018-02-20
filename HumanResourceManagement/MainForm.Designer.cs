namespace HumanResourceManagement
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainPanelRight = new System.Windows.Forms.Panel();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tab1_panel = new System.Windows.Forms.Panel();
            this.userControlPersonalInfo1 = new HumanResourceManagement.UserControlPersonalInfo();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tab2_Panel = new System.Windows.Forms.Panel();
            this.mainPanelLeft = new System.Windows.Forms.Panel();
            this.lblDOAPrettyTime = new System.Windows.Forms.Label();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.txtDateOfOriginalAppointment = new MetroFramework.Controls.MetroTextBox();
            this.txtSchoolName = new MetroFramework.Controls.MetroTextBox();
            this.lblUploadedPicture = new System.Windows.Forms.Label();
            this.lblemployee_id_hidden = new System.Windows.Forms.Label();
            this.txtDesignation = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtEmplyeeNo = new MetroFramework.Controls.MetroTextBox();
            this.lblEmployeeResult = new System.Windows.Forms.Label();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.maintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFormExit = new System.Windows.Forms.Label();
            this.lblFormMinimize = new System.Windows.Forms.Label();
            this.lblFormMaxMin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FormPanel = new System.Windows.Forms.Panel();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.btnAddUser = new System.Windows.Forms.PictureBox();
            this.btnChoosePhoto = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Lougout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystemAccounts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSchools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSQLSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.mainPanelRight.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tab1_panel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.mainPanelLeft.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.FormPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChoosePhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.mainPanelRight);
            this.panel1.Controls.Add(this.mainPanelLeft);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(3, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 567);
            this.panel1.TabIndex = 1;
            // 
            // mainPanelRight
            // 
            this.mainPanelRight.Controls.Add(this.metroTabControl1);
            this.mainPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanelRight.Location = new System.Drawing.Point(278, 24);
            this.mainPanelRight.Name = "mainPanelRight";
            this.mainPanelRight.Size = new System.Drawing.Size(751, 543);
            this.mainPanelRight.TabIndex = 2;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.tabPage1);
            this.metroTabControl1.Controls.Add(this.tabPage2);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(751, 543);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tab1_panel);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(743, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Personal Info";
            // 
            // tab1_panel
            // 
            this.tab1_panel.Controls.Add(this.userControlPersonalInfo1);
            this.tab1_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab1_panel.Location = new System.Drawing.Point(0, 0);
            this.tab1_panel.Name = "tab1_panel";
            this.tab1_panel.Size = new System.Drawing.Size(743, 501);
            this.tab1_panel.TabIndex = 0;
            // 
            // userControlPersonalInfo1
            // 
            this.userControlPersonalInfo1.AutoScroll = true;
            this.userControlPersonalInfo1.BackColor = System.Drawing.Color.White;
            this.userControlPersonalInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlPersonalInfo1.Location = new System.Drawing.Point(0, 0);
            this.userControlPersonalInfo1.Name = "userControlPersonalInfo1";
            this.userControlPersonalInfo1.Size = new System.Drawing.Size(743, 501);
            this.userControlPersonalInfo1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tab2_Panel);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(743, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Service Records";
            // 
            // tab2_Panel
            // 
            this.tab2_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab2_Panel.Location = new System.Drawing.Point(0, 0);
            this.tab2_Panel.Name = "tab2_Panel";
            this.tab2_Panel.Size = new System.Drawing.Size(743, 501);
            this.tab2_Panel.TabIndex = 0;
            // 
            // mainPanelLeft
            // 
            this.mainPanelLeft.Controls.Add(this.lblDOAPrettyTime);
            this.mainPanelLeft.Controls.Add(this.metroLabel6);
            this.mainPanelLeft.Controls.Add(this.txtDateOfOriginalAppointment);
            this.mainPanelLeft.Controls.Add(this.txtSchoolName);
            this.mainPanelLeft.Controls.Add(this.lblUploadedPicture);
            this.mainPanelLeft.Controls.Add(this.btnAddUser);
            this.mainPanelLeft.Controls.Add(this.btnChoosePhoto);
            this.mainPanelLeft.Controls.Add(this.lblemployee_id_hidden);
            this.mainPanelLeft.Controls.Add(this.txtDesignation);
            this.mainPanelLeft.Controls.Add(this.pictureBox1);
            this.mainPanelLeft.Controls.Add(this.metroLabel3);
            this.mainPanelLeft.Controls.Add(this.metroLabel1);
            this.mainPanelLeft.Controls.Add(this.btnClearSearch);
            this.mainPanelLeft.Controls.Add(this.txtEmplyeeNo);
            this.mainPanelLeft.Controls.Add(this.lblEmployeeResult);
            this.mainPanelLeft.Controls.Add(this.metroLabel4);
            this.mainPanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainPanelLeft.Location = new System.Drawing.Point(0, 24);
            this.mainPanelLeft.Name = "mainPanelLeft";
            this.mainPanelLeft.Size = new System.Drawing.Size(278, 543);
            this.mainPanelLeft.TabIndex = 1;
            this.mainPanelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanelLeft_Paint);
            // 
            // lblDOAPrettyTime
            // 
            this.lblDOAPrettyTime.AutoSize = true;
            this.lblDOAPrettyTime.Location = new System.Drawing.Point(135, 417);
            this.lblDOAPrettyTime.Name = "lblDOAPrettyTime";
            this.lblDOAPrettyTime.Size = new System.Drawing.Size(0, 13);
            this.lblDOAPrettyTime.TabIndex = 80;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(19, 389);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(184, 19);
            this.metroLabel6.TabIndex = 5;
            this.metroLabel6.Text = "Date of Original Appointment";
            // 
            // txtDateOfOriginalAppointment
            // 
            this.txtDateOfOriginalAppointment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.txtDateOfOriginalAppointment.CustomButton.Image = null;
            this.txtDateOfOriginalAppointment.CustomButton.Location = new System.Drawing.Point(88, 1);
            this.txtDateOfOriginalAppointment.CustomButton.Name = "";
            this.txtDateOfOriginalAppointment.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtDateOfOriginalAppointment.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDateOfOriginalAppointment.CustomButton.TabIndex = 1;
            this.txtDateOfOriginalAppointment.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDateOfOriginalAppointment.CustomButton.UseSelectable = true;
            this.txtDateOfOriginalAppointment.CustomButton.Visible = false;
            this.txtDateOfOriginalAppointment.Enabled = false;
            this.txtDateOfOriginalAppointment.Lines = new string[0];
            this.txtDateOfOriginalAppointment.Location = new System.Drawing.Point(19, 411);
            this.txtDateOfOriginalAppointment.MaxLength = 32767;
            this.txtDateOfOriginalAppointment.Name = "txtDateOfOriginalAppointment";
            this.txtDateOfOriginalAppointment.PasswordChar = '\0';
            this.txtDateOfOriginalAppointment.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDateOfOriginalAppointment.SelectedText = "";
            this.txtDateOfOriginalAppointment.SelectionLength = 0;
            this.txtDateOfOriginalAppointment.SelectionStart = 0;
            this.txtDateOfOriginalAppointment.ShortcutsEnabled = true;
            this.txtDateOfOriginalAppointment.Size = new System.Drawing.Size(110, 23);
            this.txtDateOfOriginalAppointment.TabIndex = 77;
            this.txtDateOfOriginalAppointment.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDateOfOriginalAppointment.UseSelectable = true;
            this.txtDateOfOriginalAppointment.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDateOfOriginalAppointment.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtDateOfOriginalAppointment.TextChanged += new System.EventHandler(this.txtDateOfOriginalAppointment_TextChanged);
            // 
            // txtSchoolName
            // 
            this.txtSchoolName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.txtSchoolName.CustomButton.Image = null;
            this.txtSchoolName.CustomButton.Location = new System.Drawing.Point(171, 2);
            this.txtSchoolName.CustomButton.Name = "";
            this.txtSchoolName.CustomButton.Size = new System.Drawing.Size(63, 63);
            this.txtSchoolName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSchoolName.CustomButton.TabIndex = 1;
            this.txtSchoolName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSchoolName.CustomButton.UseSelectable = true;
            this.txtSchoolName.CustomButton.Visible = false;
            this.txtSchoolName.Enabled = false;
            this.txtSchoolName.Lines = new string[0];
            this.txtSchoolName.Location = new System.Drawing.Point(19, 260);
            this.txtSchoolName.MaxLength = 32767;
            this.txtSchoolName.Multiline = true;
            this.txtSchoolName.Name = "txtSchoolName";
            this.txtSchoolName.PasswordChar = '\0';
            this.txtSchoolName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSchoolName.SelectedText = "";
            this.txtSchoolName.SelectionLength = 0;
            this.txtSchoolName.SelectionStart = 0;
            this.txtSchoolName.ShortcutsEnabled = true;
            this.txtSchoolName.Size = new System.Drawing.Size(237, 68);
            this.txtSchoolName.TabIndex = 78;
            this.txtSchoolName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSchoolName.UseSelectable = true;
            this.txtSchoolName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSchoolName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblUploadedPicture
            // 
            this.lblUploadedPicture.AutoSize = true;
            this.lblUploadedPicture.Location = new System.Drawing.Point(9, 4);
            this.lblUploadedPicture.Name = "lblUploadedPicture";
            this.lblUploadedPicture.Size = new System.Drawing.Size(0, 13);
            this.lblUploadedPicture.TabIndex = 74;
            this.lblUploadedPicture.Visible = false;
            // 
            // lblemployee_id_hidden
            // 
            this.lblemployee_id_hidden.AutoSize = true;
            this.lblemployee_id_hidden.Location = new System.Drawing.Point(257, 16);
            this.lblemployee_id_hidden.Name = "lblemployee_id_hidden";
            this.lblemployee_id_hidden.Size = new System.Drawing.Size(15, 13);
            this.lblemployee_id_hidden.TabIndex = 0;
            this.lblemployee_id_hidden.Text = "id";
            this.lblemployee_id_hidden.Visible = false;
            // 
            // txtDesignation
            // 
            this.txtDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.txtDesignation.CustomButton.Image = null;
            this.txtDesignation.CustomButton.Location = new System.Drawing.Point(215, 1);
            this.txtDesignation.CustomButton.Name = "";
            this.txtDesignation.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtDesignation.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDesignation.CustomButton.TabIndex = 1;
            this.txtDesignation.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDesignation.CustomButton.UseSelectable = true;
            this.txtDesignation.CustomButton.Visible = false;
            this.txtDesignation.Enabled = false;
            this.txtDesignation.Lines = new string[0];
            this.txtDesignation.Location = new System.Drawing.Point(19, 358);
            this.txtDesignation.MaxLength = 32767;
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.PasswordChar = '\0';
            this.txtDesignation.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDesignation.SelectedText = "";
            this.txtDesignation.SelectionLength = 0;
            this.txtDesignation.SelectionStart = 0;
            this.txtDesignation.ShortcutsEnabled = true;
            this.txtDesignation.Size = new System.Drawing.Size(237, 23);
            this.txtDesignation.TabIndex = 2;
            this.txtDesignation.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDesignation.UseSelectable = true;
            this.txtDesignation.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDesignation.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(19, 334);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(77, 19);
            this.metroLabel3.TabIndex = 2;
            this.metroLabel3.Text = "Designation";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Enabled = false;
            this.metroLabel1.Location = new System.Drawing.Point(15, 181);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(92, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Employee No.";
            // 
            // txtEmplyeeNo
            // 
            // 
            // 
            // 
            this.txtEmplyeeNo.CustomButton.Image = null;
            this.txtEmplyeeNo.CustomButton.Location = new System.Drawing.Point(215, 1);
            this.txtEmplyeeNo.CustomButton.Name = "";
            this.txtEmplyeeNo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtEmplyeeNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtEmplyeeNo.CustomButton.TabIndex = 1;
            this.txtEmplyeeNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmplyeeNo.CustomButton.UseSelectable = true;
            this.txtEmplyeeNo.CustomButton.Visible = false;
            this.txtEmplyeeNo.Lines = new string[0];
            this.txtEmplyeeNo.Location = new System.Drawing.Point(19, 204);
            this.txtEmplyeeNo.MaxLength = 30;
            this.txtEmplyeeNo.Name = "txtEmplyeeNo";
            this.txtEmplyeeNo.PasswordChar = '\0';
            this.txtEmplyeeNo.PromptText = "Press Enter To Search";
            this.txtEmplyeeNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmplyeeNo.SelectedText = "";
            this.txtEmplyeeNo.SelectionLength = 0;
            this.txtEmplyeeNo.SelectionStart = 0;
            this.txtEmplyeeNo.ShortcutsEnabled = true;
            this.txtEmplyeeNo.Size = new System.Drawing.Size(237, 23);
            this.txtEmplyeeNo.TabIndex = 0;
            this.txtEmplyeeNo.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmplyeeNo.UseSelectable = true;
            this.txtEmplyeeNo.WaterMark = "Press Enter To Search";
            this.txtEmplyeeNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEmplyeeNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtEmplyeeNo.TextChanged += new System.EventHandler(this.txtEmplyeeNo_TextChanged);
            this.txtEmplyeeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.metroTextBox1_KeyDown);
            // 
            // lblEmployeeResult
            // 
            this.lblEmployeeResult.AutoSize = true;
            this.lblEmployeeResult.BackColor = System.Drawing.Color.Transparent;
            this.lblEmployeeResult.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeResult.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblEmployeeResult.Location = new System.Drawing.Point(20, 226);
            this.lblEmployeeResult.Name = "lblEmployeeResult";
            this.lblEmployeeResult.Size = new System.Drawing.Size(80, 16);
            this.lblEmployeeResult.TabIndex = 17;
            this.lblEmployeeResult.Text = "Searching . . .";
            this.lblEmployeeResult.Visible = false;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(19, 239);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(88, 19);
            this.metroLabel4.TabIndex = 79;
            this.metroLabel4.Text = "School Name";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(202)))), ((int)(((byte)(233)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.maintenanceToolStripMenuItem,
            this.reportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1029, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.menuItem_Lougout,
            this.toolStripSeparator1,
            this.menuItemExit});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // maintenanceToolStripMenuItem
            // 
            this.maintenanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystemAccounts,
            this.menuSchools,
            this.toolStripSeparator2,
            this.menuDBBackup,
            this.MenuSQLSettings});
            this.maintenanceToolStripMenuItem.Name = "maintenanceToolStripMenuItem";
            this.maintenanceToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.maintenanceToolStripMenuItem.Text = "Maintenance";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.reportToolStripMenuItem.Text = "Report";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // lblFormExit
            // 
            this.lblFormExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormExit.AutoSize = true;
            this.lblFormExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFormExit.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormExit.ForeColor = System.Drawing.Color.White;
            this.lblFormExit.Location = new System.Drawing.Point(1010, 1);
            this.lblFormExit.Name = "lblFormExit";
            this.lblFormExit.Size = new System.Drawing.Size(19, 21);
            this.lblFormExit.TabIndex = 2;
            this.lblFormExit.Text = "X";
            this.lblFormExit.Click += new System.EventHandler(this.lblFormExit_Click);
            // 
            // lblFormMinimize
            // 
            this.lblFormMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormMinimize.AutoSize = true;
            this.lblFormMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFormMinimize.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormMinimize.ForeColor = System.Drawing.Color.White;
            this.lblFormMinimize.Location = new System.Drawing.Point(955, -1);
            this.lblFormMinimize.Name = "lblFormMinimize";
            this.lblFormMinimize.Size = new System.Drawing.Size(18, 21);
            this.lblFormMinimize.TabIndex = 3;
            this.lblFormMinimize.Text = "_";
            this.lblFormMinimize.Click += new System.EventHandler(this.lblFormMinimize_Click);
            // 
            // lblFormMaxMin
            // 
            this.lblFormMaxMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormMaxMin.AutoSize = true;
            this.lblFormMaxMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFormMaxMin.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormMaxMin.ForeColor = System.Drawing.Color.White;
            this.lblFormMaxMin.Location = new System.Drawing.Point(980, 1);
            this.lblFormMaxMin.Name = "lblFormMaxMin";
            this.lblFormMaxMin.Size = new System.Drawing.Size(24, 21);
            this.lblFormMaxMin.TabIndex = 4;
            this.lblFormMaxMin.Text = "☐";
            this.lblFormMaxMin.Click += new System.EventHandler(this.lblFormMaxMin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Human Resource Management";
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPanel_MouseMove);
            // 
            // FormPanel
            // 
            this.FormPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(65)))), ((int)(((byte)(136)))));
            this.FormPanel.Controls.Add(this.label1);
            this.FormPanel.Controls.Add(this.lblFormMaxMin);
            this.FormPanel.Controls.Add(this.lblFormMinimize);
            this.FormPanel.Controls.Add(this.lblFormExit);
            this.FormPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FormPanel.Location = new System.Drawing.Point(0, 0);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(1036, 26);
            this.FormPanel.TabIndex = 0;
            this.FormPanel.DoubleClick += new System.EventHandler(this.lblFormMaxMin_Click);
            this.FormPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPanel_MouseMove);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.White;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddUser.Image = global::HumanResourceManagement.Properties.Resources.add_user;
            this.btnAddUser.Location = new System.Drawing.Point(240, 7);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(32, 32);
            this.btnAddUser.TabIndex = 73;
            this.btnAddUser.TabStop = false;
            this.metroToolTip1.SetToolTip(this.btnAddUser, "Create New Employee Account");
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnChoosePhoto
            // 
            this.btnChoosePhoto.BackgroundImage = global::HumanResourceManagement.Properties.Resources.gallery_48_48;
            this.btnChoosePhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChoosePhoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoosePhoto.Location = new System.Drawing.Point(180, 139);
            this.btnChoosePhoto.Name = "btnChoosePhoto";
            this.btnChoosePhoto.Size = new System.Drawing.Size(48, 48);
            this.btnChoosePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnChoosePhoto.TabIndex = 72;
            this.btnChoosePhoto.TabStop = false;
            this.metroToolTip1.SetToolTip(this.btnChoosePhoto, "Choose Photo");
            this.btnChoosePhoto.Visible = false;
            this.btnChoosePhoto.Click += new System.EventHandler(this.btnChoosePhoto_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::HumanResourceManagement.Properties.Resources.default_avatar;
            this.pictureBox1.Location = new System.Drawing.Point(57, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnClearSearch.BackgroundImage = global::HumanResourceManagement.Properties.Resources.x;
            this.btnClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearch.ForeColor = System.Drawing.Color.Transparent;
            this.btnClearSearch.Location = new System.Drawing.Point(230, 205);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(22, 21);
            this.btnClearSearch.TabIndex = 82;
            this.metroToolTip1.SetToolTip(this.btnClearSearch, "Clear Search Result");
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Visible = false;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click_1);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::HumanResourceManagement.Properties.Resources.if_user_309035;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem1.Text = "My Account";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // menuItem_Lougout
            // 
            this.menuItem_Lougout.Image = global::HumanResourceManagement.Properties.Resources.if_sign_out_1608410;
            this.menuItem_Lougout.Name = "menuItem_Lougout";
            this.menuItem_Lougout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F4)));
            this.menuItem_Lougout.Size = new System.Drawing.Size(168, 22);
            this.menuItem_Lougout.Text = "Log-out";
            this.menuItem_Lougout.Click += new System.EventHandler(this.menuItem_Lougout_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Image = global::HumanResourceManagement.Properties.Resources.if_060_Off_183189;
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(168, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuSystemAccounts
            // 
            this.menuSystemAccounts.Image = global::HumanResourceManagement.Properties.Resources.accounts;
            this.menuSystemAccounts.Name = "menuSystemAccounts";
            this.menuSystemAccounts.Size = new System.Drawing.Size(208, 22);
            this.menuSystemAccounts.Text = "System User Accounts";
            this.menuSystemAccounts.Click += new System.EventHandler(this.menuSystemAccounts_Click);
            // 
            // menuSchools
            // 
            this.menuSchools.Image = global::HumanResourceManagement.Properties.Resources.if_commerical_building_103266;
            this.menuSchools.Name = "menuSchools";
            this.menuSchools.Size = new System.Drawing.Size(208, 22);
            this.menuSchools.Text = "Schools";
            this.menuSchools.Click += new System.EventHandler(this.menuSchools_Click);
            // 
            // menuDBBackup
            // 
            this.menuDBBackup.Image = global::HumanResourceManagement.Properties.Resources.if_database_1608662;
            this.menuDBBackup.Name = "menuDBBackup";
            this.menuDBBackup.Size = new System.Drawing.Size(208, 22);
            this.menuDBBackup.Text = "Database Backup/Restore";
            this.menuDBBackup.Click += new System.EventHandler(this.menuDBBackup_Click);
            // 
            // MenuSQLSettings
            // 
            this.MenuSQLSettings.Image = global::HumanResourceManagement.Properties.Resources.if_settings_326699;
            this.MenuSQLSettings.Name = "MenuSQLSettings";
            this.MenuSQLSettings.Size = new System.Drawing.Size(208, 22);
            this.MenuSQLSettings.Text = "SQL Settings";
            this.MenuSQLSettings.Click += new System.EventHandler(this.MenuSQLSettings_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1036, 597);
            this.Controls.Add(this.FormPanel);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mainPanelRight.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tab1_panel.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.mainPanelLeft.ResumeLayout(false);
            this.mainPanelLeft.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.FormPanel.ResumeLayout(false);
            this.FormPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChoosePhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFormExit;
        private System.Windows.Forms.Label lblFormMinimize;
        private System.Windows.Forms.Label lblFormMaxMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel FormPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Lougout;
        private System.Windows.Forms.Panel mainPanelRight;
        private System.Windows.Forms.Panel mainPanelLeft;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Panel tab1_panel;
        private System.Windows.Forms.Panel tab2_Panel;
        private System.Windows.Forms.Label lblEmployeeResult;
        private System.Windows.Forms.Label lblemployee_id_hidden;
        private MetroFramework.Controls.MetroTextBox txtDesignation;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.PictureBox btnChoosePhoto;
        private System.Windows.Forms.ToolStripMenuItem maintenanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuSystemAccounts;
        private System.Windows.Forms.ToolStripMenuItem menuSchools;
        private System.Windows.Forms.ToolStripMenuItem MenuSQLSettings;
        private System.Windows.Forms.PictureBox btnAddUser;
        private System.Windows.Forms.Label lblUploadedPicture;
        public MetroFramework.Controls.MetroTextBox txtEmplyeeNo;
        private MetroFramework.Controls.MetroTextBox txtDateOfOriginalAppointment;
        private MetroFramework.Controls.MetroTextBox txtSchoolName;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuDBBackup;
        private System.Windows.Forms.Label lblDOAPrettyTime;
        private System.Windows.Forms.Button btnClearSearch;
        private UserControlPersonalInfo userControlPersonalInfo1;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
    }
}