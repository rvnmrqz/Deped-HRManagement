using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace HumanResourceManagement
{
    public partial class UserControlServiceRecord : UserControl
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        //excel
        static Microsoft.Office.Interop.Excel.Application excelApp = null;
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
            conn = new SqlConnection(getStringValue("sqlconstring"));
        }

        //***********LOADING DATA*****************************
        public void loadRecords(String employee_id)
        {
            try
            {
                //    Console.WriteLine("Loading Service Records of employee :" + employee_id);
                TempHolder.searchedEmpID = employee_id;
                clearDisplay();
                
                openSQLConnection();
                string qry = "SELECT * FROM " + SQLbank.TBL_SERVICE_RECORDS + " WHERE " + SQLbank.EMP_ID + " = " + employee_id+"  ORDER BY "+SQLbank.FROM_DATE+";";
                cmd = new SqlCommand(qry,conn);
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

                    string station = reader[SQLbank.STATION].ToString().Trim();
                    string branch = reader[SQLbank.BRANCH].ToString().Trim() ;
                    string cause = reader[SQLbank.CAUSE].ToString().Trim();
               
                    DateTime st, ed;
                    if (DateTime.TryParse(start, out st)) start = st.ToString("MM/dd/yyyy");
                    if (DateTime.TryParse(end, out ed)) end =ed.ToString("MM/dd/yyyy");
                 
                    string lawop = reader[SQLbank.LAWOP].ToString();
                  
                    datagridServiceRecords.Rows.Add(id, school, start, end, designation, status, salary, station,branch,cause, lawop);
                }

                if (counter == 0)
                {
                    TempHolder.searchedLastSchool = "NONE";
                    TempHolder.searchedLastDesignation = "NONE";
                    TempHolder.searchedLastStatus = "NONE";
                    TempHolder.searchedLastSalary = "0";
                    TempHolder.searchedLastStation = "NONE";
                    TempHolder.searchedLastBranch = "NONE";
                    TempHolder.searchedLastCause = "NONE";
                    TempHolder.searchedLastLawop = "NONE";
                }
                Console.WriteLine("Records found: " + counter);

                btnAddRecord.Enabled = true;
                btnExport.Enabled = true;

                closeSQLConnection();
            }catch(Exception ee)
            {
                Console.WriteLine("Exception Encountered while displaying Service Records: " + ee.Message);
                MessageBox.Show("An error occured while displaying info", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void clearDisplay()
        {
            Console.WriteLine("ClearDisplay - tab2");
 
            datagridServiceRecords.Rows.Clear();

            btnDelete.Enabled = false;
            txtSchoolName.ResetText();
            txtBranch.ResetText();
            txtPosition.ResetText();

            txtStatus.ResetText();
            txtSalary.ResetText();
            txtLAWOP.ResetText();
            btnAddRecord.Enabled = false;
            btnExport.Enabled = false;

        }

        private void datagridServiceRecords_SelectionChanged(object sender, EventArgs e)
        {
            
            int selectedIndex = datagridServiceRecords.CurrentCell.RowIndex;

            lblSelectedRowID.Text = datagridServiceRecords.Rows[selectedIndex].Cells[0].Value.ToString();

            txtSchoolName.Text = datagridServiceRecords.Rows[selectedIndex].Cells[1].Value.ToString();
            txtBranch.Text = datagridServiceRecords.Rows[selectedIndex].Cells[2].Value.ToString();
            txtPosition.Text = datagridServiceRecords.Rows[selectedIndex].Cells[3].Value.ToString();
            txtFrom.Text = datagridServiceRecords.Rows[selectedIndex].Cells[4].Value.ToString();
            txtTo.Text = datagridServiceRecords.Rows[selectedIndex].Cells[5].Value.ToString();
            txtStatus.Text = datagridServiceRecords.Rows[selectedIndex].Cells[6].Value.ToString();
            txtRemarks.Text = datagridServiceRecords.Rows[selectedIndex].Cells[7].Value.ToString();
            txtSalary.Text = datagridServiceRecords.Rows[selectedIndex].Cells[8].Value.ToString();
            txtLAWOP.Text = datagridServiceRecords.Rows[selectedIndex].Cells[9].Value.ToString();

            btnDelete.Enabled = true;
            

        }

        private void datagridServiceRecords_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblRowsCount.Text = datagridServiceRecords.Rows.Count.ToString();
            detectLastRow();
        }

        private void datagridServiceRecords_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblRowsCount.Text = datagridServiceRecords.Rows.Count.ToString();
            detectLastRow();
        }


        public void detectLastRow()
        {

            int lastIndex = datagridServiceRecords.Rows.Count - 1;
            if (lastIndex != -1)
            {
                string school = datagridServiceRecords.Rows[lastIndex].Cells[1].Value.ToString();
                string from = datagridServiceRecords.Rows[lastIndex].Cells[2].Value.ToString();
                string to = datagridServiceRecords.Rows[lastIndex].Cells[3].Value.ToString();
                string designation = datagridServiceRecords.Rows[lastIndex].Cells[4].Value.ToString();
                string status = datagridServiceRecords.Rows[lastIndex].Cells[5].Value.ToString();
                string salary = datagridServiceRecords.Rows[lastIndex].Cells[6].Value.ToString();
                string station = datagridServiceRecords.Rows[lastIndex].Cells[7].Value.ToString();
                string branch = datagridServiceRecords.Rows[lastIndex].Cells[8].Value.ToString();
                string cause = datagridServiceRecords.Rows[lastIndex].Cells[9].Value.ToString();
                string lawop = datagridServiceRecords.Rows[lastIndex].Cells[10].Value.ToString();

                if (school.Length == 0) TempHolder.searchedLastSchool = "NONE";
                if (school.Length != 0 && !school.Equals("-do-")) TempHolder.searchedLastSchool = school;

                if (designation.Length == 0) TempHolder.searchedLastDesignation = "NONE";
                if (designation.Length != 0 && !designation.Equals("-do-")) TempHolder.searchedLastDesignation = designation;

                if (status.Length == 0) TempHolder.searchedLastStatus = "NONE";
                if (status.Length != 0 && !status.Equals("-do-")) TempHolder.searchedLastStatus = status;

                if (salary.Length == 0) TempHolder.searchedLastSalary = "0";
                if (salary.Length != 0 && !salary.Equals("-do-")) TempHolder.searchedLastSalary = salary;

                if (station.Length == 0) TempHolder.searchedLastStation = "NONE";
                if (station.Length != 0 && !station.Equals("-do-")) TempHolder.searchedLastStation = station;

                if (branch.Length == 0) TempHolder.searchedLastBranch = "NONE";
                if (branch.Length != 0 && !branch.Equals("-do-")) TempHolder.searchedLastBranch = branch;

                if (cause.Length == 0) TempHolder.searchedLastCause = "NONE";
                if (cause.Length != 0 && !cause.Equals("-do-")) TempHolder.searchedLastCause = cause;

                if (lawop.Length == 0) TempHolder.searchedLastLawop = "NONE";
                if (lawop.Length != 0 && !lawop.Equals("-do-")) TempHolder.searchedLastLawop = lawop;
            }
           
        }

   

        private void lblRowsCount_TextChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = lblRowsCount.Text.Length == 0 || lblRowsCount.Text.Trim().Equals("0");
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


                if (excelApp == null) excelApp = new Microsoft.Office.Interop.Excel.Application();

      

                workbook = excelApp.Workbooks.Open(templatePath);
                worksheet = workbook.Sheets[1];

                worksheet.Copy(Missing.Value, Missing.Value);   //creates a duplicate of the template but not yet saving it to the storage
                worksheet = excelApp.Workbooks[2].Sheets[1]; //replacing the value of the variable from original template to duplicate


                worksheet.Name = TempHolder.searchedSheetName+"_ServiceRecord";

                workbook.Close(); // closes the original template
                excelApp.Visible = true; //makes the duplicate visible


                //do population of cells here
                worksheet.Cells[11, 2] = TempHolder.searchedLastSchool;
                worksheet.Cells[12, 2] = TempHolder.searchedName;
                


            }
            catch (Exception ee)
            {
                Console.WriteLine("\n Error Occured: " + ee.Message);
                if (workbook != null) workbook.Close();
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

                        if(fromButtonEvent)datagridServiceRecords.Rows.RemoveAt(datagridServiceRecords.CurrentCell.RowIndex); //if false, user pressed from keyboard's delete button, no need to call this line, event will be doubled and may throw index outbound exception

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
    }
}
