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

namespace DVLD.Applications.Application_Types
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicationTypeID=-1;
        private clsApplicationType _ApplicationType;
        public frmUpdateApplicationType(int ApplicationType)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationType;
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);
            if (_ApplicationType == null)
            {
                MessageBox.Show("this form well be closed because No Application Type With ID : " + _ApplicationTypeID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();
            txtTitle.Text=_ApplicationType.ApplicationTypeTitle.ToString();
            lblApplicationTypeID.Text = _ApplicationType.ApplicationTypeID.ToString();
        }

      
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationFees =Convert.ToSingle(txtFees.Text.Trim());
            if(_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully ", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data is not Saved Successfully ", "Error ",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This field is required !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "This field is required !");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }
            if (!clsValidation.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };
        }
    }
}
