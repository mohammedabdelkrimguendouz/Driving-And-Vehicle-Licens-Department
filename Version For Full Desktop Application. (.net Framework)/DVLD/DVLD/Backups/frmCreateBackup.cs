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

namespace DVLD.Backups
{
    public partial class frmCreateBackup : Form
    {
        public frmCreateBackup()
        {
            InitializeComponent();
        }

        private void frmCreateBackup_Load(object sender, EventArgs e)
        {

        }

        private void txtFilePath_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text.Trim()))
            {
                e.Cancel = true;
                btnChoose.Focus();
                errorProvider1.SetError(txtFilePath, "Please Choose File Path");
            }
            else
            {
                errorProvider1.SetError(txtFilePath, null);
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.DefaultExt = "bak";
            saveFileDialog1.Filter = "Bakup Files(*.bak)|*.bak";
            saveFileDialog1.Title = "Save a Backup File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = saveFileDialog1.FileName;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateBackup_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some filed are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(clsBackup.CreateBackup(txtFilePath.Text))
                MessageBox.Show("Create Backup Successfully ", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(" Faild To Create Backup ", "faild",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
