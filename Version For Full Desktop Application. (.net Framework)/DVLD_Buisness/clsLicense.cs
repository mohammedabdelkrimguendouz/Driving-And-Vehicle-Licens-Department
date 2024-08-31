using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsApplication;

namespace DVLD_Buisness
{
    public class clsLicense
    {
        public enum enIssueReason
        {
            FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4
        };

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int LicenseID {  get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo;
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int LicenseClass { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(IssueReason);
            }
        }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        public clsDetainedLicense DetainedInfo;
        public bool IsDetained
        {
            get
            {
                return clsDetainedLicense.IsLicenseDetained(this.LicenseID);
            }
        }

        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsLicense(int LicenseID, int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive
            , enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplication.FindBaseApplication(ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.LicenseClass = LicenseClass;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClass);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.DetainedInfo=clsDetainedLicense.FindByLicenseID(LicenseID);
            Mode = enMode.Update;
        }

        public static clsLicense FindByLicenseID(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID=-1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";float PaidFees = 0;bool IsActive = false;byte IssueReason = 1;

            if(clsLicenseData.GetLicenseInfoByID(LicenseID,ref ApplicationID,ref DriverID,
                ref LicenseClass,ref IssueDate,ref ExpirationDate,ref Notes,ref PaidFees,ref IsActive
                ,ref IssueReason,ref CreatedByUserID))
            {
                return new clsLicense(LicenseID,ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,
                    Notes,PaidFees,IsActive,(enIssueReason)IssueReason,CreatedByUserID);
            }
            return null;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID,this.DriverID,this.LicenseClass,this.IssueDate,
                this.ExpirationDate,this.Notes,this.PaidFees,this.IsActive,(byte)this.IssueReason,this.CreatedByUserID);

            return (this.LicenseID != -1) ;
        }
        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID,this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
                this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLicense();

            }
            return false;
        }


        
        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
        public static DataTable GetDriverLocalLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLocalLicenses(DriverID);
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }
        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1;
        }

        public  bool IsLicenseExpired() 
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool DeactivateCurrentLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
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

        public clsLicense RenewLicense(int CreatedByUserID,string Notes)
        {
            clsApplication Application = new clsApplication();
            
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID =(int) clsApplication.enApplicationType.RenewDrivingLicense;
            Application.LastStatusDate = DateTime.Now;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;
            if (!Application.Save())
                return null;
            

            clsLicense  NewLicense = new clsLicense();
            NewLicense.ApplicationID =Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (! NewLicense.Save())
                return null;

            this.DeactivateCurrentLicense();

            return  NewLicense;
        }

        public int DetainLicense(int CreatedByUserID, float FineFees,int ViolationID)
        {
            clsDetainedLicense DetainedLicense = new clsDetainedLicense();
            DetainedLicense.DetainDate = DateTime.Now;
            DetainedLicense.FineFees = FineFees;
            DetainedLicense.ViolationID = ViolationID;
            DetainedLicense.CreatedByUserID = CreatedByUserID;
            DetainedLicense.LicenseID = this.LicenseID;
            if (!DetainedLicense.Save())
                return -1;
            return DetainedLicense.DetainID;
        }

        public clsLicense ReplaceLicense(int CreatedByUserID, enIssueReason IssueReason)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID=(IssueReason == enIssueReason.DamagedReplacement)?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense:
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.PaidFees = clsApplicationType.Find(Application.ApplicationTypeID).ApplicationFees;
            Application.LastStatusDate = DateTime.Now;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            
            Application.CreatedByUserID = CreatedByUserID;
            if (!Application.Save())
                return null;


            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = this.PaidFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
                return null;

            this.DeactivateCurrentLicense();

            return NewLicense;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID ,ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.LastStatusDate = DateTime.Now;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;
            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }
                
            ApplicationID = Application.ApplicationID;

            
            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID,ApplicationID);
        }

        

    }
}
