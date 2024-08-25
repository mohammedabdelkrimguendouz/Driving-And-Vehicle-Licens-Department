using DVLD.Licenses.Local_Licenses;
using DVLD.People;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.Local_Driving_License.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID=-1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _LicenseID = -1;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        public void ResetLocalDrivingLicenseApplicationInfo()
        {
            ctrlApplicationBasicInfo1.ResetApplicationBasicInfo();
            lblLicenseType.Text = "[????]";
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblPassedTests.Text = "[????]";
            llShowLicenseInfo.Enabled = false;
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = clsLicense.GetActiveLicenseIDByPersonID(_LocalDrivingLicenseApplication.ApplicantPersonID,_LocalDrivingLicenseApplication.LicenseClassID);
            llShowLicenseInfo.Enabled = (_LicenseID != -1);
            ctrlApplicationBasicInfo1.LoadApplicationBasicInfo(_LocalDrivingLicenseApplication.ApplicationID);
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            lblLicenseType.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestCount()+"/4";

        }
        public void LoadApplicationInfoByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Local Driving License Application Info With ID = " + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();

        }
        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No  Application Info With ID = " + ApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(_LicenseID);
            frm.ShowDialog();

        }
    }
}
