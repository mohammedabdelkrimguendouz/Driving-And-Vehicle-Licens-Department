using DVLD.Properties;
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
    public partial class ctrlPassword : UserControl
    {
        public enum enPasswordMode { Show=0,Hide=1}
        private enPasswordMode _PasswordMode;

        public string Password {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; } 
        }
       
        public ctrlPassword()
        {
            InitializeComponent();
            _PasswordMode = enPasswordMode.Hide;
        }

        private void _ChangePasswordToModeHide()
        {
            txtPassword.PasswordChar = '*';
            btnShowHidePassword.BackgroundImage = Resources.HidePassword_32;
            _PasswordMode = enPasswordMode.Hide;
        }
        private void _ChangePasswordToModeShow()
        {
            txtPassword.PasswordChar = '\0';
            btnShowHidePassword.BackgroundImage = Resources.ShowPassword_32;
            _PasswordMode = enPasswordMode.Show;
        }
        private void btnShowHidePassword_Click(object sender, EventArgs e)
        {
            if (_PasswordMode == enPasswordMode.Hide)
                _ChangePasswordToModeShow();
            else
                _ChangePasswordToModeHide();
        }

        public  void txtPasswordFocus()
        {
            txtPassword.Focus();
        }
    }
}
