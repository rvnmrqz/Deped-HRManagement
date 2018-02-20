using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using MetroFramework.Controls;

namespace HumanResourceManagement
{
    public partial class MainForm : Form
    {
        bool fromLogout = false;
        LoadingForm loadingform;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        //    this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
         //   this.WindowState = FormWindowState.Maximized;

            TempHolder.mainForm = this;
            conn = new SqlConnection(getStringValue("sqlconstring"));

              loadTab2();
  //          loadTab1();

            metroTabControl1.SelectedTab = tabPage1;
            txtEmplyeeNo.Select();


            TempHolder.excelApp = new Microsoft.Office.Interop.Excel.Application();
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

        //*******************APP CONFIG MANAGER************************************************
        private string getStringValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

        }

        //***************************FORM TOP PANEL EVENTS**************************************
        private void FormPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void lblFormMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblFormMaxMin_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;

            }
        }

        private void lblFormExit_Click(object sender, EventArgs e)
        {
            aboutToExit();
        }

        //******************MENU STRIP ITEM CLICK EVENTS*********************
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            MyAccountForm myAcc = new MyAccountForm();
            myAcc.ShowDialog();
        }

        private void menuItem_Lougout_Click(object sender, EventArgs e)
        {
            fromLogout = true;
            this.Close();
            if (TempHolder.loginform != null)
            {
                TempHolder.loginform.reshowForm();
            }
            else
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            aboutToExit();
        }

        private void aboutToExit()
        {
            DialogResult dr = MessageBox.Show("You're about to close the program, continue?", "Closing", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK) Application.Exit();

        }


        private void MenuSQLSettings_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.SQL_SETTINGS_PERMISSION))
            {
                SQLSettingsForm sf = new SQLSettingsForm();
                sf.ShowDialog();
            }
        }

        private void menuDBBackup_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.SQL_BACKUP_PERMISSION))
            {

            }
        }

        private void menuSchools_Click(object sender, EventArgs e)
        {
            SchoolMaintenance sm = new SchoolMaintenance();
            sm.ShowDialog();
        }

        private void menuSystemAccounts_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.MODIFY_SYS_USER_PERMISSION))
            {
                SystemUserMaintenance sum = new SystemUserMaintenance();
                sum.ShowDialog();   
            }
        }

        //****************************TAB PAGES*********************************
        private void loadTab1()
        {
            if (!tab1_panel.Controls.Contains(UserControlPersonalInfo.Instance))
            {
                tab1_panel.Controls.Add(UserControlPersonalInfo.Instance);
                UserControlPersonalInfo.Instance.Dock = DockStyle.Fill;
                UserControlPersonalInfo.Instance.BringToFront();
                TempHolder.uc_PersonalInfo = UserControlPersonalInfo.Instance;
            }
            else
            {
                UserControlPersonalInfo.Instance.BringToFront();
            }
        }

        private void loadTab2()
        {
            if (!tab2_Panel.Controls.Contains(UserControlServiceRecord.Instance))
            {
                tab2_Panel.Controls.Add(UserControlServiceRecord.Instance);
                UserControlServiceRecord.Instance.Dock = DockStyle.Fill;
                //UserControlServiceRecord.Instance.BringToFront();
                TempHolder.uc_ServiceRecord = UserControlServiceRecord.Instance;
            }
            else
            {
                UserControlServiceRecord.Instance.BringToFront();
            }
        }

        //******************************SEARCHING******************************
        private void metroTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;

            if (e.KeyData == Keys.Enter && txtEmplyeeNo.Text.Length > 0)
            {
                search();
            }
            else if (e.KeyData == Keys.Enter && txtEmplyeeNo.Text.Length == 0)
            {
                clearDisplay();
            }
            else if (lblemployee_id_hidden.Text.Length > 0 && Char.IsLetterOrDigit(keyChar))
            {
                clearDisplay();
            }
        }

        public void search()
        {
            try
            {
                Console.WriteLine("\n******************* SEARCHING ******************\n");
                clearDisplay();

                int resultCount = 0;
                showSearchResultMessage("Searching...");
                //search

                openSQLConnection();
                string cmdtext = "SELECT * FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.EMPLOYEE_NO + " = @EMPLOYEE_NO";

                cmd = new SqlCommand(cmdtext, conn);

                cmd.Parameters.AddWithValue("@EMPLOYEE_NO", txtEmplyeeNo.Text.Trim());

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    resultCount++;

                    TempHolder.searchedName = reader[SQLbank.EMP_LAST_NAME].ToString().ToUpper()+"    "+reader[SQLbank.EMP_FIRST_NAME].ToString().ToUpper()+"    " + reader[SQLbank.EMP_MIDDLE_NAME].ToString().ToUpper();
                    TempHolder.searchedSheetName = reader[SQLbank.EMP_FIRST_NAME].ToString();


                    lblemployee_id_hidden.Text = reader[SQLbank.EMP_ID].ToString();
                    //txtPlantillaNo.Text = reader[SQLbank.PLANTILLA_NO].ToString();

                    string filename = reader[SQLbank.PICTUREFILENAME].ToString().Trim();

                    if (filename.Length > 0)
                    {
                        //user has picture
                        try
                        {
                            pictureBox1.Image =GetImage(TempHolder.picturePath + filename);
                            TempHolder.searchedPictureFilename = TempHolder.picturePath + filename;
                        }
                        catch (Exception ee)
                        {
                            Console.WriteLine("Failed to load the image of the user: " + ee.Message);
                        }
                    }
                }

                if (resultCount == 0)
                {
                    btnClearSearch.Visible = false;
                    showSearchResultMessage("No employee number matched");
                }
                else
                {
                    showSearchResultMessage(null);
                    //load tab 1,2,3
                    TempHolder.uc_PersonalInfo.loadInfo(lblemployee_id_hidden.Text);
                    TempHolder.uc_ServiceRecord.loadRecords(lblemployee_id_hidden.Text);
                    btnClearSearch.Visible = true;
                }

            }
            catch (Exception ee)
            {
                showSearchResultMessage("An error occured");
                MessageBox.Show("Problem occured while searching", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Search Exception: " + ee.Message);
            }
        }

        public void showOtherEmpInfo()
        {
                //added +"" just to avoid null
                txtSchoolName.Text = TempHolder.searchedLastSchool + "";
                txtDesignation.Text = TempHolder.lastDesignation + "";
                txtDateOfOriginalAppointment.Text = TempHolder.searchedOriginalAppointment + "";
        }

        public void clearDisplay()
        {
            Console.WriteLine("Clear Display -Main");
            TempHolder.clearSearchTempValues();
            pictureBox1.Invalidate();
            pictureBox1.Image = Properties.Resources.default_avatar;
            lblUploadedPicture.ResetText();
            lblemployee_id_hidden.ResetText();
            lblemployee_id_hidden.ResetText();

            txtSchoolName.ResetText();
            txtDesignation.ResetText();
            txtDateOfOriginalAppointment.ResetText();

            //tabs
            TempHolder.uc_PersonalInfo.clearDisplay();
            TempHolder.uc_ServiceRecord.clearDisplay();
        }

        public static Image GetImage(string path)
        {
            Image img;
            using (Image temp = Image.FromFile(path))
            {
                img = new Bitmap(temp);
            }
            return img;
        }

        private void showSearchResultMessage(string msg)
        {
            if (msg != null)
            {
                lblEmployeeResult.Visible = true;
                lblEmployeeResult.Text = msg;
            }
            else lblEmployeeResult.Visible = false;
        }

        //********************************************************************

        public void editMode(bool editmodeValue)
        {
            txtEmplyeeNo.Enabled = !editmodeValue;
            btnChoosePhoto.Visible = editmodeValue;
            btnClearSearch.Visible = !editmodeValue;
            TempHolder.uc_PersonalInfo.editMode(editmodeValue);
        }

        public int saveUpdates()
        {
            /*
               -1 = error
                1 = save successful
                0 = validation failed
            */

            if (lblUploadedPicture.Text.Length != 0)
            {
                string filename = lblemployee_id_hidden.Text + ".png";
                if (copyFileToPictureFolder(filename))
                {
                    //picture is successfully copied
                    //update the value of picture_filename column in the database
                    try
                    {
                        openSQLConnection();
                        string updateqry = "UPDATE " + SQLbank.TBL_EMPLOYEES + " SET " + SQLbank.PICTUREFILENAME + "= @FILENAME WHERE " + SQLbank.EMP_ID + " = " + lblemployee_id_hidden.Text;
                        cmd = new SqlCommand(updateqry, conn);
                        cmd.Parameters.AddWithValue("@FILENAME", filename);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Failed update employee's picture info", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Saving updates in picture field error: " + ee.Message);
                        return -1;
                    }
                }
            }
            return 1;
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
                File.Copy(lblUploadedPicture.Text, (pictureFolderPath + imagefilename), true);

            }
            catch (Exception ee)
            {
                Console.WriteLine("Copying Exception : " + ee.Message);
                MessageBox.Show("An error occured while copying the user's image", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnChoosePhoto_Click(object sender, EventArgs e)
        {
            //browse pictures
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.jpg; *.jpeg; *.png";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pictureBox1.Image = new Bitmap(opendlg.FileName);
                // image file path  
                lblUploadedPicture.Text = opendlg.FileName;
            }

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.ADD_EMPLOYEE_PERMISSION))
            {
                CreateNewEmployeeAccForm cnf = new CreateNewEmployeeAccForm();
                cnf.ShowDialog();
            }
        }

        private void txtEmplyeeNo_TextChanged(object sender, EventArgs e)
        {
            showSearchResultMessage(null);
            btnClearSearch.Visible = false;

            if (lblemployee_id_hidden.Text.Length > 0 && !txtEmplyeeNo.Text.Equals(lblemployee_id_hidden.Text))
            {
                clearDisplay();
            }
        }

        private void mainPanelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TempHolder.quitExcel();

            if (!fromLogout) Application.Exit();
        }

        private void txtDateOfOriginalAppointment_TextChanged(object sender, EventArgs e)
        {
            if (txtDateOfOriginalAppointment.Text.Length != 0)
            {
                lblDOAPrettyTime.Text = computeAgo(Convert.ToDateTime(txtDateOfOriginalAppointment.Text));
            }
            else lblDOAPrettyTime.ResetText();
        }

        private  string computeAgo(DateTime dt)
        {
            string msg="";

            TimeSpan span = DateTime.Today - dt;
            double days = span.TotalDays;

            msg = days.ToString("N0") +" day";
            if (days > 1)
            {
                msg += "s";
            }

            msg += " ago";
            
            return msg;
        }

        private void btnClearSearch_Click_1(object sender, EventArgs e)
        {
            txtEmplyeeNo.ResetText();
        }
    }
}
