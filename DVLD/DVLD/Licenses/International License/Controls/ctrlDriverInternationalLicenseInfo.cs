using DVLD.Properties;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_InternationalLicenseInfos
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        public enum enGender { Male = 0, Female = 1 }
        private int _InternationalLicenseID = -1;
        public int InternationalLicenseID { get { return _InternationalLicenseID; } }

        private clsInternationalLicense _InternationalLicenseInfo;
        public clsInternationalLicense SelectedInternationalLicenseInfo { get { return _InternationalLicenseInfo; } }
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {
            _InternationalLicenseInfo =clsInternationalLicense.Find(InternationalLicenseID);
            if (_InternationalLicenseInfo == null)
            {
                ResetInternationalLicenseInfo();
                MessageBox.Show("Could not find International License  ID = " + InternationalLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillInternationalLicenseInfo();

        }
        public void ResetInternationalLicenseInfo()
        {
            lblInternationalLicenseID.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblName.Text = "[????]";
            lblGender.Text = "[????]";
            lblApplicationID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblLocalLicenseID.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblIssueReason.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;
            _InternationalLicenseID = -1;
        }

        private void _FillInternationalLicenseInfo()
        {
            _InternationalLicenseID = _InternationalLicenseInfo.InternationalLicenseID;
            lblInternationalLicenseID.Text = _InternationalLicenseID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicenseInfo.IssueDate);
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicenseInfo.DriverInfo.PersonInfo.DateOfBirth); ;
            lblName.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.NationalNo;
            lblLocalLicenseID.Text = _InternationalLicenseInfo.IssuedUsingLocalLicenseID.ToString();
            lblApplicationID.Text = _InternationalLicenseInfo.ApplicationID.ToString();
            lblIsActive.Text = (_InternationalLicenseInfo.IsActive) ? "Yes" : "No";
            lblDriverID.Text = _InternationalLicenseInfo.DriverID.ToString();
            lblIssueReason.Text = _InternationalLicenseInfo.IssueReasonText;
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicenseInfo.ExpirationDate); ;
            lblGender.Text = (_InternationalLicenseInfo.DriverInfo.PersonInfo.Gender == (short)enGender.Male) ? "Male" : "Female";
            pbGender.Image = (_InternationalLicenseInfo.DriverInfo.PersonInfo.Gender == (short)enGender.Male) ? Resources.Man_32 : Resources.Woman_32;
            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicenseInfo.DriverInfo.PersonInfo.Gender == (short)enGender.Male)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _InternationalLicenseInfo.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not  find this image = " + ImagePath,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
