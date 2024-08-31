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
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

       
        public bool IsActive { get; set; }

        public clsPerson PersonInfo;

        public clsUser()
        {
            this.UserName = "";
            this.UserID = -1;
            this.IsActive = false;
            this.Password = "";
            this.PersonID = -1;
            Mode = enMode.AddNew;
        }
        private clsUser(int UserID,int PersonID,string UserName,string Password, bool IsActive)
        {
    
            this.UserName = UserName;
            this.UserID = UserID;
            this.IsActive = IsActive;
            this.Password = Password;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.PersonID = PersonID;
            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID =clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        public static clsUser FindByUserID(int UserID)
        {

            string UserName = "", Password = "" ;
            bool IsActive = false;
            int PersonID = -1;



            if (clsUserData.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser( UserID, PersonID,  UserName,  Password, IsActive);
            }
            return null;

        }

        public static clsUser FindByPersonID(int PersonID)
        {

            string UserName = "", Password = "";
            bool IsActive = false;
            int UserID = -1;



            if (clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;

        }

        public static clsUser FindByUserNameAndPassword(string UserName,string Password)
        {

            bool IsActive = false;
            int PersonID = -1,UserID=-1;



            if (clsUserData.GetUserInfoByUserNameAndPassword( UserName,  Password,ref UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;

        }
        public static clsUser FindByUserName(string UserName)
        {

            bool IsActive = false;
            string Password = "";
            int PersonID = -1, UserID = -1;



            if (clsUserData.GetUserInfoByUserName(UserName, ref Password, ref UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;

        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID,this.PersonID, this.UserName, this.Password, this.IsActive);
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


        static public DataTable GetAllUsers()
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

        


        public bool ChangePassword(string NewPassword)
        {
            return clsUserData.ChangePassword(this.UserID, NewPassword);
        }

    }

}
