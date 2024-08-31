using DVLD.Global.Classes;
using DVLD.Global_Classes;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {

        private clsUser _User;
        private int _UserID;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }
       

        private void ctrlpassCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ctrlCurrentPassword.Password.Trim()))
            {
                e.Cancel = true;
                ctrlNewPassword.Focus();
                errorProvider1.SetError(ctrlCurrentPassword, "Current Password cannot be empty !");
                return;
            }
            else
            {
                errorProvider1.SetError(ctrlCurrentPassword, null);
            }

            string PasswordHashed = clsCryptography.ComputeHash(ctrlCurrentPassword.Password.Trim());
            if (PasswordHashed!=_User.Password)
            {
                e.Cancel = true;
                ctrlCurrentPassword.Focus();
                errorProvider1.SetError(ctrlCurrentPassword, "Current password is wrong !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlCurrentPassword,null);
            }
        }

        private void ctrlpassNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (ctrlNewPassword.Password.Trim() == "")
            {
                e.Cancel = true;
                ctrlNewPassword.Focus();
                errorProvider1.SetError(ctrlNewPassword, "This field cannot be empty !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlNewPassword, null);
            }
        }

        private void ctrlpassConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (ctrlNewPassword.Password.Trim() != ctrlConfirmPassword.Password.Trim())
            {
                e.Cancel = true;
                ctrlConfirmPassword.Focus();
                errorProvider1.SetError(ctrlConfirmPassword, "Password Confirmation does not match password !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string NewPasswordHashed = clsCryptography.ComputeHash(ctrlNewPassword.Password.Trim());

            _User.Password = NewPasswordHashed;
            if (_User.Save())
            {
                MessageBox.Show("Password Changed successfully", "Saved",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                 MessageBox.Show(" Password was not Changed successfully", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ResetDefaultValues()
        {
            ctrlConfirmPassword.Password = "";
            ctrlCurrentPassword.Password = "";
            ctrlNewPassword.Password = "";
            ctrlCurrentPassword.txtPasswordFocus();
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("No User With ID = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                gbPasswordInfo.Enabled = false;
                return;
            }
            ctrlUserCard1.LoadUserInfo(_UserID);
        }
    }
}
