using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public TestDTO testDTO
        {
            get { return new TestDTO(this.TestID, this.TestAppointmentID, this.Notes, this.CreatedByUserID, this.TestResult); }
        }
        public int TestID { get; set; }

       
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo;

        public string Notes {  get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public bool TestResult { get; set; }
        



        public clsTest(TestDTO testDTO,enMode CreationMode=enMode.AddNew)
        {

            this.TestID = testDTO.TestID;
            this.TestAppointmentID = testDTO.TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.Notes = testDTO.Notes;
            this.TestResult = testDTO.TestResult;
            this.CreatedByUserID = testDTO.CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = CreationMode;
        }

        public static clsTest Find(int TestID)
        {
            TestDTO testDTO = clsTestData.GetTestInfoByID(TestID);


            if ( testDTO!=null)
                return new clsTest(testDTO,enMode.Update);
            return null;
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.testDTO);

            return (this.TestID != -1);
        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.testDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTest();

            }
            return false;
        }

        public static List<TestDTO> GetAllTests()
        {
            return clsTestData.GetAllTests();
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool IsPassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == Enum.GetValues(typeof(clsTestType.enTestType)).Length; ;
        }
    }

}
