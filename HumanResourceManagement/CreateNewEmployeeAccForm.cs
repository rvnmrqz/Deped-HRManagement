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
using MetroFramework.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

namespace HumanResourceManagement
{
    public partial class CreateNewEmployeeAccForm : Form
    {

        public bool systemUserCreated = false;
        bool usernameAvailable = false;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public CreateNewEmployeeAccForm()
        {
            InitializeComponent();
        }

        private void lblFormExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CreateNewEmployeeAccForm_Load(object sender, EventArgs e)
        {
            TempHolder.ceaf = this;
            conn = new SqlConnection(getStringValue("sqlconstring"));

            //LOAD CIVIL STATUS
            for (int i = 0; i < TempHolder.civil_status_list.Count; i++)
            {
                cmbCivilStatus.Items.Add(TempHolder.civil_status_list[i]);
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //validate then save
                if (!hasInputProblem())
                {
                    openSQLConnection();
                    string savingSQL = "INSERT INTO " + SQLbank.TBL_EMPLOYEES + "(" +
                                        SQLbank.EMPLOYEE_NO + "," +
                                        SQLbank.PLANTILLA_NO + "," +
                                        SQLbank.EMP_LAST_NAME + "," +
                                        SQLbank.EMP_FIRST_NAME + "," +
                                        SQLbank.EMP_MIDDLE_NAME + "," +
                                        SQLbank.SEX + "," +
                                        SQLbank.DATE_OF_BIRTH + "," +
                                        SQLbank.BIRTH_PLACE + "," +
                                        SQLbank.CIVIL_STATUS + "," +
                                        SQLbank.HDMF_NO + "," +
                                        SQLbank.PHIC_NO + "," +
                                        SQLbank.BP_NO + "," +
                                        SQLbank.ACCOUNT_NO + "," +
                                        SQLbank.TIN_NO;

                    if (lblPictureDirectory.Text.Length > 0)
                    {
                        savingSQL += ", " + SQLbank.PICTURE;
                    }

                    savingSQL += ") ";
                    savingSQL += " OUTPUT INSERTED.EMP_ID " +
                    " VALUES(@EMPNO, @PLANTILLA, @LNAME, @FNAME, @MNAME, @SEX, @BIRTH, @BIRTHPLACE, @CIVILSTAT, @HDMF, @PHIC, @BP, @ACC, @TIN";

                    if (lblPictureDirectory.Text.Length > 0)
                    {
                        savingSQL += ", @PICTURE";
                    }

                    savingSQL+=")";

                    cmd = new SqlCommand(savingSQL, conn);

                    cmd.Parameters.AddWithValue("@EMPNO", txtEmplyeeNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@PLANTILLA", txtPlantillaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@LNAME", txtLname.Text.Trim());
                    cmd.Parameters.AddWithValue("@FNAME", txtFname.Text.Trim());
                    cmd.Parameters.AddWithValue("@MNAME", txtMname.Text.Trim());
                    cmd.Parameters.AddWithValue("@SEX", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@BIRTH", txtDateOfBirth.Text);
                    cmd.Parameters.AddWithValue("@BIRTHPLACE", txtBirthPlace.Text);
                    cmd.Parameters.AddWithValue("@CIVILSTAT", cmbCivilStatus.Text); 
                    cmd.Parameters.AddWithValue("@HDMF", txthdmf.Text);
                    cmd.Parameters.AddWithValue("@PHIC", txtphic.Text.Trim());
                    cmd.Parameters.AddWithValue("@BP", txtBp.Text.Trim());
                    cmd.Parameters.AddWithValue("@ACC", txtAccNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@TIN", txtTin.Text.Trim());

                    if (lblPictureDirectory.Text.Length > 0)
                    {
                        byte[] pictureByte = fileToByte(lblPictureDirectory.Text.ToString());
                        cmd.Parameters.Add("@PICTURE", SqlDbType.VarBinary, pictureByte.Length).Value = pictureByte;
                    }

                    string lastInsertId = cmd.ExecuteScalar().ToString();

                    MessageBox.Show("New Employee Saved","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    if (systemUserCreated)
                    {
                        try
                        {
                            //creation of system user acc
                            string createSysAcc = "INSERT INTO " + SQLbank.TBL_USERS + "(" + SQLbank.USERNAME + "," + SQLbank.PASSWORD + "," + SQLbank.ROLE + "," + SQLbank.FNAME + "," + SQLbank.MNAME + "," + SQLbank.LNAME;
                            if (lblPictureDirectory.Text.Length > 0)
                            {
                                createSysAcc += "," + SQLbank.PICTURE; 
                            }

                            createSysAcc += ") VALUES (@USERNAME,@PASSWORD,@ROLE,@FNAME,@MNAME,@LNAME";

                            if (lblPictureDirectory.Text.Length > 0)
                            {
                                createSysAcc += " ,@PICTURE";
                            }

                            createSysAcc+=")";

                            cmd = new SqlCommand(createSysAcc, conn);
                            cmd.Parameters.AddWithValue("@USERNAME", lblUsername.Text);
                            cmd.Parameters.AddWithValue("@PASSWORD", encrypt(lblPassword.Text));
                            cmd.Parameters.AddWithValue("@ROLE", "User");
                            cmd.Parameters.AddWithValue("@FNAME", txtFname.Text);
                            cmd.Parameters.AddWithValue("@MNAME", txtMname.Text);
                            cmd.Parameters.AddWithValue("@LNAME", txtLname.Text);


                            if (lblPictureDirectory.Text.Length > 0)
                            {
                                byte[] pictureByte = fileToByte(lblPictureDirectory.Text.ToString());
                                cmd.Parameters.Add("@PICTURE", SqlDbType.VarBinary, pictureByte.Length).Value = pictureByte;
                            }


                            cmd.ExecuteNonQuery();
                            MessageBox.Show("New System Account Created", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ee)
                        {
                            Console.WriteLine("Creating system account error: " + ee.Message);
                        }
                    }
                    clearDisplay();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Failed to save: " + ee.Message);
            }
        }

        private void clearDisplay()
        {
            //clear the image of the picturebox
            lblSystemCreationMessage.Visible = false;
            lblUsername.ResetText();
            lblPassword.ResetText();

            pictureBox.Image = HumanResourceManagement.Properties.Resources.default_avatar;
            lblPictureDirectory.ResetText();

            txtFname.ResetText();
            txtMname.ResetText();
            txtLname.ResetText();
            txtDateOfBirth.ResetText();
            txtBirthPlace.ResetText();
            cmbGender.SelectedIndex = -1;
            cmbCivilStatus.SelectedIndex = -1;

            txtEmplyeeNo.ResetText();
            txtAccNo.ResetText();
            txtPlantillaNo.ResetText();
            txthdmf.ResetText();
            txtphic.ResetText();
            txtBp.ResetText();
            txtTin.ResetText();

        }
        
        private bool hasInputProblem()
        {
            if (txtEmplyeeNo.Text.Length == 0)
            {
                MessageBox.Show("Employee no. must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmplyeeNo.Select();
                return true;
            }

            if (txtEmplyeeNo.Text.Length > 0 && !usernameAvailable)
            {
                //check the availability of the username
                try
                {
                    openSQLConnection();
                    string sql = "SELECT " + SQLbank.EMP_ID + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.EMPLOYEE_NO + " = @EMPNO";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@EMPNO", txtEmplyeeNo.Text.Trim());
                    string empid = cmd.ExecuteScalar().ToString();
                    Console.WriteLine("\n\n" + empid + "\n\n");
                    if (empid.Length == 0) usernameAvailable = true;
                    else
                    {
                        usernameAvailable = false;
                        MessageBox.Show("Employee No. is already used", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return true;
                    }

                }
                catch (Exception ee)
                {
                    Console.WriteLine("Cannot check the availability of the username: Exception: " + ee.Message);
                }
            }


            if (txtFname.Text.Length == 0)
            {
                MessageBox.Show("First name must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFname.Select();
                return true;
            }
            if (txtLname.Text.Length == 0)
            {
                MessageBox.Show("Last name must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLname.Select();
                return true;
            }

            if (txtDateOfBirth.Text.Length == 0)
            {
                MessageBox.Show("Date of birth must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDateOfBirth.Select();
                return true;

            }

            DateTime dt;
            if(!DateTime.TryParse(txtDateOfBirth.Text,out dt))
            {
                MessageBox.Show("Invalid date for date of birth", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDateOfBirth.Select();
                return true;
            }

            if (calculateAge(Convert.ToDateTime(txtDateOfBirth.Text)) < 18)
            {
                MessageBox.Show("Age must be at least 18 years old", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDateOfBirth.Select();
                return true;
            }

            if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Gender", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbGender.DroppedDown = true;
                return true;
            }

            if (cmbCivilStatus.SelectedIndex == -1) {
                MessageBox.Show("Please select a Civil status", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbCivilStatus.DroppedDown = true;
                return true;
            }
            if (txtAccNo.Text.Length == 0)
            {
                MessageBox.Show("Account no. must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAccNo.Select();
                return true;
            }
            if (txthdmf.Text.Length == 0)
            {
                MessageBox.Show("HDMF no. must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txthdmf.Select();
                return true;
            }

            if (txtBp.Text.Length == 0)
            {
                MessageBox.Show("BP no. must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBp.Select();
                return true;
            }
            if (txtPlantillaNo.Text.Length == 0)
            {
                MessageBox.Show("Plantilla no. must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPlantillaNo.Select();
                return true;
            }
            if (txtphic.Text.Length == 0)
            {
                MessageBox.Show("PHIC no. must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtphic.Select();
                return true;
            }
            if (txtTin.Text.Length == 0) { 

                MessageBox.Show("TIN no. must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTin.Select();
                return true;
            }

            return false;
        }

        //*********************************************************************
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int calculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        private void txtEmplyeeNo_TextChanged(object sender, EventArgs e)
        {
            usernameAvailable = false;
        }

        private void btnChoosePhoto_Click(object sender, EventArgs e)
        {
            //browse pictures
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.jpg; *.jpeg; *.png; *.bmp";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pictureBox.Image = new Bitmap(opendlg.FileName);
                // image file path  
                lblPictureDirectory.Text = opendlg.FileName;
            }


        }

        private bool copyFileToPictureFolder(string imagefilename)
        {
            try
            {
                string pictureFolderPath = TempHolder.picturePath;

                if (!Directory.Exists(pictureFolderPath))
                {
                    //directory does not exist, create path
                    Directory.CreateDirectory(pictureFolderPath);
                }

                //copy file
                File.Copy(lblPictureDirectory.Text, (pictureFolderPath + imagefilename),true);

            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while copying the user's image", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;          
        }

        public void updateSystemAcc(string username, string password)
        {
            lblUsername.Text = username;
            lblPassword.Text = password;
            lblSystemCreationMessage.Visible = true;
        }

        private string encrypt(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        protected bool userAuthorizedToUseFunction(List<string> authorization)
        {
            if (authorization.Contains(TempHolder.accountType))
            {
                return true;
            }
            else
            {
                MessageBox.Show("You are not authorized to use this function", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void lblCreateSystemAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.ADD_SYS_USER_PERMISSION))
            {
                CreateSystemUserDialog dialog = new CreateSystemUserDialog();
                dialog.ShowDialog();
            }
        }

        private void CreateNewEmployeeAccForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TempHolder.ceaf = null;
        }

        private void txtDateOfBirht_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;
            e.SuppressKeyPress = dateTimeTextKeyDownBoxChecker(txtDateOfBirth,keyChar,e.KeyCode);
        }

        private bool dateTimeTextKeyDownBoxChecker(MetroTextBox textbox,char keyChar, Keys keycode)
        {
            bool cancelEVent = true;

            if (Char.IsNumber(keyChar) || Char.IsControl(keyChar) || (keycode >= Keys.NumPad0 && keycode <= Keys.NumPad9))
            {
                Console.WriteLine("Key Not Blocked");
                cancelEVent = false;

                if (Char.IsNumber(keyChar) || (keycode >= Keys.NumPad0 && keycode <= Keys.NumPad9))
                {
                    string textString = textbox.Text;
                    int textlength = textbox.Text.Length;

                    switch (textlength)
                    {
                        case 0:
                            //do nothing
                            break;
                        case 1:
                            int monthFirstDigit = Convert.ToInt32(textString.Substring(0, 1));
                            if (monthFirstDigit > 1)
                            {
                                textbox.Text = "0" + textString;
                                addSlash(textbox,2);
                            }
                            break;
                        case 2:
                            addSlash(textbox,2);
                            break;
                        case 4:
                            int dayFirstDigit = Convert.ToInt32(textString.Substring(3, 1));
                            if (dayFirstDigit > 3)
                            {
                                textbox.Text = textString.Insert(3, "0");
                                addSlash(textbox,5);
                            }
                            break;
                        case 5:
                            addSlash(textbox,5);
                            break;
                        case 9:
                            Console.WriteLine("Checking");
                            //check if valid
                            DateTime dt;
                            if (!DateTime.TryParse(textbox.Text.ToString(), out dt))
                            {
                                MessageBox.Show("Invalid Date Value");
                            }
                            break;
                    }
                }
            }
            return cancelEVent;
        }

        private void addSlash(MetroTextBox textbox,int placeIndex)
        {
            string txtString = textbox.Text;
            textbox.Text = txtString.Insert(placeIndex, "/");
            textbox.SelectionStart = textbox.Text.Length;
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
