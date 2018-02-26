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
using System.Data.SqlClient;
using System.Configuration;

namespace HumanResourceManagement
{
    public partial class CreateSystemUserDialog : Form
    {

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public CreateSystemUserDialog()
        {
            InitializeComponent();
        }


        //************************SERVER CONNECTION*************************
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

        //*******************APP CONFIG MANAGER*********************************
        private string getStringValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

        }


        private void lblFormClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnUpdatePass_Click(object sender, EventArgs e)
        {
            if (isInputsValid()) //if validated
            {
                if (TempHolder.ceaf != null)
                {
                    TempHolder.ceaf.updateSystemAcc(txtUsername.Text.Trim(), txtPassword.Text);
                    TempHolder.ceaf.systemUserCreated = true;
                }
                this.Close();
            }
           
        }


        private bool isInputsValid()
        {
            //check if the username is available
            //check if password matches the confirm password
            if(txtUsername.Text.Trim().Length==0)
            {
                errorProvider1.SetError(txtUsername, "Username must not be empty");
                return false;
            }

            if (checkUsernameAvailability()==false) return false;

            if (txtPassword.Text.Length == 0)
            {
                errorProvider1.SetError(txtPassword, "Password must not be empty");
                return false;
            }
            if (!txtPassword.Text.Equals(txtConfirmPass.Text))
            {
                errorProvider1.SetError(txtConfirmPass, "Password do not matched");
                return false;
            }

            return true;
        }


        private bool checkUsernameAvailability()
        {

            //username is changed

            //check for similar username
            try
            {
                openSQLConnection();
                string cmdString = "SELECT COUNT(" + SQLbank.USERNAME + ") FROM " + SQLbank.TBL_USERS + " WHERE " + SQLbank.USERNAME + " = @USERNAME AND " + SQLbank.ID + " != " + TempHolder.loggedUser_ID + ";"; //same username but not the id (primary key) of the user
                cmd = new SqlCommand(cmdString, conn);
                cmd.Parameters.Add("@USERNAME", SqlDbType.VarChar);
                cmd.Parameters["@USERNAME"].Value = txtUsername.Text.Trim();

                Console.WriteLine(cmd.CommandText.ToString());

                if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
                {
                    //there is a similar username already
                    errorProvider1.SetError(txtUsername, "Username already used");
                    return false;
                }
                else return true;//available

            }
            catch (Exception ee)
            {
                Console.WriteLine("Checking Similar Username Exception: " + ee.Message);
                return false;
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtUsername, null);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtPassword, null);
        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtConfirmPass, null);
        }

        private void CreateSystemUserDialog_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
        }
    }
}
