using DVLD.Global_Classes;
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

namespace DVLD.Applications.Local_Driving_License_Applications
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        private int _SelectedPersonID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode=enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _Mode = enMode.Update;
        }

        private void _FillLicenseClassesInComboBox()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();
            foreach (DataRow Row in dt.Rows) 
            {
                cbLicenseClass.Items.Add(Row["ClassName"]);
            }
        }
        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.EnableFilter = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("this form well be closed because No Local Driving License Application With ID : " + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblDriverLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(_LocalDrivingLicenseApplication.LicenseClassInfo.ClassName);
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = _LocalDrivingLicenseApplication.CreatedByUserInfo.UserName;
        }

        private void _ResetDefaultValues()
        {
            _FillLicenseClassesInComboBox();
            if (_Mode == enMode.AddNew)
            {
                this.Text = lblTitle.Text = "New Local Driving License Application ";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
                lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
                lblDriverLicenseApplicationID.Text = "[????]";
                cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find((int)clsLocalDrivingLicenseApplication.enLicenseClass.Ordinarydrivinglicense).ClassName);
            }
            else
            {
                this.Text = lblTitle.Text = "Update Local Driving License Application";
                
            }

            tpApplicationInfo.Enabled = false;
            btnSave.Enabled = false;
            
            
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }
            else
            {
                MessageBox.Show("Please Select a Person  ", "Select a person",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            clsLicenseClass LicenseClass = clsLicenseClass.Find(cbLicenseClass.Text);


            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass
                (ApplicantPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClass.LicenseClassID);


            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose anther License Class ,the Selected Person Already have an active application" +
                    "for the selected class with id = " + ActiveApplicationID, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(ApplicantPersonID, LicenseClass.LicenseClassID))
            {
                MessageBox.Show("Person Alrady have a license active  for this license class  ", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            int Age = DateTime.Now.Year - ctrlPersonCardWithFilter1.SelectedPersonInfo.DateOfBirth.Year;

            if (Age < LicenseClass.MinimumAllowedAge)
            {
                MessageBox.Show("A person must be older than or aqual to  = " + LicenseClass.MinimumAllowedAge +
                    " Years of age to apply for this license class .", "Not Allowed",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = ApplicantPersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (byte)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClass.LicenseClassID;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblApplicationFees.Text);



            if (_LocalDrivingLicenseApplication.Save())
            {
                this.Text = lblTitle.Text = "Update Local Driving License Application";
                _Mode = enMode.Update;
                lblDriverLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

                MessageBox.Show("Data Saved Successfully ", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
                MessageBox.Show("Data is not Saved Successfully ", "Error ",
               MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        //private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        //{
        //    _SelectedPersonID = obj;
        //}

        private void frmAddUpdateLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpPersonInfo"];
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(object sender, ctrlPersonCardWithFilter.PersonSelectedEventArgs e)
        {
            _SelectedPersonID = e.PersonID;
        }
    }
}
