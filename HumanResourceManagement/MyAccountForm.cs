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
using System.IO;

namespace HumanResourceManagement
{
    public partial class MyAccountForm : Form
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


        public MyAccountForm()
        {
            InitializeComponent();
        }

        //**************************FORM EVENTS*******************************
        private void MyAccountForm_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
            prepareDisplay();
        }

        private void FormPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void lblFormExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePass changeForm = new ChangePass();
            changeForm.ShowDialog();
        }

        //**********************HELPER METHODS********************************

        private void prepareDisplay()
        {
            string file_path = TempHolder.picturePath+TempHolder.pictureFilename;

            if (TempHolder.userImage != null) pictureBox1.Image = TempHolder.userImage;

            lblAccountType.Text = TempHolder.accountType;
            txtFirstname.Text = TempHolder.fname;
            txtMiddleInitial.Text = TempHolder.mname;
            txtLastname.Text = TempHolder.lname;
            txtUsername.Text = TempHolder.username.Trim();

            txtFirstname.BackColor = SystemColors.Window;
            txtMiddleInitial.BackColor = SystemColors.Window;
            txtLastname.BackColor = SystemColors.Window;
            txtUsername.BackColor = SystemColors.Window;

        }

        private void enableInputs(bool value)
        {
            btnChangePhoto.Visible = value;
            btnChangePhoto.Enabled = value;
            pictureBox1.Enabled = value;
            txtFirstname.Enabled = value;
            txtMiddleInitial.Enabled = value;
            txtLastname.Enabled = value;
            txtUsername.Enabled = value;
        }
        
        private void attemptToSave()
        {
            
            if (isInputValid())
            {
                //inputs are valid, save changes
                try
                {
                    enableInputs(false);

                    byte[] bytePicture;
                    cmd = new SqlCommand();

                    openSQLConnection();
                    string cmdText = "UPDATE " + SQLbank.TBL_USERS + " SET "
                        + SQLbank.FNAME + " = @FNAME, "
                        + SQLbank.MNAME + " = @MNAME, "
                        + SQLbank.LNAME + " = @LNAME ,"
                        + SQLbank.USERNAME + " = @USERNAME ";

                    if (lblUploadedPicturePath.Text.Trim().Length > 0)
                    {
                        bytePicture = fileToByte(lblUploadedPicturePath.Text.ToString());
                        cmdText += ", " + SQLbank.PICTURE + " = @PICTURE";
                        cmd.Parameters.Add("@PICTURE", SqlDbType.VarBinary, bytePicture.Length).Value = bytePicture;
                        TempHolder.userImage = byteToImage(bytePicture);
                    }
                    cmdText+= " WHERE " + SQLbank.ID + "=" + TempHolder.loggedUser_ID;

                 
                    cmd.Parameters.Add("@FNAME", SqlDbType.VarChar);
                    cmd.Parameters.Add("@MNAME", SqlDbType.VarChar);
                    cmd.Parameters.Add("@LNAME", SqlDbType.VarChar);
                    cmd.Parameters.Add("@USERNAME", SqlDbType.VarChar);

                    cmd.Parameters["@FNAME"].Value = txtFirstname.Text;
                    cmd.Parameters["@MNAME"].Value = txtMiddleInitial.Text;
                    cmd.Parameters["@LNAME"].Value = txtLastname.Text;
                    cmd.Parameters["@USERNAME"].Value = txtUsername.Text;

                    cmd.CommandText = cmdText;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                    TempHolder.fname = txtFirstname.Text;
                    TempHolder.mname = txtMiddleInitial.Text;
                    TempHolder.lname = txtLastname.Text;
                    TempHolder.username = txtUsername.Text;
          

                    MessageBox.Show("Update Successfully Saved", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnEditCancel.Text = "Edit";

                }
                catch (Exception ee)
                {
                    Console.WriteLine("Saving Exception: " + ee.Message);
                    MessageBox.Show("An error occured while saving changes", "Failed to save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private bool isInputValid()
        {
            enableInputs(false);
            bool remarks = true;

            //FIRST NAME
            if (txtFirstname.Text.Length == 0)
            {
                txtFirstname.BackColor = SystemColors.HotTrack;
                remarks = false;
            }
            else
            {
                txtFirstname.BackColor = SystemColors.Window;
            }

            //LAST NAME
            if(txtLastname.Text.Length == 0)
            {
                txtLastname.BackColor = SystemColors.HotTrack;
                remarks = false;
            }
            else
            {
                txtLastname.BackColor = SystemColors.Window;
            }

            //USERNAME
            if(txtUsername.Text.Length == 0)
            {
                txtUsername.BackColor = SystemColors.HotTrack;
                remarks =  false;
            }
            else
            {
                txtUsername.Text = txtUsername.Text.Trim();
                txtUsername.BackColor = SystemColors.Window;
            }

            if (!txtUsername.Text.Trim().Equals(TempHolder.username, StringComparison.CurrentCultureIgnoreCase))
            {
                //username is changed

                //check for similar username
                try
                {
                    openSQLConnection();
                    string cmdString = "SELECT COUNT(" + SQLbank.USERNAME + ") FROM " + SQLbank.TBL_USERS + " WHERE " + SQLbank.USERNAME + " = @USERNAME AND " + SQLbank.ID + " != " + TempHolder.loggedUser_ID+";"; //same username but not the id (primary key) of the user
                    cmd = new SqlCommand(cmdString,conn);
                    cmd.Parameters.Add("@USERNAME", SqlDbType.VarChar);
                    cmd.Parameters["@USERNAME"].Value = txtUsername.Text.Trim();

                    Console.WriteLine(cmd.CommandText.ToString());

                    if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
                    {
                        //there is a similar username already
                        txtUsername.BackColor = SystemColors.HotTrack;
                        remarks = false;
                        MessageBox.Show("Username already used", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
    
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Checking Similar Username Exception: " + ee.Message);
                }
                finally
                {
                    closeSQLConnection();
                }
                

            }

            enableInputs(true);
            return remarks;
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

        //***************************COMPONENT EVENTS**************************
        private void btnEditCancel_Click(object sender, EventArgs e)
        {
            if (btnEditCancel.Text.Equals("Edit"))
            {
                //edit mode

                btnEditCancel.Text = "Cancel";

                //enable text fields
                enableInputs(true);
            }
            else if (btnEditCancel.Text.Equals("Cancel"))
            {
                //cancel editing
                this.ActiveControl = null; //to remove focus on components
                enableInputs(false);    //disable text fields
                prepareDisplay(); //redisplay user info
                btnEditCancel.Text = "Edit";
            }
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            if (btnEditCancel.Text.Equals("Cancel"))
            {
                //user is in edit mode
                //save changes
                attemptToSave();
            }
            else
            {
                //not in edit mode, just close the dialog
                this.Close();
            }
        }

        private void txtFirstname_TextChanged(object sender, EventArgs e)
        {
            if (txtFirstname.Text.Length == 0) txtFirstname.BackColor = SystemColors.HotTrack;
            else txtFirstname.BackColor = SystemColors.Window;

        }

        private void txtLastname_TextChanged(object sender, EventArgs e)
        {
            if (txtLastname.Text.Length == 0) txtLastname.BackColor = SystemColors.HotTrack;
            else txtLastname.BackColor = SystemColors.Window;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length == 0) txtUsername.BackColor = SystemColors.HotTrack;
            else txtUsername.BackColor = SystemColors.Window;
        }

        private void btnChoosePhoto_Click(object sender, EventArgs e)
        {
            browsePhoto();
        }

        protected void browsePhoto()
        {
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.jpg; *.jpeg; *.bmp; *.png;";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pictureBox1.Image = new Bitmap(opendlg.FileName);
                // image file path  
                lblUploadedPicturePath.Text = opendlg.FileName;
            }
        }

        private void btnChangePhoto_Click(object sender, EventArgs e)
        {
            browsePhoto();
        }



        private byte[] fileToByte(string filepath)
        {
            byte[] file;
            using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }

            return file;
        }

        private Image byteToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

    }
}
