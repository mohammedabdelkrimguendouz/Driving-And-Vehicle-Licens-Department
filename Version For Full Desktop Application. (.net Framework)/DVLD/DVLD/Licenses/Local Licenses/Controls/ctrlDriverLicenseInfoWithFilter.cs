using DVLD.People;
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

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public class LicenseSelectedEventArgs : EventArgs
        {
            public int LicenseID { get; }
            public LicenseSelectedEventArgs(int LicenseID)
            {
                this.LicenseID = LicenseID;
            }
        }

        public event EventHandler<LicenseSelectedEventArgs> OnLicenseSelected;

        public void RaiseOnLicenseSelected(int LicenseID)
        {
            RaiseOnLicenseSelected(new LicenseSelectedEventArgs(LicenseID));
        }
        protected virtual void RaiseOnLicenseSelected(LicenseSelectedEventArgs e)
        {
            OnLicenseSelected?.Invoke(this, e);
        }
        private bool _EnableFilter = true;
        public bool EnableFilter
        {
            get { return _EnableFilter; }
            set
            {
                _EnableFilter = value;
                gbFilters.Enabled = _EnableFilter;
            }
        }

        private int _LicenseID = -1; 
        public int LicenseID { get { return ctrlDriverLicenseInfo1.LicenseID; } }
        public clsLicense SelectedLicenseInfo { get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; } }
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));

            if (e.KeyChar == (char)13)
                btnFindLicense.PerformClick();
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text))
            {
                e.Cancel = true;
                txtLicenseID.Focus();
                errorProvider1.SetError(txtLicenseID, "This field is required !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.ResetLicenseInfo();
            txtLicenseIDFocus();
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            _FindNow();
        }
        private void _FindNow()
        {
            _LicenseID = int.Parse(txtLicenseID.Text.Trim());
            ctrlDriverLicenseInfo1.LoadLicenseInfo(_LicenseID);

            if (OnLicenseSelected != null && EnableFilter)
                RaiseOnLicenseSelected(ctrlDriverLicenseInfo1.LicenseID);
        }

        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("filed is not valide!, put the mouse over the red icon to see the error", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseIDFocus();
                return;
            }
            _FindNow();
        }
    }
}
