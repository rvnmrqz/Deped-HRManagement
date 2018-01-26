using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.InteropServices;

namespace HumanResourceManagement
{
    public partial class LoadingForm : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(getStringValue("sqlconstring"));
            //run background worker
            systemLoader.RunWorkerAsync();
        }

        //************************SERVER CONNECTION****************************
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
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return config.AppSettings.Settings[key].Value.ToString();

        }

        //***********************SYSTEM VALUE LOADER****************************
        private void systemLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("background worker started");
            try
            {
                //load permissions
                loadPermissions();

                //load salary grade
                loadSalaryGradeAndSteps();

                //load civil status
                loadCivilStatus();

                //load status in service record
                loadServiceStatus();

                //load cause
                loadCause();

            }
            catch (Exception ee)
            {
                Console.WriteLine("bgw_loadSystemValues Exception " + ee.Message);
            }
        }

        private void systemLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
         
            TempHolder.systemValuesLoaded = true;
            this.Close();
            MainForm main = new MainForm();
            main.Show(); 
        }

        //************************************************************************
        private void loadPermissions()
        {
            try
            {
                Permissions.clear();
               
                openSQLConnection();
                string getQry = "SELECT * FROM " + SQLbank.TBL_SYS_PERMISSIONS;
                cmd = new SqlCommand(getQry, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    string sqlSettings = reader[SQLbank.SQL_SETTINGS].ToString().Trim();
                    string excelExtractor = reader[SQLbank.EXCEL_EXTRACTOR].ToString().Trim();
                    string sqlBackup = reader[SQLbank.SQL_BACKUP].ToString().Trim();

                    string mod_employee = reader[SQLbank.MOD_EMPLOYEE].ToString().Trim();
                    string add_employee = reader[SQLbank.ADD_EMPLOYEE].ToString().Trim();
                    string del_employee = reader[SQLbank.DEL_EMPLOYEE].ToString().Trim();

                    string mod_sys_user = reader[SQLbank.MOD_SYS_USER].ToString().Trim();
                    string add_sys_user = reader[SQLbank.ADD_SYS_USER].ToString().Trim();
                    string del_sys_user = reader[SQLbank.DEL_SYS_USER].ToString().Trim();

                    string mod_serv_rec = reader[SQLbank.MOD_SERVICE_REC].ToString().Trim();
                    string add_serv_rec = reader[SQLbank.ADD_SERVICE_REC].ToString().Trim();
                    string del_serv_rec = reader[SQLbank.DEL_SERVICE_REC].ToString().Trim();

                    if (sqlSettings.Length > 0) Permissions.SQL_SETTINGS_PERMISSION.Add(sqlSettings);
                    if (excelExtractor.Length > 0) Permissions.EXCEL_EXTRACTOR_PERMISSION.Add(excelExtractor);
                    if (sqlBackup.Length > 0) Permissions.SQL_BACKUP_PERMISSION.Add(sqlBackup);

                    if (mod_employee.Length > 0) Permissions.MODIFY_EMPLOYEE_INFO_PERMISSION.Add(mod_employee);
                    if (add_employee.Length > 0) Permissions.ADD_EMPLOYEE_PERMISSION.Add(add_employee);
                    if (del_employee.Length > 0) Permissions.DELETE_EMPLOYEE_PERMISSION.Add(del_employee);
                 
                    if (mod_sys_user.Length > 0) Permissions.MODIFY_SYS_USER_PERMISSION.Add(mod_sys_user);
                    if (add_sys_user.Length > 0) Permissions.ADD_SYS_USER_PERMISSION.Add(add_sys_user);
                    if (del_sys_user.Length > 0) Permissions.DELETE_SYS_USER_PERMISSION.Add(del_sys_user);

                    if (mod_serv_rec.Length > 0) Permissions.MODIFY_SERVICE_RECORD.Add(mod_serv_rec);
                    if (add_serv_rec.Length > 0) Permissions.ADD_SERVICE_RECORD.Add(add_serv_rec);
                    if (del_serv_rec.Length > 0) Permissions.DELETE_SERVICE_RECORD.Add(del_serv_rec);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("Failed to load Permissions: " + ee.Message);
            }
        }

        private void loadSalaryGradeAndSteps()
        {
            
            try
            {

                TempHolder.salary_grade_list.Clear();
                TempHolder.steps_list.Clear();

                openSQLConnection();
                int SGstart, SGend = 0;
                int Sstart = 0, Send = 0;

                cmd = new SqlCommand("SELECT * FROM " + SQLbank.SYS_VALUES + ";", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string salaryGradeStart = reader[SQLbank.SYS_SALARY_GRADE_START].ToString();
                    string salaryGradeEnd = reader[SQLbank.SYS_SALARY_GRADE_END].ToString();
                    string stepStart = reader[SQLbank.SYS_STEP_START].ToString();
                    string stepEnd = reader[SQLbank.SYS_STEP_END].ToString();

                    if (Int32.TryParse(salaryGradeStart, out SGstart) && Int32.TryParse(salaryGradeEnd, out SGend))
                    {
                        for (int i = SGstart; i < SGend; i++)
                        {
                            TempHolder.salary_grade_list.Add(i.ToString());
                        }
                        Console.WriteLine("Salary Grade Loaded");
                    }

                    if (Int32.TryParse(stepStart, out Sstart) && Int32.TryParse(stepEnd, out Send))
                    {
                        for (int i = SGstart; i < SGend; i++)
                        {
                            TempHolder.steps_list.Add(i.ToString());
                        }
                        Console.WriteLine("Step Grade Loaded");
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("bgw_loadSystemValues Exception " + ee.Message);
            }
        }

        private void loadCivilStatus()
        {
            try
            {
                TempHolder.civil_status_list.Clear();

                openSQLConnection();
                string qry = "SELECT "+SQLbank.SYS_CIVIL_STATUS+" FROM " + SQLbank.SYS_VALUES+";";
                cmd = new SqlCommand(qry, conn);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string cs = reader[SQLbank.SYS_CIVIL_STATUS].ToString();
                    if (cs.Length != 0) TempHolder.civil_status_list.Add(cs);
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine("Faield to load Civil Status, Exception: " + ee.Message);
            }
        }

        private void loadServiceStatus()
        {
            try
            {
                TempHolder.status_list.Clear();

                openSQLConnection();
                string qry = "SELECT "+SQLbank.SYS_STATUS+" FROM " + SQLbank.SYS_VALUES;
                cmd = new SqlCommand(qry, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string stat = reader[SQLbank.SYS_STATUS].ToString();
                    if (stat.Length != 0) TempHolder.status_list.Add(stat);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("Faield to load service status"+ ee.Message);
            }
        }

        private void loadCause()
        {
            try
            {
                TempHolder.cause_list.Clear();

                openSQLConnection();
                string qry = "SELECT " + SQLbank.SYS_CAUSE + " FROM " + SQLbank.SYS_VALUES;
                cmd = new SqlCommand(qry, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string cause = reader[SQLbank.SYS_CAUSE].ToString();
                    if (cause.Length != 0) TempHolder.cause_list.Add(cause);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("Faield to load service status" + ee.Message);
            }
        }
      
    }
}
