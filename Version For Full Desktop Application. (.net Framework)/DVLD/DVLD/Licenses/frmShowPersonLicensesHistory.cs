using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmShowPersonLicensesHistory : Form
    {
        private int _PersonID=-1;
        public frmShowPersonLicensesHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }
        public frmShowPersonLicensesHistory()
        {
            InitializeComponent();
        }

       
        private void frmShowPersonLicensesHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.EnableFilter = false;
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.EnableFilter = true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(object sender, People.Controls.ctrlPersonCardWithFilter.PersonSelectedEventArgs e)
        {
            _PersonID = e.PersonID;
            if (ctrlPersonCardWithFilter1.PersonID == -1)
                ctrlDriverLicenses1.Clear();
            else
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
        }
    }
}
