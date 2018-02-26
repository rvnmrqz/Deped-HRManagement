using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace HumanResourceManagement
{
    public partial class SystemUserMaintenance : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        bool searching = false;
        string errorMsg = null;
        string keyword="",query = "";

        bool savingNew = false;


        int CANCEL = -1;
        int EDITMODE = 1;
        int ADDINGNEW = 2;
        int NOTEDITMODE = 3;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public SystemUserMaintenance()
        {
            InitializeComponent();
        }


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

        private void SystemUserMaintenance_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
            cmbfilterby.SelectedIndex = 0;
            populateRoles();
            loadUsers();
        }

        private void FormPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void lblFormExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void populateRoles()
        {
            for (int i = 0; i < TempHolder.roles_list.Count; i++)
            {
                cmbRoles.Items.Add(TempHolder.roles_list[i]);
            }
        
        }

        //********************LOADER*************************************************

        private void loadUsers()
        {

            if (bgLoader.IsBusy)
            {
                bgLoader.CancelAsync();
            }
            query = "SELECT * FROM " + SQLbank.TBL_USERS;

            dgvUsers.Rows.Clear();
            clearInfoGroup();
            bgLoader.RunWorkerAsync();
 
        }

        private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                errorMsg = null;
                openSQLConnection();
                cmd = new SqlCommand(query, conn);
                if (query.Contains("@KEYWORD"))
                {
                    cmd.Parameters.AddWithValue("@KEYWORD", keyword + "%");
                }
                reader = cmd.ExecuteReader();

            }
            catch (Exception ee)
            {
                errorMsg = "Problem occured while loading: " + ee.Message;
            }
        }



        private void bgLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          

            int ctr = 0;
            while (reader.Read())
            {
                dgvUsers.Rows.Add(reader[0].ToString(), reader[1].ToString(),reader[2].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[4].ToString());
                ctr++;
            }

            if (searching)
            {
                MessageBox.Show("Match result(s): " + ctr, "Search Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            if (errorMsg != null) MessageBox.Show(errorMsg, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);

            searching = false;

        }

        //**************************************************************************

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            clearInfoGroup();
            if (dgvUsers.Rows.Count > 0)
            {
                int selectedindex = dgvUsers.CurrentRow.Index;
                lblSelectedID.Text = dgvUsers.Rows[selectedindex].Cells[0].Value.ToString();
                lblOriginalUsername.Text = dgvUsers.Rows[selectedindex].Cells[1].Value.ToString();
                txtUsername.Text = dgvUsers.Rows[selectedindex].Cells[1].Value.ToString();
                lblPassword.Text = dgvUsers.Rows[selectedindex].Cells[2].Value.ToString();
                txtFirstname.Text = dgvUsers.Rows[selectedindex].Cells[3].Value.ToString();
                txtMiddlename.Text = dgvUsers.Rows[selectedindex].Cells[4].Value.ToString();
                txtLastname.Text = dgvUsers.Rows[selectedindex].Cells[5].Value.ToString();
                cmbRoles.Text = dgvUsers.Rows[selectedindex].Cells[6].Value.ToString();

                btnEditSave.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnEditSave.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void clearInfoGroup()
        {
            lblOriginalUsername.ResetText();
            txtUsername.ResetText();
            lblPassword.ResetText();
            txtFirstname.ResetText();
            txtMiddlename.ResetText();
            txtLastname.ResetText();
            cmbRoles.SelectedIndex = -1;
        }

        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && txtSearchKey.Text.Trim().Length != 0)
            {

                keyword = txtSearchKey.Text;

                if (!bgLoader.IsBusy)
                {
                    searching = true;
                    query = generateFilteredQuery();
                    dgvUsers.Rows.Clear();
                    clearInfoGroup();
                    bgLoader.RunWorkerAsync();
                }
            }
            else if(e.KeyCode == Keys.Enter && txtSearchKey.Text.Trim().Length == 0)
            {
                //return to default
                loadUsers();
            }
        }

        private void dgvUsers_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblResults.Text = dgvUsers.Rows.Count.ToString();
        }

        private void dgvUsers_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblResults.Text = dgvUsers.Rows.Count.ToString();
        }

        private string generateFilteredQuery()
        {
            /*
            0 Username
            1 First name
            2 Middle name
            3 Last name
            */
            string qry = "SELECT * FROM " + SQLbank.TBL_USERS;
            switch (cmbfilterby.SelectedIndex)
            {
 
                case 0:
                    qry += " WHERE " + SQLbank.USERNAME + " LIKE @KEYWORD";
                    break;

                case 1:
                    qry += " WHERE " + SQLbank.FNAME + " LIKE @KEYWORD";
                    break;

                case 2:
                    qry += " WHERE " + SQLbank.MNAME + " LIKE @KEYWORD";
                    break;

                case 3:
                    qry += " WHERE " + SQLbank.LNAME + " LIKE @KEYWORD";
                    break;
            }
      
            return qry;
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
                    dgvUsers.Enabled = false;
                    savingNew = false;
                    btnAddNew.Enabled = false;
                    enableFields(true);
                    btnEditSave.Text = "Save";
                    btnCancel.Visible = true;
                    break;
                case 2:
                    savingNew = true;
                    dgvUsers.ClearSelection();
                    dgvUsers.Enabled = false;
                    clearInfoGroup();
                    enableFields(true);
                    btnEditSave.Enabled = true;
                    btnDelete.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnEditSave.Text = "Save";
                    btnCancel.Visible = true;
                    txtUsername.Select();
                    break;
                default:
                    dgvUsers.Enabled = true;
                    savingNew = false;
                    btnAddNew.Enabled = true;
                    enableFields(false);
                    btnEditSave.Text = "Edit";
                    btnCancel.Visible = false;
                    btnEditSave.Enabled = (dgvUsers.CurrentRow != null);
                    btnDelete.Enabled = (dgvUsers.CurrentRow != null);
                    
                    if (value == -1)
                    {
                        //user clicked, canceled.
                        //reselect the row to redisplay original value
                        if (dgvUsers.RowCount > 0)
                        {
                            int currentselectedIndex = dgvUsers.CurrentCell.RowIndex;
                            dgvUsers.ClearSelection();
                            dgvUsers.Rows[currentselectedIndex].Selected = true;
                        }
                    }
                    break;
            }
        }

        private void reselectLastSelectedRow()
        {
            if (dgvUsers.RowCount > 0)
            {
                int selectedindex = dgvUsers.CurrentCell.RowIndex;
                dgvUsers.ClearSelection();
                dgvUsers.Rows[selectedindex].Selected = true;
            }
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            if (btnEditSave.Text.ToLower().Contains("edit"))
            {
                editMode(EDITMODE);
            }
            else
            {
                saveUser(savingNew);
            }
        }

        private void saveUser(bool newUser)
        {
            //NO CODES YET FOR IMAGES
            if (isInputValid())
            {
                try
                {
                    string qry = "";

                    Console.WriteLine("SaveUser, newUser: " + newUser);
                    if (newUser)
                    {
                        qry = "INSERT INTO " + SQLbank.TBL_USERS + "(" + SQLbank.USERNAME + "," +
                            SQLbank.PASSWORD + "," +
                            SQLbank.ROLE + "," +
                            SQLbank.FNAME + "," +
                            SQLbank.MNAME + "," +
                            SQLbank.LNAME + ") " +
                            "VALUES (@USERNAME, @PASSWORD, @ROLE, @FNAME, @MNAME, @LNAME)";
                    }
                    else
                    {
                        qry = "UPDATE " + SQLbank.TBL_USERS + " SET " + SQLbank.USERNAME + "=@USERNAME, " +
                            SQLbank.PASSWORD + " = @PASSWORD," +
                            SQLbank.ROLE + " = @ROLE," +
                            SQLbank.FNAME + " = @FNAME, " +
                            SQLbank.MNAME + " = @MNAME, " +
                            SQLbank.LNAME + " = @LNAME WHERE " + SQLbank.ID + " = " + lblSelectedID.Text;
                    }

                    Console.WriteLine("Saving new: " + savingNew + "\nQuery: " + qry);

                    openSQLConnection();
                    cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@USERNAME", txtUsername.Text.ToString());
                    cmd.Parameters.AddWithValue("@PASSWORD", lblPassword.Text);
                    cmd.Parameters.AddWithValue("@ROLE", cmbRoles.Text);
                    cmd.Parameters.AddWithValue("@FNAME", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("@MNAME", txtMiddlename.Text);
                    cmd.Parameters.AddWithValue("@LNAME", txtLastname.Text);

                    cmd.ExecuteNonQuery();

                    if (newUser) dgvUsers.Rows.Add(lblSelectedID.Text, txtUsername.Text, lblPassword.Text, txtFirstname.Text, txtMiddlename.Text, txtLastname.Text, cmbRoles.Text);
                    else dgvUsers.Rows[dgvUsers.CurrentCell.RowIndex].SetValues(lblSelectedID.Text, txtUsername.Text, lblPassword.Text, txtFirstname.Text, txtMiddlename.Text, txtLastname.Text, cmbRoles.Text);
                    reselectLastSelectedRow();
                   
                    editMode(NOTEDITMODE);
                    savingNew = false;
                    MessageBox.Show("Saved");

                }
                catch (Exception ee)
                {
                    Console.WriteLine("\n\nSaving exception: " + ee.Message);
                    MessageBox.Show("Failed to save \n" + ee.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool isInputValid()
        {
            if (txtUsername.Text.Trim().Length == 0)
            {
                MessageBox.Show("Username must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (savingNew || (!savingNew && !lblOriginalUsername.Text.ToString().Equals((txtUsername.Text.ToString())))) 
            {
                // checks if the new user's username is already used
                try
                {
                    openSQLConnection();
                    cmd = new SqlCommand("SELECT "+SQLbank.ID+" from " + SQLbank.TBL_USERS + " WHERE " + SQLbank.USERNAME + " = '" + txtUsername.Text.ToString() + "'", conn);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Username is already used", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Cannot check for username duplicat \n" + ee.Message);
                    return false;
                }
            }

            if(txtFirstname.Text.Trim().Length == 0)
            {
                MessageBox.Show("First name must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (txtLastname.Text.Trim().Length == 0)
            {
                MessageBox.Show("Last name must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if(cmbRoles.SelectedIndex == -1)
            {
                MessageBox.Show("Choose an account role", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if(lblPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("No password set", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            editMode(CANCEL);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.DELETE_SYS_USER_PERMISSION)){
                if (DialogResult.OK == MessageBox.Show("Delete Selected User?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    int currentIndex = dgvUsers.CurrentCell.RowIndex;
                    try
                    {
                        openSQLConnection();
                        cmd = new SqlCommand("DELETE FROM " + SQLbank.TBL_USERS + " WHERE " + SQLbank.ID + " = " + dgvUsers.Rows[currentIndex].Cells[0].Value , conn);
                        cmd.ExecuteNonQuery();
                        dgvUsers.Rows.RemoveAt(currentIndex);
                        editMode(NOTEDITMODE);
                        MessageBox.Show("User Deleted", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Failed to delete selected user\n" + ee.Message);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            editMode(NOTEDITMODE);
            loadUsers();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                editMode(ADDINGNEW);

            }
            catch (Exception)
            {

            }
        }

        private void linkSetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (PasswordDialog pd = new PasswordDialog())
            {
                if(pd.ShowDialog() == DialogResult.OK)
                {
                    lblPassword.Text = pd.encryptedPassword;
                }
            }
                
        }

        private void btnChoosePhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if(op.ShowDialog() == DialogResult.OK)
            {
                lblUploadedPicture.Text = op.FileName;
            }
        }

        private void enableFields( bool val)
        {
            txtUsername.Enabled = val;
            txtFirstname.Enabled = val;
            txtMiddlename.Enabled = val;
            txtLastname.Enabled = val;
            cmbRoles.Enabled = val;
            linkSetPassword.Enabled = val;
        }



    }
}
