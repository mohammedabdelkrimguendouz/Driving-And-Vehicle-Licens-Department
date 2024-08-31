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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Violations
{
    public partial class frmChooseViolation : Form
    {
        public delegate void DataBackEventHandler(object sender, int ViolationID,float FineFees);

        public event DataBackEventHandler DataBack;
        private clsViolation _Violation;
        public frmChooseViolation()
        {
            InitializeComponent();
        }
        private void _FillViolationsInCompoBox()
        {
            DataTable TableViolation = clsViolation.GetAllViolations();
            foreach (DataRow Row in TableViolation.Rows)
            {
                cbViolation.Items.Add(Row["ViolationTitle"]);
            }
            cbViolation.SelectedIndex = 0;
        }
        private void frmChooseViolation_Load(object sender, EventArgs e)
        {
            _FillViolationsInCompoBox();
            cbViolation_SelectedIndexChanged(null,null);
        }

        private void cbViolation_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Violation = clsViolation.Find(cbViolation.Text);
            if(_Violation==null)
            {
                MessageBox.Show("No _Violation With Title = " + cbViolation.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblViolationID.Text=_Violation.ViolationID.ToString();
            txtDescription.Text = _Violation.ViolationDescription;
            lblFineFees.Text = _Violation.FineFees.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            DataBack?.Invoke(this, _Violation.ViolationID, _Violation.FineFees);
            this.Close();
        }
    }
}
