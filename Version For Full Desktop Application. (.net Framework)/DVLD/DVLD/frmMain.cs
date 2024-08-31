using DVLD.Applications;
using DVLD.Applications.Application_Types;
using DVLD.Applications.International_Licenses;
using DVLD.Applications.Local_Driving_License_Applications;
using DVLD.Applications.ReleaseDetainedLicense;
using DVLD.Applications.Renew_International_License;
using DVLD.Applications.Renew_Local_License;
using DVLD.Applications.Replacement_for_Damaged_or_Lost__International_Licenses;
using DVLD.Applications.Replacement_for_Damaged_or_Lost_Licenses;
using DVLD.Backups;
using DVLD.Drivers;
using DVLD.Global_Classes;
using DVLD.Licenses.Detaind_Licenses;
using DVLD.People;
using DVLD.Tests.TestTypes;
using DVLD.Users;
using DVLD.Violations;
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

namespace DVLD
{
    public partial class frmMain : Form
    {
        private frmLogin _frmLogin;
        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent();
            _frmLogin=frmLogin;
        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        
        private void singOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frm = new frmShowUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
        }

        private void ApplicationTypestoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void TestTypestoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

       

       

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm= new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

      

       

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseApplications frm = new frmListInternationalLicenseApplications();
            frm.ShowDialog();
        }
       
        private void createBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateBackup frm = new frmCreateBackup();
            frm.ShowDialog();
        }

        private void restoreBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRestoreBackup frm = new frmRestoreBackup();
            frm.ShowDialog();
        }

        private void NewlocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void NewinternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void RenewlocalLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void ReplacementlocalLicenseToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmReplacementDrivingLicenseDamagedOrLost frm = new frmReplacementDrivingLicenseDamagedOrLost();
            frm.ShowDialog();
        }

        private void RenewInternationalLicenseToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmRenewInternationalLicenseApplication frm = new frmRenewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void ReplacementInternationalLicenseToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmReplacementInternationalLicenseDamagedOrLost frm = new frmReplacementInternationalLicenseDamagedOrLost();
            frm.ShowDialog();
        }

        private void manageViolationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListViolations frm = new frmListViolations();
            frm.ShowDialog();
        }
    }
}
