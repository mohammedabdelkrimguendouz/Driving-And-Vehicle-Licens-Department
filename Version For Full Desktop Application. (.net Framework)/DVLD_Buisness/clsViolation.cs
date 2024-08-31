using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsViolation
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        

        public int ViolationID { get; set; }
        public string ViolationTitle { get; set; }

        public string ViolationDescription { get; set; }
        public float FineFees { get; set; }

        public clsViolation()
        {
            this.ViolationID = -1;
            this.FineFees = 0;
            this.ViolationTitle = "";
            this.ViolationDescription = "";
            Mode = enMode.AddNew;
        }
        private clsViolation(int ViolationID, string ViolationTitle, string ViolationDescription, float ViolationFees)
        {
            this.ViolationID = ViolationID;
            this.ViolationTitle = ViolationTitle;
            this.FineFees = ViolationFees;
            this.ViolationDescription = ViolationDescription;
            Mode = enMode.Update;
        }

        public static clsViolation Find(int ViolationID)
        {
            string ViolationTitle = "";
            float ViolationFees = 0;
            string ViolationDescription = "";
            if (clsViolationData.GetViolationInfoByID(ViolationID, ref ViolationTitle, ref ViolationDescription, ref ViolationFees))
                return new clsViolation(ViolationID, ViolationTitle, ViolationDescription, ViolationFees);
            return null;
        }
        public static clsViolation Find(string ViolationTitle)
        {
            int ViolationID = -1;
            float ViolationFees = 0;
            string ViolationDescription = "";
            if (clsViolationData.GetViolationInfoByTitle(ViolationTitle, ref ViolationID, ref ViolationDescription, ref ViolationFees))
                return new clsViolation(ViolationID, ViolationTitle, ViolationDescription, ViolationFees);
            return null;
        }

        private bool _AddNewViolation()
        {
            this.ViolationID = clsViolationData.AddNewViolation(this.ViolationTitle, this.ViolationDescription, this.FineFees);

            return (this.ViolationID != -1);
        }
        private bool _UpdateViolation()
        {
            return clsViolationData.UpdateViolation(this.ViolationID, this.ViolationTitle, this.ViolationDescription, this.FineFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewViolation())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateViolation();

            }
            return false;
        }

        public static DataTable GetAllViolations()
        {
            return clsViolationData.GetAllViolations();
        }
    }
}
