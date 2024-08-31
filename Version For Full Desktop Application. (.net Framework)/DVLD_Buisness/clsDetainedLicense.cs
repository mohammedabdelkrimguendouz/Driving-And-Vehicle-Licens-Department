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
        public int DetainID { get; set; }
        public int LicenseID { get; set; }

        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }

        public int ViolationID {  get; set; }
        public clsViolation ViolationInfo;
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo;
        public int ReleaseApplicationID { get; set; }
        public clsApplication ReleaseApplicationInfo;

        public clsDetainedLicense()
        {

            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ViolationID = -1;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = -1;

            this.ReleaseApplicationID = -1;
            Mode = enMode.AddNew;
        }
        private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees,int ViolationID,
            int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID,
             int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;

            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.ViolationID = ViolationID;
            this.ViolationInfo = clsViolation.Find(ViolationID);
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.IsReleased = IsReleased;

            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleasedByUserInfo = clsUser.FindByUserID(ReleasedByUserID);
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleaseApplicationInfo = clsApplication.FindBaseApplication(ReleaseApplicationID);
            Mode = enMode.Update;
        }

        public static clsDetainedLicense FindByDetainID(int DetainID)
        {
            int CreatedByUserID = -1, LicenseID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1, ViolationID =-1;
            bool IsReleased = false;
            float FineFees = 0; 
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate,
            ref FineFees,ref ViolationID,
            ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref
             ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, ViolationID,
             CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID,
              ReleaseApplicationID);
            }

            return null;
        }
        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int CreatedByUserID = -1, DetainID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1, ViolationID=-1;
            bool IsReleased = false;
            float FineFees = 0;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID, ref DetainID, ref DetainDate,
            ref FineFees,ref ViolationID,
            ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref
             ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, ViolationID,
             CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID,
              ReleaseApplicationID);
            }

            return null;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID,this.DetainDate, this.FineFees,this.ViolationID,
             this.CreatedByUserID);

            return (this.DetainID != -1);
        }
        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.ViolationID,
             this.CreatedByUserID);
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

        public static DataTable GetAllDetainedLicenses()
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
