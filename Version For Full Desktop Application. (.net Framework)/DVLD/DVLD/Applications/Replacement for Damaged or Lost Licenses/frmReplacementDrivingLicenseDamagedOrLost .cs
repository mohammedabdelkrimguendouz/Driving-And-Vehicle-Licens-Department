using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.Replacement_for_Damaged_or_Lost_Licenses
{
    public partial class frmReplacementDrivingLicenseDamagedOrLost : Form
    {
        private int _NewLicenseID = -1;
       
        public frmReplacementDrivingLicenseDamagedOrLost()
        {
            InitializeComponent();
        }

        private int  _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }

        private clsLicense.enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return clsLicense.enIssueReason.DamagedReplacement;
            return clsLicense.enIssueReason.LostReplacement;
        }

        private void frmReplacementDrivingLicenseDamagedOrLost_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            rbDamagedLicense.Checked = true;
            rbDamagedLicense_CheckedChanged(null, null);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = this.Text = "Replacement For Damaged License";
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();
            
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = this.Text = "Replacement For Lost License";
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();
        }



        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to issue a replacement of this License ?", "Confirm ", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;


            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReplaceLicense(clsGlobal.CurrentUser.UserID, _GetIssueReason());
            if (NewLicense == null)
            {
                MessageBox.Show(" Faild to Replace the License ! ", "Faild",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _NewLicenseID = NewLicense.LicenseID;
            lblReplacedApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblReplacedLicenseID.Text = _NewLicenseID.ToString();

            MessageBox.Show(" License Replaced Successfully with ID = " + _NewLicenseID, "Succeeded",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
            btnIssueReplacement.Enabled = false;
            llShowNewLicenseInfo.Enabled = true;

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(object sender, Licenses.Local_Licenses.Controls.ctrlDriverLicenseInfoWithFilter.LicenseSelectedEventArgs e)
        {
            int SelectedLicenseID = e.LicenseID;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
                return;


            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active Please choose an active license . ",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }
            btnIssueReplacement.Enabled = true;
        }
    }
}
