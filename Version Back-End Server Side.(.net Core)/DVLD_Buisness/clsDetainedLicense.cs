using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsApplication;

namespace DVLD_Buisness
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public DetainedLicenseDTO detainedLicenseDTO
        {
            get {
                return new DetainedLicenseDTO(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees,
                this.ViolationID, this.CreatedByUserID, this.IsReleased, this.ReleaseDate,
                this.ReleasedByUserID, this.ReleaseApplicationID);
            }
        }
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        //public clsLicense LicenseInfo;

        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }

        public int ViolationID {  get; set; }
        public clsViolation ViolationInfo;
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo;
        public int? ReleaseApplicationID { get; set; }
        public clsApplication ReleaseApplicationInfo;

       
        public clsDetainedLicense(DetainedLicenseDTO detainedLicenseDTO ,enMode CreationMode=enMode.AddNew)
        {
            this.DetainID = detainedLicenseDTO.DetainID;
            this.LicenseID = detainedLicenseDTO.LicenseID;
            //this.LicenseInfo=clsLicense.FindByLicenseID(this.LicenseID);
            this.DetainDate = detainedLicenseDTO.DetainDate;
            this.FineFees = detainedLicenseDTO.FineFees;
            this.ViolationID = detainedLicenseDTO.ViolationID;
            this.ViolationInfo = clsViolation.Find(ViolationID);
            this.CreatedByUserID = detainedLicenseDTO.CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.IsReleased = detainedLicenseDTO.IsReleased;

            this.ReleaseDate = detainedLicenseDTO.ReleaseDate;
            this.ReleasedByUserID = detainedLicenseDTO.ReleasedByUserID;
            this.ReleasedByUserInfo = clsUser.FindByUserID(ReleasedByUserID??-1);
            this.ReleaseApplicationID = detainedLicenseDTO.ReleaseApplicationID;
            this.ReleaseApplicationInfo = clsApplication.FindBaseApplication(ReleaseApplicationID??-1);
            Mode = CreationMode;
        }

        public static clsDetainedLicense FindByDetainedID(int DetainID)
        {
            DetainedLicenseDTO detainedLicenseDTO = clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID);

            if (detainedLicenseDTO!=null)
            {
                return new clsDetainedLicense(detainedLicenseDTO,enMode.Update);
            }

            return null;
        }
        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            DetainedLicenseDTO detainedLicenseDTO = clsDetainedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID);

            if (detainedLicenseDTO != null)
            {
                return new clsDetainedLicense(detainedLicenseDTO, enMode.Update);
            }

            return null;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.detainedLicenseDTO);
             

            return (this.DetainID != -1);
        }
        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(this.detainedLicenseDTO);
            
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateDetainedLicense();

            }
            return false;
        }

        public static List<DetainedLicensesListDTO> GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID,int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }
    }
}
