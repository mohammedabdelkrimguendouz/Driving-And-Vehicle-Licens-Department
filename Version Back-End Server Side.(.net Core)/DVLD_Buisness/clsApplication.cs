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

        public ApplicationDTO applicationDTO
        {
            get {
                return new ApplicationDTO(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, this.LastStatusDate, (int)this.ApplicationStatus, this.PaidFees, this.CreatedByUserID);
            }
        }
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
        
        public clsApplication(ApplicationDTO applicationDTO,enMode CreationMode=enMode.AddNew)
        {
            this.ApplicationID = applicationDTO.ApplicationID;
            this.ApplicantPersonID = applicationDTO.ApplicantPersonID;
            this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
            this.PaidFees = applicationDTO.PaidFees;
            this.ApplicationStatus =(enApplicationStatus) applicationDTO.ApplicationStatus;
            this.ApplicationDate = applicationDTO.ApplicationDate;
            this.CreatedByUserID = applicationDTO.CreatedByUserID;
            this.CreatedByUserInfo=clsUser.FindByUserID(CreatedByUserID);
            this.ApplicationTypeID = applicationDTO.ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.LastStatusDate = applicationDTO.LastStatusDate;
            Mode = CreationMode;
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            ApplicationDTO applicationDTO = clsApplicationData.GetApplicationInfoByID(ApplicationID);

            if (applicationDTO!=null)
            {
                return new clsApplication(applicationDTO,enMode.Update);
            }
                
            return null;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.applicationDTO);

            return (this.ApplicationID != -1);
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.applicationDTO);
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

        public static int GetActiveApplicationID(int ApplicantPersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(ApplicantPersonID, (int)ApplicationTypeID);
        }
        public  int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(this.ApplicantPersonID,(int) ApplicationTypeID);
        }

        public static bool DeosPersonHaveActiveApplication(int ApplicantPersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.DeosPersonHaveActiverApplication(ApplicantPersonID,(int) ApplicationTypeID);
        }
        public  bool DeosPersonHaveActiverApplication(clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.DeosPersonHaveActiverApplication(this.ApplicantPersonID,(int) ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public static List<ApplicationDTO> GetAllApplications()
        {
            return  clsApplicationData.GetAllApplications();
        }

        public static bool IsApplicationTypeValide(clsApplication.enApplicationType ApplicationTypeID)
        {
            var enumValues = Enum.GetValues(typeof(clsApplication.enApplicationType)).Cast<int>();

            return ((int)ApplicationTypeID >= enumValues.Min() && (int)ApplicationTypeID <= enumValues.Max());


        }

        public static bool IsApplicationStatusValide(clsApplication.enApplicationStatus ApplicationStatus)
        {
            var enumValues = Enum.GetValues(typeof(clsApplication.enApplicationStatus)).Cast<int>();

            return ((int)ApplicationStatus >= enumValues.Min() && (int)ApplicationStatus <= enumValues.Max());


        }

    }
}
