using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.SqlClient;

namespace HumanResourceManagement
{
    public partial class Form1 : Form
    {

        bool connectedToServer = false;
        SQLSettingsForm sqlSettingsForm;

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }


        //***********************FORM EVENTS***********************************
        private void Form1_Load(object sender, EventArgs e)
        {
            TempHolder.loginform = this;
            sqlSettingsForm = new SQLSettingsForm();
            txtUsername.Focus();
            testConnection();

        }

        public void reshowForm()
        {
            txtUsername.Select();
            this.Show();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void lblFormExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblFormMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        

        //*********************** COMPONENT EVENTS*******************************************
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (fieldsChecked())
            {
                //login
                if (getStringValue("adminUser").Equals(txtUsername.Text) && getStringValue("adminPass").Equals(txtPassword.Text)) {
                    //sql settings mode
                    Form settingsForm = new SQLSettingsForm();
                    settingsForm.ShowDialog();
                }
                else if(connectedToServer)
                {    //normal login
                    if (isUserAuthenticated())
                    {
                        if (TempHolder.systemValuesLoaded)
                        {
                            MainForm mf = new MainForm();
                            mf.Show();
                        }
                        else
                        {
                            LoadingForm lf = new LoadingForm();
                            lf.Show();
                        }
                    
                        txtUsername.Clear();
                        txtPassword.Clear();
                        lblConnectionMsg.Visible = false;
                        this.Hide();
                    }

                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) btnLogin.PerformClick();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) btnLogin.PerformClick();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (lblConnectionMsg.Text.Contains("Wrong username/password")) lblConnectionMsg.Visible = false;

            if (txtPassword.Text.Length > 0)
            {
                lblErrorPass.Visible = false;
                passwordEyePanel.BringToFront();
            }
            else
            {
                txtPassword.BringToFront();
            }
        }

        private void passShow_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '\0';
            passHide.BringToFront();
        }

        private void passShow_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '•';
            passShow.BringToFront();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (lblConnectionMsg.Text.Contains("Wrong username/password")) lblConnectionMsg.Visible = false;

            if (txtUsername.Text.Length > 0)
            {
                lblErrorUser.Visible = false;
            }
        }


        //**************************HELPER METHODS************************************
        private bool isUserAuthenticated()
        {
            try
            {
                openSQLConnection();
                int resCount = 0;
                string qry = "SELECT * FROM "+SQLbank.TBL_USERS+" WHERE  "+SQLbank.USERNAME+" = @USERNAME COLLATE SQL_Latin1_General_CP1_CS_AS AND "+SQLbank.PASSWORD+" = @PASS";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@USERNAME", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@PASS", encrypt(txtPassword.Text));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resCount++;
                    TempHolder.loggedUser_ID = reader[SQLbank.ID].ToString();
                    TempHolder.username = reader[SQLbank.USERNAME].ToString();
                    TempHolder.password = reader[SQLbank.PASSWORD].ToString();
                    TempHolder.fname = reader[SQLbank.FNAME].ToString();
                    TempHolder.mname = reader[SQLbank.MNAME].ToString();
                    TempHolder.lname = reader[SQLbank.LNAME].ToString();
                    TempHolder.accountType = reader[SQLbank.ROLE].ToString().ToLower();
                    TempHolder.pictureFilename = reader[SQLbank.PICTUREFILENAME].ToString();
                }

                if (resCount > 0)
                {
                    return true;
                }
                else
                {
                    //wrong username or password
                    showBottomMessage("Wrong username/password");
                    return false;
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine("Failed to login, exception: " + ee.Message);
                showBottomMessage("Problem occured while logging-in");
                return false;
            }
        }

        private bool fieldsChecked()
        {
            lblErrorUser.Visible = false;
            lblErrorPass.Visible = false;

            if (txtUsername.Text.Length == 0)
            {
                lblErrorUser.Text = "Username must not be empty";
                lblErrorUser.Visible = true;
                return false;
            }

            if (txtPassword.Text.Length == 0)
            {
                lblErrorPass.Text = "Password must not be empty";
                lblErrorPass.Visible = true;
                return false;
            }
            return true;
        }

        private string encrypt(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        private void showBottomMessage(string msg)
        {
            lblConnectionMsg.Visible = true;
            lblConnectionMsg.Text = msg;
        }


        //*******************APP CONFIG MANAGER*********************************
        private string getStringValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

        }

        private void updateValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Minimal);
        }

        //***********************SQL CONNECTIONS*******************************
        private void openSQLConnection()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();
            }
        }

        private void closeSQLConnection()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }

        public void testConnection()
        {
            if (!bgw_connectiontester.IsBusy)
            {
                showBottomMessage("Connecting to Server...");
                conn = new SqlConnection(getStringValue("sqlconstring"));
                bgw_connectiontester.RunWorkerAsync();
            }

        }

        private void bgw_connectiontester_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Console.WriteLine("Testing Server Connection");
                conn.Open();
                conn.Close();
                connectedToServer = true;
            }
            catch (Exception ee)
            {
                Console.WriteLine("Exception while testing connection: " + ee.Message);
                conn.Close();
                connectedToServer = false;
            }
        }

        private void bgw_connectiontester_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!connectedToServer)
            {
                showBottomMessage("Not Connected to Database");
                if (TempHolder.sqlSettingsForm==null)
                {
                    sqlSettingsForm = new SQLSettingsForm();
                    sqlSettingsForm.ShowDialog();
                    sqlSettingsForm.BringToFront();
                }
            }
            else lblConnectionMsg.Visible = false;
        }

    }
}
