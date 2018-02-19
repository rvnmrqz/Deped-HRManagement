
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
        public static ImportExcelDialog importExcelDialog;

        public static string picturePath = Application.StartupPath + "\\EmployeePictures\\";

        //selectedRow (doubleclicked)
        public static bool editMode = false;
        public static string selectedIndex;
        public static string selectedId;
        public static string selectedSchool;
        public static string selectedFrom;
        public static string selectedTo;
        public static string selectedDesignation;
        public static string selectedStatus;
        public static string selectedSalary;
        public static string selectedStation;
        public static string selectedBranch;
        public static string selectedCause;
        public static string selectedLawop;


        //searching
        public static string searchedEmpID = "";
        public static string searchedPictureFilename;
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


        //excel
        public static Microsoft.Office.Interop.Excel.Application excelApp = null;
        public static string officerName;
        public static string officerPosition;



        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(int handle, out int processId);

        public static void quitExcel()
        {
            try
            {
                Console.WriteLine("Quit excel");
                if (excelApp != null)
                {
                    excelApp.DisplayAlerts = false;
                    excelApp.Quit();
                    // Marshal.ReleaseComObject(excelApp);

                    int proID;

                    GetWindowThreadProcessId(excelApp.Hwnd, out proID);
                    Process[] allProcesses = Process.GetProcessesByName("excel");
                    foreach (Process excelProcess in allProcesses)
                    {
                        if (excelProcess.Id == proID)
                        {
                            excelProcess.Kill();
                            Console.WriteLine("Excel killed");
                        }
                    }
                }


            }
            catch(Exception ee)
            {
                Console.WriteLine("Exception occured while quitting excel: " + ee.Message);
            }
           
        }
        
        public static void clearSearchTempValues()
        {
            searchedEmpID = null;
            searchedPictureFilename = null;
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
        public static List<string> roles_list = new List<string>();
        public static AutoCompleteStringCollection schoolCollection = new AutoCompleteStringCollection();
   

  

    }

}
