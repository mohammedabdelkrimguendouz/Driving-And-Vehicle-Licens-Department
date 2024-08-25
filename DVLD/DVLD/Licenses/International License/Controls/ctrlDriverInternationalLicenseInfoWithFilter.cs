using DVLD.Licenses.Local_Licenses.Controls;
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

namespace DVLD.Licenses.International_License.Controls
{
    public partial class ctrlDriverInternationalLicenseInfoWithFilter : UserControl
    {
      
        public class InternationalLicenseSelectedEventArgs:EventArgs
        {
            public int InternationalLicenseID { get; }
            public InternationalLicenseSelectedEventArgs(int InternationalLicenseID)
            {
                this.InternationalLicenseID = InternationalLicenseID;
            }
        }

        public event EventHandler<InternationalLicenseSelectedEventArgs> OnInternationalLicenseSelected;

        public void  RaiseOnInternationalLicenseSelected(int InternationalLicenseID)
        {
            RaiseOnInternationalLicenseSelected(new InternationalLicenseSelectedEventArgs(InternationalLicenseID));
        }
        protected virtual void RaiseOnInternationalLicenseSelected( InternationalLicenseSelectedEventArgs e)
        {
            OnInternationalLicenseSelected?.Invoke(this, e);
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

        private int _InternationalLicenseID = -1;
        public int InternationalLicenseID { get { return ctrlDriverInternationalLicenseInfo1.InternationalLicenseID; } }
        public clsInternationalLicense SelectedInternationalLicenseInfo { get { return ctrlDriverInternationalLicenseInfo1.SelectedInternationalLicenseInfo; } }
        public ctrlDriverInternationalLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void txtInternationalLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));

            if (e.KeyChar == (char)13)
                btnFindInternationalLicense.PerformClick();
        }

        private void txtInternationalLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInternationalLicenseID.Text))
            {
                e.Cancel = true;
                txtInternationalLicenseID.Focus();
                errorProvider1.SetError(txtInternationalLicenseID, "This field is required !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtInternationalLicenseID, null);
            }
        }
        public void txtInternationalLicenseIDFocus()
        {
            txtInternationalLicenseID.Focus();
        }

        private void ctrlDriverInternationalLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.ResetInternationalLicenseInfo();
            txtInternationalLicenseIDFocus();
        }

        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {
            txtInternationalLicenseID.Text = InternationalLicenseID.ToString();
            _FindNow();
        }
        private void _FindNow()
        {
            _InternationalLicenseID = int.Parse(txtInternationalLicenseID.Text.Trim());
            ctrlDriverInternationalLicenseInfo1.LoadInternationalLicenseInfo(_InternationalLicenseID);

            if (OnInternationalLicenseSelected != null && EnableFilter)
                RaiseOnInternationalLicenseSelected(ctrlDriverInternationalLicenseInfo1.InternationalLicenseID);
        }

        private void btnFindInternationalLicense_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("filed is not valide!, put the mouse over the red icon to see the error", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInternationalLicenseIDFocus();
                return;
            }
            _FindNow();
        }
    }
}
