using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0,Update = 1  };
        public enMode Mode = enMode.AddNew;
        
        

        public int PersonID { set; get; }
        public string NationalNo { set; get; }

        public short Gender { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }

        public string ThirdName { set; get; }

        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }



        public string ImagePath { set; get; }

        public int NationalityCountryID { set; get; }

        public clsCountry CountryInfo;
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public clsPerson()
        {
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            Email = "";
            Phone = "";
            Address = "";
            Gender = 0;
            DateOfBirth = DateTime.Now.AddYears(-18);
            NationalityCountryID = -1;
            ImagePath = "";
            CountryInfo = new clsCountry();
            Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            string Email, string Phone, string Address, short Gender, DateTime DateOfBirth, int NationalityCountryID, string ImagePath)

        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            Mode = enMode.Update;
        }



        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.Email, this.Phone,
                this.Address, this.Gender, this.DateOfBirth, this.NationalityCountryID, this.ImagePath);
            return (this.PersonID != -1);
        }

        public static clsPerson Find(int PersonID)
        {

            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            string NationalNo = "", SecondName = "", ThirdName = ""; short Gender = 0;
            DateTime DateOfBirth = DateTime.Now.AddYears(-18);
            int NationalityCountryID = -1;

            if (clsPersonData.GetPersonInfoByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Email, ref Phone,
             ref Address, ref Gender, ref DateOfBirth, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, Email, Phone,
               Address, Gender, DateOfBirth, NationalityCountryID, ImagePath);

            }
            return null;

        }

        public static clsPerson Find(string NationalNo)
        {

            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            int PersonID = -1; string SecondName = "", ThirdName = ""; short Gender = 0;
            DateTime DateOfBirth = DateTime.Now.AddYears(-18);
            int NationalityCountryID = -1;

            if (clsPersonData.GetPersonInfoByNationalNo(NationalNo,ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Email, ref Phone,
             ref Address, ref Gender, ref DateOfBirth, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, Email, Phone,
               Address, Gender, DateOfBirth, NationalityCountryID, ImagePath);

            }
            return null;

        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID,this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.Email, this.Phone,
                this.Address, this.Gender, this.DateOfBirth, this.NationalityCountryID, this.ImagePath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
      

        static public DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople() ;

        }

        static public bool ISPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExistByID(PersonID);
        }
        static public bool ISPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExistByNationalNo(NationalNo);
        }
    }
}
