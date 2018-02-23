using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResourceManagement
{
    public partial class UserControlServiceRecord : UserControl
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        //excel
  
        Workbook workbook = null;
        Worksheet worksheet = null;


        private static UserControlServiceRecord _instance;
        public static UserControlServiceRecord Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserControlServiceRecord();
                }
                if (_instance.IsDisposed)
                {
                    _instance = new UserControlServiceRecord();
                }

                return _instance;
            }
        }
        public UserControlServiceRecord()
        {
            InitializeComponent();
        }

        //*******************APP CONFIG MANAGER*********************************
        private string getStringValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

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

        //****************************EVENTS**********************************
        private void UserControlServiceRecord_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                conn = new SqlConnection(getStringValue("sqlconstring"));
            }
  
        }

        //***********LOADING DATA*****************************
        public void loadRecords(String employee_id)
        {
            try
            {
               Console.WriteLine("Loading Service Records of employee :" + employee_id);
                TempHolder.searchedEmpID = employee_id;
                clearDisplay();

                openSQLConnection();
                string qry = "SELECT * FROM " + SQLbank.TBL_SERVICE_RECORDS + " WHERE " + SQLbank.EMP_ID + " = " + employee_id + "  ORDER BY " + SQLbank.FROM_DATE + ";";
                cmd = new SqlCommand(qry, conn);
                reader = cmd.ExecuteReader();
                int counter = 0;
                while (reader.Read())
                {
                    counter++;
                    string id = reader[SQLbank.ID].ToString().Trim();

                    string school = reader[SQLbank.SCHOOL_NAME].ToString().Trim();

                    string start = reader[SQLbank.FROM_DATE].ToString().Trim();
                    string end = reader[SQLbank.TO_DATE].ToString().Trim();
                    string designation = reader[SQLbank.DESIGNATION].ToString().Trim();
                    string status = reader[SQLbank.STATUS].ToString().Trim();
                    string salary = reader[SQLbank.SALARY].ToString().Trim();

                    decimal dc;
                    if (Decimal.TryParse(salary, out dc)) salary = dc.ToString("N");

                    string station = reader[SQLbank.STATION].ToString().Trim();
                    string branch = reader[SQLbank.BRANCH].ToString().Trim();
                    string cause = reader[SQLbank.CAUSE].ToString().Trim();

                    DateTime st, ed;
                    if (DateTime.TryParse(start, out st)) start = st.ToString("MM/dd/yyyy");
                    if (DateTime.TryParse(end, out ed)) end = ed.ToString("MM/dd/yyyy");
                    else
                    {
                        //not in correct format, or empty
                        if (end.Trim().Length == 0) end = "PRESENT";
                    }


                    if (cause.ToLower().Contains("original")) TempHolder.searchedOriginalAppointment = start;

                    string lawop = reader[SQLbank.LAWOP].ToString();

                    dgvServiceRecord.Rows.Add(id, school, start, end, designation, status, salary, station, branch, cause, lawop);
                }

                if (counter == 0)
                {
                    lblNoServiceRecord.Visible = true;
                    btnImport.Visible = true;
                    TempHolder.lastSalary = "0.00";
                }
              
                Console.WriteLine("Records found: " + counter);

                btnAddRecord.Enabled = true;
                closeSQLConnection();
            }
            catch (Exception ee)
            {
                Console.WriteLine("Exception Encountered while displaying Service Records: " + ee.Message);
                MessageBox.Show("An error occured while displaying info", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void clearDisplay()
        {
            TempHolder.clearLastRecord();

            if (dgvServiceRecord.Rows.Count > 0) dgvServiceRecord.Rows.Clear();

            btnDelete.Enabled = false;
            lblNoServiceRecord.Visible = false;
            txtSchoolName.ResetText();
            txtStation.ResetText();
            txtBranch.ResetText();
            txtDesignation.ResetText();
            txtStatus.ResetText();
            txtFrom.ResetText();
            txtTo.ResetText();
            txtSalary.ResetText();
            txtCause.ResetText();
            txtLAWOP.ResetText();
            btnImport.Visible = false;
            btnEdit.Enabled = false;
            btnExport.Enabled = false;
            btnAddRecord.Enabled = false;
        }

        private void datagridServiceRecords_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvServiceRecord.Rows.Count > 0)
            {
                int selectedIndex = dgvServiceRecord.CurrentCell.RowIndex;

                lblSelectedRowID.Text = dgvServiceRecord.Rows[selectedIndex].Cells[0].Value.ToString();
                txtSchoolName.Text = dgvServiceRecord.Rows[selectedIndex].Cells[1].Value.ToString();
                txtFrom.Text = dgvServiceRecord.Rows[selectedIndex].Cells[2].Value.ToString();
                txtTo.Text = dgvServiceRecord.Rows[selectedIndex].Cells[3].Value.ToString().Trim();
                txtDesignation.Text = dgvServiceRecord.Rows[selectedIndex].Cells[4].Value.ToString();
                txtStatus.Text = dgvServiceRecord.Rows[selectedIndex].Cells[5].Value.ToString();
                txtSalary.Text = dgvServiceRecord.Rows[selectedIndex].Cells[6].Value.ToString();
                txtStation.Text = dgvServiceRecord.Rows[selectedIndex].Cells[7].Value.ToString();
                txtBranch.Text = dgvServiceRecord.Rows[selectedIndex].Cells[8].Value.ToString();
                txtCause.Text = dgvServiceRecord.Rows[selectedIndex].Cells[9].Value.ToString();
                txtLAWOP.Text = dgvServiceRecord.Rows[selectedIndex].Cells[10].Value.ToString();
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
          

        }

        private void datagridServiceRecords_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblNoServiceRecord.Visible = false;
            lblRowsCount.Text = dgvServiceRecord.Rows.Count.ToString();
            detectLastRow();
        }

        private void datagridServiceRecords_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblRowsCount.Text = dgvServiceRecord.Rows.Count.ToString();

            if(dgvServiceRecord.Rows.Count == 0 && (TempHolder.searchedEmpID != null))
            {
                btnImport.Visible = true;
            }

            if (dgvServiceRecord.Rows.Count == 0)
            {
                lblNoServiceRecord.Visible = true;
                txtFrom.ResetText();
                txtTo.ResetText();
                txtSchoolName.ResetText();
                txtStation.ResetText();
                txtBranch.ResetText();
                txtDesignation.ResetText();
                txtStatus.ResetText();
                txtSalary.ResetText();
                txtCause.ResetText();
                txtLAWOP.ResetText();
                TempHolder.clearLastRecord();
            }
            detectLastRow();
        }

        public void detectLastRow()
        {
            int lastIndex = dgvServiceRecord.Rows.Count - 1;
            if (lastIndex != -1)
            {
                TempHolder.lastServiceRecordId = dgvServiceRecord.Rows[lastIndex].Cells[0].Value.ToString();
                string school = dgvServiceRecord.Rows[lastIndex].Cells[1].Value.ToString();
                string from = dgvServiceRecord.Rows[lastIndex].Cells[2].Value.ToString();
                string to = dgvServiceRecord.Rows[lastIndex].Cells[3].Value.ToString();
                string designation = dgvServiceRecord.Rows[lastIndex].Cells[4].Value.ToString();
                string status = dgvServiceRecord.Rows[lastIndex].Cells[5].Value.ToString();
                string salary = dgvServiceRecord.Rows[lastIndex].Cells[6].Value.ToString();
                string station = dgvServiceRecord.Rows[lastIndex].Cells[7].Value.ToString();
                string branch = dgvServiceRecord.Rows[lastIndex].Cells[8].Value.ToString();
                string cause = dgvServiceRecord.Rows[lastIndex].Cells[9].Value.ToString();
                string lawop = dgvServiceRecord.Rows[lastIndex].Cells[10].Value.ToString();

                DateTime dt;
                if (DateTime.TryParse(from, out dt)) TempHolder.lastFrom = dt.ToString("MM/dd/yyyy");
                if (DateTime.TryParse(to, out dt))
                {
                    TempHolder.lastTo = dt.ToString("MM/dd/yyyy");
                    TempHolder.lastIsPresent = false;
                }
                else if (to.ToLower().Equals("present")) TempHolder.lastIsPresent = true;

                TempHolder.searchedLastSchool = school;
                TempHolder.lastDesignation = designation;
                TempHolder.lastStatus = status;
                if (salary.Length == 0) TempHolder.lastSalary = "0.00";
                TempHolder.lastSalary = salary;
                TempHolder.lastStation = station;
                TempHolder.lastBranch = branch;
                TempHolder.lastCause = cause;
                TempHolder.lastLAWOP = lawop;

                String searchValue = "original";
                int rowIndex = -1;
                foreach (DataGridViewRow row in dgvServiceRecord.Rows)
                {
                    if (row.Cells[9].Value.ToString().ToLower().Contains(searchValue))
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }

                if (rowIndex != -1)
                {
                    TempHolder.searchedOriginalAppointment = dgvServiceRecord.Rows[rowIndex].Cells[2].Value.ToString();
                }
                else
                {
                    TempHolder.searchedOriginalAppointment = null;
                }

            }
            else
            {
                TempHolder.clearLastRecord();
            }

            TempHolder.mainForm.showOtherEmpInfo();

        }

        private void lblRowsCount_TextChanged(object sender, EventArgs e)
        {
            bool dgvNotEmpty = !(lblRowsCount.Text.Trim().Length == 0 || lblRowsCount.Text.Trim().Equals("0"));
            btnExport.Enabled = dgvNotEmpty;
            btnDelete.Enabled = dgvNotEmpty;

        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.ADD_SERVICE_RECORD))
            {
                AddServiceRecordDialog ad = new AddServiceRecordDialog();
                ad.ShowDialog();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string templatePath = System.Windows.Forms.Application.StartupPath + "/template.xlsx";

                if (TempHolder.excelApp.Visible)
                {
                    Console.WriteLine("Excel is visible");
                    TempHolder.excelApp = new Microsoft.Office.Interop.Excel.Application();
                }
                Workbooks workbooks = TempHolder.excelApp.Workbooks;
                workbook = workbooks.Open(@templatePath);
                worksheet = workbook.Sheets[1];

                //copying existing template
                worksheet.Copy(Missing.Value, Missing.Value);   //creates a duplicate of the template but not yet saving it to the storage
                worksheet = TempHolder.excelApp.Workbooks[2].Sheets[1]; //replacing the value of the variable from original template to duplicate
                workbook.Close(); // closes the original template

                worksheet.Name = TempHolder.searchedSheetName + "_ServiceRecord";
                
                //do population of cells here
                worksheet.Cells[11, 2] = TempHolder.searchedLastSchool;
                worksheet.Cells[12, 2] = TempHolder.searchedName;
                worksheet.Cells[14, 2] = TempHolder.searchedBirthday+"     "+TempHolder.searchedBirthPlace;

                TempHolder.excelApp.Visible = true; //makes the duplicate visible


                //transferring of data grid to excel sheets
                int wsRow = 23, wsCol = 1;
                int wsFirstRow = wsRow, wsFirstCol = wsCol;
                int wsCurrentRow = 1, wsLastCol = 1;    

                for (int row = 0; row < dgvServiceRecord.Rows.Count; row++)
                {
                    for (int col = 2; col < dgvServiceRecord.Columns.Count; col++)
                    {
                        worksheet.Cells[wsRow, wsCol] = dgvServiceRecord.Rows[row].Cells[col].Value.ToString();
                        //worksheet.Cells[wsRow,wsCol].Borders.LineStyle = XlLineStyle.xlContinuous;
                        wsCol++;
                    }
                    wsLastCol = wsCol;
                    wsCurrentRow = wsRow;
                    wsRow++;
                    wsCol = 1;
                }

                //to add borders
                worksheet.Range[worksheet.Cells[wsFirstRow, wsFirstCol], worksheet.Cells[wsCurrentRow, --wsLastCol]].Borders.LineStyle = XlLineStyle.xlContinuous;



                //add reference
                int startOfBottomInfo = 0;
                string bottomMsg = "    Issued in compliance with Executive Order No. 54 dated August 10, 1954 and in accordance with circular number 58 dated August 1954 of the System.";
                worksheet.Cells[++wsCurrentRow, wsFirstCol] = bottomMsg;
                startOfBottomInfo = wsCurrentRow;

                worksheet.Range[worksheet.Cells[(wsCurrentRow), wsFirstCol], worksheet.Cells[(++wsCurrentRow), (wsLastCol)]].Merge();
                worksheet.Range[worksheet.Cells[(wsCurrentRow), wsFirstCol], worksheet.Cells[(wsCurrentRow), (wsLastCol)]].Rows.WrapText = true;
               

                worksheet.Cells[++wsCurrentRow, wsFirstCol] = "CERTIFIED CORRECT:"; worksheet.Cells[wsCurrentRow,wsFirstCol].Font.Bold = true;
                worksheet.Cells[++wsCurrentRow, wsFirstCol] = "For the Schools Division Superintendent";

                //to align to left
                worksheet.Range[worksheet.Cells[startOfBottomInfo, wsFirstCol], worksheet.Cells[(wsCurrentRow), (wsLastCol)]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;


                worksheet.Cells[wsCurrentRow += 2, wsFirstCol] = TempHolder.officerName; worksheet.Cells[wsCurrentRow, wsFirstCol].Font.Bold = true;
                worksheet.Range[worksheet.Cells[wsCurrentRow,wsFirstCol], worksheet.Cells[wsCurrentRow,(wsFirstCol+2)]].Merge(); 

                worksheet.Cells[++wsCurrentRow, wsFirstCol] = TempHolder.officerPosition;
                worksheet.Range[worksheet.Cells[wsCurrentRow, wsFirstCol], worksheet.Cells[wsCurrentRow, (wsFirstCol + 2)]].Merge();

            }
            catch (Exception ee)
            {
                Console.WriteLine("\n Error Occured: " + ee.Message);
                MessageBox.Show("An error occured while exporting to excel", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteRow(true);
        }

        private void datagridServiceRecords_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = !deleteRow(false);
        }

        private bool deleteRow(bool fromButtonEvent)
        {
            if (Permissions.authorizedToUseFunction(Permissions.DELETE_SERVICE_RECORD))
            {
                if (DialogResult.OK == MessageBox.Show("Delete selected Service Record?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    //delete record
                    try
                    {
                        openSQLConnection();
                        string delSQL = "DELETE FROM " + SQLbank.TBL_SERVICE_RECORDS + " WHERE  " + SQLbank.ID + " = " + lblSelectedRowID.Text;
                        Console.WriteLine("Delete Query: " + delSQL);
                        cmd = new SqlCommand(delSQL, conn);
                        cmd.ExecuteNonQuery();

                        if (fromButtonEvent) dgvServiceRecord.Rows.RemoveAt(dgvServiceRecord.CurrentCell.RowIndex); //if false, user pressed from keyboard's delete button, no need to call this line, event will be doubled and may throw index outbound exception

                        MessageBox.Show("Service Record Deleted", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine("Deleting Error: " + ee.Message);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else return false;

        }

        private void datagridServiceRecords_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editRow(e.RowIndex);
        }

        private void editRow(int selectedIndex)
        {
            if (Permissions.authorizedToUseFunction(Permissions.MODIFY_SERVICE_RECORD))
            {

                TempHolder.selectedIndex = dgvServiceRecord.CurrentCell.RowIndex;
                TempHolder.selectedId = dgvServiceRecord.Rows[selectedIndex].Cells[0].Value.ToString();
                TempHolder.selectedSchool = dgvServiceRecord.Rows[selectedIndex].Cells[1].Value.ToString();
                TempHolder.selectedFrom = dgvServiceRecord.Rows[selectedIndex].Cells[2].Value.ToString();
                TempHolder.selectedTo = dgvServiceRecord.Rows[selectedIndex].Cells[3].Value.ToString();
                TempHolder.selectedDesignation = dgvServiceRecord.Rows[selectedIndex].Cells[4].Value.ToString();
                TempHolder.selectedStatus = dgvServiceRecord.Rows[selectedIndex].Cells[5].Value.ToString();
                TempHolder.selectedSalary = dgvServiceRecord.Rows[selectedIndex].Cells[6].Value.ToString();
                TempHolder.selectedStation = dgvServiceRecord.Rows[selectedIndex].Cells[7].Value.ToString();
                TempHolder.selectedBranch = dgvServiceRecord.Rows[selectedIndex].Cells[8].Value.ToString();
                TempHolder.selectedCause = dgvServiceRecord.Rows[selectedIndex].Cells[9].Value.ToString();
                TempHolder.selectedLawop = dgvServiceRecord.Rows[selectedIndex].Cells[10].Value.ToString();

                TempHolder.editMode = true;
                AddServiceRecordDialog asr = new AddServiceRecordDialog();
                asr.ShowDialog();
            }
        }

        private void txtFrom_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editRow(dgvServiceRecord.CurrentCell.RowIndex);
        }

        public bool getPreviousRowInfo(int selectedindex)
        {
            //-1 means adding new

            int prevRowIndex;
            if (dgvServiceRecord.RowCount > 0)
            {
                if (selectedindex > 0) prevRowIndex = selectedindex - 1;
                else if (selectedindex == -1) prevRowIndex = dgvServiceRecord.RowCount - 1;
                else { 
                    MessageBox.Show("First row is your currently selected row there is no previous service record","Oops",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return false;
                }

                TempHolder.setPrevRowVar(getDGVString(prevRowIndex, 0),
                    getDGVString(prevRowIndex, 1),
                    getDGVString(prevRowIndex, 2),
                    getDGVString(prevRowIndex, 3),
                    getDGVString(prevRowIndex, 4),
                    getDGVString(prevRowIndex, 5),
                    getDGVString(prevRowIndex, 6),
                    getDGVString(prevRowIndex, 7),
                    getDGVString(prevRowIndex, 8),
                    getDGVString(prevRowIndex, 9),
                    getDGVString(prevRowIndex, 10));
                return true;
            }
            else
            {
                MessageBox.Show("There is no previous service record", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return false;
        }


        private string getDGVString(int rowindex, int cellindex)
        {
            return dgvServiceRecord.Rows[rowindex].Cells[cellindex].Value.ToString();
        }

        //*************IMPORTING******************************************************
        private void btnImport_Click(object sender, EventArgs e)
        {
            //check authority
            if (Permissions.authorizedToUseFunction(Permissions.MODIFY_SERVICE_RECORD))
            {
                try
                {
                    OpenFileDialog op = new OpenFileDialog();
                    op.Filter = "Excel File |*.xls;*.xlsx;*.xlsm";
                    op.Multiselect = false;
                    if(op.ShowDialog() == DialogResult.OK)
                    {
                        TempHolder.importExcelDialog = new ImportExcelDialog(op.FileName,op.SafeFileName);   
                        TempHolder.importExcelDialog.ShowDialog();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("An error occured while importing employee's service record \n\n" + ee.Message,"Oops",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Console.WriteLine("Import Error: " + ee.Message);
                }
            }
        }

        /*
        private void readExcelSheetNames()
        {
            try
            {
                openExcelConnection();

                DataTable dtExcelSchema = new DataTable();

                dtExcelSchema = excelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //to read all excel sheet name
                cmbSheetNames.Items.Clear();
                for (int i = 0; i < dtExcelSchema.Rows.Count; i++)
                {
                    string sheet = dtExcelSchema.Rows[i]["TABLE_NAME"].ToString();
                    sheet = sheet.TrimEnd("$".ToCharArray());
                    cmbSheetNames.Items.Add(sheet);
                }

                //to set default selected sheet
                if (cmbSheetNames.Items.Count > 0)
                {
                    cmbSheetNames.SelectedIndex = 0;
                    lblSelectedFields.Enabled = true;
                }

                closeExcelConnection();

            }
            catch (Exception ee)
            {
                MessageBox.Show("Problem Occured while reading sheets\n" + ee.Message, "Failed to read ExcelSheets", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
    }
}
