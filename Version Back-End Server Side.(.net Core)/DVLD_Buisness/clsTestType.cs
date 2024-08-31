using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public TestTypeDTO testTypeDTO
        {
            get { return new TestTypeDTO((int)this.ID, this.Title, this.Description, this.Fees); }
        }
        public enum enTestType { PsychologicalTest=1 ,VisionTest=2 ,WrittenTest=3, StreetTest=4 }

        public clsTestType.enTestType ID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public float Fees { get; set; }

       
        public clsTestType(TestTypeDTO testTypeDTO,enMode CreationMode=enMode.AddNew)
        {
            this.ID = (enTestType)testTypeDTO.ID;
            this.Title = testTypeDTO.Title;
            this.Fees = testTypeDTO.Fees;
            this.Description = testTypeDTO.Description;
            Mode = CreationMode;
        }

        public static clsTestType Find(clsTestType.enTestType ID)
        {
            TestTypeDTO testTypeDTO = clsTestTypeData.GetTestTypeInfoByID((int)ID);


            if (testTypeDTO !=null)
                return new clsTestType(testTypeDTO,enMode.Update);
            return null;
        }

        private bool _AddNewTestType()
        {
            this.ID =(clsTestType.enTestType) clsTestTypeData.AddNewTestType(this.testTypeDTO);

            return (this.Title != "");
        }
        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType(this.testTypeDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTestType();

            }
            return false;
        }

        public static List<TestTypeDTO> GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public static bool IsTestTypeValide(clsTestType.enTestType TestTypeID)
        {
            var enumValues = Enum.GetValues(typeof(clsTestType.enTestType)).Cast<int>();

            return ((int)TestTypeID >= enumValues.Min() && (int)TestTypeID <= enumValues.Max());


        }
    }
}
