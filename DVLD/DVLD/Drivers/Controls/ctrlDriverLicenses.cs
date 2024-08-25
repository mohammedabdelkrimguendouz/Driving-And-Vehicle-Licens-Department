using DVLD.Licenses.International_License;
using DVLD.Licenses.Local_Licenses;
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
    public partial class ctrlDriverLicenses : UserControl
    {
        private static DataTable _dtDriverLocalLicensesHistory;
        private static DataTable _dtDriverInternationalLicensesHistory;
        private int _DriverID = -1;
        private clsDriver _Driver;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        private void _LoadLoacalLicencesInfo()
        {
            _dtDriverLocalLicensesHistory = clsLicense.GetDriverLocalLicenses(_DriverID);
            dgvLocalLicenses.DataSource = _dtDriverLocalLicensesHistory;
            lblRecordsCountLocalLicense.Text = dgvLocalLicenses.Rows.Count.ToString();
            tcDriverLicenses.SelectedTab = tcDriverLicenses.TabPages["tpLocalLicenses"];
            if (dgvLocalLicenses.Rows.Count > 0)
            {
                
                dgvLocalLicenses.Columns["LicenseID"].HeaderText = "Lic.ID";
                dgvLocalLicenses.Columns["LicenseID"].Width = 70;

                dgvLocalLicenses.Columns["ApplicationID"].HeaderText = "App.ID";
                dgvLocalLicenses.Columns["ApplicationID"].Width = 70;

                dgvLocalLicenses.Columns["ClassName"].HeaderText = "Class Name";
                dgvLocalLicenses.Columns["ClassName"].Width = 150;

                dgvLocalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns["IssueDate"].Width = 80;

                dgvLocalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns["ExpirationDate"].Width = 80;

                dgvLocalLicenses.Columns["IsActive"].HeaderText = "Is Active";
                dgvLocalLicenses.Columns["IsActive"].Width = 75;
            }
        }
        private void _LoadInternationalLicencesInfo()
        {
            _dtDriverInternationalLicensesHistory = clsLicense.GetInternationalLicenses(_DriverID);
            dgvInternationalLicenses.DataSource = _dtDriverInternationalLicensesHistory;
            lblRecordsCountInternationalLicense.Text = dgvInternationalLicenses.Rows.Count.ToString();
            
           
            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns["InternationalLicenseID"].HeaderText = "Int.Lic.ID";
                dgvInternationalLicenses.Columns["InternationalLicenseID"].Width = 70;

                dgvInternationalLicenses.Columns["ApplicationID"].HeaderText = "App.ID";
                dgvInternationalLicenses.Columns["ApplicationID"].Width = 60;

                dgvInternationalLicenses.Columns["DriverID"].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns["DriverID"].Width = 60;

                dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License.ID";
                dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].Width = 60;

                dgvInternationalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns["IssueDate"].Width = 110;

                dgvInternationalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns["ExpirationDate"].Width = 110;

                dgvInternationalLicenses.Columns["IsActive"].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns["IsActive"].Width = 90;
            }
            
        }
        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            clsDriver Driver = clsDriver.FindByDriverID(_DriverID);
            if( Driver == null )
            {
                MessageBox.Show("Could not find  driver with id = "+_DriverID, " Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _DriverID = -1;
                return;
            }
            _LoadInternationalLicencesInfo();
            _LoadLoacalLicencesInfo();
            
            
        }
        public void LoadInfoByPersonID(int PersonID)
        {
            
            clsDriver Driver = clsDriver.FindByPersonID(PersonID);
            if (Driver == null)
            {
                MessageBox.Show("Could not find  driver linked with person id =  "+PersonID, " Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _DriverID = -1;
                return;
            }
            _DriverID = Driver.DriverID;
            _LoadInternationalLicencesInfo();
            _LoadLoacalLicencesInfo();
        }

        public void Clear()
        {
            if(_dtDriverInternationalLicensesHistory!=null)
               _dtDriverInternationalLicensesHistory.Clear();
            if (_dtDriverLocalLicensesHistory != null)
                _dtDriverLocalLicensesHistory.Clear();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(
                (int)dgvLocalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void InternationalLicenseInfotoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowDriverInternationalLicenseInfo frm = new frmShowDriverInternationalLicenseInfo(
               (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

    }
}