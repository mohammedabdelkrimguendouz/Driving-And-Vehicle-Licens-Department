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
using static DVLD_Buisness.clsTestType;

namespace DVLD.Violations
{
    public partial class frmUpdateViolation : Form
    {
        private int _ViolationID = -1;
        private clsViolation _Violation;
        public frmUpdateViolation(int ViolationID)
        {
            InitializeComponent();
            _ViolationID = ViolationID;     
        }

        private void frmUpdateViolation_Load(object sender, EventArgs e)
        {
            _Violation = clsViolation.Find(_ViolationID);
            if (_Violation == null)
            {
                MessageBox.Show("this form well be closed because No Violation  With ID : " + _ViolationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            txtFineFees.Text = _Violation.FineFees.ToString();
            txtTitle.Text = _Violation.ViolationTitle;
            txtDescription.Text = _Violation.ViolationDescription;
            lblViolationID.Text = _Violation.ViolationID.ToString();
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

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "This field is required !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDescription, null);
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "This field is required !");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, null);
            }
            if (!clsValidation.IsNumber(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Violation.ViolationTitle = txtTitle.Text.Trim();
            _Violation.ViolationDescription = txtDescription.Text.Trim();
            _Violation.FineFees = Convert.ToSingle(txtFineFees.Text.Trim());
            if (_Violation.Save())
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
    }
}
