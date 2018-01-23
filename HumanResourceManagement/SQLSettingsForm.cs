using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace HumanResourceManagement
{
    public partial class SQLSettingsForm : Form
    {

        string sqlconstring;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public SQLSettingsForm()
        {
            InitializeComponent();
        }

        private void SQLSettingsForm_Load(object sender, EventArgs e)
        {
            prepareDisplay();
            TempHolder.sqlSettingsForm = this;
        }

        private void SQLSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TempHolder.sqlSettingsForm = null;
        }

        //**************************** EVENTTS **************************************************************

        private void btnSaveSqlSettings_Click(object sender, EventArgs e)
        { 
               //saving
            if (isValidated())
            {
                if (saveChanges())
                {
                   
                    if (TempHolder.loginform != null)
                    {
                        TempHolder.loginform.testConnection();
                    }
                    MessageBox.Show("SQL Settings Saved", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }  
            }

        }


        private void cmbAuthorization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthorization.SelectedIndex == 0)
            {
                //windows auth
                SQLServerPanel.Enabled = false;
            }
            else
            {
                //sql server auth
                SQLServerPanel.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '●';
            }
        }


        private void btnTestconnection_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(generateConnectionString());
                con.Open();
                con.Close();
                MessageBox.Show("Connection Successful", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ee)
            {
                MessageBox.Show("Connection Failed", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //**************************** HELPER METHODS ********************************************************

        private string generateConnectionString()
        {
            string temp;
            if (cmbAuthorization.SelectedIndex == 0)
            {
                //windows auth
                temp = "Integrated Security=SSPI;Initial Catalog=" + txtDatabase.Text.ToString().Trim() + ";Data Source=" + txtServer.Text.ToString().Trim() + ";";
            }
            else
            {
                //sql server auth
                temp = "server=" + txtServer.Text.ToString() + ";user id = " + txtUser.Text.ToString() + "; password = " + txtPassword.Text.ToString() + "; database = " + txtDatabase.Text.ToString() + "; connection reset = false";
            }

            return temp;
        }


        private void prepareDisplay()
        {
            sqlconstring = getStringValue("sqlconstring");
            cmbAuthorization.SelectedIndex = Convert.ToInt32(getStringValue("authIndex").Trim());
            txtUser.Text = getStringValue("user");
            txtPassword.Text = getStringValue("password");
            txtServer.Text = getStringValue("server");
            txtDatabase.Text = getStringValue("database");
        } 

        private bool isValidated()
        {
            if(cmbAuthorization.SelectedIndex == 1)
            {
                //sql server is selected
                if (txtUser.Text.ToString().Trim().Length == 0)
                {
                    showErrorMessage("User must not be empty");
                    return false;
                }
            }


            if (txtServer.Text.ToString().Trim().Length == 0)
            {
                showErrorMessage("Host/Server must not be empty");
                return false;
            }
            if(txtDatabase.Text.ToString().Trim().Length == 0)
            {
                showErrorMessage("Database must not be empty");
                return false;
            }

            return true;
        }

        private void showErrorMessage(string msg)
        {
            MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private bool saveChanges()
        {
            try
            {

                /*SQL SERVER CONNECTION STRING FORMATS
                --------------------------------------------------------------------------------------------------------------------------------------
                for SQL Server Authentication
                @"server=DESKTOP-TSC8L25\SQLSERVER;user id = kim\SQLSERVER; password = master123; database = Test_Database; connection reset = false";
                ---------------------------------------------------------------------------------------------------------------------------------------
                for Windows Authentication
                @"Integrated Security=SSPI;Initial Catalog=Test_Database;Data Source=DESKTOP-TSC8L25\SQLSERVER;"
                */

                updateValue("authIndex", cmbAuthorization.SelectedIndex.ToString().Trim());
                updateValue("user", txtUser.Text.ToString());
                updateValue("password", txtPassword.Text.ToString());
                updateValue("server", txtServer.Text.ToString());
                updateValue("database", txtDatabase.Text.ToString());
                updateValue("sqlconstring", generateConnectionString());
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("Error occured: " + e.Message, "Faile to save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
      
        }



        //******************************** CONFIGURATION MANAGER **************************************
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

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void top_panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

      
    }

}

