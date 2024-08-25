using DVLD.Global_Classes;
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

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode {AddNew=0,Update=1 };
        private enMode _Mode=enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule=0,RetakeTestSchedule=1};
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.PsychologicalTest;
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;
                switch(_TestTypeID)
                {
                    case clsTestType.enTestType.PsychologicalTest:
                        gbTestType.Text = "Psychological Test";
                        pbTestType.Image = Resources.psychology_512;
                        break;
                    case clsTestType.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestType.Image = Resources.Vision_512;
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestType.Image = Resources.Written_512;
                        break;
                    case clsTestType.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestType.Image = Resources.Street_512;
                        break;

                }
            }
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int TestAppointmentID=-1)
        {
            if (TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _TestAppointmentID = TestAppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID
                (_LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("Error: No Local Driving License Application  With ID : " + _LocalDrivingLicenseApplicationID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpTestDate.Enabled=btnSave.Enabled = false;
                return;
            }
            if (_LocalDrivingLicenseApplication.DeosAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.FirstTimeSchedule)
            {
                lblTitle.Text = "Schedule Test";
                gbRetakeTestInfo.Enabled = false;
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = "0";
            }
            else
            {
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
                lblRetakeTestAppID.Text = "0";
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
            }
            lblLocalDrivingKicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicantPersonInfo.FullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.GetTotalTrialsPerTest(_TestTypeID).ToString();

            if(_Mode==enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
                lblRetakeTestAppID.Text = "N/A";
                dtpTestDate.MinDate = DateTime.Now;
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }
            
            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
                return;
            if (!_HandleAppointmentLockedConstraint())
                return;
            if (!_HandlePerviousTestConstraint())
                return;

        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {

                MessageBox.Show("Error: No Appointment  With ID : " + _TestAppointmentID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpTestDate.Enabled=btnSave.Enabled = false;
                return false;
            }
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            dtpTestDate.Value=_TestAppointment.AppointmentDate;
            lblFees.Text = _TestAppointment.PaidFees.ToString();


            if(_TestAppointment.RetakeTestApplicationID==-1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
                gbRetakeTestInfo.Enabled = false;
            }
            else
            {
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
                lblTitle.Text = "Schedule Retake Fees";
                gbRetakeTestInfo.Enabled = true;
            }


            return true;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode==enMode.AddNew&& clsLocalDrivingLicenseApplication.IsThereAnActiveScheduleTest(_LocalDrivingLicenseApplicationID,_TestTypeID))
            {
                btnSave.Enabled = false;
                gbRetakeTestInfo.Enabled = false;
                dtpTestDate.Enabled = false;
                lblUserMessage.Text = "Person already have an active appointment for this test ";
                return false;
            }
            return true;
        }


        private bool _HandleAppointmentLockedConstraint()
        {
            if(_TestAppointment.IsLocked)
            {
                lblUserMessage.Text = "Person already sat for the test ,Appointment Locked";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandlePerviousTestConstraint()
        {
            switch(_TestTypeID)
            {
                case clsTestType.enTestType.PsychologicalTest:
                    lblUserMessage.Visible = false;
                    return true;
                case clsTestType.enTestType.VisionTest:
                    bool DoesPassedPsychologicalTest = _LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.PsychologicalTest);
                    lblUserMessage.Visible = !DoesPassedPsychologicalTest;
                    if (lblUserMessage.Visible)
                        lblUserMessage.Text = "Cannot schedule ,Psychological test should be passed first";
                    btnSave.Enabled = DoesPassedPsychologicalTest;
                    dtpTestDate.Enabled = DoesPassedPsychologicalTest;
                    return DoesPassedPsychologicalTest;
                case clsTestType.enTestType.WrittenTest:

                    bool DoesPassedVisionTest = _LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.VisionTest);
                    lblUserMessage.Visible = !DoesPassedVisionTest;
                    if(lblUserMessage.Visible)
                        lblUserMessage.Text = "Cannot schedule ,vision test should be passed first";
                    btnSave.Enabled = DoesPassedVisionTest;
                    dtpTestDate.Enabled = DoesPassedVisionTest;
                    return DoesPassedVisionTest;

                case clsTestType.enTestType.StreetTest:
                    bool DoesPassedWrittenTest = _LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.WrittenTest);
                    lblUserMessage.Visible = !DoesPassedWrittenTest;
                    if (lblUserMessage.Visible)
                        lblUserMessage.Text = "Cannot schedule ,Written test should be passed first";
                    btnSave.Enabled = DoesPassedWrittenTest;
                    dtpTestDate.Enabled = DoesPassedWrittenTest;
                    return DoesPassedWrittenTest;

            }
            return false;
        }

        private bool _HandleRetakeApplication()
        {
            if(_Mode==enMode.AddNew && _CreationMode==enCreationMode.RetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();
                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.ApplicationDate = DateTime.Now;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;

                if(!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild Creation Application",
                    "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }
            return true;
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID; ;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.TestTypeID = _TestTypeID;
            if(_TestAppointment.Save())
            {
                MessageBox.Show(" Saved Data Successfly ",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                
            }
            else
            
                MessageBox.Show("Error: Data Is not Saved  Successfly",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
        }
    }
}
