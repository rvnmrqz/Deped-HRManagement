using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace HumanResourceManagement
{
    public partial class SchoolMaintenance : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        string errorMsg;


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        //*******************APP CONFIG MANAGER*********************************
        private string getStringValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

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
        }//*********************************************************************

        public SchoolMaintenance()
        {
            InitializeComponent();
        }

        private void SchoolMaintenance_Load(object sender, EventArgs e)
        {

            cmbBy.SelectedIndex = 1;
            conn = new SqlConnection(getStringValue("sqlconstring"));
            if (!bgSchoolLoader.IsBusy) bgSchoolLoader.RunWorkerAsync();
        }


        private void bgSchoolLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            errorMsg = null;
            loadSchools();
        }

        private void bgSchoolLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                while (reader.Read())
                {
                    dgvSchools.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                }

                if (errorMsg != null) MessageBox.Show(errorMsg, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ee)
            {
                MessageBox.Show("An error occured while displaying Schools\n" + ee.Message);
            }
        }


        private void loadSchools()
        {
            try
            {
                openSQLConnection();

                string qry = "SELECT * FROM " + SQLbank.TBL_SCHOOLS;
                cmd = new SqlCommand(qry, conn);
                reader = cmd.ExecuteReader();
            }
            catch (Exception ee)
            {
                errorMsg = "Failed to load Schools \n" + ee.Message;
            }
        }


        private void lblFormExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            if (btnEditSave.Text.ToLower().Contains("edit"))
            {
                editMode(1);
            }
            else
            {
                //do saving here
                save(false,"Update successful!");
                editMode(2);

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            editMode(3);
        }

        private void editMode(int value)
        {
            //1 - edit mode
            //2 - not edit mode
            //3 - cancel

            if (value == 1)
            {
                infoGroup.Enabled = true;
                dgvSchools.Enabled = false;
                btnEditSave.Text = "Save";
                btnCancel.Visible = true;
            }
            else
            {
                infoGroup.Enabled = false;
                dgvSchools.Enabled = true;
                btnEditSave.Text = "Edit";
                btnCancel.Visible = false;

                if (value == 3)
                {
                    //user clicked, canceled.
                    //reselect the row to redisplay original value
                    if (dgvSchools.RowCount > 0)
                    {
                        int currentselectedIndex = dgvSchools.CurrentCell.RowIndex;
                        dgvSchools.ClearSelection();
                        dgvSchools.Rows[currentselectedIndex].Selected = true;
                    }
                }
            }

        }

        private void save(bool newSchool,string savedMsg)
        {
            try
            {
                openSQLConnection();
                string qry = "";
                if (newSchool)
                {
                    qry = "INSERT INTO " + SQLbank.TBL_SCHOOLS + "(" + SQLbank.SCHOOL_ID + "," + SQLbank.SCHOOL_NAME + "," + SQLbank.DISTRICT + ")" +
                        " VALUES(@SCHOOLID,@SCHOOLNAME,@DISTRICT)";
                }
                else
                {
                    qry = "UPDATE " + SQLbank.TBL_SCHOOLS + " SET " +
                    SQLbank.SCHOOL_ID + " = @SCHOOLID ," +
                    SQLbank.SCHOOL_NAME + " = @SCHOOLNAME , " +
                    SQLbank.DISTRICT + " = @DISTRICT" +
                    " WHERE " + SQLbank.ID + "=" + lblSelectedSchoolId.Text;
                }


                cmd = new SqlCommand(qry,conn);

                cmd.Parameters.AddWithValue("@SCHOOLID", txtSchoolId.Text);
                cmd.Parameters.AddWithValue("@SCHOOLNAME", txtSchoolname.Text);
                cmd.Parameters.AddWithValue("@DISTRICT", txtdistrict.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Saving failed\n" + ee.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void dgvSchools_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblResults.Text = dgvSchools.Rows.Count.ToString();
        }

        private void dgvSchools_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblResults.Text = dgvSchools.Rows.Count.ToString();
        }

        private void dgvSchools_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSchools.Rows.Count > 0)
            {
                btnEditSave.Visible = true;

                int selectedindex = dgvSchools.CurrentCell.RowIndex;
                lblSelectedSchoolId.Text = dgvSchools.Rows[selectedindex].Cells[0].Value.ToString();
                txtSchoolId.Text = dgvSchools.Rows[selectedindex].Cells[1].Value.ToString();
                txtSchoolname.Text = dgvSchools.Rows[selectedindex].Cells[2].Value.ToString();
                txtdistrict.Text = dgvSchools.Rows[selectedindex].Cells[3].Value.ToString();


            }
        }


    }
}
