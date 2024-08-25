using DVLD.Global;
using DVLD.Global_Classes;
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

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(true, "PersonID","NationalNo","FirstName","SecondName",
            "ThirdName","LastName","GenderCaption","DateOfBirth","CountryName","Phone","Email");

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void _RefreshListPeople()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
             _dtPeople = _dtAllPeople.DefaultView.ToTable(true, "PersonID", "NationalNo", "FirstName", "SecondName",
            "ThirdName", "LastName", "GenderCaption", "DateOfBirth", "CountryName", "Phone", "Email");
            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;
            if(dgvPeople.Rows.Count>0)
            {
                dgvPeople.Columns["PersonID"].HeaderText = "Person ID";
                dgvPeople.Columns["PersonID"].Width = 100;

                dgvPeople.Columns["NationalNo"].HeaderText = "National No";
                dgvPeople.Columns["NationalNo"].Width = 105;

                dgvPeople.Columns["FirstName"].HeaderText = "First Name";
                dgvPeople.Columns["FirstName"].Width = 110;

                dgvPeople.Columns["LastName"].HeaderText = "Last Name";
                dgvPeople.Columns["LastName"].Width = 110;

                dgvPeople.Columns["SecondName"].HeaderText = "Second Name";
                dgvPeople.Columns["SecondName"].Width = 110;

                dgvPeople.Columns["ThirdName"].HeaderText = "Third Name";
                dgvPeople.Columns["ThirdName"].Width = 110;

                dgvPeople.Columns["GenderCaption"].HeaderText = "Gender";
                dgvPeople.Columns["GenderCaption"].Width = 70;

                dgvPeople.Columns["CountryName"].HeaderText = "Nationality";
                dgvPeople.Columns["CountryName"].Width = 95;

         

                dgvPeople.Columns["Phone"].HeaderText = "Phone";
                dgvPeople.Columns["Phone"].Width = 120;

                dgvPeople.Columns["Email"].HeaderText = "Email";
                dgvPeople.Columns["Email"].Width = 125;

                dgvPeople.Columns["DateOfBirth"].HeaderText = "Date Of Birth";
                dgvPeople.Columns["DateOfBirth"].Width = 130;
            }
        }

       
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            clsPerson Person = clsPerson.Find(PersonID);
            if (MessageBox.Show("Are you sure do you want to delete Person [" + PersonID + "]", "Confirm Delete", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsUtil.DeleteImageFromProjectImagesFolder(Person.ImagePath)&&clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Person Deleted Successfully", "Successful", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    _RefreshListPeople();
                }
                else
                {
                    MessageBox.Show("Person Was not deleted because it has data linked to it", "Error Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSendEmail frm = new frmSendEmail((string)dgvPeople.CurrentRow.Cells[10].Value);
            frm.ShowDialog();
        }

        

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID    );
            frm.ShowDialog();
            _RefreshListPeople();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshListPeople();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbGender.Visible = (cbFilter.Text == "Gender");
            if (cbGender.Visible)
                cbGender.SelectedIndex = 0;

            txtFilterValue.Visible = (cbFilter.Text != "Gender") && (cbFilter.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllPeople.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Nationality":
                    FilterColumn = "CountryName";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
               
            }

            if(txtFilterValue.Text.Trim()=="") 
                _dtPeople.DefaultView.RowFilter = "";
            else if (FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter =  string.Format("[{0}]={1}",FilterColumn,txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] Like '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshListPeople();
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterIColumn = "GenderCaption";
            string FilterValue = cbGender.Text.Trim();
            if (cbGender.Text == "All")
                _dtPeople.DefaultView.RowFilter = "";
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}]= '{1}'", FilterIColumn, FilterValue);
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshListPeople();
        }
    }
}
