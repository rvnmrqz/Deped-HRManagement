using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResourceManagement
{
    class Permissions
    {
        static string unAuthorizedMsg = "You are not authorized to use this function, contact your admin for inquiries";

        //PERMISSIONS
        public static List<string> SQL_SETTINGS_PERMISSION = new List<string>();
        public static List<string> EXCEL_EXTRACTOR_PERMISSION = new List<string>();
        public static List<string> SQL_BACKUP_PERMISSION = new List<string>();

        public static List<string> MODIFY_EMPLOYEE_INFO_PERMISSION = new List<string>();
        public static List<string> ADD_EMPLOYEE_PERMISSION = new List<string>();
        public static List<string> DELETE_EMPLOYEE_PERMISSION = new List<string>();

        public static List<string> MODIFY_SYS_USER_PERMISSION = new List<string>();
        public static List<string> ADD_SYS_USER_PERMISSION = new List<string>();
        public static List<string> DELETE_SYS_USER_PERMISSION = new List<string>();

        public static List<string> MODIFY_SERVICE_RECORD = new List<string>();
        public static List<string> ADD_SERVICE_RECORD = new List<string>();
        public static List<string> DELETE_SERVICE_RECORD = new List<string>();



        public static void clear()
        {
            SQL_SETTINGS_PERMISSION.Clear();
            EXCEL_EXTRACTOR_PERMISSION.Clear();
            SQL_BACKUP_PERMISSION.Clear();

            MODIFY_EMPLOYEE_INFO_PERMISSION.Clear();
            ADD_EMPLOYEE_PERMISSION.Clear();
            DELETE_EMPLOYEE_PERMISSION.Clear();

            MODIFY_SYS_USER_PERMISSION.Clear();
            ADD_SYS_USER_PERMISSION.Clear();
            DELETE_SYS_USER_PERMISSION.Clear();
        }

        public static bool authorizedToUseFunction(List<string> authorization)
        {
            if (authorization.Contains(TempHolder.accountType)) return true;//user is authorized
            else {
                MessageBox.Show(unAuthorizedMsg, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

    }
}
