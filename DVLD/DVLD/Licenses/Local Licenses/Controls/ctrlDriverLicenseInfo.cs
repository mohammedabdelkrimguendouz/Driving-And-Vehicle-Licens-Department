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
using System.IO;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        public enum enGender { Male=0,Female=1}
        private int _LicenseID = -1;
        public int LicenseID { get { return _LicenseID; } }

        private clsLicense _License;
        public clsLicense SelectedLicenseInfo { get { return _License; } }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            _License = clsLicense.FindByLicenseID(LicenseID);
            if (_License == null)
            {
                ResetLicenseInfo();
                MessageBox.Show("Could not find license  ID = " + LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLicenseInfo();

        }
        public void ResetLicenseInfo()
        {
            lblLicenseClass.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblName.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueReason.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNotes.Text = "[????]";
            lblIsActive.Text= "[????]";
            lblDriverID.Text= "[????]";
            lblExpirationDate.Text= "[????]";
            lblIsDetained.Text= "[????]";
            pbPersonImage.Image = Resources.Male_512;
            _LicenseID = -1;
        }

        private void _FillLicenseInfo()
        {
            _LicenseID = _License.LicenseID;
            lblLicenseClass.Text = _License.LicenseClassInfo.ClassName;
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth); ;
            lblName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblIssueReason.Text = _License.IssueReasonText ;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNotes.Text = (_License.Notes=="")?"No Notes": _License.Notes;
            lblIsActive.Text = (_License.IsActive)?"Yes":"No";
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate); ;
            lblIsDetained.Text = (_License.IsDetained) ? "Yes" : "No";
            lblGender.Text = (_License.DriverInfo.PersonInfo.Gender == (short)enGender.Male) ? "Male" : "Female";
            pbGender.Image = (_License.DriverInfo.PersonInfo.Gender == (short)enGender.Male) ? Resources.Man_32 : Resources.Woman_32;
            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == (short)enGender.Male)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not  find this image = " + ImagePath,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


    }
}
