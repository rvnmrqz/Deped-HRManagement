namespace HumanResourceManagement
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblFormExit = new System.Windows.Forms.Label();
            this.lblFormMinimize = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblConnectionMsg = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.passwordEyePanel = new System.Windows.Forms.Panel();
            this.passShow = new System.Windows.Forms.PictureBox();
            this.passHide = new System.Windows.Forms.PictureBox();
            this.lblErrorPass = new System.Windows.Forms.Label();
            this.lblErrorUser = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bgw_connectiontester = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.passwordEyePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.passShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passHide)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormExit
            // 
            this.lblFormExit.AutoSize = true;
            this.lblFormExit.BackColor = System.Drawing.Color.Transparent;
            this.lblFormExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFormExit.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFormExit.Location = new System.Drawing.Point(687, 5);
            this.lblFormExit.Name = "lblFormExit";
            this.lblFormExit.Size = new System.Drawing.Size(20, 21);
            this.lblFormExit.TabIndex = 0;
            this.lblFormExit.Text = "X";
            this.toolTip1.SetToolTip(this.lblFormExit, "Close");
            this.lblFormExit.Click += new System.EventHandler(this.lblFormExit_Click);
            // 
            // lblFormMinimize
            // 
            this.lblFormMinimize.AutoSize = true;
            this.lblFormMinimize.BackColor = System.Drawing.Color.Transparent;
            this.lblFormMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFormMinimize.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFormMinimize.Location = new System.Drawing.Point(663, 4);
            this.lblFormMinimize.Name = "lblFormMinimize";
            this.lblFormMinimize.Size = new System.Drawing.Size(18, 21);
            this.lblFormMinimize.TabIndex = 1;
            this.lblFormMinimize.Text = "_";
            this.toolTip1.SetToolTip(this.lblFormMinimize, "Minimize");
            this.lblFormMinimize.Click += new System.EventHandler(this.lblFormMinimize_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.btnLogin);
            this.panel2.Controls.Add(this.txtUsername);
            this.panel2.Controls.Add(this.passwordEyePanel);
            this.panel2.Controls.Add(this.lblErrorPass);
            this.panel2.Controls.Add(this.lblErrorUser);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(207, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 288);
            this.panel2.TabIndex = 1;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblConnectionMsg);
            this.panel1.Location = new System.Drawing.Point(0, 235);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 31);
            this.panel1.TabIndex = 13;
            // 
            // lblConnectionMsg
            // 
            this.lblConnectionMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblConnectionMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConnectionMsg.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionMsg.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionMsg.Location = new System.Drawing.Point(0, 0);
            this.lblConnectionMsg.Name = "lblConnectionMsg";
            this.lblConnectionMsg.Size = new System.Drawing.Size(286, 31);
            this.lblConnectionMsg.TabIndex = 12;
            this.lblConnectionMsg.Text = "               ";
            this.lblConnectionMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(37, 147);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(216, 29);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.Image = global::HumanResourceManagement.Properties.Resources.black_key;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogin.Location = new System.Drawing.Point(62, 196);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(167, 36);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(37, 81);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(216, 29);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            // 
            // passwordEyePanel
            // 
            this.passwordEyePanel.Controls.Add(this.passShow);
            this.passwordEyePanel.Controls.Add(this.passHide);
            this.passwordEyePanel.Location = new System.Drawing.Point(224, 148);
            this.passwordEyePanel.Name = "passwordEyePanel";
            this.passwordEyePanel.Size = new System.Drawing.Size(27, 27);
            this.passwordEyePanel.TabIndex = 9;
            // 
            // passShow
            // 
            this.passShow.BackColor = System.Drawing.Color.White;
            this.passShow.Image = global::HumanResourceManagement.Properties.Resources.show_password;
            this.passShow.Location = new System.Drawing.Point(0, 0);
            this.passShow.Name = "passShow";
            this.passShow.Size = new System.Drawing.Size(27, 27);
            this.passShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.passShow.TabIndex = 7;
            this.passShow.TabStop = false;
            this.passShow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.passShow_MouseDown);
            this.passShow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.passShow_MouseUp);
            // 
            // passHide
            // 
            this.passHide.BackColor = System.Drawing.Color.White;
            this.passHide.Image = global::HumanResourceManagement.Properties.Resources.hide_password;
            this.passHide.Location = new System.Drawing.Point(0, 0);
            this.passHide.Name = "passHide";
            this.passHide.Size = new System.Drawing.Size(27, 27);
            this.passHide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.passHide.TabIndex = 8;
            this.passHide.TabStop = false;
            // 
            // lblErrorPass
            // 
            this.lblErrorPass.AutoSize = true;
            this.lblErrorPass.BackColor = System.Drawing.Color.Transparent;
            this.lblErrorPass.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorPass.ForeColor = System.Drawing.Color.Yellow;
            this.lblErrorPass.Location = new System.Drawing.Point(38, 175);
            this.lblErrorPass.Name = "lblErrorPass";
            this.lblErrorPass.Size = new System.Drawing.Size(14, 16);
            this.lblErrorPass.TabIndex = 10;
            this.lblErrorPass.Text = "  ";
            this.lblErrorPass.Visible = false;
            // 
            // lblErrorUser
            // 
            this.lblErrorUser.AutoSize = true;
            this.lblErrorUser.BackColor = System.Drawing.Color.Transparent;
            this.lblErrorUser.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorUser.ForeColor = System.Drawing.Color.Yellow;
            this.lblErrorUser.Location = new System.Drawing.Point(37, 108);
            this.lblErrorUser.Name = "lblErrorUser";
            this.lblErrorUser.Size = new System.Drawing.Size(14, 16);
            this.lblErrorUser.TabIndex = 11;
            this.lblErrorUser.Text = "  ";
            this.lblErrorUser.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(34, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 22);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(33, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username";
            // 
            // bgw_connectiontester
            // 
            this.bgw_connectiontester.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_connectiontester_DoWork);
            this.bgw_connectiontester.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_connectiontester_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::HumanResourceManagement.Properties.Resources.deped_teachers_1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(711, 419);
            this.Controls.Add(this.lblFormMinimize);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblFormExit);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.passwordEyePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.passShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passHide)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblFormMinimize;
        private System.Windows.Forms.Label lblFormExit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox passHide;
        private System.Windows.Forms.PictureBox passShow;
        private System.Windows.Forms.Panel passwordEyePanel;
        private System.Windows.Forms.Label lblErrorPass;
        private System.Windows.Forms.Label lblErrorUser;
        private System.Windows.Forms.Label lblConnectionMsg;
        private System.ComponentModel.BackgroundWorker bgw_connectiontester;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

