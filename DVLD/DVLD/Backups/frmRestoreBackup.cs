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
    public partial class frmRestoreBackup : Form
    {
        public frmRestoreBackup()
        {
            InitializeComponent();
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
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.DefaultExt = "bak";
            openFileDialog1.Filter = "Bakup Files(*.bak)|*.bak";
            openFileDialog1.Title = "Open a Backup File";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestoreBackup_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some filed are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsBackup.RestoreBackup(txtFilePath.Text))
                MessageBox.Show("Restore Backup Successfully ", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(" Faild To Restore Backup ", "faild",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
