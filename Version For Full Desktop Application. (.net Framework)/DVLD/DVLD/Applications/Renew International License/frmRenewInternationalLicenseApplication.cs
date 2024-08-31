using DVLD.Global_Classes;
using DVLD.Licenses.Local_Licenses.Controls;
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
using DVLD.Licenses.Local_Licenses;
using DVLD.Licenses.International_License;

namespace DVLD.Applications.Renew_International_License
{
    public partial class frmRenewInternationalLicenseApplication : Form
    {
        private int _NewInternationalLicenseID = -1;
        public frmRenewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmRenewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfoWithFilter1.txtInternationalLicenseIDFocus();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(clsSetting.GetDefaultValidityLengthForAnInternationalLicense()));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnRenewInternationalLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to Renew this International License ?", "Confirm ", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsInternationalLicense NewInternationalLicense = ctrlDriverInternationalLicenseInfoWithFilter1
                .SelectedInternationalLicenseInfo.RenewInternationalLicense(clsGlobal.CurrentUser.UserID);
                

            if (NewInternationalLicense == null)
            {
                MessageBox.Show(" Faild to Renew the International License ! ", "Faild",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _NewInternationalLicenseID = NewInternationalLicense.InternationalLicenseID;
            lblRenewApplicationID.Text = NewInternationalLicense.ApplicationID.ToString();
            lblRenewedInternationalLicenseID.Text = _NewInternationalLicenseID.ToString();

            MessageBox.Show(" International  License Renewed Successfully with ID = " + _NewInternationalLicenseID, "Succeeded",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);


            ctrlDriverInternationalLicenseInfoWithFilter1.EnableFilter = false;
            btnRenewInternationalLicense.Enabled = false;
            llShowNewLicenseInfo.Enabled = true;
        }

        private void ctrlDriverInternationalLicenseInfoWithFilter1_OnInternationalLicenseSelected(object sender, Licenses.International_License.Controls.ctrlDriverInternationalLicenseInfoWithFilter.InternationalLicenseSelectedEventArgs e)
        {
            int SelectedInternationalLicenseID = e.InternationalLicenseID;

            lblOldInternationalLicenseID.Text = SelectedInternationalLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedInternationalLicenseID != -1);

            if (SelectedInternationalLicenseID == -1)
                return;

            

            if (!ctrlDriverInternationalLicenseInfoWithFilter1.SelectedInternationalLicenseInfo.IsInternationalLicenseExpired())
            {
                MessageBox.Show("Selected International License is not yet expiared ,it will wxpire on " + clsFormat.DateToShort(ctrlDriverInternationalLicenseInfoWithFilter1.SelectedInternationalLicenseInfo.ExpirationDate),
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewInternationalLicense.Enabled = false;
                return;
            }

            if (!ctrlDriverInternationalLicenseInfoWithFilter1.SelectedInternationalLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active Please choose an active license . ",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewInternationalLicense.Enabled = false;
                return;
            }
            btnRenewInternationalLicense.Enabled = true;
        }
    }
}
