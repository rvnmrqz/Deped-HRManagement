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

namespace HumanResourceManagement
{
    public partial class ImportExcelDialog : Form
    {
        string excelPath, excelName, excelConString;


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
        }


        //****************CONNECTIONS**********************************************
        private void openSQLConnection()
        {
            if (sqlcon != null && sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
                sqlcon.Open();
            }
            else
            {
                sqlcon.Open();
            }
        }

        private void closeSQLConnection()
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }
        }

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


        //****************************************************************************
        public ImportExcelDialog(string excelpath, string excelName)
        {
            Console.WriteLine("Constructor");
            this.excelPath = excelpath;
            this.excelName = excelName;

            InitializeComponent();
        }

        private void ImportExcelDialog_Load(object sender, EventArgs e)
        {
            lblExcelName.Text = excelName;
            bgLoader.RunWorkerAsync();
        }

        private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            readExcel();
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
            catch(Exception ee)
            {
                Console.WriteLine("Problem occured while reading sheets: " + ee.Message);
            }
        }

        private void readExcelSheetNames()
        {
            try
            {
                Console.WriteLine("Reading Excel Sheet's Names");


                openExcelConnection();

                DataTable dtExcelSchema = new DataTable();

                dtExcelSchema = excelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //to read all excel sheet name

                for (int i = 0; i < dtExcelSchema.Rows.Count; i++)
                {
                    string sheet = dtExcelSchema.Rows[i]["TABLE_NAME"].ToString();
                    sheet = sheet.TrimEnd("$".ToCharArray());
                    sheetNamesList.Add(sheet);
                    Console.WriteLine("Sheet scanned: " + sheet);
                }
                closeExcelConnection();

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
            for (int i = 0; i < sheetNamesList.Count; i++)
            {
                cmbSheets.Items.Add(sheetNamesList[i]);
                Console.WriteLine(sheetNamesList[i] + " added to combobox");
            }
            if (cmbSheets.Items.Count > 0) cmbSheets.SelectedIndex = 0;

        }

        private bool readExcelSchema()
        {
            try
            {
                openExcelConnection();

                DataTable dtExcelSchema = new DataTable();

                dtExcelSchema = excelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = cmbSheets.Text.ToString() + "$";
                dtExcelSchema = excelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, SheetName, null });

                closeExcelConnection();

                DataView dv = new DataView(dtExcelSchema);

                dv.Sort = "ORDINAL_POSITION";

                //to avoid redundant item
                columnList.Clear();

                foreach (DataRowView rowView in dv)
                {
                    DataRow row = rowView.Row;
                    string rawColName = row["COLUMN_NAME"].ToString();
                    columnList.Add(rawColName);
                    // string colName = Regex.Replace(row["COLUMN_NAME"].ToString().Trim(), @"[^\w\d]", "_");
                    // colName = colName.ToLower().TrimEnd('_');

                }

                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show("Failed to read Excel Sheets", "Problem Occured while reading Excel Schema\n" + ee.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void cmbSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSheets.SelectedIndex != -1)
            {
                btnStart.Enabled = readExcelSchema();
                
            }
            else btnStart.Enabled = false;
        }


      





    }
}
