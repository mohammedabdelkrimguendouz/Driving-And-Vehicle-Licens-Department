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

        public DriverDTO driverDTO
        {
            get { return new DriverDTO(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate); }
        }
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
                return  DoesDriverIssuedInternationalLicense(this.DriverID);
            }
        }
       

       
        public clsDriver(DriverDTO driverDTO,enMode CreationMode=enMode.AddNew)
        {
            this.DriverID = driverDTO.DriverID;
            this.PersonID = driverDTO.PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedDate = driverDTO.CreatedDate;
            this.CreatedByUserID = driverDTO.CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = CreationMode;
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            DriverDTO driverDTO = clsDriverData.GetDriverInfoByID(DriverID);


            if (driverDTO!=null)
                return new clsDriver(driverDTO,enMode.Update);
            return null;
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            DriverDTO driverDTO = clsDriverData.GetDriverInfoByPersonID(PersonID);


            if (driverDTO != null)
                return new clsDriver(driverDTO, enMode.Update);
            return null;
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.driverDTO);

            return (this.DriverID != -1);
        }
        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(this.driverDTO);
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

        public static List<DriversListDTO> GetAllLDrivers()
        {
            return clsDriverData.GetAllDriveres();
        }

        public static bool DoesDriverIssuedInternationalLicense(int DriverID)
        {
            return (clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(DriverID) != -1);
        }
    }
}
