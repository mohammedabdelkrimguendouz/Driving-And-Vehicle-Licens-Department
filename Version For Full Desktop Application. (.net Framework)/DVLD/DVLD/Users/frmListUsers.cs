using DVLD.Global;
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

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers;
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _RefreshListUsers()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;   
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;
            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns["PersonID"].HeaderText = "Person ID";
                dgvUsers.Columns["PersonID"].Width = 80;

                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["UserID"].Width = 80;

                dgvUsers.Columns["FullName"].HeaderText = "Full Name";
                dgvUsers.Columns["FullName"].Width = 190;

                dgvUsers.Columns["UserName"].HeaderText = "User Name";
                dgvUsers.Columns["UserName"].Width = 120;

                dgvUsers.Columns["IsActive"].HeaderText = "Is Active";
                dgvUsers.Columns["IsActive"].Width = 80;

            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Phone Call Was Not Implemented ! ", "Werning",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

       

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure do you want to delete User [" + UserID + "]", "Confirm Delete", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("User Deleted Successfully", "Successful", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    _RefreshListUsers();
                }
                else
                {
                    MessageBox.Show("User Was not deleted because it has data linked to it", "Error Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmShowUserInfo frm = new frmShowUserInfo(UserID);
            frm.ShowDialog();
            _RefreshListUsers();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID" || cbFilter.Text == "User ID")
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

            _dtAllUsers.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "")
                _dtAllUsers.DefaultView.RowFilter = "";
            else if (FilterColumn == "PersonID" || FilterColumn=="UserID")
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
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
            if (cbIsActive.Text == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterIColumn, FilterValue);
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshListUsers();
        }

        

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            _RefreshListUsers();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            _RefreshListUsers();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSendEmail frm = new frmSendEmail( clsPerson.Find((int)dgvUsers.CurrentRow.Cells[1].Value).Email);
            frm.ShowDialog();
        }
    }
}
