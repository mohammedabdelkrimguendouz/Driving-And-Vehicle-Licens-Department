using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsUser;

namespace DVLD_Buisness
{
    public class clsUser
    {
        
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public object AllUserInfo
        {
            get
            {
                return new 
                {
                    userID= this.UserID,
                    userName = this.UserName,
                    isActive = this.IsActive,
                    personInfo = this.PersonInfo.AllPersonInfo,
                };
            }
        }
        public UserDTO userDTO
        {
            get { return new UserDTO(this.PersonID, this.UserID, this.UserName, this.Password, this.IsActive); }
        }
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

       
        public bool IsActive { get; set; }

        public clsPerson PersonInfo;

      
        public clsUser(UserDTO userDTO,enMode CreationMode=enMode.AddNew)
        {
            this.UserName = userDTO.UserName;
            this.UserID = userDTO.UserID;
            this.IsActive = userDTO.IsActive;
            this.Password = userDTO.Password;
            this.PersonID = userDTO.PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
           
            Mode = CreationMode;
        }

        private bool _AddNewUser()
        {
            this.UserID =clsUserData.AddNewUser(this.userDTO);
            return (this.UserID != -1);
        }

        public static clsUser FindByUserID(int UserID)
        {

            UserDTO userDTO = clsUserData.GetUserInfoByID(UserID);


            if (userDTO != null)
            {
                return new clsUser(userDTO, enMode.Update);
            }
            return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {

            UserDTO userDTO = clsUserData.GetUserInfoByPersonID(PersonID);


            if (userDTO != null)
            {
                return new clsUser(userDTO, enMode.Update);
            }
            return null;

        }

        public static clsUser FindByUserNameAndPassword(string UserName,string Password)
        {

            UserDTO userDTO = clsUserData.GetUserInfoByUserNameAndPassword(UserName,Password);


            if (userDTO != null)
            {
                return new clsUser(userDTO, enMode.Update);
            }
            return null;

        }
        public static clsUser FindByUserName(string UserName)
        {

            UserDTO userDTO = clsUserData.GetUserInfoByUserName(UserName);


            if ( userDTO!=null)
            {
                return new clsUser(userDTO,enMode.Update);
            }
            return null;

        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.userDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }


        static public List<UsersListDTO> GetAllUsers()
        {
            return clsUserData.GetAllUsers();

        }

        static public bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExistByUserID(UserID);
        }
        static public bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExistByUserName(UserName);
        }

        static public bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        


       

    }

}
