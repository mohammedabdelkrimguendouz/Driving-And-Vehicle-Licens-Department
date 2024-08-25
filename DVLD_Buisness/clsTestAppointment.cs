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
        public int RetakeTestApplicationID {  get; set; }
        public clsApplication RetakeTestApplicationInfo;

        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }


        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;
        }
        private clsTestAppointment(int TestAppointmentID,  clsTestType.enTestType TestTypeID,  int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate,  float PaidFees,  int CreatedByUserID, bool IsLocked,  int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.TestTypeInfo = clsTestType.Find(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplicationInfo=clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestApplicationInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);
            Mode = enMode.Update;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID=1;
            int LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1, RetakeTestApplicationID=-1;
            DateTime AppointmentDate = DateTime.Now;
            bool IsLocked = false;
            float PaidFees = 0;
            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType) TestTypeID,  LocalDrivingLicenseApplicationID,
                 AppointmentDate,  PaidFees,  CreatedByUserID,  IsLocked,  RetakeTestApplicationID);
            return null;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((byte)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                 this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }
        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, (byte)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                 this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
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

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }

        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            int CreatedByUserID = -1, RetakeTestApplicationID = -1, TestAppointmentID=-1;
            DateTime AppointmentDate = DateTime.Now;
            bool IsLocked = false;
            float PaidFees = 0;
            if (clsTestAppointmentData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (byte) TestTypeID, ref TestAppointmentID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
                 AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            return null;
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }
        public  DataTable GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (byte)TestTypeID);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
      


    }
}
