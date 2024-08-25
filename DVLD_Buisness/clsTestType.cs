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
        public enum enTestType { PsychologicalTest=1 ,VisionTest = 2,WrittenTest=3, StreetTest=4 }

        public clsTestType.enTestType ID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public float Fees { get; set; }

        public clsTestType()
        {
            this.ID = clsTestType.enTestType.PsychologicalTest;
            this.Fees = 0;
            this.Title = "";
            this.Description = "";
            Mode = enMode.AddNew;
        }
        private clsTestType(clsTestType.enTestType ID, string TestTypeTitle,string TestTypeDescription ,float TestTypeFees)
        {
            this.ID = ID;
            this.Title = TestTypeTitle;
            this.Fees = TestTypeFees;
            this.Description = TestTypeDescription;
            Mode = enMode.Update;
        }

        public static clsTestType Find(clsTestType.enTestType ID)
        {
            string TestTypeTitle = "";
            float TestTypeFees = 0;
            string TestTypeDescription="";
            if (clsTestTypeData.GetTestTypeInfoByID((int)ID, ref TestTypeTitle,ref TestTypeDescription, ref TestTypeFees))
                return new clsTestType(ID, TestTypeTitle,TestTypeDescription, TestTypeFees);
            return null;
        }

        private bool _AddNewTestType()
        {
            this.ID =(clsTestType.enTestType) clsTestTypeData.AddNewTestType(this.Title,this.Description, this.Fees);

            return (this.Title != "");
        }
        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((int)this.ID, this.Title,this.Description, this.Fees);
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

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }
    }
}
