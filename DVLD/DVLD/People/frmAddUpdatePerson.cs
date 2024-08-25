using DVLD.Global_Classes;
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

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {

        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        public enum enGender { Male = 0, Female = 1 }

        int _PersonID=-1;
        clsPerson _Person;
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _FillCountriesInCompoBox()
        {
            DataTable TableCountries = clsCountry.GetAllCountries();
            foreach (DataRow Row in TableCountries.Rows)
            {
                cbCountry.Items.Add(Row["CountryName"]);

            }
        }
        private void _ResetDefaultValues()
        {
            _FillCountriesInCompoBox();
            if(_Mode==enMode.AddNew)
            {
                this.Text = lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                this.Text = lblTitle.Text = "Update Person";
            }
              
               
            lblPersonID.Text = "N/A";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtNationalNo.Text = "";
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            pbPersonImage.Image = (rbMale.Checked) ? Resources.Male_512 : Resources.Female_512;
            pbPersonImage.ImageLocation = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            cbCountry.SelectedIndex = cbCountry.FindString("Algeria");
            txtAddress.Text = "";
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != "");
            
        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("this form well be closed because No Person With ID : " + _PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            lblPersonID.Text = _Person.PersonID.ToString();
            txtAddress.Text = _Person.Address;
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
         
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);
            if (_Person.Gender == (short)enGender.Male)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
            if (_Person.ImagePath != "")
                pbPersonImage.ImageLocation = _Person.ImagePath;
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != "");
        }
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == "")
                pbPersonImage.Image = Resources.Male_512;
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == "")
                pbPersonImage.Image = Resources.Female_512;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Choose Image ";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.ImageLocation = openFileDialog1.FileName;
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = "";
            if(rbFemale.Checked)
                pbPersonImage.Image = Resources.Female_512;
            else
                pbPersonImage.Image = Resources.Male_512;
            llRemoveImage.Visible = false;
        }

        private void ValidateEmptyTextBox(object sender , CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            if (string.IsNullOrEmpty(Temp.Text))
            {
                e.Cancel = true;
                Temp.Focus();
                errorProvider1.SetError(Temp, "This field is required !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(txtNationalNo.Text.Trim()=="")
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "This field is required !");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (_Person.NationalNo!=txtNationalNo.Text.Trim()&&clsPerson.ISPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidation.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalide Formate Email (yourmail'[6-30 Letter]'.gmail.com)");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private bool _HabdelPersonImage()
        {
            if(_Person.ImagePath!=pbPersonImage.ImageLocation)
            {
                if(_Person.ImagePath!="")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }catch(Exception Ex)
                    {
                        return false;
                    }
                }
                if(pbPersonImage.ImageLocation!="")
                {
                    string SourceImageFile = pbPersonImage.ImageLocation;
                    if(clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;

                    }
                }
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HabdelPersonImage())
                return;


            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.ThirdName =  txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Gender = (rbMale.Checked) ? (short)enGender.Male : (short)enGender.Female;
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;
            _Person.Address = txtAddress.Text.Trim();
            _Person.ImagePath = (pbPersonImage.ImageLocation != null) ? pbPersonImage.ImageLocation : "";
            if (_Person.Save())
            {
                
                this.Text=lblTitle.Text = "Update Person ";
                _Mode = enMode.Update;
                lblPersonID.Text = _Person.PersonID.ToString();
                MessageBox.Show("Data Saved Successfully ", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Data is not Saved Successfully ", "Error ",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
