using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResourceManagement
{
    public partial class ReportForm : Form
    {
        //excel
        Workbook workbook = null;
        Worksheet worksheet = null;

        string searchQry;
        SqlConnection conn;
        SqlCommand cmd;
     
        SqlDataReader reader;

        string errorMsg = null;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

            if (cmbWhat.Items.Count > 0) cmbWhat.SelectedIndex = 0;

            conn = new SqlConnection(getStringValue("sqlconstring"));

        }

        //************************SERVER CONNECTION************************************
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

        //*******************APP CONFIG MANAGER*****************************************
        private string getStringValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

        }


        private void cmbWhat_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                0-School
                1-Employee
                2-System User
            */
            cmbfilterby.Items.Clear();

            switch (cmbWhat.SelectedIndex)
            {
                case 0:
                    cmbfilterby.Items.Add("School ID");
                    cmbfilterby.Items.Add("School Name");
                    cmbfilterby.Items.Add("District");
                    break;
                case 1:
                    cmbfilterby.Items.Add("Employee No.");
                    cmbfilterby.Items.Add("First Name");
                    cmbfilterby.Items.Add("Middle Name");
                    cmbfilterby.Items.Add("Last Name");
                    cmbfilterby.Items.Add("Account No.");
                    cmbfilterby.Items.Add("TIN No.");
                    cmbfilterby.Items.Add("HDMF No.");
                    cmbfilterby.Items.Add("PHIC No.");
                    cmbfilterby.Items.Add("BP No.");
                    break;
                case 2:
                    cmbfilterby.Items.Add("Username");
                    cmbfilterby.Items.Add("First Name");
                    cmbfilterby.Items.Add("Middle Name");
                    cmbfilterby.Items.Add("Last Name");
                    break;
            }

            if (cmbfilterby.Items.Count > 0) cmbfilterby.SelectedIndex = 0;
        }

        //*********************SEARCHING************************************************
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("Search Clicked");
                if (!backgroundWorker1.IsBusy)
                {
                    Console.WriteLine("Background worker not busy");
                    errorMsg = null;
                    lblBottomMessage.Text = "Loading...";
                    searchQry = generateQuery();
                    Console.WriteLine("Search Query: " + searchQry);
                    if (searchQry != null)
                    {
                        backgroundWorker1.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show("Failed to search", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Cannot Search\n" + ee.Message,"Oops",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                openSQLConnection();
                cmd = new SqlCommand(searchQry,conn);
                cmd.Parameters.AddWithValue("@KEYWORD", txtSearchKey.Text.ToString()+"%");
                reader = cmd.ExecuteReader();
                   
            }
            catch (Exception)
            {
                errorMsg = "An Error occured while loading";
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Worker Complete");

            if (errorMsg == null)
            {
             //   dgv.Columns.Clear();
             //   dgv.Rows.Clear();

                try
                {
                    int resultColCount = reader.FieldCount;
                    Console.WriteLine("Result Column Count: " + resultColCount);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    dataTable.Load(reader);

                    dgv.DataSource = dataTable;
                    MessageBox.Show("Loaded!");
                    lblBottomMessage.Text = "Ready";
                }
                catch (Exception ee)
                {
                    lblBottomMessage.Text = "Failed to display results";
                    MessageBox.Show("Failed to display results\n"+ee.Message,"Oops",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                lblBottomMessage.Text = errorMsg;
            }
        }

        private string generateQuery()
        {
            /*
                0 - School
                1 - Employee
                2 - System User
            */
            switch (cmbWhat.SelectedIndex)
            {
                case 0:
                    return generateSchoolQuery();
                    break;
                case 1:
                    return generateEmployeeQuery();
                    break;
                case 2:
                    return generateSystemUserQuery();
                    break;
            }
            return null;
        }

        private string generateSchoolQuery()
        {
            /*
            School ID
            School Name
            District
            */
            string qry = null;
            string selectedField = SQLbank.SCHOOL_ID + " as 'School ID'," + SQLbank.SCHOOL_NAME + " as 'School Name'," + SQLbank.DISTRICT+" as 'District' ";

            switch (cmbfilterby.SelectedIndex)
            {
                case 0:
                    return qry = "SELECT " +selectedField+  " FROM " + SQLbank.TBL_SCHOOLS + " WHERE "+ SQLbank.SCHOOL_ID + " LIKE @KEYWORD";
                    break;
                case 1:
                    return qry = "SELECT " + selectedField + " FROM " + SQLbank.TBL_SCHOOLS + " WHERE " + SQLbank.SCHOOL_NAME + " LIKE @KEYWORD";
                    break;
                case 2:
                    return qry= "SELECT " +selectedField+ " FROM " + SQLbank.TBL_SCHOOLS + " WHERE " + SQLbank.DISTRICT + " LIKE @KEYWORD";
                    break;
                default:
                    return null;
                    break;
            }
            return null;
        }

        private string generateEmployeeQuery()
        {
            /*
            0 - "Employee No."
            1 - First Name
            2 - Middle Name
            3 - Last Name
            4 - Account No.
            5 - TIN No.
            6 - HDMF No.
            7 - PHIC No.
            8 - BP No.*/
            string qry = null;
            string selectedFields = SQLbank.EMPLOYEE_NO + " as 'Employee No.'," +
                SQLbank.EMP_FIRST_NAME + " as 'First Name' ," +
                SQLbank.EMP_MIDDLE_NAME + " as 'Middle Name' ," +
                SQLbank.EMP_LAST_NAME + " as 'Last Name' ," +
                SQLbank.SEX + " as 'Gender' ," +
                SQLbank.DATE_OF_BIRTH + " as 'Date of Birth' ," +
                SQLbank.BIRTH_PLACE + " as 'Birth Place' ," +
                SQLbank.PLANTILLA_NO + " as 'Plantilla No.' ," +
                SQLbank.HDMF_NO + " as 'HDMF No.'," +
                SQLbank.PHIC_NO + " as 'PHIC No.' ," +
                SQLbank.BP_NO + " as 'BP No.' ," +
                SQLbank.ACCOUNT_NO + " as 'Account No.' ," + SQLbank.TIN_NO;


            switch (cmbfilterby.SelectedIndex)
            {
                case 0:
                    return qry = "SELECT " +selectedFields+" FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.EMPLOYEE_NO + " LIKE @KEYWORD";
                    break;

                case 1:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.EMP_FIRST_NAME + " LIKE @KEYWORD";
                    break;

                case 2:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.EMP_MIDDLE_NAME + " LIKE @KEYWORD";
                    break;
           
                case 3:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.EMP_LAST_NAME + " LIKE @KEYWORD";
                    break;
               
                case 4:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.ACCOUNT_NO + " LIKE @KEYWORD";
                    break;
              
                case 5:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.TIN_NO + " LIKE @KEYWORD";
                    break;
                case 6:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.HDMF_NO + " LIKE @KEYWORD";
                    break;
             
                case 7:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.PHIC_NO + " LIKE @KEYWORD";
                    break;
           
                case 8:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.BP_NO + " LIKE @KEYWORD";
                    break;
               
                default:
                    return null;
                    break;
            }
            return null;
        }

        private string generateSystemUserQuery()
        {
            string qry = null;
            string selectedFields = SQLbank.USERNAME + "," + SQLbank.FNAME + "," + SQLbank.MNAME + "," + SQLbank.LNAME;


            switch (cmbfilterby.SelectedIndex)
            {
                case 0:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_USERS + " WHERE " + SQLbank.USERNAME + " LIKE @KEYWORD";
                    break;

                case 1:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_USERS + " WHERE " + SQLbank.FNAME + " LIKE @KEYWORD";
                    break;
                case 2:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_USERS + " WHERE " + SQLbank.MNAME + " LIKE @KEYWORD";
                    break;

                case 3:
                    return qry = "SELECT " + selectedFields + " FROM " + SQLbank.TBL_USERS + " WHERE " + SQLbank.LNAME + " LIKE @KEYWORD";
                    break;

                default:
                    return null;
                    break;
            }
            return null;
        }

        //*******************************************************************************

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

        private void FormPanel_DoubleClick(object sender, EventArgs e)
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

        private void lblBottomMessage_Click(object sender, EventArgs e)
        {

        }

        private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            btnPrint.Enabled = dgv.RowCount > 0;
        }

        private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            btnPrint.Enabled = dgv.RowCount <= 0;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                try
                {
                    if (TempHolder.excelApp.Visible)
                    {
                        Console.WriteLine("Excel is visible");
                        TempHolder.excelApp = new Microsoft.Office.Interop.Excel.Application();
                    }
                    
                    // creating new WorkBook within Excel application  
                    _Workbook workbook = TempHolder.excelApp.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    _Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    TempHolder.excelApp.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Sheet1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    worksheet.Name = "Exported from gridview";
                    // storing header part in Excel  

                    int dgvColumnCount = dgv.ColumnCount;
                    int dgvRowCount = dgv.RowCount;

                    for (int i = 1; i < dgvColumnCount + 1; i++)
                    {
                        worksheet.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
                    }
                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dgv.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvColumnCount; j++)
                        {
                            worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                        }
                    }

                    //to add borders
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[dgvRowCount, dgvColumnCount]].Borders.LineStyle = XlLineStyle.xlContinuous;


                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to export to Excel", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
       
        }

        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }
    }
}
