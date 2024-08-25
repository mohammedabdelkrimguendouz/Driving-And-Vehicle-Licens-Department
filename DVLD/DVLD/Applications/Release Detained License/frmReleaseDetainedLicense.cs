using DVLD.Drivers;
using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.People.Controls;
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

namespace DVLD.Applications.ReleaseDetainedLicense
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private int _SelectedLicenseID=-1;
       
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
           
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        //{
        //    //_SelectedLicenseID = obj;
        //    //lblLicenseID.Text = _SelectedLicenseID.ToString();
        //    //llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);
            
        //    //if (_SelectedLicenseID == -1)
        //    //    return;


        //    //if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
        //    //{
        //    //    MessageBox.Show("Selected license is not detained   choose an anthor one . ",
        //    //        "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //    btnReleaseDetainLicense.Enabled = false;
        //    //    return;
        //    //}

        //    //btnReleaseDetainLicense.Enabled = true;
        //    //lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
        //    //lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
        //    //lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
        //    //lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        //    //lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
        //    //lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
        //}

        private void btnReleaseDetainLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to release this detain  License ?", "Confirm ", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ApplicationID = -1;

            bool IsReleased = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);
            if (!IsReleased)
            {
                MessageBox.Show(" Faild to release this Detain  License ! ", "Faild",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(" License release Detained Successfully ", "Succeeded",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblApplicationID.Text = ApplicationID.ToString();
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
            btnReleaseDetainLicense.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(object sender, Licenses.Local_Licenses.Controls.ctrlDriverLicenseInfoWithFilter.LicenseSelectedEventArgs e)
        {
            _SelectedLicenseID = e.LicenseID;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
                return;


            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected license is not detained   choose an anthor one . ",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReleaseDetainLicense.Enabled = false;
                return;
            }

            btnReleaseDetainLicense.Enabled = true;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
        }
    }
}
