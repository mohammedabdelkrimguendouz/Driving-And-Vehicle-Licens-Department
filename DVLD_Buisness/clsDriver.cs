using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public DateTime CreatedDate { get; set; }
        
        public bool IsIssuedInternationalLicense
        {
            get
            {
                return DoesDriverIssuedInternationalLicense(this.DriverID);
            }
        }
       

        public clsDriver()
        {
            this.DriverID = -1;
            this.CreatedDate = DateTime.Now;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedDate = CreatedDate;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetDriverInfoByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            return null;
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            return null;
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID);

            return (this.DriverID != -1);
        }
        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateDriver();

            }
            return false;
        }

        public static DataTable GetAllLDrivers()
        {
            return clsDriverData.GetAllDriveres();
        }

        public static bool DoesDriverIssuedInternationalLicense(int DriverID)
        {
            return (clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(DriverID) != -1);
        }
    }
}
