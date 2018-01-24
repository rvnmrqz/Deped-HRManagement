using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HumanResourceManagement
{
    public partial class AddServiceRecordDialog : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public AddServiceRecordDialog()
        {
            InitializeComponent();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void top_panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void AddServiceRecordDialog_Load(object sender, EventArgs e)
        {
            //prepare display
            txtSchoolName.Text = TempHolder.searchedLastSchool;
            txtPosition.Text = TempHolder.searchedLastDesignation;
            cmbStatus.Text = TempHolder.searchedLastStatus;
            txtSalary.Text = TempHolder.searchedLastSalary;
            txtStation.Text = TempHolder.searchedLastStation;
            txtBranch.Text = TempHolder.searchedLastBranch;
            txtCause.Text = TempHolder.searchedLastCause;
            txtLAWOP.Text = TempHolder.searchedLastLawop;

            
        }

        private void chkSchool_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSchool.Checked)
            {
                txtSchoolName.Enabled = true;
                txtSchoolName.Select();
            }
            else {
                txtSchoolName.Enabled = false;
                txtSchoolName.Text = TempHolder.searchedLastSchool;
            }
        }

        private void chkDesignation_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDesignation.Checked)
            {
                txtPosition.Enabled = true;
                txtPosition.Select();
            }
            else {
                txtPosition.Enabled = false;
                txtPosition.Text = TempHolder.searchedLastDesignation;
            }
        }

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkStatus.Checked)
            {
                cmbStatus.Enabled = true;
                cmbStatus.Select();
            }
            else {
                cmbStatus.Enabled = false;
                cmbStatus.Text = TempHolder.searchedLastStatus;
            }
        }

        private void chkStation_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkStation.Checked)
            {
                txtStation.Enabled = true;
                txtStation.Select();
            }
            else {
                txtStation.Enabled = false;
                txtStation.Text = TempHolder.searchedLastStation;
            }
        }

        private void chkBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkBranch.Checked)
            {
                txtBranch.Enabled = true;
                txtBranch.Select();
            }
            else {
                txtBranch.Enabled = false;
                txtBranch.Text = TempHolder.searchedLastBranch;
            }
        }

        private void chkSalary_CheckedChanged(object sender, EventArgse e)
        {
            if (!chkSalary.Checked)
            {
                txtSalary.Enabled = true;
                txtSalary.Select();
            }
            else {
                txtSalary.Enabled = false;
                txtSalary.Text = TempHolder.searchedLastSalary;
            }

        }

        private void chkLawop_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkLawop.Checked)
            {
                txtLAWOP.Enabled = true;
                txtLAWOP.Select();
            }
            else {
                txtLAWOP.Enabled = false;
                txtLAWOP.Text = TempHolder.searchedLastSalary;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {

        }
    }
}
