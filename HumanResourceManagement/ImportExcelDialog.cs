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
using System.Data.OleDb;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Configuration;

namespace HumanResourceManagement
{
    public partial class ImportExcelDialog : Form
    {

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        decimal sStart = 1, sEnd = 1;

        Workbook workbook;

        string excelPath, excelName, excelConString;
        string qry,selectedSheetName,transfermsg;


        List<string> sheetNamesList = new List<string>();
        List<string> columnList = new List<string>();

        OleDbConnection excelCon;
        SqlConnection sqlcon;


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImportExcelDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            TempHolder.importExcelDialog = null;
            //   TempHolder.quitExcel();
        }


        private void ImportExcelDialog_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
            lblExcelName.Text = excelName;
            bgLoader.RunWorkerAsync();
        }


        //************************EXCEL CONNECTIONS**********************************************
        private void openExcelConnection()
        {
            if (excelCon != null)
            {
                if (excelCon.State == ConnectionState.Open)
                {
                    excelCon.Close();
                    excelCon.Open();
                }
                else
                {
                    excelCon.Open();
                }
            }
        }

        private void closeExcelConnection()
        {
            if (excelCon != null)
            {
                if (excelCon.State == ConnectionState.Open)
                {
                    excelCon.Close();
                }
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
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

        }


        //****************************************************************************
        public ImportExcelDialog(string excelpath, string excelName)
        {
            Console.WriteLine("Constructor");
            this.excelPath = excelpath;
            this.excelName = excelName;

            InitializeComponent();
        }


        private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            readExcel(); //identifies if it is xls or xlsx, reads sheet names
        }

        private void readExcel()
        {

            try
            {
                Console.WriteLine("Reading Excel: " + excelPath);

                if (excelPath.Contains("xlsx"))
                {
                    Console.WriteLine("It is XLSX");
                    excelConString = @"provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + "; Extended Properties = 'Excel 12.0'";
                }
                else
                {
                    Console.WriteLine("It is NOT XLSX");
                    excelConString = @"provider=Microsoft.JET.OLEDB.4.0;Data Source=" + excelPath + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                excelCon = new OleDbConnection(excelConString);

                readExcelSheetNames();

            }
            catch (Exception ee)
            {
                Console.WriteLine("Problem occured while reading sheets: " + ee.Message);
            }
        }

        private void readExcelSheetNames()
        {
            try
            {
             
                openExcelConnection();

                System.Data.DataTable dtExcelSchema = new System.Data.DataTable();

                dtExcelSchema = excelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //to read all excel sheet name

                for (int i = 0; i < dtExcelSchema.Rows.Count; i++)
                {
                    string sheet = dtExcelSchema.Rows[i]["TABLE_NAME"].ToString();
                    sheet = sheet.TrimEnd("$".ToCharArray());
                    sheetNamesList.Add(sheet);
                }
                closeExcelConnection();

                dtExcelSchema = null;

            }
            catch (Exception ee)
            {

                Console.WriteLine("Problem occured while reading sheets: " + ee.Message);
                // MessageBox.Show("Problem Occured while reading sheets\n" + ee.Message, "Failed to read ExcelSheets", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bgLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Background worker completed");

            //populate combobox sheets
            populateComboBoxSheets();
        }


        private void populateComboBoxSheets()
        {
            for (int i = 0; i < sheetNamesList.Count; i++)
            {
                cmbSheets.Items.Add(sheetNamesList[i]);
                Console.WriteLine(sheetNamesList[i] + " added to combobox");
            }
            if (cmbSheets.Items.Count > 0) cmbSheets.SelectedIndex = 0;
            else if(cmbSheets.Items.Count == 0 )
            {
                MessageBox.Show("Excel's sheet cannot be read, please make sure the file is not corrupted or protected.\nNote: Copying data to new excel file may solve this problem","Oops",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void startRow_ValueChanged(object sender, EventArgs e)
        {
            sStart = startRow.Value;
        }

        private void endRow_ValueChanged(object sender, EventArgs e)
        {
            sEnd = endRow.Value;
        }

        //****************************TRANSFERRING**********************************************
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isRowsValid())
            {
                selectedSheetName = cmbSheets.Text;
                panel1.Enabled = false;
                bgTransfer.RunWorkerAsync();
            }
        }

        private bool isRowsValid()
        {
            if(sStart>sEnd)
            {
                MessageBox.Show("Start and End row's value invalid or in wrong order", "Transfer Canceled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void bgTransfer_DoWork(object sender, DoWorkEventArgs e)
        {
            transfer();
        }
        
        private void transfer()
        {

            try
            {

                workbook = TempHolder.excelApp.Workbooks.Open(excelPath);
                Worksheet worksheet = (Worksheet)workbook.Worksheets.get_Item(selectedSheetName);

                qry = "INSERT  INTO " + SQLbank.TBL_SERVICE_RECORDS + "(" + SQLbank.EMP_ID + ","
                    + SQLbank.FROM_DATE + ","
                    + SQLbank.TO_DATE + ","
                    + SQLbank.DESIGNATION + ","
                    + SQLbank.STATUS + ","
                    + SQLbank.SALARY + ","
                    + SQLbank.STATION + ","
                    + SQLbank.BRANCH + ","
                    + SQLbank.CAUSE + ","
                    + SQLbank.LAWOP + ") VALUES ";

                int rowCounter = 0;

                openSQLConnection();
                cmd = new SqlCommand();
                cmd.Connection = conn;


                for (int i = (int)sStart; i <= sEnd; i++)
                {

                    string from = worksheet.Cells[i, 1].Value + "";
                    string to = worksheet.Cells[i, 2].Value + "";
                    string designation = worksheet.Cells[i, 3].Value + "";
                    string status = worksheet.Cells[i, 4].Value + "";
                    string salary = worksheet.Cells[i, 5].Value + "";
                    string station = worksheet.Cells[i, 6].Value + "";
                    string branch = worksheet.Cells[i, 7].Value + "";
                    string cause = worksheet.Cells[i, 8].Value + "";
                    string lawop = worksheet.Cells[i, 9].Value + "";

                    //qry += Environment.NewLine;
                    if (rowCounter > 0) qry += ",";
                    //qry += "('" + from + "','" + to + "','" + designation + "','" + status + "'," + salary + ",'" + station + "','" + branch + "','" + cause + "','" + lawop + "')";
                    qry += "(@EMPID" + i + ",@FROM" + i + ", @TO" + i + " , @DESIGNATION" + i + " , @STATUS" + i + ", @SALARY" + i + ", @STATION" + i + ", @BRANCH" + i + ", @CAUSE" + i + ", @LAWOP" + i + ")";
                    cmd.Parameters.AddWithValue("@EMPID" + i, TempHolder.searchedEmpID);
                    cmd.Parameters.AddWithValue("@FROM" + i, from);

                    if (to.Trim().Length == 0 || to.ToLower().Trim().Contains("present"))
                    {
                        cmd.Parameters.AddWithValue("@TO" + i, DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@TO" + i, to);
                    }   
                   

                    cmd.Parameters.AddWithValue("@DESIGNATION" + i, designation);
                    cmd.Parameters.AddWithValue("@STATUS" + i, status);
                    cmd.Parameters.AddWithValue("@SALARY" + i, salary);
                    cmd.Parameters.AddWithValue("@STATION" + i, station);
                    cmd.Parameters.AddWithValue("@BRANCH" + i, branch);
                    cmd.Parameters.AddWithValue("@CAUSE" + i, cause);
                    cmd.Parameters.AddWithValue("@LAWOP" + i, lawop);
                    rowCounter++;

                }
                cmd.CommandText = qry;

                cmd.ExecuteNonQuery();
                //close workbook
                workbook.Close();

                transfermsg = "Transfer Complete";

            }
            catch (Exception ee)
            {
                transfermsg = "Failed to transfer: " + ee.Message;
            }
            
        }

        private void bgTransfer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(transfermsg, "Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
           
            TempHolder.uc_ServiceRecord.loadRecords(TempHolder.searchedEmpID);
            this.Close();

        }
        

        //***************************************************************************************
        private void cmbSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSheets.SelectedIndex != -1)
            {
                btnStart.Enabled = true;

            }
            else btnStart.Enabled = false;
        }

    }
}
