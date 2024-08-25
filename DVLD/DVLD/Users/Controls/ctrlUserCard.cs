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

namespace DVLD.Users
{
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID = -1;
        public int UserID { get { return _UserID; } }

        private clsUser _User;
        public clsUser SelectedUserInfo { get { return _User; } }
      
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void _ResetUserInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
            lblIsActive.Text = "[????]";
            _UserID = -1;
        }

        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            _UserID = _User.UserID;
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = (_User.IsActive) ? "Yes" : "No";

        }
        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);
            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show("No User With ID = " + UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();

        }

       
    }
}
