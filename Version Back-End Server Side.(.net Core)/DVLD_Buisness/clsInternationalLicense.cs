using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public InternationalLicenseDTO internationalLicenseDTO
        {
            get => new InternationalLicenseDTO(this.InternationalLicenseID, this.DriverID, this.IssuedUsingLocalLicenseID,
                IssueDate, ExpirationDate, IsActive, (byte)IssueReason, CreatedByUserID, ApplicationID);
        }
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


        public clsInternationalLicense(InternationalLicenseDTO internationalLicenseDTO,
            ApplicationDTO applicationDTO,enMode CreationMode=enMode.AddNew)
            :base(applicationDTO,(clsApplication.enMode)CreationMode)
        {
            
            this.ApplicationID = internationalLicenseDTO.ApplicationID;  
            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IssueReason = IssueReason;
            this.IsActive = IsActive;
            Mode = CreationMode;
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            InternationalLicenseDTO internationalLicenseDTO = clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID);

           

            if (internationalLicenseDTO!=null)
            {
                clsApplication Application = clsApplication.FindBaseApplication(internationalLicenseDTO.ApplicationID);

                if (Application != null)
                    return new clsInternationalLicense(internationalLicenseDTO,Application.applicationDTO,enMode.Update);
            }

            return null;



        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.internationalLicenseDTO);

            return (this.InternationalLicenseID != -1);
        }
        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.internationalLicenseDTO);
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

        public static List<InternationalLicensesListDTO> GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
        
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }
        public static List<InternationalLicensesListDTO> GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public clsInternationalLicense RenewInternationalLicense(int CreatedByUserID)
        {
            clsInternationalLicense NewInternationalLicense = new clsInternationalLicense
                (
                   new InternationalLicenseDTO(-1, this.DriverID, this.IssuedUsingLocalLicenseID, DateTime.Now,
                   DateTime.Now.AddYears(this.DefaultValidityLength), true, (byte)clsInternationalLicense.enIssueReason.Renew,
                   CreatedByUserID, -1),

                   new ApplicationDTO(-1, this.DriverInfo.PersonID, DateTime.Now,
                   (int)clsApplication.enApplicationType.RenewInternationalLicense, DateTime.Now, (int)clsApplication.enApplicationStatus.Completed,
                       clsApplicationType.Find((int)clsApplication.enApplicationType.RenewInternationalLicense).ApplicationFees,
                      CreatedByUserID)
                );

            
            if (!NewInternationalLicense.Save())
                return null;

            this.DeactivateCurrentInternationalLicense();

            return NewInternationalLicense;
        }

        public clsInternationalLicense ReplaceInternationalLicense(int CreatedByUserID,clsInternationalLicense.enIssueReason IssueReason)
        {

            int ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedInternationalLicense :
                (int)clsApplication.enApplicationType.ReplaceLostInternationalLicense;

            clsInternationalLicense NewInternationalLicense = new clsInternationalLicense
                 (
                    new InternationalLicenseDTO(-1, this.DriverID, this.IssuedUsingLocalLicenseID, DateTime.Now,
                    this.ExpirationDate, true, (byte)IssueReason,
                    CreatedByUserID, -1),

                    new ApplicationDTO(-1, this.DriverInfo.PersonID, DateTime.Now,
                     ApplicationTypeID, DateTime.Now, (int)clsApplication.enApplicationStatus.Completed
                       , clsApplicationType.Find(ApplicationTypeID).ApplicationFees,
                       CreatedByUserID)
                 );

            if (!NewInternationalLicense.Save())
                return null;

            this.DeactivateCurrentInternationalLicense();

            return NewInternationalLicense;
        }


        public static bool IsIssueReasonValide(enIssueReason IssueReason)
        {
            var enumValues = Enum.GetValues(typeof(enIssueReason)).Cast<int>();

            return ((int)IssueReason >= enumValues.Min() && (int)IssueReason <= enumValues.Max());


        }
    }
}
