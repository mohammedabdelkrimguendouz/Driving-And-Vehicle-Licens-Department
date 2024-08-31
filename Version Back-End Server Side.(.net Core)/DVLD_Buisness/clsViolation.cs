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
        
        public ViolationDTO violationDTO
        {
            get { return new ViolationDTO(this.ViolationID,this.ViolationTitle,this.ViolationDescription,this.FineFees);}
        }
        public int ViolationID { get; set; }
        public string ViolationTitle { get; set; }

        public string ViolationDescription { get; set; }
        public float FineFees { get; set; }

      
        public clsViolation(ViolationDTO violationDTO,enMode CreationMode=enMode.AddNew)
        {
            this.ViolationID = violationDTO.ViolationID;
            this.ViolationTitle = violationDTO.ViolationTitle;
            this.FineFees = violationDTO.FineFees;
            this.ViolationDescription = violationDTO.ViolationDescription;
            Mode = CreationMode;
        }

        public static clsViolation Find(int ViolationID)
        {
            ViolationDTO violationDTO = clsViolationData.GetViolationInfoByID(ViolationID);
            if (violationDTO !=null)
                return new clsViolation(violationDTO,enMode.Update);
            return null;
        }
        public static clsViolation Find(string ViolationTitle)
        {
            ViolationDTO violationDTO = clsViolationData.GetViolationInfoByTitle(ViolationTitle);
            if (violationDTO != null)
                return new clsViolation(violationDTO, enMode.Update);
            return null;
        }

        private bool _AddNewViolation()
        {
            this.ViolationID = clsViolationData.AddNewViolation(this.violationDTO);

            return (this.ViolationID != -1);
        }
        private bool _UpdateViolation()
        {
            return clsViolationData.UpdateViolation(this.violationDTO);
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

        public static List<ViolationDTO> GetAllViolations()
        {
            return clsViolationData.GetAllViolations();
        }
        public static bool IsViolationExist(int ViolationID)
        {
            return clsViolationData.IsViolationExistByViolationID(ViolationID);
        }

        public static bool IsViolationExist(string ViolationTitle)
        {
            return clsViolationData.IsViolationExistByViolationTitle(ViolationTitle);
        }

    }
}
