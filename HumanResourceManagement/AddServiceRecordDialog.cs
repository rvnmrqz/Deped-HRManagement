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

        //************************INITIAL LOAD*******************************************

        private void AddServiceRecordDialog_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
            loadSystemValues();

            if (TempHolder.editMode)
            {
                //display double clicked row
                lblFormTitle.Text = "Edit Serivce Record";

                chkPresent.Checked = false;
                txtSchoolName.Text = TempHolder.selectedSchool;
                txtDateFrom.Text = TempHolder.selectedFrom;
                txtDateTo.Text = TempHolder.selectedTo;
                txtDesignation.Text = TempHolder.selectedDesignation;
                cmbStatus.Text = TempHolder.selectedStatus;
                txtSalary.Text = TempHolder.selectedSalary;
                txtStation.Text = TempHolder.selectedStation;
                txtBranch.Text = TempHolder.selectedBranch;
                cmbCause.Text = TempHolder.selectedCause;
                txtLAWOP.Text = TempHolder.selectedLawop;
            }else
            {
                //default
                lblFormTitle.Text = "Add Serivce Record";
                prepareDisplay();
            }
        }

        private void loadSystemValues()
        {
            //status
            for (int i = 0; i < TempHolder.status_list.Count; i++)
            {
                cmbStatus.Items.Add(TempHolder.status_list[i]);
            }

            //cause
            for (int i = 0; i < TempHolder.cause_list.Count; i++)
            {
                cmbCause.Items.Add(TempHolder.cause_list[i]);
            }
        }

        private void prepareDisplay()
        {
            txtDateFrom.Text = DateTime.Today.ToString("MM/dd/yyyy");

            if (TempHolder.lastFrom == null)
            {
                //no records yet, it is original
                cmbCause.Text = "Original";
            }
            else if (TempHolder.lastFrom != null && TempHolder.lastCause.ToLower().Contains("original"))
            {
                //there is already an entry and it is the original appointment
                cmbCause.SelectedIndex = -1;
            }
            else cmbCause.Text = TempHolder.lastCause;

            //prepare display
            if (TempHolder.lastFrom != null)
            {
                foreach (CheckBox chk in panelChkBox.Controls)
                {
                    chk.Checked = true;
                }
            }
        }

        //************************SERVER CONNECTION**************************************
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

        //*******************APP CONFIG MANAGER******************************************
        private string getStringValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

        }

        //************************SAVING*************************************************
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (TempHolder.editMode)
            {
                updateExistingEntry();
            }
            else
            {
                addNewRecord();
            }
        }

        private void addNewRecord()
        {
            if (isInputValid())
            {
                try
                {
                    openSQLConnection();

                    string sql = "INSERT INTO " + SQLbank.TBL_SERVICE_RECORDS + " ("
                        + SQLbank.EMP_ID
                        + "," + SQLbank.SCHOOL_NAME
                        + "," + SQLbank.FROM_DATE;

                    if (!chkPresent.Checked) sql += "," + SQLbank.TO_DATE;

                    sql += "," + SQLbank.DESIGNATION
                    + "," + SQLbank.STATUS
                    + "," + SQLbank.SALARY
                    + "," + SQLbank.STATION
                    + "," + SQLbank.BRANCH
                    + "," + SQLbank.CAUSE
                    + "," + SQLbank.LAWOP + ")"
                    + " OUTPUT INSERTED." + SQLbank.ID
                    + " VALUES(@EMPID,@SCHOOLNAME,@FROM";

                    if (!chkPresent.Checked) sql += ",@TO";

                    sql += ",@DESIGNATION,@STATUS,@SALARY,@STATION,@BRANCH,@CAUSE,@LAWOP)";

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@EMPID", TempHolder.searchedEmpID);
                    cmd.Parameters.AddWithValue("@SCHOOLNAME", txtSchoolName.Text);
                    cmd.Parameters.AddWithValue("@FROM", txtDateFrom.Text);

                    if (!chkPresent.Checked) cmd.Parameters.AddWithValue("@TO", txtDateTo.Text);

                    cmd.Parameters.AddWithValue("@DESIGNATION", txtDesignation.Text);
                    cmd.Parameters.AddWithValue("@STATUS", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@SALARY", txtSalary.Text);
                    cmd.Parameters.AddWithValue("@STATION", txtStation.Text);
                    cmd.Parameters.AddWithValue("@BRANCH", txtBranch.Text);
                    cmd.Parameters.AddWithValue("@CAUSE", cmbCause.Text);
                    cmd.Parameters.AddWithValue("@LAWOP", txtLAWOP.Text);

                    Console.WriteLine("Saving query: " + sql);

                    string lastinsertId = cmd.ExecuteScalar().ToString();

                    if (chkPresent.Checked && TempHolder.lastIsPresent)
                    {
                        try
                        {
                            //update last record
                            DateTime dtFrom = DateTime.Parse(txtDateFrom.Text);
                            DateTime dtOldTo = dtFrom.AddDays(-1);
                            updateLastRecordTo(dtOldTo.ToString("MM/dd/yyyy"));

                        }
                        catch(Exception ee)
                        {
                            Console.WriteLine("Generating TO datetime value Failed: "+ee.Message);
                        }
                     
                    }

                    //to reload and rearrange the list based on from date
                    TempHolder.uc_ServiceRecord.loadRecords(TempHolder.searchedEmpID);

                    prepareDisplay();

                    MessageBox.Show("New entry successfully added","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    txtDateFrom.Select();
                }
                catch (Exception ee)
                {
                    Console.WriteLine("\nAdding Service Record Exception: " + ee.Message);
                    MessageBox.Show("Adding Service Record Failed", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void updateExistingEntry()
        {
            try
            {
                openSQLConnection();

                string sql = "UPDATE " + SQLbank.TBL_SERVICE_RECORDS + " SET " + SQLbank.SCHOOL_NAME + "= @SCHOOL , " +
                    SQLbank.FROM_DATE + " = @FROM , " +
                    SQLbank.TO_DATE + " = @TO , " +
                    SQLbank.DESIGNATION + " = @DESIGNATION , " +
                    SQLbank.STATUS + " = @STATUS , " +
                    SQLbank.SALARY + " = @SALARY , " +
                    SQLbank.STATION + " = @STATION , " +
                    SQLbank.BRANCH + " = @BRANCH , " +
                    SQLbank.CAUSE + " = @CAUSE , " +
                    SQLbank.LAWOP + " = @LAWOP WHERE " + SQLbank.ID + " = " + TempHolder.selectedId;

                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SCHOOL", txtSchoolName.Text.Trim());
                cmd.Parameters.AddWithValue("@FROM", txtDateFrom.Text.Trim());
                cmd.Parameters.AddWithValue("@TO", txtDateTo.Text.Trim());
                cmd.Parameters.AddWithValue("@DESIGNATION", txtDesignation.Text.Trim());
                cmd.Parameters.AddWithValue("@STATUS", cmbStatus.Text.Trim());
                cmd.Parameters.AddWithValue("@SALARY", txtSalary.Text);
                cmd.Parameters.AddWithValue("@STATION", txtStation.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH", txtBranch.Text.Trim());
                cmd.Parameters.AddWithValue("@CAUSE", cmbCause.Text.Trim());
                cmd.Parameters.AddWithValue("@LAWOP", txtLAWOP.Text.Trim());

                Console.WriteLine(cmd.CommandText);

                cmd.ExecuteNonQuery();

                TempHolder.uc_ServiceRecord.loadRecords(TempHolder.searchedEmpID);

                MessageBox.Show("Update Successful", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show("Failed to updated entry\n"+ee.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateLastRecordTo(string toDate)
        {
            try
            {
                openSQLConnection();
                string updateSql = "UPDATE " + SQLbank.TBL_SERVICE_RECORDS + " SET " + SQLbank.TO_DATE + " = @DATE WHERE " + SQLbank.ID + " = " + TempHolder.lastServiceRecordId;
                cmd = new SqlCommand(updateSql, conn);
                cmd.Parameters.AddWithValue("@DATE",toDate);
                cmd.ExecuteNonQuery();
               // MessageBox.Show("Updated Last Record's To. ID: " + TempHolder.lastServiceRecordId);
            }
            catch (Exception ee)
            {
                Console.WriteLine("Failed to update Last Record's TO, Exception: " + ee.Message);
            }
        }
   
        private bool isInputValid()
        {
         
            DateTime dtStart = DateTime.Now, dtEnd = DateTime.Now;
         
            if (!DateTime.TryParse(txtDateFrom.Text, out dtStart))
            {
                showMessage("Invalid date for FROM");
                return false;
            }

          
            //if chkTo is checked, and the FROM date is behind the date of last record, return false
            if(chkPresent.Checked && TempHolder.lastFrom != null)
            {
                DateTime dtLastFrom;
                if(DateTime.TryParse(TempHolder.lastFrom,out dtLastFrom))
                {
                    double dif = (dtStart - dtLastFrom).TotalDays;
                    if (dif < 0)
                    {
                        showMessage("Cannot use Present as TO, FROM date is behind of last entry");
                        return false;
                    }else if (dif == 0)
                    {
                        showMessage("FROM's Date is the same as the last entry");
                        return false;
                    }
                }
                else
                {
                    //cannot check last FROM, invalid format
                    showMessage("Last entry's date from is in invalid format");
                    return false;
                }
            }
           
            if (!chkPresent.Checked && !DateTime.TryParse(txtDateTo.Text, out dtEnd))
            {
                showMessage("Invalid date for TO");
                return false;
            }


            double totalDaysBetween = (dtEnd - dtStart).TotalDays;
           
            if (!chkPresent.Checked &&  totalDaysBetween < 0)
            {
                //date TO value is less than FROM
                // eg. FROM = 08/27/2018 and TO = 08/26/2018
                //returns -1 for total days
                showMessage("Value of FROM and TO is in opposite order");
                return false;
            }
           
            if(totalDaysBetween == 0)
            {
                DialogResult dr = MessageBox.Show("FROM and TO is within the same day, Continue saving?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    //cancel saving
                    return false;
                }
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
            if (txtSalary.Text.Trim().Length == 0)
            {
                showMessage("Salary must not be empty");
                return false;
            }
            decimal dc;
            if (Decimal.TryParse(txtSalary.Text, out dc))
            {
                txtSalary.Text = dc.ToString("F");
            }
            else {
                showMessage("Invalid Salary");
                return false;
            }

            if (txtStation.Text.Trim().Length == 0)
            {
                showMessage("Station must not be empty");
                return false;
            }
            if (txtBranch.Text.Trim().Length == 0)
            {
                showMessage("Branch must not be empty");
                return false;
            }
          
            if (cmbCause.SelectedIndex == -1)
            {
                showMessage("Cause must not be empty");
                return false;
            }
            if (txtLAWOP.Text.Trim().Length == 0)
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

        //********************************************************************************

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
                txtDesignation.Text = TempHolder.lastDesignation;
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
                cmbStatus.Text = TempHolder.lastStatus;
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
                txtStation.Text = TempHolder.lastStation;
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
                txtBranch.Text = TempHolder.lastBranch;
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
                txtLAWOP.Text = TempHolder.lastLAWOP;
            }
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
                txtSalary.Text = TempHolder.lastSalary;
            }
        }

        private void chkPresent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPresent.Checked)
            {
                txtDateTo.Enabled = false;
                txtDateTo.Text = "PRESENT";
            }
            else
            {
                txtDateTo.ResetText();
                txtDateTo.Enabled = true;
                txtDateTo.Select();
            }
        }

        private void txtDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;
            e.SuppressKeyPress = dateTimeTextKeyDownBoxChecker(txtDateFrom, keyChar,e.KeyCode);
        }

        private void txtDateTo_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;
            e.SuppressKeyPress = dateTimeTextKeyDownBoxChecker(txtDateTo, keyChar, e.KeyCode);
        }

        private bool dateTimeTextKeyDownBoxChecker(MetroTextBox textbox, char keyChar, Keys keycode)
        {
            bool cancelEVent = true;

            if (Char.IsNumber(keyChar) || Char.IsControl(keyChar) || (keycode >= Keys.NumPad0 && keycode <= Keys.NumPad9))
            {
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

        private void txtSalary_Validated(object sender, EventArgs e)
        {

            decimal salary;
            if (Decimal.TryParse(txtSalary.Text, out salary))
            {
                txtSalary.Text = salary.ToString("N");

            }
        }

        private void AddServiceRecordDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            TempHolder.editMode = false;
        }
    }
}
