using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.International_License;
using DVLD.Licenses.Local_Licenses;
using DVLD.Licenses.Local_Licenses.Controls;
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

namespace DVLD.Applications.Replacement_for_Damaged_or_Lost__International_Licenses
{
    public partial class frmReplacementInternationalLicenseDamagedOrLost : Form
    {
        private int _NewInternationalLicenseID = -1;
        public frmReplacementInternationalLicenseDamagedOrLost()
        {
            InitializeComponent();
        }

        private int _GetApplicationTypeID()
        {
            if (rbDamagedInternationalLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDamagedInternationalLicense;
            return (int)clsApplication.enApplicationType.ReplaceLostInternationalLicense;
        }

        private clsInternationalLicense.enIssueReason _GetIssueReason()
        {
            if (rbDamagedInternationalLicense.Checked)
                return clsInternationalLicense.enIssueReason.DamagedReplacement;
            return clsInternationalLicense.enIssueReason.LostReplacement;
        }

        private void frmReplacementInternationalLicenseDamagedOrLost_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfoWithFilter1.txtInternationalLicenseIDFocus();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            rbDamagedInternationalLicense.Checked = true;
            rbDamagedInternationalLicense_CheckedChanged(null, null);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbDamagedInternationalLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = this.Text = "Replacement For Damaged International License";
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();
        }

        private void rbLostInternationalLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = this.Text = "Replacement For Lost International License";
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverInternationalLicenseInfoWithFilter1
                .SelectedInternationalLicenseInfo.DriverInfo.PersonID;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverInternationalLicenseInfo frm = new frmShowDriverInternationalLicenseInfo(_NewInternationalLicenseID);
            frm.ShowDialog();
        }

        private void ctrlDriverInternationalLicenseInfoWithFilter1_OnInternationalLicenseSelected(object sender, Licenses.International_License.Controls.ctrlDriverInternationalLicenseInfoWithFilter.InternationalLicenseSelectedEventArgs e)
        {
            int SelectedInternationalLicenseID = e.InternationalLicenseID;

            lblOldInternationalLicenseID.Text = SelectedInternationalLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedInternationalLicenseID != -1);

            if (SelectedInternationalLicenseID == -1)
                return;


            if (!ctrlDriverInternationalLicenseInfoWithFilter1.SelectedInternationalLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected international license is not active Please choose an active license . ",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }
            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to issue a replacement of this International License ?", "Confirm ", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.No)
                return;


            clsInternationalLicense NewInternationalLicense = ctrlDriverInternationalLicenseInfoWithFilter1
                .SelectedInternationalLicenseInfo.ReplaceInternationalLicense(clsGlobal.CurrentUser.UserID, _GetIssueReason());
            if (NewInternationalLicense == null)
            {
                MessageBox.Show(" Faild to Replace the international License ! ", "Faild",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _NewInternationalLicenseID = NewInternationalLicense.InternationalLicenseID;
            lblReplacedApplicationID.Text = NewInternationalLicense.ApplicationID.ToString();
            lblReplacedInternationalLicenseID.Text = _NewInternationalLicenseID.ToString();

            MessageBox.Show("international License Replaced Successfully with ID = " + _NewInternationalLicenseID, "Succeeded",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

            gbReplacementFor.Enabled = false;
            ctrlDriverInternationalLicenseInfoWithFilter1.EnableFilter = false;
            btnIssueReplacement.Enabled = false;
            llShowNewLicenseInfo.Enabled = true;
        }
    }
}
