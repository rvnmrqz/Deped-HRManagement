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
using System.Security.Cryptography;

namespace HumanResourceManagement
{
    public partial class ChangePass : Form
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

        public ChangePass()
        {
            InitializeComponent();
        }

        //************************FORM EVENTS***********************************
        private void ChangePass_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
        }

        private void ChangePass_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void lblFormClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //************************SERVER CONNECTION*****************************
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

        //*******************COMPONENT EVENTS***********************************

        private void btnUpdatePass_Click(object sender, EventArgs e)
        {
            if (inputsValid())
            {
                //do updates here
                try
                {
                    openSQLConnection();

                    string cmdtext = "UPDATE " + SQLbank.TBL_USERS + " SET " + SQLbank.PASSWORD + " = @PASSWORD WHERE "+SQLbank.ID+" = "+TempHolder.loggedUser_ID;
                    cmd = new SqlCommand(cmdtext, conn);
                    cmd.Parameters.Add("@PASSWORD",SqlDbType.VarChar);
                    string encrypted = encrypt(txtNewPass.Text.ToString());
                    cmd.Parameters["@PASSWORD"].Value = encrypted;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Your password is updated","Change Password",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    TempHolder.password = encrypted;

                    this.Close();
                }
                catch (Exception ee)
                {
                    Console.WriteLine("\n\nbtnUpdate Exception: " + ee.Message+"\n\n");
                }
                finally
                {
                    closeSQLConnection();
                }
                    
            }
        }

        private void txtCurrentPass_TextChanged(object sender, EventArgs e)
        {
            txtCurrentPass.BackColor = SystemColors.Window;
            lblCurrentPassMsg.Text = "";
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            txtNewPass.BackColor = SystemColors.Window;
        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            txtConfirmPass.BackColor = SystemColors.Window;
            lblConfirmPassMsg.Text = "";
        }

        //***********************HELPER METHODS***********************************
        private bool inputsValid()
        {
            bool remarks = true;

            if (txtCurrentPass.Text.Length == 0)
            {
                txtCurrentPass.BackColor = SystemColors.HotTrack;
                remarks = false;
            }
            else
            {
                txtCurrentPass.BackColor = SystemColors.Window;
                //check if typed current is the same with current password

                if (!TempHolder.password.Trim().Equals(encrypt(txtCurrentPass.Text).Trim()))
                {
                    txtCurrentPass.BackColor = SystemColors.HotTrack;
                    lblCurrentPassMsg.Text = "Wrong Current Password";
                    remarks = false;
                }
                else
                {
                    txtCurrentPass.BackColor = SystemColors.Window;
                    lblCurrentPassMsg.Text = "";
                }
            }


            if (txtNewPass.Text.Length == 0 && txtCurrentPass.Text.Length!=0)
            {
                txtNewPass.BackColor = SystemColors.HotTrack;
                remarks = false;
            }
            else
            {
                txtNewPass.BackColor = SystemColors.Window;
            }

            if (txtNewPass.Text.Length != 0 && (!txtNewPass.Text.Equals(txtConfirmPass.Text)))
            {
                //new pass and confirm pass does not match
                txtConfirmPass.BackColor = SystemColors.HotTrack;
                lblConfirmPassMsg.Text = "Password does not match";
                remarks = false;
            }
            else
            {
                txtConfirmPass.BackColor = SystemColors.Window;
                lblConfirmPassMsg.Text = "";
            }


            return remarks;
        }

        private string encrypt(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

    
    }
}
