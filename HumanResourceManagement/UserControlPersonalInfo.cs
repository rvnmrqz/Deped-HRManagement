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
using MetroFramework.Controls;
using System.IO;

namespace HumanResourceManagement
{
    public partial class UserControlPersonalInfo : UserControl
    {

        String emp_id = "";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        private static UserControlPersonalInfo _instance;
        public static UserControlPersonalInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserControlPersonalInfo();
                }
                if (_instance.IsDisposed)
                {
                    _instance = new UserControlPersonalInfo();
                }

                return _instance;
            }
        }

        public UserControlPersonalInfo()
        {
            InitializeComponent();
        }

        private void UserControlPersonalInfo_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
            loadCivilStatus();
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

        //****************LOADING INFO*************************************************

        private void loadCivilStatus()
        {
            try
            {
                cmbCivilStatus.Items.Clear();

                for (int i = 0; i < TempHolder.civil_status_list.Count; i++)
                {
                    cmbCivilStatus.Items.Add(TempHolder.civil_status_list[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load Civil Status: " + ex.Message);
            }
        }

        public void loadInfo(string employee_id)
        {
            emp_id = employee_id;

            Console.WriteLine("Loading Personal Infor of employee: " + employee_id);
            clearDisplay();
            try
            {
                openSQLConnection();
                string cmdtext = "SELECT * FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.EMP_ID + " = " + employee_id + ";";
                cmd = new SqlCommand(cmdtext, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtFname.Text = reader[SQLbank.EMP_FIRST_NAME].ToString();
                    txtMname.Text = reader[SQLbank.EMP_MIDDLE_NAME].ToString();
                    txtLname.Text = reader[SQLbank.EMP_LAST_NAME].ToString();

                    if (reader[SQLbank.SEX].ToString().ToLower().Contains('f')) cmbGender.SelectedIndex = 1; //it is female
                    else if (reader[SQLbank.SEX].ToString().Trim().Length == 0) cmbGender.SelectedIndex = -1; //no gender specified
                    else cmbGender.SelectedIndex = 0; //it is male

                    txtBirthPlace.Text = reader[SQLbank.BIRTH_PLACE].ToString();
         
                    string dateofbirth = reader[SQLbank.DATE_OF_BIRTH].ToString();
                    DateTime dt;
                    if(DateTime.TryParse(dateofbirth,out dt))
                    {
                        txtDateOfBirth.Text = dt.ToString("MM/dd/yyyy");
                    }

            

                    cmbCivilStatus.Text = reader[SQLbank.CIVIL_STATUS].ToString();
                    txtPlantillaNo.Text = reader[SQLbank.PLANTILLA_NO].ToString();
                    txthdmf.Text = reader[SQLbank.HDMF_NO].ToString();
                    txtphic.Text = reader[SQLbank.PHIC_NO].ToString();
                    txtBp.Text = reader[SQLbank.BP_NO].ToString();
                    txtAccNo.Text = reader[SQLbank.ACCOUNT_NO].ToString();
                    txtTin.Text = reader[SQLbank.TIN_NO].ToString();

                    btnRemoveEmpAcc.Visible = true;
                    btnEdit.Visible = true;

                }

            }
            catch (Exception ee)
            {
                Console.WriteLine("Load Personal Info Exception: " + ee.Message);
            }
        }

        public void clearDisplay()
        {
            btnRemoveEmpAcc.Visible = false;
            btnEdit.Visible = false;
            txtFname.ResetText();
            txtMname.ResetText();
            txtLname.ResetText();
            cmbGender.SelectedIndex = -1;
            cmbCivilStatus.SelectedIndex = -1;
            txtBirthPlace.ResetText();
            txtDateOfBirth.ResetText();
            txtAge.ResetText();
            txtPlantillaNo.ResetText();
            txthdmf.ResetText();
            txtphic.ResetText();
            txtBp.ResetText();
            txtAccNo.ResetText();
            txtTin.ResetText();
        }
        //****************SAVING/EDITING***********************************************
        public void editMode(bool editmode)
        {
            btnEdit.Visible = !editmode;
            btnEdit.Enabled = !editmode;
            btnSaveChanges.Visible = editmode;
            btnCancelEditing.Visible = editmode;
            panel_personalInfo.Enabled = editmode;
         
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.MODIFY_EMPLOYEE_INFO_PERMISSION))
            {
                TempHolder.mainForm.editMode(true);
            }
        }

        private void btnCancelEditing_Click(object sender, EventArgs e)
        {

            TempHolder.mainForm.editMode(false);
            TempHolder.mainForm.search();

        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {

            if (isFieldsValid())
            {
                if (TempHolder.mainForm.saveUpdates() == 1)
                {
                    //save changes
                    try
                    {
                        openSQLConnection();
                        string updateQry = "UPDATE " + SQLbank.TBL_EMPLOYEES + " SET "
                            + SQLbank.EMP_LAST_NAME + " = @LASTNAME , "
                            + SQLbank.EMP_MIDDLE_NAME + " = @MIDDLENAME , "
                            + SQLbank.EMP_FIRST_NAME + " = @FIRSTNAME , "
                            + SQLbank.BIRTH_PLACE + " = @BIRTHPLACE, "
                            + SQLbank.DATE_OF_BIRTH + " = @DATEOFBIRTH , "
                            + SQLbank.CIVIL_STATUS + "= @CIVILSTATUS , "
                            + SQLbank.SEX + " = @SEX , "
                            + SQLbank.PLANTILLA_NO+" = @PLANTILLA, "
                            + SQLbank.ACCOUNT_NO + "= @ACCOUNTNO , "
                            + SQLbank.HDMF_NO + " = @HDMFNO , "
                            + SQLbank.PHIC_NO + " = @PHICNO , "
                            + SQLbank.BP_NO + " = @BPNO , "
                            + SQLbank.TIN_NO + "= @TINNO WHERE "
                            + SQLbank.EMP_ID + "=" + emp_id;

                        cmd = new SqlCommand(updateQry, conn);

                        cmd.Parameters.AddWithValue("@LASTNAME",txtLname.Text.Trim());
                        cmd.Parameters.AddWithValue("@MIDDLENAME", txtMname.Text.Trim());
                        cmd.Parameters.AddWithValue("@FIRSTNAME", txtFname.Text.Trim());
                        cmd.Parameters.AddWithValue("@BIRTHPLACE", txtBirthPlace.Text.Trim());
                        cmd.Parameters.AddWithValue("@DATEOFBIRTH", txtDateOfBirth.Text.Trim());
                        cmd.Parameters.AddWithValue("@CIVILSTATUS", cmbCivilStatus.Text.Trim());
                        cmd.Parameters.AddWithValue("@SEX", cmbGender.Text.Trim());
                        cmd.Parameters.AddWithValue("@PLANTILLA", txtPlantillaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@ACCOUNTNO", txtAccNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@HDMFNO", txthdmf.Text.Trim());
                        cmd.Parameters.AddWithValue("@PHICNO", txtphic.Text.Trim());
                        cmd.Parameters.AddWithValue("@BPNO", txtBp.Text.Trim());
                        cmd.Parameters.AddWithValue("@TINNO", txtTin.Text.Trim());



                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Saving changes successful", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TempHolder.mainForm.editMode(false);

                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine("Saving Exception: " + ee.Message);

                        DialogResult dr = MessageBox.Show("Problem occured while trying to save", "Oops", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (dr == DialogResult.Retry) btnSaveChanges.PerformClick();

                    }
                }
            }
          
        }

        private bool isFieldsValid()
        {
            //validate mainform first

            if (txtLname.Text.Length == 0)
            {
                MessageBox.Show("First name must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLname.Select();
                return false;
            }
            if (txtLname.Text.Length == 0)
            {
                MessageBox.Show("Last name must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLname.Select();
                return false;
            }
            if (txtDateOfBirth.Text.Length == 0)
            {
                MessageBox.Show("Date of Birth must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDateOfBirth.Select();
                return false;
            }
            DateTime dt;
            if(DateTime.TryParse(txtDateOfBirth.Text,out dt) && txtDateOfBirth.Text.Length==10)
            {
                //validate age in birthday
                if (calculateAge(dt) < 18)
                {
                    MessageBox.Show("Age must be at least 18 yrs old", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDateOfBirth.Select();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Invalid date value for Date of Birth", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDateOfBirth.Select();
                return false;
            }

            if (txtAge.Text.Trim().Length == 0) return false;
           
            if(cmbCivilStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Civil status must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbCivilStatus.DroppedDown = true;
                return false;
            }

            if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Gender must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbGender.DroppedDown = true;
                return false;
            }
            
            if(txtPlantillaNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("Plantilla number must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPlantillaNo.Select();
                return false;
            }

            if(txtAccNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("Account number must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAccNo.Select();
                return false;
            }

            if(txthdmf.Text.Trim().Length == 0)
            {
                MessageBox.Show("HDMF number must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txthdmf.Select();
                return false;
            }
            
            if(txtphic.Text.Trim().Length == 0)
            {
                MessageBox.Show("PHIC number must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtphic.Select();
                return false;
            }

            if(txtBp.Text.Trim().Length == 0)
            {
                MessageBox.Show("BP number must not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBp.Select();
                return false;
            }

            if(txtTin.Text.Length == 0)
            {
                MessageBox.Show("TIN number not be empty", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTin.Select();
                return false;
            }

            return true;
        }

        private int calculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
        //****************************************************************************

        private void btnRemoveEmpAcc_Click(object sender, EventArgs e)
        {
            if (Permissions.authorizedToUseFunction(Permissions.DELETE_EMPLOYEE_PERMISSION))
            {
                DialogResult dr = MessageBox.Show("You're about to permanently delete employee's account. This cannot be undone, Continue?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    //proceed to deleting employee's account
                    try
                    {
                        openSQLConnection();
                        string delSQL = "DELETE FROM " + SQLbank.TBL_EMPLOYEES + " WHERE " + SQLbank.EMP_ID + " = " + emp_id;
                        cmd = new SqlCommand(delSQL, conn);
                        cmd.ExecuteNonQuery();

                        if (TempHolder.searchedPictureFilename != null)
                        {
                            if (File.Exists(TempHolder.searchedPictureFilename))
                            {
                                File.Delete(TempHolder.searchedPictureFilename);
                            }
                        }
                        MessageBox.Show("Employee Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TempHolder.mainForm.clearDisplay();

                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine("Deleting Error: " + ee.Message);
                        MessageBox.Show("Failed to delete Employee's Account", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtDateOfBirth_TextChanged(object sender, EventArgs e)
        {
            DateTime dt;
            if(DateTime.TryParse(txtDateOfBirth.Text,out dt) && txtDateOfBirth.Text.Length==10)
            {
                txtAge.Text = calculateAge(dt)+"";
                TempHolder.searchedBirthday = txtDateOfBirth.Text;
            }
            else
            {
                txtAge.ResetText();
            }
        }

        private bool dateTimeTextKeyDownBoxChecker(MetroTextBox textbox, char keyChar, Keys keycode)
        {
            bool cancelEVent = true;

            if (Char.IsNumber(keyChar) || Char.IsControl(keyChar) || (keycode >= Keys.NumPad0 && keycode <= Keys.NumPad9))
            {
                Console.WriteLine("Key Not Blocked");
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
                          
                            //check if valid
                            DateTime dt;
                            if (!DateTime.TryParse(textbox.Text.ToString(), out dt))
                            {
                                MessageBox.Show("Invalid Date Value");
                                txtAge.ResetText();
                            }
                            break;
                        default:
                            txtAge.ResetText();
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

        private void txtDateOfBirth_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;
            e.SuppressKeyPress = dateTimeTextKeyDownBoxChecker(txtDateOfBirth, keyChar,e.KeyCode);           
        }

        private void txtBirthPlace_TextChanged(object sender, EventArgs e)
        {
            if (txtBirthPlace.Text.Length > 0) TempHolder.searchedBirthPlace = txtBirthPlace.Text;

        }
    }
}
