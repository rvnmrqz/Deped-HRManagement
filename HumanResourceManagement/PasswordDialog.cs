using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResourceManagement
{
    public partial class PasswordDialog : Form
    {

        public string encryptedPassword = "";

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public PasswordDialog()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdatePass_Click(object sender, EventArgs e)
        {
           
        }

        private bool checkInputs()
        {
            if(txtPassword.Text.ToString().Length == 0)
            {
                MessageBox.Show("Password must not be empty","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;
            }

            if(!txtPassword.Text.ToString().Equals(txtConfirmPass.Text.ToString()))
            {
                MessageBox.Show("Passwords do not match", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void PasswordDialog_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void PasswordDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkInputs())
            {
                encryptedPassword = encrypt(txtPassword.Text.ToString());
            }
            else
            {
                e.Cancel = true;
            }
        }

        private string encrypt(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
