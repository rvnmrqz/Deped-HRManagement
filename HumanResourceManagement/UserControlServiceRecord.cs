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

namespace HumanResourceManagement
{
    public partial class UserControlServiceRecord : UserControl
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

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
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
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

        public void loadRecords(String employee_id)
        {
            try
            {
            //    Console.WriteLine("Loading Service Records of employee :" + employee_id);
             
                openSQLConnection();
                string qry = "SELECT * FROM " + SQLbank.TBL_SERVICE_RECORDS + " WHERE " + SQLbank.EMP_ID + " = " + employee_id+";";
                cmd = new SqlCommand(qry,conn);
                reader = cmd.ExecuteReader();
                int counter = 0;
                while (reader.Read())
                {
                    counter++;
                    string id = reader[SQLbank.ID].ToString();
                    string school = reader[SQLbank.SCHOOL_NAME].ToString();
                    string branch = reader[SQLbank.BRANCH].ToString();
                    string position = reader[SQLbank.POSITION_TITLE].ToString();
                    string start = reader[SQLbank.FROM_DATE].ToString();
                    string end = reader[SQLbank.TO_DATE].ToString();

                    DateTime st, ed;
                    if (DateTime.TryParse(start, out st)) start = st.ToString("MM/dd/yyyy");
                    if (DateTime.TryParse(end, out ed)) end =ed.ToString("MM/dd/yyyy");
           

                    string status = reader[SQLbank.STATUS].ToString();
                    string remarks = reader[SQLbank.REMARKS].ToString();
                    string salary = reader[SQLbank.SALARY].ToString();
                    string lawop = reader[SQLbank.LAWOP].ToString();
                    datagridServiceRecords.Rows.Add(id, school, branch, position,start,end, status, remarks, salary, lawop);
                }
                Console.WriteLine("Records found: " + counter);

                btnAddRecord.Enabled = true;
                btnPrint.Enabled = true;

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
            btnPrint.Enabled = false;

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
        }

        private void datagridServiceRecords_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblRowsCount.Text = datagridServiceRecords.Rows.Count.ToString();
        }


        private void datagridServiceRecords_MouseClick(object sender, MouseEventArgs e)
        {
            /*if(e.Button == MouseButtons.Right)
            {
                //show context menu

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Cut"));
                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));

                int currentMouseOverRow = datagridServiceRecords.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(datagridServiceRecords, new Point(e.X, e.Y));

            }*/
        }

        private void lblRowsCount_TextChanged(object sender, EventArgs e)
        {
            btnPrint.Enabled = lblRowsCount.Text.Length == 0 || lblRowsCount.Text.Trim().Equals("0");
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.ADD_SERVICE_RECORD))
            {
                AddServiceRecordDialog ad = new AddServiceRecordDialog();
                ad.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.DELETE_SERVICE_RECORD))
            {
               if( MessageBox.Show("Delete selected Service Record?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //delete record
                    try
                    {
                        openSQLConnection();
                        string delSQL = "DELETE FROM " + SQLbank.TBL_SERVICE_RECORDS + " WHERE  " + SQLbank.ID + " = " + lblSelectedRowID.Text;
                        Console.WriteLine("Delete Query: " + delSQL);
                        cmd = new SqlCommand(delSQL, conn);
                        cmd.ExecuteNonQuery();

                        datagridServiceRecords.Rows.RemoveAt(datagridServiceRecords.CurrentCell.RowIndex);
                        MessageBox.Show("Service Record Deleted", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine("Deleting Error: " + ee.Message);
                    }
                }
            }
        }
    }
}
