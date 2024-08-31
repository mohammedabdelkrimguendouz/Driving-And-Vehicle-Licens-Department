using DVLD.People;
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

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _ApplicationID = -1;
        public int ApplicationID { get { return _ApplicationID; } }

        private clsApplication _Application;
        public clsApplication SelectedApplicationInfo { get { return _Application; } }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void LoadApplicationBasicInfo(int ApplicationID)
        {
            _Application = clsApplication.FindBaseApplication(ApplicationID);
            if (_Application == null)
            {
                ResetApplicationBasicInfo();
                MessageBox.Show("No Application With ID = " + ApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();

        }

        private void _FillPersonInfo()
        {
            _ApplicationID = _Application.ApplicationID;
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatusText;
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate); ;
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserName;
            lblApplicant.Text = _Application.ApplicantPersonInfo.FullName;
            lblFees.Text = _Application.PaidFees.ToString();
            lblType.Text = _Application.ApplicationTypeInfo.ApplicationTypeTitle;
        }
        public void ResetApplicationBasicInfo()
        {
            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblDate.Text = "[??/???/????]";
            lblStatusDate.Text = "[??/???/????]";
            lblCreatedByUser.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblFees.Text = "[????]";
            lblType.Text = "[????]";
            lblApplicationID.Text = "[????]";
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_Application.ApplicantPersonID);
            frm.ShowDialog();
            LoadApplicationBasicInfo(_ApplicationID);
        }
    }
}
