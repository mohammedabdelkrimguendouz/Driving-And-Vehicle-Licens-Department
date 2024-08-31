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

        public LicenseDTO licenseDTO
        {
            get { return new LicenseDTO(this.LicenseID,this.ApplicationID,this.DriverID,this.LicenseClass,
                this.IssueDate,this.ExpirationDate,this.Notes,this.PaidFees,this.IsActive,(byte)this.IssueReason,
                this.CreatedByUserID); }
        }
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


        public clsLicense(LicenseDTO licenseDTO,enMode CreationMode=enMode.AddNew)
        {
            this.LicenseID = licenseDTO.LicenseID;
            this.ApplicationID = licenseDTO.ApplicationID;
            this.ApplicationInfo = clsApplication.FindBaseApplication(ApplicationID);
            this.DriverID = licenseDTO.DriverID;
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.LicenseClass = licenseDTO.LicenseClass;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClass);
            this.IssueDate = licenseDTO.IssueDate;
            this.ExpirationDate = licenseDTO.ExpirationDate;
            this.Notes = licenseDTO.Notes;
            this.PaidFees = licenseDTO.PaidFees;
            this.IsActive = licenseDTO.IsActive;
            this.IssueReason = (enIssueReason)licenseDTO.IssueReason;
            this.CreatedByUserID = licenseDTO.CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.DetainedInfo=clsDetainedLicense.FindByLicenseID(LicenseID);
            Mode = CreationMode;
        }

        public static clsLicense FindByLicenseID(int LicenseID)
        {
            LicenseDTO licenseDTO= clsLicenseData.GetLicenseInfoByID(LicenseID);

            if (licenseDTO!=null)
            {
                return new clsLicense(licenseDTO,enMode.Update);
            }
            return null;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.licenseDTO);

            return (this.LicenseID != -1) ;
        }
        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.licenseDTO);
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



        public static List<InternationalLicensesListDTO> GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
        public static List<DriverLocalLicensesDTO> GetDriverLocalLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLocalLicenses(DriverID);
        }

        public static List<LicenseDTO> GetAllLicenses()
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
            clsApplication Application = new clsApplication(new ApplicationDTO(-1, this.DriverInfo.PersonID,
                DateTime.Now, (int)clsApplication.enApplicationType.RenewDrivingLicense,DateTime.Now,
                (int)clsApplication.enApplicationStatus.Completed,
                clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees,
                CreatedByUserID)
                );
            
            
            if (!Application.Save())
                return null;
            

            clsLicense  NewLicense = new clsLicense(new LicenseDTO(-1, Application.ApplicationID,this.DriverID,
                this.LicenseClass,DateTime.Now, DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength),
                Notes, this.LicenseClassInfo.ClassFees,true, (int)clsLicense.enIssueReason.Renew,CreatedByUserID));
            

            if (! NewLicense.Save())
                return null;

            this.DeactivateCurrentLicense();

            return  NewLicense;
        }

        public clsDetainedLicense DetainLicense(int CreatedByUserID, float FineFees,int ViolationID)
        {
            clsDetainedLicense DetainedLicense = new clsDetainedLicense

                (
                   new DetainedLicenseDTO(-1, this.LicenseID, DateTime.Now, FineFees, ViolationID,
                   CreatedByUserID, false, null, null, null)
                );
           
            if (!DetainedLicense.Save())
                return null;

            return DetainedLicense;
        }


        public clsLicense ReplaceLicense(int CreatedByUserID, enIssueReason IssueReason)
        {

            int ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            clsApplication Application = new clsApplication
                (
                  new ApplicationDTO(-1, this.DriverInfo.PersonID,DateTime.Now, ApplicationTypeID,
                  DateTime.Now,(int) clsApplication.enApplicationStatus.Completed,
                  clsApplicationType.Find(ApplicationTypeID).ApplicationFees,
                  CreatedByUserID)
                );

           
            if (!Application.Save())
                return null;


            clsLicense NewLicense = new clsLicense(
                new LicenseDTO(-1, Application.ApplicationID, this.DriverID, this.LicenseClass,
                DateTime.Now, this.ExpirationDate,Notes, this.PaidFees,
                true,(byte)IssueReason,CreatedByUserID)
                );


            if (!NewLicense.Save())
                return null;

            this.DeactivateCurrentLicense();

            return NewLicense;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID)
        {
            int ApplicationID = -1;
            clsApplication Application = new clsApplication
                (
                   new ApplicationDTO(-1, this.DriverInfo.PersonID,DateTime.Now,
                   (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense,DateTime.Now,
                   (int)clsApplication.enApplicationStatus.Completed, clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees,
                   CreatedByUserID)
                );

            
            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }
                
            ApplicationID = Application.ApplicationID;



            
            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID,ApplicationID);
        }

        public static bool IsIssueReasonValide(enIssueReason IssueReason)
        {
            var enumValues = Enum.GetValues(typeof(enIssueReason)).Cast<int>();

            return ((int)IssueReason >= enumValues.Min() && (int)IssueReason <= enumValues.Max());


        }



    }
}
