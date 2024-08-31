using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsApplication;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Buisness
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }
         public enum enApplicationType { NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense=3,
            ReplaceDamagedDrivingLicense=4, ReleaseDetainedDrivingLicsense=5, NewInternationalLicense=6,RetakeTest=7,
            RenewInternationalLicense = 8, ReplaceLostInternationalLicense=9,ReplaceDamagedInternationalLicense = 10
           
        };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson ApplicantPersonInfo;
        
      
        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo;

        public DateTime LastStatusDate { get; set; }

        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {
                switch(ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknow";
                };
            }
        }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.PaidFees = 0;
            this.ApplicationStatus = enApplicationStatus.New;
            this.ApplicationDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.ApplicationTypeID =-1;
            this.LastStatusDate = DateTime.Now;
            Mode = enMode.AddNew;
        }
        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
          clsApplication.enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
            this.PaidFees = PaidFees;
            this.ApplicationStatus = ApplicationStatus;
            this.ApplicationDate = ApplicationDate;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo=clsUser.FindByUserID(CreatedByUserID);
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.LastStatusDate = LastStatusDate;
            Mode = enMode.Update;
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int CreatedByUserID = -1, ApplicationTypeID=-1, ApplicantPersonID=-1;
            float PaidFees = 0;byte ApplicationStatus =1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate =DateTime.Now;

            if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                 (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
                
            return null;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                 (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                 (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplication();

            }
            return false;
        }

        
        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID,(byte) enApplicationStatus.Cancelled);
        }

        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)enApplicationStatus.Completed);
        }
        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static int GetActiveApplicationID(int ApplicantPersonID, int ApplicationTypeID)
        {

            return clsApplicationData.GetActiveApplicationID(ApplicantPersonID, ApplicationTypeID);
        }
        public  int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(this.ApplicantPersonID,(int) ApplicationTypeID);
        }

        public static bool DeosPersonHaveActiverApplication(int ApplicantPersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DeosPersonHaveActiverApplication(ApplicantPersonID, ApplicationTypeID);
        }
        public  bool DeosPersonHaveActiverApplication(clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.DeosPersonHaveActiverApplication(this.ApplicantPersonID,(int) ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {

            return clsApplicationData.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        


    }
}
