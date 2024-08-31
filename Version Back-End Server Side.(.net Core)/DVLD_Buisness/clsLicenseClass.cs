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

        public LicenseClassDTO licenseClassDTO
        {
            get {
                return new LicenseClassDTO(this.LicenseClassID, this.ClassName, this.ClassDescription, this.DefaultValidityLength,
                this.MinimumAllowedAge, this.ClassFees);
            }
        }
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte DefaultValidityLength { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public float ClassFees { get; set; }

      
        private clsLicenseClass(LicenseClassDTO licenseClassDTO, enMode CreationMode=enMode.AddNew)
        {
            this.LicenseClassID = licenseClassDTO.LicenseClassID;
            this.ClassFees = licenseClassDTO.ClassFees;
            this.ClassName = licenseClassDTO.ClassName;
            this.DefaultValidityLength = licenseClassDTO.DefaultValidityLength;
            this.MinimumAllowedAge = licenseClassDTO.MinimumAllowedAge;
            this.ClassDescription = licenseClassDTO.ClassDescription;
            Mode = CreationMode;
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
            LicenseClassDTO licenseClassDTO = clsLicenseClassData.GetLicenseClassInfoByID(LicenseClassID);


            if (licenseClassDTO!=null)
                return new clsLicenseClass(licenseClassDTO,enMode.Update);
            return null;
        }

        public static clsLicenseClass Find(string ClassName)
        {
            LicenseClassDTO licenseClassDTO = clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName);


            if (licenseClassDTO != null)
                return new clsLicenseClass(licenseClassDTO, enMode.Update);
            return null;
        }

        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass(this.licenseClassDTO);

            return (this.LicenseClassID != -1);
        }
        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.UpdateLicenseClass(this.licenseClassDTO);
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

        public static List<LicenseClassDTO> GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }
    }
}
