using DVLD.Properties;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
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

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {


        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;

        private int _TestID = -1;

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }
        public int TestID
        {
            get
            {
                return _TestID;
            }
        }



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
                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.PsychologicalTest:
                        pbTestType.Image = Resources.psychology_512;
                        gbTestType.Text = "Psychological Test";
                        break;
                    case clsTestType.enTestType.VisionTest:
                        pbTestType.Image = Resources.Vision_512;
                        gbTestType.Text = "Vision Test";
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        pbTestType.Image = Resources.Written_512;
                        gbTestType.Text = "Written Test";
                        break;
                    case clsTestType.enTestType.StreetTest:
                        pbTestType.Image = Resources.Street_512;
                        gbTestType.Text = "Street Test";
                        break;

                }
            }
        }

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

       
        public void LoadInfo(int TestAppointmentID)
        {
           
            _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {

                MessageBox.Show("Error: No Appointment  With ID : " + _TestAppointmentID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application  With ID : " + _LocalDrivingLicenseApplicationID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            lblLocalDrivingKicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicantPersonInfo.FullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.GetTotalTrialsPerTest(_TestTypeID).ToString();
            lblTestDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestID == -1) ? "Not Taken Yet" : _TestID.ToString();
        }
        
    
    }
}
