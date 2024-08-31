using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public ApplicationTypeDTO applicationTypeDTO
        {
            get { return new ApplicationTypeDTO(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees); }
        }
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

       
        public clsApplicationType(ApplicationTypeDTO applicationTypeDTO,enMode CreationMode=enMode.AddNew)
        {
            this.ApplicationTypeID = applicationTypeDTO.ApplicationTypeID;
            this.ApplicationTypeTitle = applicationTypeDTO.ApplicationTypeTitle;
            this.ApplicationFees = applicationTypeDTO.ApplicationFees;
            Mode = CreationMode;
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            ApplicationTypeDTO applicationTypeDTO = clsApplicationTypeData.GetApplicationTypeInfoByID(ApplicationTypeID);
            if (applicationTypeDTO!=null)
                return new clsApplicationType(applicationTypeDTO , enMode.Update);
            return null;
        }
       

        private bool _AddNewApplicationType()
        {
            this.ApplicationFees = clsApplicationTypeData.AddNewApplicationType(this.applicationTypeDTO);

            return (this.ApplicationTypeID != -1);
        }
        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.applicationTypeDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplicationType();

            }
            return false;
        }

        public static List<ApplicationTypeDTO> GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
    }
}
