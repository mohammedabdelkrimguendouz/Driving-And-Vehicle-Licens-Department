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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.PsychologicalTest;
        private int _TestAppointmentID = -1;
        private clsTest _Test;
        private int _TestID;
        public frmTakeTest(int TestAppointmentID,clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestTypeID;
            ctrlScheduledTest1.LoadInfo(_TestAppointmentID);

            btnSave.Enabled = ctrlScheduledTest1.TestAppointmentID != -1;

            _TestID = ctrlScheduledTest1.TestID;
            if (_TestID!= -1)
            {
                _Test = clsTest.Find(_TestID);
                if (_Test.TestResult)
                    rbTestPass.Checked = true;
                else
                    rbTestFail.Checked = true;

                txtNotes.Text = _Test.Notes;
                lblUserMessage.Visible = true;
                rbTestPass.Enabled= rbTestFail.Enabled = false;
            }
            else
                _Test = new clsTest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? Afther that you cannot change\n " +
                    "the Pass/Fail result afthr you save ? ", "Confim", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
           
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.TestResult = rbTestPass.Checked;
            _Test.TestAppointmentID = _TestAppointmentID;
             
             if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully ", "Saved",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
                     
             else
                    MessageBox.Show("Data is not Saved Successfully ", "Error ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                     
                
        }
    }
}
