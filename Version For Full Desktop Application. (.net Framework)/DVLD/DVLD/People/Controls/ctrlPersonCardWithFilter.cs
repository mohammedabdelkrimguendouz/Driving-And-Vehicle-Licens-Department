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

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public class PersonSelectedEventArgs : EventArgs
        {
            public int PersonID { get; }
            public PersonSelectedEventArgs(int PersonID)
            {
                this.PersonID = PersonID;
            }
        }

        public event EventHandler<PersonSelectedEventArgs> OnPersonSelected;

        public void RaiseOnPersonSelected(int PersonID)
        {
            RaiseOnPersonSelected(new PersonSelectedEventArgs(PersonID));
        }
        protected virtual void RaiseOnPersonSelected(PersonSelectedEventArgs e)
        {
            OnPersonSelected?.Invoke(this, e);
        }

        private bool _ShowAddNewPerson = true;
        public bool ShowAddNewPerson
        {
            get { return _ShowAddNewPerson; }
            set 
            { 
                _ShowAddNewPerson = value;
                btnAddNewPerson.Enabled = _ShowAddNewPerson;
            }
        }

      

        private bool _EnableFilter = true;
        public bool EnableFilter
        {
            get { return _EnableFilter; }
            set
            {
                _EnableFilter = value;
                gbFilters.Enabled = _EnableFilter;
            }
        }

        public int PersonID { get { return ctrlPersonCard1.PersonID; } }

        public clsPerson SelectedPersonInfo { get { return ctrlPersonCard1.SelectedPersonInfo; } }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = cbFilter.FindString("Person ID");
            txtFilterValue.Text = PersonID.ToString();
            _FindNow();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            ctrlPersonCard1.ResetPersonInfo();
            cbFilter.SelectedIndex = 0;
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnFindPerson.PerformClick();
            if (cbFilter.Text == "Person ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

     
        private void DataBackEvent(object sender,int PersonID)
        {
            cbFilter.SelectedIndex = cbFilter.FindString("Person ID");
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

       

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterValue.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }
        private void _FindNow()
        {
            if (cbFilter.Text == "Person ID")
                ctrlPersonCard1.LoadPersonInfo(Convert.ToInt32(txtFilterValue.Text.Trim()));
            else
                ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text.Trim());

            if (OnPersonSelected != null && EnableFilter)
                RaiseOnPersonSelected(ctrlPersonCard1.PersonID);
        }

       

        public void FilterFocus()
        {
            
            txtFilterValue.Focus();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("filed is not valide!, put the mouse over the red icon to see the error", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FindNow();
        }
    }
}
