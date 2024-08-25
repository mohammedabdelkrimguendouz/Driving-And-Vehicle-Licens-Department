using DVLD.Global_Classes;
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
using static DVLD_Buisness.clsLocalDrivingLicenseApplication;

namespace DVLD.Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        
        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDrivingLicense_Load(object sender, EventArgs e)
        {


            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No  Application  With ID = " + _LocalDrivingLicenseApplicationID, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (!_LocalDrivingLicenseApplication.IsPassedAllTests())
            {
                MessageBox.Show("Person should pass all tests first ", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            if(LicenseID!=-1)
            {
                MessageBox.Show("Person  Already has license before With license ID = "+LicenseID, "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlDrivingLicenseApplicationInfo1.
                LoadApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

        }

        private void btnIssueDrivingLicense_Click(object sender, EventArgs e)
        {
           

            int LicenseID = _LocalDrivingLicenseApplication.IssuedLicenseForTheFirstTime
                (txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID!=-1)
            {
                MessageBox.Show(" License Issued Successfully with ID = "+LicenseID, "Succeeded",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(" License was not  Issued ! ", "Faild",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
