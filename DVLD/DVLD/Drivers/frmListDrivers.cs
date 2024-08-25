using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses.Controls;
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

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        private static DataTable _dtAllDrivers;

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _dtAllDrivers = clsDriver.GetAllLDrivers();
            dgvDrivers.DataSource = _dtAllDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;
            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns["DriverID"].HeaderText = "Driver ID";
                dgvDrivers.Columns["DriverID"].Width = 60;

                dgvDrivers.Columns["PersonID"].HeaderText = "Person ID";
                dgvDrivers.Columns["PersonID"].Width = 60;

                dgvDrivers.Columns["NationalNo"].HeaderText = "National No";
                dgvDrivers.Columns["NationalNo"].Width = 90;

                dgvDrivers.Columns["FullName"].HeaderText = "Full Name";
                dgvDrivers.Columns["FullName"].Width = 130;

                dgvDrivers.Columns["CreatedDate"].HeaderText = "Date";
                dgvDrivers.Columns["CreatedDate"].Width = 70;

                dgvDrivers.Columns["NumberOfActiveLicenses"].HeaderText = "Active Licenses";
                dgvDrivers.Columns["NumberOfActiveLicenses"].Width = 110;

                
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Driver ID"|| cbFilter.Text == "Person ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            
            _dtAllDrivers.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

            }
            if (txtFilterValue.Text.Trim() == "")
                _dtAllDrivers.DefaultView.RowFilter = "";
            else if (FilterColumn == "NationalNo"|| FilterColumn=="FullName")
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(
                (int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

        }

        private void showPersonLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
