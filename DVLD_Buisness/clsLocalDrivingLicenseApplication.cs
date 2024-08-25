using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsApplication;
using static DVLD_Buisness.clsLicense;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Buisness
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
        public enum enLicenseClass
        {
            SmallMotorcycle = 1, HeavyMotorcycleLicense = 2, Ordinarydrivinglicense = 3,
            Commercial = 4, Agricultural = 5, Smallandmediumbus = 6, Truckandheavyvehicle = 7
        }
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { get; set; }
        
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, clsApplication.enApplicationStatus ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
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
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.Update;
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                clsApplication Application=clsApplication.FindBaseApplication(ApplicationID);

                if (Application!=null)
                    return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID,Application.ApplicantPersonID
                        ,Application.ApplicationDate,Application.ApplicationTypeID,
                        (clsApplication.enApplicationStatus)Application.ApplicationStatus,Application.LastStatusDate
                        ,Application.ApplicationTypeInfo.ApplicationFees,Application.CreatedByUserID, LicenseClassID);
            }

            return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                ( ApplicationID,ref LocalDrivingLicenseApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                if (Application != null)
                    return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, Application.ApplicantPersonID
                        , Application.ApplicationDate, Application.ApplicationTypeID,
                        (clsApplication.enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate
                        , Application.ApplicationTypeInfo.ApplicationFees, Application.CreatedByUserID, LicenseClassID);
            }

            return null;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
           this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication
                (this.ApplicationID, this.LicenseClassID);
             return (this.LocalDrivingLicenseApplicationID != -1);
           
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
                return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
            
        }

        public  bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();

            }
            return false;
        }

        public bool Delete()
        {
            if (!clsLocalDrivingLicenseApplicationData.
                DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID))
                return false;
            return base.Delete();

        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

       
        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool IsPassedAllTests()
        {
            return clsTest.IsPassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool IsPassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            
            return clsTest.IsPassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public static bool DoesPassedTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassedTestType(LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }
        public  bool DoesPassedTestType(clsTestType.enTestType TestTypeID )
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassedTestType(this.LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }
        public bool DeosAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DeosAttendTestType(this.LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }

        public static int GetTotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.GetTotalTrialsPerTest(LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }
        public  int GetTotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.GetTotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }

        public bool IsThereAnActiveScheduleTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduleTest(this.LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }
        static public bool IsThereAnActiveScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduleTest(LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        public int IssuedLicenseForTheFirstTime(string Notes,int CreatedByUserID)
        {
            int DriverID=-1;
            clsDriver Driver =clsDriver.FindByPersonID(this.ApplicantPersonID);
            if (Driver == null)
            {
                Driver = new clsDriver();
                Driver.CreatedByUserID = CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;
                Driver.PersonID = this.ApplicantPersonID;
                if (Driver.Save())
                    DriverID = Driver.DriverID;
                else
                    return -1;
            }   
            else
                DriverID = Driver.DriverID;


            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = Driver.DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (!License.Save())
                return -1;

            this.SetComplete();

            return License.LicenseID ;
        }

        




    }
}
