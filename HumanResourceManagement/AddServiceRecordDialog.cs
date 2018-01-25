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
using MetroFramework.Controls;
using System.Data.SqlClient;
using System.Configuration;

namespace HumanResourceManagement
{
    public partial class AddServiceRecordDialog : Form
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

        public AddServiceRecordDialog()
        {
            InitializeComponent();
        }

        private void lblExit_Click(object sender, EventArgs e)
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

        private void AddServiceRecordDialog_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
            loadStatus();
            prepareDisplay();
           
       
        }

        private void loadStatus()
        {
            for (int i = 0; i < TempHolder.status_list.Count; i++)
            {
                cmbStatus.Items.Add(TempHolder.status_list[i]);
            }
        }

        private void prepareDisplay()
        {
            //prepare display
            txtDateFrom.Text = DateTime.Today.ToString("MM/dd/yyyy");
            txtSchoolName.Text = TempHolder.searchedLastSchool;
            txtDesignation.Text = TempHolder.searchedLastDesignation;
            cmbStatus.Text = TempHolder.searchedLastStatus;
            txtSalary.Text = TempHolder.searchedLastSalary;
            txtStation.Text = TempHolder.searchedLastStation;
            txtBranch.Text = TempHolder.searchedLastBranch;
            txtCause.Text = TempHolder.searchedLastCause;
            txtLAWOP.Text = TempHolder.searchedLastLawop;
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

        private void chkSchool_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSchool.Checked)
            {
                txtSchoolName.Enabled = true;
                txtSchoolName.Select();
            }
            else {
                txtSchoolName.Enabled = false;
                txtSchoolName.Text = TempHolder.searchedLastSchool;
            }
        }

        private void chkDesignation_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDesignation.Checked)
            {
                txtDesignation.Enabled = true;
                txtDesignation.Select();
            }
            else {
                txtDesignation.Enabled = false;
                txtDesignation.Text = TempHolder.searchedLastDesignation;
            }
        }

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkStatus.Checked)
            {
                cmbStatus.Enabled = true;
                cmbStatus.Select();
            }
            else {
                cmbStatus.Enabled = false;
                cmbStatus.Text = TempHolder.searchedLastStatus;
            }
        }

        private void chkStation_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkStation.Checked)
            {
                txtStation.Enabled = true;
                txtStation.Select();
            }
            else {
                txtStation.Enabled = false;
                txtStation.Text = TempHolder.searchedLastStation;
            }
        }

        private void chkBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkBranch.Checked)
            {
                txtBranch.Enabled = true;
                txtBranch.Select();
            }
            else {
                txtBranch.Enabled = false;
                txtBranch.Text = TempHolder.searchedLastBranch;
            }
        }
        
        private void chkLawop_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkLawop.Checked)
            {
                txtLAWOP.Enabled = true;
                txtLAWOP.Select();
            }
            else {
                txtLAWOP.Enabled = false;
                txtLAWOP.Text = TempHolder.searchedLastSalary;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (isInputValid())
            {
                try
                {
                    openSQLConnection();
                    string sql = "INSERT INTO " + SQLbank.TBL_SERVICE_RECORDS + " ("
                        +SQLbank.EMP_ID
                        +","+SQLbank.SCHOOL_NAME
                        +","+SQLbank.FROM_DATE
                        +","+SQLbank.TO_DATE
                        +","+SQLbank.DESIGNATION
                        +","+SQLbank.STATUS
                        +","+SQLbank.SALARY
                        +","+SQLbank.STATION
                        +","+SQLbank.BRANCH
                        +","+SQLbank.CAUSE
                        +","+SQLbank.LAWOP+")"
                        +" OUTPUT INSERTED."+SQLbank.ID
                        +" VALUES(@EMPID,@SCHOOLNAME,@FROM,@TO,@DESIGNATION,@STATUS,@SALARY,@STATION,@BRANCH,@CAUSE,@LAWOP)";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@EMPID", TempHolder.searchedEMpId);
                    cmd.Parameters.AddWithValue("@SCHOOLNAME", txtSchoolName.Text);
                    cmd.Parameters.AddWithValue("@FROM", txtDateTo.Text);
                    cmd.Parameters.AddWithValue("@TO", txtDateTo.Text);
                    cmd.Parameters.AddWithValue("@DESIGNATION", txtDesignation.Text);
                    cmd.Parameters.AddWithValue("@STATUS", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@SALARY", txtSalary.Text);
                    cmd.Parameters.AddWithValue("@STATION", txtStation.Text);
                    cmd.Parameters.AddWithValue("@BRANCH", txtBranch.Text);
                    cmd.Parameters.AddWithValue("@CAUSE", txtCause.Text);
                    cmd.Parameters.AddWithValue("@LAWOP", txtLAWOP.Text);
                    string lastinsertId = cmd.ExecuteScalar().ToString();
                    Console.WriteLine(lastinsertId);

                    TempHolder.uc_ServiceRecord.addToTable(lastinsertId, txtSchoolName.Text, txtDateFrom.Text, txtDateTo.Text, txtDesignation.Text, cmbStatus.Text, txtSalary.Text, txtStation.Text, txtBranch.Text, txtCause.Text, txtLAWOP.Text);
                    MessageBox.Show("Successfully added");
                }
                catch (Exception ee)
                {
                    Console.WriteLine("\nAdding Service Record Exception: " + ee.Message);
                    MessageBox.Show("Adding Service Record Failed", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool isInputValid()
        {
            DateTime dt;
            if(!DateTime.TryParse(txtDateFrom.Text,out dt))
            {
                showMessage("Invalid date for FROM");
                return false;
            }

            //compare the date of last row to FROM value
            //compare the value of FROM and TO if inverted

            if(!chkPresent.Checked && !DateTime.TryParse(txtDateTo.Text,out dt))
            {
                showMessage("Invalid date for TO");
                return false;
            }

            if (txtSchoolName.Text.Trim().Length == 0)
            {
                showMessage("School name must not be empty");
                return false;
            }
            if (txtDesignation.Text.Trim().Length == 0)
            {
                showMessage("Designation must not be empty");
                return false;
            }
            if (cmbStatus.SelectedIndex == -1)
            {
                showMessage("Status must not be empty");
                return false;
            }
            if (txtStation.Text.Trim().Length == 0)
            {
                showMessage("Station must not be empty");
                return false;
            }
            if(txtBranch.Text.Trim().Length ==0)
            {
                showMessage("Branch must not be empty");
                return false;
            }
            if (txtSalary.Text.Trim().Length == 0)
            {
                showMessage("Salary must not be empty");
                return false;
            }
            if (txtCause.Text.Trim().Length == 0)
            {
                showMessage("Cause must not be empty");
                return false;
            }
            if(txtLAWOP.Text.Trim().Length == 0)
            {
                showMessage("LAWOP must not be empty");
                return false;
            }
            return true;
        }

        private void showMessage(string msg)
        {
            MessageBox.Show(msg, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void hotspo(object sender, EventArgs e)
        {

        }

        private void chkSalary_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSalary.Checked)
            {
                txtSalary.Enabled = true;
                txtSalary.Select();
            }
            else {
                txtSalary.Enabled = false;
                txtSalary.Text = TempHolder.searchedLastSalary;
            }
        }

        private void chkPresent_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void txtDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;
            e.SuppressKeyPress = dateTimeTextKeyDownBoxChecker(txtDateFrom, keyChar);
        }

        private void txtDateTo_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;
            e.SuppressKeyPress = dateTimeTextKeyDownBoxChecker(txtDateTo, keyChar);
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
    }
}
