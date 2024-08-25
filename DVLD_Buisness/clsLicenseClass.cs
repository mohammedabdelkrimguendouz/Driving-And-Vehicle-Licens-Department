using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLicenseClass
    {
        
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte DefaultValidityLength { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public float ClassFees { get; set; }

        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassFees = 0;
            this.ClassName = "";
            this.DefaultValidityLength = 18;
            this.MinimumAllowedAge = 18;
            this.ClassDescription = "";
            Mode = enMode.AddNew;
        }
        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassFees = ClassFees;
            this.ClassName = ClassName;
            this.DefaultValidityLength = DefaultValidityLength;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.ClassDescription = ClassDescription;
            Mode = enMode.Update;
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDescription="";
            byte MinimumAllowedAge = 18, DefaultValidityLength = 10;
            float ClassFees = 0;
            if (clsLicenseClassData.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            return null;
        }

        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1; string ClassDescription = "";
            byte MinimumAllowedAge = 18, DefaultValidityLength = 10;
            float ClassFees = 0;
            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID , ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            return null;
        }

        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass(this.ClassName,this.ClassDescription,this.MinimumAllowedAge,this.DefaultValidityLength, this.ClassFees);

            return (this.LicenseClassID != -1);
        }
        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLicenseClass();

            }
            return false;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }
    }
}
