using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        
        public TestAppointmentDTO testAppointmentDTO
        {
            get {
                return new TestAppointmentDTO(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
            }
        }

        public int TestAppointmentID {  get; set; }

        public clsTestType.enTestType TestTypeID { get; set; }
        public clsTestType TestTypeInfo;
        public int LocalDrivingLicenseApplicationID {  get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;

        public DateTime AppointmentDate {  get; set; }
        public float PaidFees {  get; set; }
        public int CreatedByUserID {  get; set; }
        public clsUser CreatedByUserInfo {  get; set; }

        public bool IsLocked {  get; set; }
        public int? RetakeTestApplicationID {  get; set; }
        public clsApplication RetakeTestApplicationInfo;

        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }


       
        public clsTestAppointment(TestAppointmentDTO testAppointmentDTO,enMode CreationMode = enMode.AddNew)
        {
            this.TestAppointmentID = testAppointmentDTO.TestAppointmentID;
            this.TestTypeID = (clsTestType.enTestType)testAppointmentDTO.TestTypeID;
            this.TestTypeInfo = clsTestType.Find(TestTypeID);
            this.LocalDrivingLicenseApplicationID = testAppointmentDTO.LocalDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplicationInfo=clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = testAppointmentDTO.AppointmentDate;
            this.PaidFees = testAppointmentDTO.PaidFees;
            this.CreatedByUserID = testAppointmentDTO.CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.IsLocked = testAppointmentDTO.IsLocked;
            this.RetakeTestApplicationID = testAppointmentDTO.RetakeTestApplicationID;
            this.RetakeTestApplicationInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID??0);
            Mode = CreationMode;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            TestAppointmentDTO testAppointmentDTO = clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID);

            if (testAppointmentDTO!=null)
                return new clsTestAppointment(testAppointmentDTO,enMode.Update);
            return null;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(this.testAppointmentDTO);

            return (this.TestAppointmentID != -1);
        }
        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.testAppointmentDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTestAppointment();

            }
            return false;
        }

        public static List<TestAppointmentsListDTO> GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }

        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            TestAppointmentDTO testAppointmentDTO = clsTestAppointmentData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (byte)TestTypeID);


            if (testAppointmentDTO!=null)
                return new clsTestAppointment(testAppointmentDTO,enMode.Update);
            return null;
        }

        public static List<ApplicationTestAppointmentsDTO> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }
        public  List<ApplicationTestAppointmentsDTO> GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
      
    }
}
