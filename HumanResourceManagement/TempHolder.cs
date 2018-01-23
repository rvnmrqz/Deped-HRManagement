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
        public static List<string> school_name_list = new List<string>();


  

    }

}
