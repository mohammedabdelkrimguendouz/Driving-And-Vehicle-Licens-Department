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

namespace DVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        private static DataTable _dtAllApplicationTypes;

    

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns["ApplicationTypeID"].HeaderText = "ID";
                dgvApplicationTypes.Columns["ApplicationTypeID"].Width = 50;


                dgvApplicationTypes.Columns["ApplicationTypeTitle"].HeaderText = "Title";
                dgvApplicationTypes.Columns["ApplicationTypeTitle"].Width = 190;



                dgvApplicationTypes.Columns["ApplicationFees"].HeaderText = "Fees";
                dgvApplicationTypes.Columns["ApplicationFees"].Width = 90;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationTypes_Load(null,null);
        }
    }
}
