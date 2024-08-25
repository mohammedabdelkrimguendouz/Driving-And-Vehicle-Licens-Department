using DVLD.Applications.ReleaseDetainedLicense;
using DVLD.Licenses.Local_Licenses;
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
using System.Xml.Linq;

namespace DVLD.Licenses.Detaind_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {
        private static DataTable _dtAllDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dtAllDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvListDetainedLicenses.DataSource = _dtAllDetainedLicenses;
            lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;
            if (dgvListDetainedLicenses.Rows.Count > 0)
            {
                dgvListDetainedLicenses.Columns["DetainID"].HeaderText = "Detain ID";
                dgvListDetainedLicenses.Columns["DetainID"].Width = 60;

                dgvListDetainedLicenses.Columns["LicenseID"].HeaderText = "License ID";
                dgvListDetainedLicenses.Columns["LicenseID"].Width = 60;

                dgvListDetainedLicenses.Columns["DetainDate"].HeaderText = "Detain Date";
                dgvListDetainedLicenses.Columns["DetainDate"].Width = 75;

                dgvListDetainedLicenses.Columns["ViolationTitle"].HeaderText = "Violation Title";
                dgvListDetainedLicenses.Columns["ViolationTitle"].Width = 130;
                

                dgvListDetainedLicenses.Columns["IsReleased"].HeaderText = "Is Released";
                dgvListDetainedLicenses.Columns["IsReleased"].Width = 50;

                dgvListDetainedLicenses.Columns["FineFees"].HeaderText = "Fine Fees";
                dgvListDetainedLicenses.Columns["FineFees"].Width = 60;

                dgvListDetainedLicenses.Columns["ReleaseDate"].HeaderText = "Release Date";
                dgvListDetainedLicenses.Columns["ReleaseDate"].Width = 75;

                dgvListDetainedLicenses.Columns["NationalNo"].HeaderText = "National No";
                dgvListDetainedLicenses.Columns["NationalNo"].Width = 100;

                dgvListDetainedLicenses.Columns["FullName"].HeaderText = "Full Name";
                dgvListDetainedLicenses.Columns["FullName"].Width = 140;

                dgvListDetainedLicenses.Columns["ReleaseApplicationID"].HeaderText = "R.App ID";
                dgvListDetainedLicenses.Columns["ReleaseApplicationID"].Width = 85;

            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Detain ID"|| cbFilter.Text=="Release Application ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIsReleased.Visible = (cbFilter.Text == "Is Released") ;
            if (cbIsReleased.Visible)
                cbIsReleased.SelectedIndex = 0;

            txtFilterValue.Visible = (cbFilter.Text != "Is Released") && (cbFilter.Text != "None");
            if(txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllDetainedLicenses.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString(); 
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Violation Title":
                    FilterColumn = "ViolationTitle";
                    break;
                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

            }
            
            if (txtFilterValue.Text.Trim() == "")
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
            else if (FilterColumn == "ReleaseApplicationID"|| FilterColumn == "DetainID")
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterIColumn = "IsReleased";
            string FilterValue = "";

            switch (cbIsReleased.Text)
            {
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (cbIsReleased.Text.Trim() == "All")
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
            else
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterIColumn, FilterValue);
            lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find((string)dgvListDetainedLicenses.CurrentRow.Cells[7].Value).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value;
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find((string)dgvListDetainedLicenses.CurrentRow.Cells[7].Value).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value;
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(LicenseID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void cmsDetainedLicenses_Opening(object sender, CancelEventArgs e)
        {
            bool IsReleased = (bool)dgvListDetainedLicenses.CurrentRow.Cells[4].Value;
            
            releaseDetainedLicenseToolStripMenuItem.Enabled = !IsReleased;
        }
    }
}
