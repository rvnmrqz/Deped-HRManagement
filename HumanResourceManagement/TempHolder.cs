
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResourceManagement
{
    class TempHolder
    {
        public static CreateNewEmployeeAccForm ceaf;
        public static SQLSettingsForm sqlSettingsForm;
        public static Form1 loginform;
        public static MainForm mainForm;

        public static string picturePath = Application.StartupPath + "\\EmployeePictures\\";

        //searching
        public static string searchedEmpID = "";
        public static string searchedSheetName = "";

        public static string searchedName;
        public static string searchedLastSchool;
        public static string searchedOriginalAppointment;
        public static string searchedBirthday;
        public static string searchedBirthPlace;

        public static string lastServiceRecordId;
        public static bool lastIsPresent;
        public static string lastFrom;
        public static string lastTo;
        public static string lastDesignation;
        public static string lastStatus;
        public static string lastSalary;
        public static string lastStation;
        public static string lastBranch;
        public static string lastCause;
        public static string lastLAWOP;


        public static void clearSearchTempValues()
        {
            searchedEmpID = null;
            searchedSheetName = null;
            searchedName = null;
            searchedBirthPlace = null;
            clearLastRecord();
        }

        public static void clearLastRecord()
        {
            Console.WriteLine("ClearLastRecord");
            lastServiceRecordId = null;
            lastIsPresent = false;
            lastFrom = null;
            lastTo = null;
            searchedOriginalAppointment = null;
            searchedLastSchool = null;
            lastDesignation = null;
            lastStatus = null;
            lastSalary = null;
            lastStation = null;
            lastBranch = null;
            lastCause = null;
            lastLAWOP = null;
        }

        //logged user
        public static string loggedUser_ID,username,password, fname,mname,lname,accountType,pictureFilename;
        public static UserControlPersonalInfo uc_PersonalInfo;
        public static UserControlServiceRecord uc_ServiceRecord;


        //SYSTEM VALUES
        public static bool systemValuesLoaded = false;
        public static List<string> civil_status_list = new List<string>();
        public static List<string> salary_grade_list = new List<string>();
        public static List<string> steps_list = new List<string>();
        public static List<string> status_list = new List<string>();
        public static List<string> cause_list = new List<string>();
        public static List<string> school_name_list = new List<string>();


  

    }

}
