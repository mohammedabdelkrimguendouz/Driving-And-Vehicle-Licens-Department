using DVLD.Properties;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD.People.frmAddUpdatePerson;

namespace DVLD.People
{
    public partial class ctrlPersonCard : UserControl
    {

        public enum enGender { Male=0,Female=1}

        private int _PersonID=-1;
        public int PersonID {get { return _PersonID; }}

        private clsPerson _Person;
        public clsPerson SelectedPersonInfo { get { return _Person; }}
        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void LoadPersonInfo(int PersonID)
        {
             _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person With ID = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _PersonID = -1;
                return;
            }
            _FillPersonInfo();
                
        }

        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblAddress.Text = _Person.Address;
            lblCountry.Text = _Person.CountryInfo.CountryName;
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblEmail.Text = _Person.Email;
            lblDateOfBirth.Text = clsFormat.DateToShort((DateTime)_Person.DateOfBirth);
            lblPhone.Text = _Person.Phone;
            lblGender.Text = (_Person.Gender == (short)enGender.Male) ? "Male" : "Female";
            pbGender.Image= (_Person.Gender == (short)enGender.Male) ? Resources.Man_32 : Resources.Woman_32;
            llEditPersonInfo.Enabled = true;
            _LoadPersonImage();


        }

        private void _LoadPersonImage()
        {
            if (_Person.Gender == (short)enGender.Male)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;
            if (_Person.ImagePath != "")
                if(File.Exists(_Person.ImagePath))
                    pbPersonImage.ImageLocation = _Person.ImagePath;
                else
                    MessageBox.Show("Not find this image = " + _Person.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person With National Number = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _PersonID = -1;
                return;
            }
            _FillPersonInfo();
        }

        public void ResetPersonInfo()
        {
            lblAddress.Text = "[????]";
            lblCountry.Text = "[????]";
            lblDateOfBirth.Text= "[????]";
            lblName.Text = "[????]";
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblPhone.Text = "[????]";
            lblPersonID.Text = "[????]";
            llEditPersonInfo.Enabled = false;
            pbPersonImage.Image = Resources.Male_512;
            _PersonID = -1;
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson Form = new frmAddUpdatePerson(_PersonID);
            Form.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        
    }
}

