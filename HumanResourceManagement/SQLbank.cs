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
        public static string SEX = "sex";
        public static string BIRTH_PLACE = "birth_place";
        public static string DATE_OF_BIRTH = "date_of_birth";
        public static string CIVIL_STATUS = "civil_status";
        public static string SCHOOL_NAME = "school_name";
        public static string CLASSIFICATION = "classification";
        public static string ELIGIBILITY = "eligibility";
        public static string PICTUREFILENAME = "picture_filename";

        //SERVICE RECORDS TABLE
        public static string TBL_SERVICE_RECORDS = "tbl_service_records";
        //public static string ID = "id"; 
        //public static string SCHOOL_NAME = "school_name";
        public static string DESIGNATION = "designation";
        public static string STATUS = "status";
        public static string FROM_DATE = "from_date";
        public static string TO_DATE = "to_date";
        public static string STATION = "station";
        public static string BRANCH = "branch";
        public static string CAUSE = "cause";
        public static string SALARY = "salary";
        public static string LAWOP = "lawop";


        //SCHOOLS
        public static string TBL_SCHOOLS = "tbl_schools";
        public static string SCHOOL_ID = "school_id";
        //public static string SCHOOL_NAME = "school_name";
        public static string DISTRICT = "district";

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

        //SYSTEM VALUES
        public static string SYS_VALUES = "sys_values";
        public static string SYS_CIVIL_STATUS = "civil_status";
        public static string SYS_STATUS = "status";
        public static string SYS_SALARY_GRADE_START = "salary_grade_start";
        public static string SYS_SALARY_GRADE_END = "salary_grade_end";
        public static string SYS_STEP_START = "step_start";
        public static string SYS_STEP_END = "step_end";
        public static string SYS_CAUSE = "cause";

        //EXCEL VALUES
        public static string SYS_EXCEL = "sys_excel";
        public static string OFFICER_NAME = "officer_name";
        public static string OFFICER_POSITION = "officer_position";
       
    }
}
