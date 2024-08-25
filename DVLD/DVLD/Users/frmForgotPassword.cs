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
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmForgotPassword : Form
    {
        private clsUser _User;
        private string _VerificationCode = "";
        public frmForgotPassword()
        {
            InitializeComponent();
        }

        private void _SendEmailVerification()
        {
            _VerificationCode = clsUtil.GenerateGuid();
            string Subject = "Password Reset Request";
            string Body = $"Hello [{_User.PersonInfo.FullName}] , \n\n " +
                $"You have requested to reset your password. Please use the following verification code to complete the process:\n" +
                $"Verification Code: {_VerificationCode} \n\n If you did not request a password reset, please ignore this email.\n\n" +
                $"Thank you,\n" +
                $"[DVLD]";
            clsContact.SendEmail(_User.PersonInfo.Email, Subject, Body);

        }

        private void frmForgotPassword_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            gbVerification.Enabled = gbResetPassword.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnFind.PerformClick();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _User = clsUser.FindByUserName(txtUserName.Text.Trim());
            if (_User == null)
            {
                txtUserName.Focus();
                MessageBox.Show($"Not Exist User Name :  {txtUserName.Text.Trim()}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            gbVerification.Enabled = true;
            lblEmail.Text = _User.PersonInfo.Email;
            txtVerificationCode.Focus();
            _SendEmailVerification();
        }

        private void btnVerification_Click(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text != _VerificationCode)
            {
                txtVerificationCode.Focus();
                MessageBox.Show(" Verification code does not match ", "Validation Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            gbResetPassword.Enabled = true;
            ctrlNewPassword.txtPasswordFocus();
        }

        private void ctrlNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ctrlNewPassword.Password.Trim()))
            {
                e.Cancel = true;
                ctrlNewPassword.Focus();
                errorProvider1.SetError(ctrlNewPassword, " Password cannot be empty !");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlNewPassword, null);
            }
        }

        private void ctrlConfirmNewPassword_Validating(object sender, CancelEventArgs e)
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string NewPasswordHashed = clsCryptography.ComputeHash(ctrlNewPassword.Password.Trim());
            if (_User.ChangePassword(NewPasswordHashed))
            {
                MessageBox.Show("Password Reset successfully", "Saved",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                gbResetPassword.Enabled = false;
                gbVerification.Enabled = false;
                txtUserName.Enabled = false;
                btnFind.Enabled = false;
            }
            else
                MessageBox.Show(" Password was not Reset successfully", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
