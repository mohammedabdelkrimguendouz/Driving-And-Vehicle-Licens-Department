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

namespace DVLD.Violations
{
    public partial class frmListViolations : Form
    {
        private static DataTable _dtAllViolations;
        public frmListViolations()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListViolations_Load(object sender, EventArgs e)
        {
            _dtAllViolations = clsViolation.GetAllViolations();
            dgvViolations.DataSource = _dtAllViolations;
            lblRecordsCount.Text = dgvViolations.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;
            if (dgvViolations.Rows.Count > 0)
            {
                dgvViolations.Columns["ViolationID"].HeaderText = "ID";
                dgvViolations.Columns["ViolationID"].Width = 40;


                dgvViolations.Columns["ViolationTitle"].HeaderText = "Title";
                dgvViolations.Columns["ViolationTitle"].Width = 85;

                dgvViolations.Columns["ViolationDescription"].HeaderText = "Description";
                dgvViolations.Columns["ViolationDescription"].Width = 150;



                dgvViolations.Columns["FineFees"].HeaderText = "Fine Fees";
                dgvViolations.Columns["FineFees"].Width = 100;
            }
        }

        private void editViolationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateViolation frm = new frmUpdateViolation((int)
                dgvViolations.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListViolations_Load(null, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllViolations.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvViolations.Rows.Count.ToString();
     
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "ID":
                    FilterColumn = "ViolationID";
                    break;
                case "Title":
                    FilterColumn = "ViolationTitle";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "")
                _dtAllViolations.DefaultView.RowFilter = "";
            else if (FilterColumn == "ViolationTitle")
                _dtAllViolations.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllViolations.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvViolations.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }
    }
}
