using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagement
{
    class SQLbank
    {
        public static string TBL_USERS = "tbl_users";
        public static string ID = "id"; // primary key
        public static string USERNAME = "username";
        public static string PASSWORD = "password";
        public static string EMAIL = "email";
        public static string ROLE = "role";
        public static string FNAME = "fname";
        public static string MNAME = "mname";
        public static string LNAME = "lname";

        public static string TBL_EMPLOYEES = "tbl_employees";
        public static string EMP_ID = "emp_id"; //primary key
        public static string EMPLOYEE_NO = "employee_no";
        public static string EMP_LAST_NAME = "last_name";
        public static string EMP_MIDDLE_NAME = "middle_name";
        public static string EMP_FIRST_NAME = "first_name";
        public static string PLANTILLA_NO = "plantilla_no";
        public static string HDMF_NO = "hdmf_no";
        public static string BP_NO = "bp_no";
        public static string ACCOUNT_NO = "account_no";
        public static string TIN_NO = "tin_no";
        public static string PHIC_NO = "phic_no";
        public static string POSITION_TITLE = "position_title";
        public static string SALARY_GRADE = "salary_grade";
        public static string SEX = "sex";
        public static string DATE_OF_BIRTH = "date_of_birth";
        public static string CIVIL_STATUS = "civil_status";
        public static string SCHOOL_NAME = "school_name";
        public static string STEP = "step";
        public static string CLASSIFICATION = "classification";
        public static string ELIGIBILITY = "eligibility";
        public static string STATUS = "status";
        public static string DATE_OF_ORIGINAL_APPOINTMENT = "date_of_original_appointment";
        public static string DATE_OF_LAST_PROMOTION = "date_of_last_promotion";
        public static string DATE_OF_LAST_CONTINUOUS_APPOINTMENT = "date_of_last_continuos_appointment";
        public static string START_DATE = "start_date";
        public static string END_DATE = "end_date";
        public static string REMARKS = "remarks";
        public static string PICTUREFILENAME = "picture_filename";


        //SERVICE RECORDS TABLE
        public static string TBL_SERVICE_RECORDS = "tbl_service_records";
        //public static string ID = "id"; 
        //public static string SCHOOL_NAME = "school_name";
        public static string DESIGNATION = "designation";
        // public static string STATUS = "status";
        public static string FROM_DATE = "from_date";
        public static string TO_DATE = "to_date";
        public static string STATION = "station";
        public static string BRANCH = "branch";
        public static string CAUSE = "cause";
        public static string SALARY = "salary";
        public static string LAWOP = "lawop";

        //SYSTEM TABLES******************************************************

        //PERMISSIONS
        public static string TBL_SYS_PERMISSIONS = "sys_permissions";
        public static string SQL_SETTINGS = "sql_settings";
        public static string EXCEL_EXTRACTOR = "excel_extractor";
        public static string SQL_BACKUP = "sql_backup";

        public static string MOD_EMPLOYEE = "modify_employee";
        public static string ADD_EMPLOYEE = "add_employee";
        public static string DEL_EMPLOYEE = "delete_employee";

        public static string MOD_SYS_USER = "modify_system_user";
        public static string ADD_SYS_USER = "add_system_user";
        public static string DEL_SYS_USER = "delete_system_user";

        public static string MOD_SERVICE_REC = "modify_service_record";
        public static string ADD_SERVICE_REC = "add_service_record";
        public static string DEL_SERVICE_REC = "delete_service_record";
        //********************************************************************


        public static string TBL_CIVIL_STATUS = "sys_civil_status";
        public static string CIVIL_DESCRIPTION = "civil_description";

        //SALARY GRADES
        public static string TBL_SALARY_GRADES = "sys_salary_grade_and_steps";
        public static string DESCRIPTION = "description";
        public static string VALUE_START = "value_start";
        public static string VALUE_END = "value_end";

        //SERVICE STATUS 
        public static string TBL_STATUS = "sys_status";
        public static string STATUS_DESC = "status_desc";

       

    }
}
