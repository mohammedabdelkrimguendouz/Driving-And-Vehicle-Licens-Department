using DVLD.Global_Classes;
using DVLD.Licenses.Local_Licenses;
using DVLD.Violations;
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

namespace DVLD.Licenses.Detaind_Licenses
{
    public partial class frmDetainLicense : Form
    {
        private int _DetainID = -1;
        private int _ViolationID = -1;
        private int _SelectedLicenseID = -1;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            

            if (MessageBox.Show("Are you sure do you want to  detain this License ?", "Confirm ", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;


            _DetainID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainLicense(clsGlobal.CurrentUser.UserID, Convert.ToSingle(lblFineFees.Text.Trim()),_ViolationID);
            if (_DetainID == -1)
            {
                MessageBox.Show(" Faild to Detain the License ! ", "Faild",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            MessageBox.Show(" License Detained Successfully with ID = " + _DetainID, "Succeeded",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblDetainID.Text = _DetainID.ToString();
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
            btnDetainLicense.Enabled = false;
            btnChooseViolation.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(object sender, Local_Licenses.Controls.ctrlDriverLicenseInfoWithFilter.LicenseSelectedEventArgs e)
        {
            _SelectedLicenseID = e.LicenseID;

            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
                return;


            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected license already detained  Please choose an anthor one . ",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }
            btnChooseViolation.Enabled = true;
           
        }

        private void btnChooseViolation_Click(object sender, EventArgs e)
        {
            frmChooseViolation frm = new frmChooseViolation();
            frm.DataBack += _DataBackEvent;
            frm.ShowDialog();
        }
        private void _DataBackEvent(object sender, int ViolationID,float FineFees)
        {
            lblFineFees.Text = FineFees.ToString();
            _ViolationID=ViolationID;
            btnDetainLicense.Enabled = true;
        }
    }
}
