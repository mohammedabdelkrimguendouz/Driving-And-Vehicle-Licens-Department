using DVLD.Global_Classes;
using DVLD_Buisness.Global_Classes;
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
using DVLD.Licenses;
using DVLD.Licenses.International_License;

namespace DVLD.Applications.International_Licenses
{
    public partial class frmNewInternationalLicenseApplication : Form
    {

        private int _InternationalLicenseID = -1;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(clsSetting.GetDefaultValidityLengthForAnInternationalLicense()));
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private bool _HandleActiveInternationalLicenseContraint()
        {

            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.DriverID);
            if (ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license .",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                _InternationalLicenseID = ActiveInternationalLicenseID;
                llShowLicenseInfo.Enabled = true;
                return false;
            }
            return true;
        }
        private bool _HandleLicenseClassConstraint()
        {

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.LicenseClassID !=
                (int)clsLocalDrivingLicenseApplication.enLicenseClass.Ordinarydrivinglicense)
            {
                MessageBox.Show("The license class cannot br an international license,it must be Class 3 - Ordinary driving license .",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                return false;
            }
            return true;

            
        }
        private bool _HandleActiveLicenseConstraint()
        {

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active Please choose an active license . ",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                return false;
            }
            return true;

        }
        private bool _HandleLicenseExpiredConstraint()
        {

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared ,it will wxpire on " + clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate),
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                return false;
            }
            return true;

        }
        private bool _HandleDetainedLicenseConstraint()
        {

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected license already detained  Please choose an anthor one . ",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                return false;
            }
            return true;

        }

       
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();
        }

    
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverInternationalLicenseInfo frm = new frmShowDriverInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void btnIssueInternationalLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to Isuued International License ?", "Confirm ", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;


            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.IsActive = true;
            InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(InternationalLicense.DefaultValidityLength);
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueReason = clsInternationalLicense.enIssueReason.FirstTime;
            if (!InternationalLicense.Save())
            {
                MessageBox.Show(" Faild to Issue  International License ! ", "Faild",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = _InternationalLicenseID.ToString();
            lblInternationalLicenseAppID.Text = InternationalLicense.ApplicationID.ToString();

            MessageBox.Show("Issued International License  Successfully with ID = " + _InternationalLicenseID, "Succeeded",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);


            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
            btnIssueInternationalLicense.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(object sender, Licenses.Local_Licenses.Controls.ctrlDriverLicenseInfoWithFilter.LicenseSelectedEventArgs e)
        {
            int SelectesLicenseID = e.LicenseID;

            lblLocalLicenseID.Text = SelectesLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectesLicenseID != -1);

            if (SelectesLicenseID == -1)
                return;

            if (!_HandleLicenseClassConstraint())
                return;
            if (!_HandleActiveLicenseConstraint())
                return;
            if (!_HandleLicenseExpiredConstraint())
                return;
            if (!_HandleDetainedLicenseConstraint())
                return;
            if (!_HandleActiveInternationalLicenseContraint())
                return;




            btnIssueInternationalLicense.Enabled = true;
        }
    }
}
