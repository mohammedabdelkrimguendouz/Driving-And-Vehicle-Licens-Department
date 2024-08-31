using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Buisness.clsTestType;

namespace DVLD.Tests.VisionTest
{
    public partial class frmListTestAppointments : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.PsychologicalTest;
       

        private static DataTable _dtAllTestAppointments;

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.PsychologicalTest:
                    lblTitle.Text = this.Text = "Psychological Test Appointments";
                    pbTestType.Image = Resources.psychology_512;
                    break;
                case clsTestType.enTestType.VisionTest:
                    lblTitle.Text = this.Text = "Vision Test Appointments";
                    pbTestType.Image = Resources.Vision_512;
                    break;
                case clsTestType.enTestType.WrittenTest:
                    lblTitle.Text = this.Text = "Written Test Appointments";
                    pbTestType.Image = Resources.Written_512;
                    break;
                case clsTestType.enTestType.StreetTest:
                    this.Text = lblTitle.Text = "Street Test Appointments";
                    pbTestType.Image = Resources.Street_512;
                    break;

            }
        }
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
             _LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (LocalDrivingLicenseApplication.IsThereAnActiveScheduleTest
                (_TestTypeID))
            {
                MessageBox.Show("Person already have an active appointment for this test ,you  cannot add new appointment",
                    "No Allowed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if(LocalDrivingLicenseApplication.DoesPassedTestType(_TestTypeID))
            {
                MessageBox.Show("Person already Passed this test before , you can only retake faild test ",
                    "No Allowed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null,null);
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicationID
                (_LocalDrivingLicenseApplicationID);
            _dtAllTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType
                (ctrlLocalDrivingLicenseApplicationInfo1.LocalDrivingLicenseApplicationID, _TestTypeID);

            dgvTestAppointments.DataSource = _dtAllTestAppointments;

            lblRecordsCount.Text = dgvTestAppointments.Rows.Count.ToString();
            if (dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                //dgvVisionTestAppointments.Columns["TestAppointmentID"].Width = 80;

                dgvTestAppointments.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                //dgvVisionTestAppointments.Columns["AppointmentDate"].Width = 80;



                dgvTestAppointments.Columns["PaidFees"].HeaderText = "Paid Fees";
                //dgvVisionTestAppointments.Columns["PaidFees"].Width = 120;

                dgvTestAppointments.Columns["IsLocked"].HeaderText = "Is Locked";
                //dgvVisionTestAppointments.Columns["IsLocked"].Width = 80;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value;
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID,TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null,null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value;
            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestTypeID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }
    }
}
