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

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {

        
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        private int _UserID=-1;
        private clsUser _User;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode=enMode.AddNew;
        }
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("this form well be closed because No User With ID : " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            ctrlpassPassword.Password= _User.Password;
            ctrlpassConfirmPassword.Password = _User.Password;
            txtUserName.Text = _User.UserName;
            chkIsActive.Checked = _User.IsActive;
        }

        private void _ResetDefaultValues()
        {
            if(_Mode==enMode.AddNew)
            {
                this.Text= lblTitle.Text = "Add New User";
                _User = new clsUser();
            }
            else
            {
                this.Text = lblTitle.Text = "Update User";
                ctrlPersonCardWithFilter1.EnableFilter = false;
            }
            btnSave.Enabled = false;
            tpLoginInfo.Enabled = false;
            txtUserName.Text = "";
            ctrlpassConfirmPassword.Password = "";
            ctrlpassPassword.Password = "";
            chkIsActive.Checked = true;
        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This field is required !");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, null);
            }

            if (_User.UserName != txtUserName.Text.Trim() && clsUser.IsUserExist(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User Name is used for another User !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, null);
            }
        }

        private void ctrlpassPassword_Validating(object sender, CancelEventArgs e)
        {
            if (ctrlpassPassword.Password.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(ctrlpassPassword, "This field cannot be empty !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlpassPassword, null);
            }
        }

        private void ctrlpassConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (ctrlpassPassword.Password.Trim() != ctrlpassConfirmPassword.Password.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(ctrlpassConfirmPassword, "Password Confirmation does not match password !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlpassConfirmPassword, null);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.Update)
            {
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user ,choose anthore one ", "Select anthore person",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    tpLoginInfo.Enabled = true;
                    btnSave.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }

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
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = ctrlpassPassword.Password.Trim();
            _User.IsActive = chkIsActive.Checked;
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            if(_User.Save())
            {
                this.Text = lblTitle.Text = "Update User ";
                _Mode = enMode.Update;
                lblUserID.Text = _User.UserID.ToString();

                MessageBox.Show("Data Saved Successfully ", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

               
            }
            else
                MessageBox.Show("Data is not Saved Successfully ", "Error ",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpPersonInfo"];
        }
    }
}
