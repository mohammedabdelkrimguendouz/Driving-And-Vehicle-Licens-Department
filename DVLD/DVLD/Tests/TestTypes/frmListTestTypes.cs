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

namespace DVLD.Tests.TestTypes
{
    public partial class frmListTestTypes : Form
    {
        private static DataTable _dtAllTestTypes;

       
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns["TestTypeID"].HeaderText = "ID";
                dgvTestTypes.Columns["TestTypeID"].Width = 40;


                dgvTestTypes.Columns["TestTypeTitle"].HeaderText = "Title";
                dgvTestTypes.Columns["TestTypeTitle"].Width = 85;

                dgvTestTypes.Columns["TestTypeDescription"].HeaderText = "Description";
                dgvTestTypes.Columns["TestTypeDescription"].Width = 150;



                dgvTestTypes.Columns["TestTypeFees"].HeaderText = "Fees";
                dgvTestTypes.Columns["TestTypeFees"].Width = 100;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestTypes_Load(null, null);
        }
    }
}
