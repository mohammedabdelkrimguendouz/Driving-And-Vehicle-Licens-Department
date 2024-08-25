using DVLD.Global.Classes;
using DVLD.Global_Classes;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
                chkRememberMe.Checked = true;
            else
                chkRememberMe.Checked = false;
            txtUserName.Text = UserName;
            ctrlPassword.Password = Password;
        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string PasswordHashed = clsCryptography.ComputeHash(ctrlPassword.Password.Trim());
            clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(), PasswordHashed);
            if (User != null)
            {
                if (chkRememberMe.Checked)
                    clsGlobal.RememberUserNameAndPassword(txtUserName.Text.Trim(), ctrlPassword.Password.Trim());
                else
                    clsGlobal.RememberUserNameAndPassword("", "");
                if (!User.IsActive)
                {
                    MessageBox.Show("Your account is not active , please contact your admin !", "In Active Account",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                clsGlobal.CurrentUser = User;
                
               
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
                
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalide UserName Or Password !", "Wrong Credintials",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmForgotPassword frm = new frmForgotPassword();
            frm.ShowDialog();
        }
    }
}
