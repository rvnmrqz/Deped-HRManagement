using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResourceManagement
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

            if (cmbWhat.Items.Count > 0) cmbWhat.SelectedIndex = 0;
        }

        private void cmbWhat_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                0-School
                1-Employee
                2-System User
            */
            cmbfilterby.Items.Clear();

            switch (cmbWhat.SelectedIndex)
            {
                case 0:
                    cmbfilterby.Items.Add("School ID");
                    cmbfilterby.Items.Add("School Name");
                    cmbfilterby.Items.Add("District");
                    break;
                case 1:
                    cmbfilterby.Items.Add("Employee No.");
                    cmbfilterby.Items.Add("First Name");
                    cmbfilterby.Items.Add("Middle Name");
                    cmbfilterby.Items.Add("Last Name");
                    cmbfilterby.Items.Add("Account No.");
                    cmbfilterby.Items.Add("TIN No.");
                    cmbfilterby.Items.Add("HDMF No.");
                    cmbfilterby.Items.Add("PHIC No.");
                    cmbfilterby.Items.Add("BP No.");
                    break;
                case 2:
                    cmbfilterby.Items.Add("Username");
                    cmbfilterby.Items.Add("First Name");
                    cmbfilterby.Items.Add("Middle Name");
                    cmbfilterby.Items.Add("Last Name");
                    break;
            }

            if (cmbfilterby.Items.Count > 0) cmbfilterby.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
