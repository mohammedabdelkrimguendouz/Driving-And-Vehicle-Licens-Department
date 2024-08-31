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
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enLicenseClass
        {
            SmallMotorcycle = 1, HeavyMotorcycleLicense = 2, Ordinarydrivinglicense = 3,
            Commercial = 4, Agricultural = 5, Smallandmediumbus = 6, Truckandheavyvehicle = 7
        }

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public LocalDrivingLicenseApplicationDTO localDrivingLicenseApplicationDTO
        {
            get
            {
                return new LocalDrivingLicenseApplicationDTO(this.LocalDrivingLicenseApplicationID,
                    this.LicenseClassID, this.ApplicationID);
            }
        }
        public int LocalDrivingLicenseApplicationID { get; set; }
        
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;




        public clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationDTO localDrivingLicenseApplicationDTO,
            ApplicationDTO applicationDTO,enMode CreationMode=enMode.AddNew)
            :base(applicationDTO,(clsApplication.enMode)CreationMode)
        {
            
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationDTO.LocalDrivingLicenseApplicationID;
            this.ApplicationID = localDrivingLicenseApplicationDTO.ApplicationID;
            
            this.LicenseClassID = localDrivingLicenseApplicationDTO.LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = CreationMode;
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            

            LocalDrivingLicenseApplicationDTO localDrivingLicenseApplication = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID);


            if (localDrivingLicenseApplication!=null)
            {
                clsApplication Application=clsApplication.FindBaseApplication(localDrivingLicenseApplication.ApplicationID);

                if (Application!=null)
                    return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplication,
                        Application.applicationDTO,enMode.Update);
            }

            return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            LocalDrivingLicenseApplicationDTO localDrivingLicenseApplication = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                 (ApplicationID);


            if (localDrivingLicenseApplication != null)
            {
                clsApplication Application = clsApplication.FindBaseApplication(localDrivingLicenseApplication.ApplicationID);

                if (Application != null)
                    return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplication,
                        Application.applicationDTO, enMode.Update);
            }

            return null;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
           this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication
                (this.localDrivingLicenseApplicationDTO);
             return (this.LocalDrivingLicenseApplicationID != -1);
           
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
                return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.localDrivingLicenseApplicationDTO);   
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

        public static List<LocalDrivingLicenseApplicationsListDTO> GetAllLocalDrivingLicenseApplications()
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

        public clsLicense IssuedLicenseForTheFirstTime(string Notes,int CreatedByUserID)
        {
            int DriverID=-1;
            clsDriver Driver =clsDriver.FindByPersonID(this.ApplicantPersonID);
            if (Driver == null)
            {
                Driver = new clsDriver(new DriverDTO(-1, this.ApplicantPersonID,CreatedByUserID,DateTime.Now));
                if (Driver.Save())
                    DriverID = Driver.DriverID;
                else
                    return null;
            }   
            else
                DriverID = Driver.DriverID;


            clsLicense License = new clsLicense
                (
                   new LicenseDTO(-1, this.ApplicationID, Driver.DriverID, this.LicenseClassID,
                   DateTime.Now, DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength),
                   Notes, this.LicenseClassInfo.ClassFees,true,(byte) clsLicense.enIssueReason.FirstTime,
                   CreatedByUserID)

                );
           

            if (!License.Save())
                return null;

            this.SetComplete();

            return License ;
        }

        
      

    }
}
