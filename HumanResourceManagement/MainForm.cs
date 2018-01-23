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
            TempHolder.mainForm = this;
            conn = new SqlConnection(getStringValue("sqlconstring"));

            loadSalaryGradesAndSteps();
          
            loadTab2();
            loadTab1();

           
            metroTabControl1.SelectedTab = tabPage1;
            txtEmplyeeNo.Select();
        }

        private void loadSalaryGradesAndSteps()
        {
            for (int i = 0; i < TempHolder.salary_grade_list.Count; i++)
            {
                cmbSalaryGrade.Items.Add(TempHolder.salary_grade_list[i]);
            }
            for (int x = 0; x < TempHolder.steps_list.Count ; x++)
            {
                cmbSteps.Items.Add(TempHolder.steps_list[x]);
            }
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
            if(this.WindowState == FormWindowState.Maximized)
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
            Application.Exit();
        }

        //******************MENU STRIP ITEM CLICK EVENTS*********************
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
            MyAccountForm myAcc = new MyAccountForm();
            myAcc.ShowDialog();
        }

        private void menuItem_Lougout_Click(object sender, EventArgs e)
        {
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
            DialogResult dr = MessageBox.Show("You're about to close the program, continue?", "Closing", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dr == DialogResult.OK)  Application.Exit();
        }

        private void excelToSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.EXCEL_EXTRACTOR_PERMISSION)==null)
            {
                //show backup dialog then show excel extractor
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
                UserControlServiceRecord.Instance.BringToFront();
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
            if (e.KeyData == Keys.Enter && txtEmplyeeNo.Text.Length > 0)
            {
                search();
            }
            else if(e.KeyData == Keys.Enter && txtEmplyeeNo.Text.Length==0){
                clearDisplay();
            }
            else if (lblemployee_id_hidden.Text.Length > 0)
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
                    lblemployee_id_hidden.Text = reader[SQLbank.EMP_ID].ToString();
                    txtPlantillaNo.Text = reader[SQLbank.PLANTILLA_NO].ToString();
                    txtPositionTitle.Text = reader[SQLbank.POSITION_TITLE].ToString();
                    cmbSalaryGrade.Text = reader[SQLbank.SALARY_GRADE].ToString();
                    cmbSteps.Text = reader[SQLbank.STEP].ToString();

                    string dateOfOriginalAppointment = reader[SQLbank.DATE_OF_ORIGINAL_APPOINTMENT].ToString();
                    if (dateOfOriginalAppointment.Length > 0)
                    {
                        DateTime dt;
                        if(DateTime.TryParse(dateOfOriginalAppointment,out dt)){
                            txtOriginalAppointment.Text = dt.ToString("MM/dd/yyyy");
                        }
                    }

                    string filename = reader[SQLbank.PICTUREFILENAME].ToString().Trim();
                    if (filename.Length > 0)
                    {
                        //user has picture
                        try
                        {
                            pictureBox1.Image = Image.FromFile(@TempHolder.picturePath + filename);
                        }
                        catch (Exception ee)
                        {
                            Console.WriteLine("Failed to load the image of the user: " + ee.Message);
                        }
                    }
                }

                if (resultCount == 0) showSearchResultMessage("No employee number matched");
                else
                {
                    showSearchResultMessage(null);

                    //load tab 1,2,3
                    TempHolder.uc_PersonalInfo.loadInfo(lblemployee_id_hidden.Text);
                    TempHolder.uc_ServiceRecord.loadRecords(lblemployee_id_hidden.Text);
                }

            }
            catch (Exception ee)
            {
                showSearchResultMessage("An error occured");
                MessageBox.Show("Problem occured while searching", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Search Exception: " + ee.Message);
            }
        }

        public void clearDisplay()
        {
            Console.WriteLine("Clear Display -Main");
            pictureBox1.Invalidate();
            pictureBox1.Image = Properties.Resources.default_avatar;

            lblPictureDirectory.ResetText();

            lblemployee_id_hidden.ResetText();
            lblemployee_id_hidden.ResetText();
            txtPlantillaNo.ResetText();
            txtPositionTitle.ResetText();
            cmbSalaryGrade.SelectedIndex = -1;
            cmbSteps.SelectedIndex = -1;
            txtOriginalAppointment.ResetText();
           

            //tabs
            TempHolder.uc_PersonalInfo.clearDisplay();
            TempHolder.uc_ServiceRecord.clearDisplay();
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
            if (editmodeValue) txtPlantillaNo.Select();
            else txtEmplyeeNo.Select();

            txtEmplyeeNo.Enabled = !editmodeValue;
            txtPlantillaNo.Enabled = editmodeValue;
            txtPositionTitle.Enabled = editmodeValue;
            cmbSalaryGrade.Enabled = editmodeValue;
            cmbSteps.Enabled = editmodeValue;
            txtOriginalAppointment.Enabled = editmodeValue;
            btnChoosePhoto.Visible = editmodeValue;

            TempHolder.uc_PersonalInfo.editMode(editmodeValue);
        }

        public int saveUpdates()
        {
            /*
               -1 = error
                1 = save successful
                0 = validation failed
            */
            try
            {
                if (fieldsAreValid())
                {
                    openSQLConnection();
                    string updateSQL = "UPDATE " + SQLbank.TBL_EMPLOYEES + " SET " +
                        SQLbank.PLANTILLA_NO + " = @PLANTILLA ," +
                        SQLbank.POSITION_TITLE + " = @POSITION," +
                        SQLbank.STEP + " = @STEP, " +
                        SQLbank.DATE_OF_ORIGINAL_APPOINTMENT + " = @DOA " +
                        " WHERE " + SQLbank.EMP_ID + " = " + lblemployee_id_hidden.Text;

                    cmd = new SqlCommand(updateSQL, conn);
                    cmd.Parameters.AddWithValue("@PLANTILLA", txtPlantillaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@POSITION", txtPositionTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@STEP", cmbSteps.Text);
                    cmd.Parameters.AddWithValue("@DOA", txtOriginalAppointment.Text);

                    cmd.ExecuteNonQuery();

                    if (lblPictureDirectory.Text.Length != 0)
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
                            }
                        }
                    }

                        return 1;
                }
                else return 0;
           
            }
            catch (Exception ee)
            {
                Console.WriteLine("Mainform - saveUpdates(), Exception: " + ee.Message);
                return -1;
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
                File.Copy(lblPictureDirectory.Text, (pictureFolderPath + imagefilename), true);

            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while copying the user's image", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        protected bool fieldsAreValid()
        {
            if (txtPlantillaNo.Text.Length == 0)
            {
                MessageBox.Show("Plantilla number must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtPositionTitle.Text.Length == 0)
            {
                MessageBox.Show("Designation must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (cmbSalaryGrade.SelectedIndex == -1)
            {
                MessageBox.Show("Salary Grade must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (cmbSteps.SelectedIndex == -1)
            {
                MessageBox.Show("Salary Step must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtOriginalAppointment.Text.Length == 0)
            {
                MessageBox.Show("Date of Original Appointment must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        
            DateTime dt;
            if (!DateTime.TryParse(txtOriginalAppointment.Text, out dt) || txtOriginalAppointment.Text.Length!=10)
            {
                MessageBox.Show("Invalid date for Original Appointment", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                return false;
            }
            return true;
        }

        private bool dateTimeTextKeyDownBoxChecker(MetroTextBox textbox, char keyChar)
        {
            bool cancelEVent = true;

            if (Char.IsNumber(keyChar) || Char.IsControl(keyChar))
            {
                Console.WriteLine("Key Not Blocked");
                cancelEVent = false;

                if (Char.IsNumber(keyChar))
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
                                addSlash(textbox, 2);
                            }
                            break;
                        case 2:
                            addSlash(textbox, 2);
                            break;
                        case 4:
                            int dayFirstDigit = Convert.ToInt32(textString.Substring(3, 1));
                            if (dayFirstDigit > 3)
                            {
                                textbox.Text = textString.Insert(3, "0");
                                addSlash(textbox, 5);
                            }
                            break;
                        case 5:
                            addSlash(textbox, 5);
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

        private void addSlash(MetroTextBox textbox, int placeIndex)
        {
            string txtString = textbox.Text;
            textbox.Text = txtString.Insert(placeIndex, "/");
            textbox.SelectionStart = textbox.Text.Length;
        }

        private void toolItem_SQLBackup_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.SQL_BACKUP_PERMISSION)==null)
            {

            }
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
                lblPictureDirectory.Text = opendlg.FileName;
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
            if (txtEmplyeeNo.Text.Length == 0) showSearchResultMessage(null);
        }

        private void txtOriginalAppointment_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;
            e.SuppressKeyPress = dateTimeTextKeyDownBoxChecker(txtOriginalAppointment, keyChar);
        }

        private void mainPanelLeft_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
