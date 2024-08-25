using DVLD.Applications.Local_Driving_License;
using DVLD.Applications.Local_Driving_License_Applications;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests.VisionTest;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        private static DataTable _dtAllLocalDrivingLicenseApplications;
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;
            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns["LocalDrivingLicenseApplicationID"].Width = 60;

                dgvLocalDrivingLicenseApplications.Columns["ClassName"].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns["ClassName"].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns["NationalNo"].HeaderText = "National No";
                dgvLocalDrivingLicenseApplications.Columns["NationalNo"].Width = 80;

                dgvLocalDrivingLicenseApplications.Columns["FullName"].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns["FullName"].Width = 140;

                dgvLocalDrivingLicenseApplications.Columns["ApplicationDate"].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns["ApplicationDate"].Width = 70;

                dgvLocalDrivingLicenseApplications.Columns["PassedTestCount"].HeaderText = "Passed Test";
                dgvLocalDrivingLicenseApplications.Columns["PassedTestCount"].Width = 50;

                dgvLocalDrivingLicenseApplications.Columns["Status"].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns["Status"].Width = 80;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "L.D.L.AppID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbStatus.Visible = (cbFilter.Text == "Status");
            if (cbStatus.Visible)
                cbStatus.SelectedIndex = 0;

            txtFilterValue.Visible = (cbFilter.Text != "Status") && (cbFilter.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "")
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            else if (FilterColumn == "LocalDrivingLicenseApplicationID")
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterIColumn = "Status";
            string FilterValue = cbStatus.Text.Trim();
            
            if (cbStatus.Text.Trim() == "All")
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}]='{1}'", FilterIColumn, FilterValue);
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void btnAddNewLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure do you want to delete this application", "Confirm Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;
            
                int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
                clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                    FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                if(LocalDrivingLicenseApplication!=null)
                {
                    if (LocalDrivingLicenseApplication.Delete())
                    {
                        MessageBox.Show("Application Deleted Successfully", "Successful", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        frmListLocalDrivingLicenseApplications_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Could not delete applicatoin, other data depends on it", "Error ", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
              
                
            
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure do you want to Cancel this application", "Confirm Cancel", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;
            
                int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
                clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                    FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                if (LocalDrivingLicenseApplication != null)
                {
                    if (LocalDrivingLicenseApplication.Cancel())
                    {
                        MessageBox.Show("Application Cancelled Successfully", "Successful", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        frmListLocalDrivingLicenseApplications_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Could not cancel applicatoin", "Error Cancel", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
                

            
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLocalDrivingLicenseApplicationInfo frm = new frmShowLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

       

        

        

        private void _SechduleTest(clsTestType.enTestType TestTypeID)
        {
            int LocalDrivingcenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingcenseApplicationID, TestTypeID);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }
        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingcenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LocalDrivingcenseApplicationID);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingcenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingcenseApplicationID).GetActiveLicenseID();
            if (LicenseID != -1)
            {
                frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingcenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingcenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingcenseApplicationID);
            if (LocalDrivingcenseApplication != null)
            {
                clsApplication.enApplicationStatus ApplicationStatus = LocalDrivingcenseApplication.ApplicationStatus;
                bool IsLicenseExist = LocalDrivingcenseApplication.IsLicenseIssued();
                bool IsPassAllTest = LocalDrivingcenseApplication.IsPassedAllTests();

                bool DoesPassPsychologicalTest = LocalDrivingcenseApplication.DoesPassedTestType(clsTestType.enTestType.PsychologicalTest);
                bool DoesPassVisionTest = LocalDrivingcenseApplication.DoesPassedTestType(clsTestType.enTestType.VisionTest);
                bool DoesPassWrittenTest = LocalDrivingcenseApplication.DoesPassedTestType(clsTestType.enTestType.WrittenTest);
                bool DoesPassStreetTest = LocalDrivingcenseApplication.DoesPassedTestType(clsTestType.enTestType.StreetTest);

                editApplicationToolStripMenuItem.Enabled = (ApplicationStatus == clsApplication.enApplicationStatus.New);
                deleteApplicationToolStripMenuItem.Enabled = (ApplicationStatus == clsApplication.enApplicationStatus.New);
                cancelApplicationToolStripMenuItem.Enabled= (ApplicationStatus == clsApplication.enApplicationStatus.New);


                issueDrivingLicenseToolStripMenuItem.Enabled = IsPassAllTest
                    && !IsLicenseExist;

                showLicenseToolStripMenuItem.Enabled = IsLicenseExist && (ApplicationStatus==clsApplication.enApplicationStatus.Completed );


                

                sechduleTestsToolStripMenuItem.Enabled = (!DoesPassStreetTest || !DoesPassVisionTest || !DoesPassWrittenTest||!DoesPassPsychologicalTest) && (ApplicationStatus == clsApplication.enApplicationStatus.New);

                if (sechduleTestsToolStripMenuItem.Enabled)
                {
                    sechdulePsychologicalTestToolStripMenuItem.Enabled = !DoesPassPsychologicalTest;
                    sechduleVisionTestToolStripMenuItem.Enabled = !DoesPassVisionTest&& DoesPassPsychologicalTest;
                    sechduleWrittenTestToolStripMenuItem.Enabled = !DoesPassWrittenTest&&DoesPassVisionTest&& DoesPassPsychologicalTest;
                    sechduleStreetTestToolStripMenuItem.Enabled = !DoesPassStreetTest&&DoesPassWrittenTest&&DoesPassVisionTest&& DoesPassPsychologicalTest;
                }

            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsPerson.Find(NationalNo).PersonID;
            
            
            if (PersonID != -1)
            {
                frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No Person Found!", "No Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sechdulePsychologicalTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SechduleTest(clsTestType.enTestType.PsychologicalTest);
        }

        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SechduleTest(clsTestType.enTestType.VisionTest);
        }

        private void sechduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SechduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SechduleTest(clsTestType.enTestType.StreetTest);
        }
    }
}
