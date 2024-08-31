using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsInternationalLicense;

namespace DVLD_Buisness
{
    public class clsInternationalLicense:clsApplication
    {
        public enum enIssueReason
        {
            FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4
        };
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }


        
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
       
        public byte DefaultValidityLength
        {
            get
            {
                return clsSetting.GetDefaultValidityLengthForAnInternationalLicense();
            }
        }

        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                switch (IssueReason)
                {
                    case enIssueReason.FirstTime:
                        return "First Time";
                    case enIssueReason.Renew:
                        return "Renew";
                    case enIssueReason.LostReplacement:
                        return "Replacement for Lost ";
                    case enIssueReason.DamagedReplacement:
                        return "Replacement for Damaged ";
                    default:
                        return "Unknown";
                };
            }
        }


        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IssueReason = enIssueReason.FirstTime;
            this.IsActive = false;
            Mode = enMode.AddNew;
        }
        private clsInternationalLicense(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, clsApplication.enApplicationStatus ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID,int InternationalLicenseID, int DriverID,
           int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, enIssueReason IssueReason, bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.PaidFees = PaidFees;
            base.ApplicationStatus = ApplicationStatus;
            base.ApplicationDate = ApplicationDate;
            base.CreatedByUserID = CreatedByUserID;
            base.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            base.ApplicationTypeID = ApplicationTypeID;
            base.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            base.ApplicantPersonID = ApplicationPersonID;
            base.ApplicantPersonInfo = clsPerson.Find(ApplicationPersonID);
            base.LastStatusDate = LastStatusDate;

            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IssueReason = IssueReason;
            this.IsActive = IsActive;
            Mode = enMode.Update;
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;byte IssueReason = 1;
            bool IsActive = false;

            bool IsFound = clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive,ref IssueReason
                , ref CreatedByUserID);


            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                if (Application != null)
                    return new clsInternationalLicense(ApplicationID, Application.ApplicantPersonID
                        , Application.ApplicationDate, Application.ApplicationTypeID,
                        (clsApplication.enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate
                        , Application.ApplicationTypeInfo.ApplicationFees, Application.CreatedByUserID, InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate
                    ,(enIssueReason)IssueReason, IsActive);
            }

            return null;






            

        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate,
                this.ExpirationDate, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }
        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate,
                this.ExpirationDate, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public bool Save()
        {

            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateInternationalLicense();

            }
            return false;
        }

        public bool IsInternationalLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool DeactivateCurrentInternationalLicense()
        {
            return clsInternationalLicenseData.DeactivateInternationalLicense(this.InternationalLicenseID);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
        
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public clsInternationalLicense RenewInternationalLicense(int CreatedByUserID)
        {
            clsInternationalLicense NewInternationalLicense = new clsInternationalLicense();

            NewInternationalLicense.DriverID = this.DriverID;
            NewInternationalLicense.IssuedUsingLocalLicenseID = this.IssuedUsingLocalLicenseID;
            NewInternationalLicense.IssueDate = DateTime.Now;
            NewInternationalLicense.ExpirationDate = DateTime.Now.AddYears(this.DefaultValidityLength);

            NewInternationalLicense.IsActive = true;
            NewInternationalLicense.IssueReason = clsInternationalLicense.enIssueReason.Renew;

            NewInternationalLicense.ApplicantPersonID = this.DriverInfo.PersonID;
            NewInternationalLicense.ApplicationDate = DateTime.Now;
            NewInternationalLicense.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewInternationalLicense;
            NewInternationalLicense.LastStatusDate = DateTime.Now;
            NewInternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            NewInternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewInternationalLicense).ApplicationFees;
            NewInternationalLicense.CreatedByUserID = CreatedByUserID;
           


            

            if (!NewInternationalLicense.Save())
                return null;

            this.DeactivateCurrentInternationalLicense();

            return NewInternationalLicense;
        }

        public clsInternationalLicense ReplaceInternationalLicense(int CreatedByUserID,clsInternationalLicense.enIssueReason IssueReason)
        {
            clsInternationalLicense NewInternationalLicense = new clsInternationalLicense();

            NewInternationalLicense.DriverID = this.DriverID;
            NewInternationalLicense.IssuedUsingLocalLicenseID = this.IssuedUsingLocalLicenseID;
            NewInternationalLicense.IssueDate = DateTime.Now;
            NewInternationalLicense.ExpirationDate = this.ExpirationDate;

            NewInternationalLicense.IsActive = true;
            NewInternationalLicense.IssueReason = IssueReason;

            NewInternationalLicense.ApplicantPersonID = this.DriverInfo.PersonID;
            NewInternationalLicense.ApplicationDate = DateTime.Now;
            NewInternationalLicense.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedInternationalLicense :
                (int)clsApplication.enApplicationType.ReplaceLostInternationalLicense;
            NewInternationalLicense.LastStatusDate = DateTime.Now;
            NewInternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            NewInternationalLicense.PaidFees = clsApplicationType.Find(NewInternationalLicense.ApplicationTypeID).ApplicationFees;
            NewInternationalLicense.CreatedByUserID = CreatedByUserID;


            if (!NewInternationalLicense.Save())
                return null;

            this.DeactivateCurrentInternationalLicense();

            return NewInternationalLicense;
        }
    }
}
