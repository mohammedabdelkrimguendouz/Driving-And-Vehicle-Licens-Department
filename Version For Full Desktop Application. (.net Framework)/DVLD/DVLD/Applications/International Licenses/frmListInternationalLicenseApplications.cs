using DVLD.Licenses;
using DVLD.Licenses.International_License;
using DVLD.People;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_Licenses
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        private static DataTable _dtAllInternationalLicenses;
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtAllInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();
            dgvInternationalLicenses.DataSource = _dtAllInternationalLicenses;
            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

            cbFilter.SelectedIndex = 0;
            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns["InternationalLicenseID"].HeaderText = "Int.Lic.ID";
                dgvInternationalLicenses.Columns["InternationalLicenseID"].Width = 100;

                dgvInternationalLicenses.Columns["ApplicationID"].HeaderText = "App.ID";
                dgvInternationalLicenses.Columns["ApplicationID"].Width = 100;

                dgvInternationalLicenses.Columns["DriverID"].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns["DriverID"].Width = 100;

                dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License.ID";
                dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].Width = 100;

                dgvInternationalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns["IssueDate"].Width = 120;

                dgvInternationalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns["ExpirationDate"].Width = 120;

                dgvInternationalLicenses.Columns["IsActive"].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns["IsActive"].Width = 80;
            }
        }

        private void btnAddNewInternationalLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            frmListInternationalLicenseApplications_Load(null,null);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));

        }
       
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIsActive.Visible = (cbFilter.Text == "Is Active");
            if (cbIsActive.Visible)
                cbIsActive.SelectedIndex = 0;

            txtFilterValue.Visible = (cbFilter.Text != "Is Active") && (cbFilter.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllInternationalLicenses.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "")
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
            else
               _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterIColumn = "IsActive";
            string FilterValue = "";

            switch (cbIsActive.Text)
            {
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (cbIsActive.Text.Trim() == "All")
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
            else
                _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterIColumn, FilterValue);
            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverInternationalLicenseInfo frm = new frmShowDriverInternationalLicenseInfo(
               (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
