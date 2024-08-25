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

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmShowLocalDrivingLicenseApplicationInfo : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        public frmShowLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseApplicationInfo1.
            LoadApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
        }
    }
}
