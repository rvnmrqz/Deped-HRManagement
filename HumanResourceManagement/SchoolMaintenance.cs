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


        string searchQry;

        
        bool savingNew = false;

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
        }
        
        //*********************************************************************

        public SchoolMaintenance()
        {
            InitializeComponent();
        }

        private void SchoolMaintenance_Load(object sender, EventArgs e)
        {

            cmbBy.SelectedIndex = 1;
            conn = new SqlConnection(getStringValue("sqlconstring"));
            loadSchools();
        }

        private void bgSchoolLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            errorMsg = null;
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
            if (!bgSchoolLoader.IsBusy) bgSchoolLoader.RunWorkerAsync();
        }

        private void clearInfoGroup()
        {
            txtSchoolId.ResetText();
            txtSchoolname.ResetText();
            txtdistrict.ResetText();
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
                txtSchoolId.Select();
            }
            else
            {
                //do saving here
                save(savingNew);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            editMode(-1);
        }

        private void editMode(int value)
        {
            //-1 - cancel
            //1 - edit mode
            //2 - adding new
            //3 - not edit mode

            switch (value)
            {
                case 1:
                    savingNew = false;
                    btnAddNew.Visible = false;
                    txtSchoolId.Enabled = true;
                    txtSchoolname.Enabled = true;
                    txtdistrict.Enabled = true;
                    dgvSchools.Enabled = false;
                    btnEditSave.Text = "Save";
                    btnCancel.Visible = true;
                    break;
                case 2:
                    savingNew = true;
                    clearInfoGroup();
                    btnEditSave.Enabled = true;
                    btnDelete.Enabled = false;
                    btnAddNew.Visible = false;
                    txtSchoolId.Enabled = true;
                    txtSchoolname.Enabled = true;
                    txtdistrict.Enabled = true;
                    dgvSchools.Enabled = false;
                    btnEditSave.Text = "Save";
                    btnCancel.Visible = true;
                    txtSchoolId.Select();
                    break;
                default:
                    savingNew = false;
                    btnAddNew.Visible = true;
                    txtSchoolId.Enabled = false;
                    txtSchoolname.Enabled = false;
                    txtdistrict.Enabled = false;
                    dgvSchools.Enabled = true;
                    btnEditSave.Text = "Edit";
                    btnCancel.Visible = false;

                    btnEditSave.Enabled = (dgvSchools.CurrentRow != null);
                    btnDelete.Enabled = (dgvSchools.CurrentRow != null);

                    if (value == -1)
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
                    break; 
            }
        }

        private void save(bool newSchool)
        {
            if (isInputValid())
            {
                try
                {
                    openSQLConnection();
                    string qry = "", savedMsg = "";
                    if (newSchool)
                    {
                        savedMsg = "New School Saved";
                        qry = "INSERT INTO " + SQLbank.TBL_SCHOOLS + "(" + SQLbank.SCHOOL_ID + "," + SQLbank.SCHOOL_NAME + "," + SQLbank.DISTRICT + ")" +
                            " OUTPUT INSERTED.ID VALUES(@SCHOOLID,@SCHOOLNAME,@DISTRICT)";
                    }
                    else
                    {
                        savedMsg = "Update Successful";
                        qry = "UPDATE " + SQLbank.TBL_SCHOOLS + " SET " +
                        SQLbank.SCHOOL_ID + " = @SCHOOLID ," +
                        SQLbank.SCHOOL_NAME + " = @SCHOOLNAME , " +
                        SQLbank.DISTRICT + " = @DISTRICT  OUTPUT INSERTED.ID" +
                        " WHERE " + SQLbank.ID + "=" + lblSelectedSchoolId.Text;
                    }


                    cmd = new SqlCommand(qry, conn);

                    cmd.Parameters.AddWithValue("@SCHOOLID", txtSchoolId.Text);
                    cmd.Parameters.AddWithValue("@SCHOOLNAME", txtSchoolname.Text);
                    cmd.Parameters.AddWithValue("@DISTRICT", txtdistrict.Text);

                    string id = cmd.ExecuteScalar().ToString();

                    if (newSchool)
                    {
                        //insert in dgv
                        dgvSchools.Rows.Add(id, txtSchoolId.Text, txtSchoolname.Text, txtdistrict.Text);
                    }
                    else
                    {
                        //update selected row
                        dgvSchools.Rows[dgvSchools.CurrentRow.Index].SetValues(id, txtSchoolId.Text, txtSchoolname.Text, txtdistrict.Text);
                    }
                    editMode(3);
                    MessageBox.Show(savedMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Saving failed\n" + ee.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool isInputValid()
        {
            if (txtSchoolId.Text.Trim().Length == 0)
            {
                MessageBox.Show("School ID must not be empty", "Saving canceled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSchoolId.Select();
                return false;
            }

            if(txtSchoolname.Text.Trim().Length == 0)
            {
                MessageBox.Show("School name must not be empty", "Saving Canceled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSchoolname.Select();
                return false;
            }

            if(txtdistrict.Text.Trim().Length == 0)
            {
                MessageBox.Show("District must not be empty", "Saving Canceled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtdistrict.Select();
                return false;
            }

            

            return true;
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


            bool enabled = (dgvSchools.RowCount > 0);

            btnEditSave.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }

        private void dgvSchools_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblResults.Text = dgvSchools.Rows.Count.ToString();
            bool enabled = (dgvSchools.RowCount > 0);

            btnEditSave.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }

        private void dgvSchools_SelectionChanged(object sender, EventArgs e)
        {
            clearInfoGroup();

            if (dgvSchools.Rows.Count > 0)
            {
                btnEditSave.Visible = true;
                btnDelete.Visible = true;
                btnDelete.Enabled = true;

                int selectedindex = dgvSchools.CurrentCell.RowIndex;
                lblSelectedSchoolId.Text = dgvSchools.Rows[selectedindex].Cells[0].Value.ToString();
                txtSchoolId.Text = dgvSchools.Rows[selectedindex].Cells[1].Value.ToString();
                txtSchoolname.Text = dgvSchools.Rows[selectedindex].Cells[2].Value.ToString();
                txtdistrict.Text = dgvSchools.Rows[selectedindex].Cells[3].Value.ToString();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            editMode(2);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvSchools.RowCount>0 && dgvSchools.CurrentRow != null)
            {
                try
                {
                    if(MessageBox.Show("Delete currently selected row?","Delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        openSQLConnection();
                        cmd = new SqlCommand("DELETE FROM " + SQLbank.TBL_SCHOOLS + " WHERE " + SQLbank.ID + "=" + lblSelectedSchoolId.Text, conn);
                        cmd.ExecuteNonQuery();
                        dgvSchools.Rows.RemoveAt(dgvSchools.CurrentRow.Index);
                        MessageBox.Show("Deleted!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Failed to Delete \n" + ee.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                btnEditSave.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            editMode(-1);
            dgvSchools.Rows.Clear();
            loadSchools();
        }

        private void cmbBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBy.SelectedIndex)
            {
                case 0:
                    //schoolid
                    searchQry = "SELECT * FROM " + SQLbank.TBL_SCHOOLS + " WHERE " + SQLbank.SCHOOL_ID + " LIKE @KEYWORD";
                    break;
                case 1:
                    //schoolname
                    searchQry = "SELECT * FROM " + SQLbank.TBL_SCHOOLS + " WHERE " + SQLbank.SCHOOL_NAME + " LIKE @KEYWORD";
                    break;
                case 2:
                    //district
                    searchQry = "SELECT * FROM " + SQLbank.TBL_SCHOOLS + " WHERE " + SQLbank.DISTRICT + " LIKE @KEYWORD";
                    break;
            }

        }


        //SEARCHING
        private void txtEmplyeeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && txtEmplyeeNo.Text.Trim().Length != 0)
            {
                try
                {
                
                    dgvSchools.Rows.Clear();
                    clearInfoGroup();
                    editMode(3);

                    openSQLConnection();
                    cmd = new SqlCommand(searchQry, conn);
                    cmd.Parameters.AddWithValue("@KEYWORD",  txtEmplyeeNo.Text.Trim() + "%");
                    reader = cmd.ExecuteReader();
                    int ctr = 0;
                    while (reader.Read())
                    {
                        ctr++;
                        dgvSchools.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                    }

                    if (ctr == 0) MessageBox.Show("No results found");
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Failed to search\n" + ee.Message,"Oops",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }else if(e.KeyCode == Keys.Enter && txtEmplyeeNo.Text.Trim().Length == 0)
            {
                clearInfoGroup();
                editMode(3);
                refresh();  
            }

        }
    }
}
