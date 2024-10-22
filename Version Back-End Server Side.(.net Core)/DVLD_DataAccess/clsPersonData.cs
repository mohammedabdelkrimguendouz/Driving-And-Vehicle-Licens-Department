using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class PersonDTO
    {

        public int PersonID { set; get; }
        public string NationalNo { set; get; }

        public byte Gender { set; get; }
        public string FirstName { set; get; }
        public string? SecondName { set; get; }

        public string? ThirdName { set; get; }

        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }

        public string? ImagePath { set; get; }

        public int NationalityCountryID { set; get; }

        public PersonDTO(int personID, string nationalNo, byte gender, string firstName, string? secondName, 
            string? thirdName, string lastName, string email, string phone, string address, DateTime 
            dateOfBirth, string? imagePath, int nationalityCountryID)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            Gender = gender;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            ImagePath = imagePath;
            NationalityCountryID = nationalityCountryID;
        }

        public PersonDTO() { }
    }

    public class PeopleListDTO
    {

        public int PersonID { set; get; }
        public string NationalNo { set; get; }

        public byte Gender { set; get; }
        public string GenderCaption { set; get; }
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

        public string CountryName { set; get; }

        public PeopleListDTO(int personID, string nationalNo, byte gender, string genderCaption, string firstName, string secondName, string thirdName, string lastName,
            string email, string phone, string address, DateTime dateOfBirth, string imagePath, int 
            nationalityCountryID, string countryName)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            Gender = gender;
            GenderCaption = genderCaption;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            ImagePath = imagePath;
            NationalityCountryID = nationalityCountryID;
            CountryName = countryName;
        }
    }


    public class clsPersonData
    {
        public static int AddNewPerson(PersonDTO personDTO)
        {
            int PersonID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    
                    using (SqlCommand Command = new SqlCommand("SP_AddNewPerson", Connection))
                    {
                        Command.CommandType= CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@NationalNo", personDTO.NationalNo);
                        Command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);


                        if (personDTO.SecondName != "" && personDTO.SecondName != null)
                            Command.Parameters.AddWithValue("@SecondName", personDTO.SecondName);
                        else
                            Command.Parameters.AddWithValue("@SecondName", DBNull.Value);

                        if (personDTO.ThirdName != "" && personDTO.ThirdName != null)
                            Command.Parameters.AddWithValue("@ThirdName", personDTO.ThirdName);
                        else
                            Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

                        Command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                        Command.Parameters.AddWithValue("@Email", personDTO.Email);

                        Command.Parameters.AddWithValue("@Phone", personDTO.Phone);
                        Command.Parameters.AddWithValue("@Address", personDTO.Address);
                        Command.Parameters.AddWithValue("@Gender", personDTO.Gender);
                        Command.Parameters.AddWithValue("@DateOfBirth", personDTO.DateOfBirth);
                        Command.Parameters.AddWithValue("@NationalityCountryID", personDTO.NationalityCountryID);


                        if (personDTO.ImagePath != "" && personDTO.ImagePath != null)
                            Command.Parameters.AddWithValue("@ImagePath", personDTO.ImagePath);
                        else
                            Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        PersonID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                PersonID = -1;
            }
            return PersonID;
        }


        public static PersonDTO GetPersonInfoByID(int PersonID)
        {
            PersonDTO personDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetPersonInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                personDTO = new PersonDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetString(Reader.GetOrdinal("NationalNo")),
                                        Reader.GetByte(Reader.GetOrdinal("Gender")),
                                        Reader.GetString(Reader.GetOrdinal("FirstName")),
                                        Reader.IsDBNull(Reader.GetOrdinal("SecondName"))?"":Reader.GetString(Reader.GetOrdinal("SecondName")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ThirdName"))?"":Reader.GetString(Reader.GetOrdinal("ThirdName")),
                                        Reader.GetString(Reader.GetOrdinal("LastName")),
                                        
                                        Reader.GetString(Reader.GetOrdinal("Email")),
                                        Reader.GetString(Reader.GetOrdinal("Phone")),
                                        Reader.GetString(Reader.GetOrdinal("Address")),
                                        Reader.GetDateTime(Reader.GetOrdinal("DateOfBirth")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ImagePath"))?"":Reader.GetString(Reader.GetOrdinal("ImagePath")),
                                        Reader.GetInt32(Reader.GetOrdinal("NationalityCountryID"))
                                    );
                            }
                            else
                                personDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                personDTO=null;
            }
            return personDTO;

        }

        public static PersonDTO GetPersonInfoByNationalNo(string NationalNo)
        {
           PersonDTO personDTO = null;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetPersonInfoByNationalNo", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                personDTO = new PersonDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetString(Reader.GetOrdinal("NationalNo")),
                                        Reader.GetByte(Reader.GetOrdinal("Gender")),
                                        Reader.GetString(Reader.GetOrdinal("FirstName")),
                                        Reader.IsDBNull(Reader.GetOrdinal("SecondName")) ? "" : Reader.GetString(Reader.GetOrdinal("SecondName")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ThirdName")) ? "" : Reader.GetString(Reader.GetOrdinal("ThirdName")),
                                        Reader.GetString(Reader.GetOrdinal("LastName")),

                                        Reader.GetString(Reader.GetOrdinal("Email")),
                                        Reader.GetString(Reader.GetOrdinal("Phone")),
                                        Reader.GetString(Reader.GetOrdinal("Address")),
                                        Reader.GetDateTime(Reader.GetOrdinal("DateOfBirth")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ImagePath")) ? "" : Reader.GetString(Reader.GetOrdinal("ImagePath")),
                                        Reader.GetInt32(Reader.GetOrdinal("NationalityCountryID"))
                                    );
                            }
                            else
                                personDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                personDTO = null;
            }
            return personDTO;
        }

        public static bool UpdatePerson(PersonDTO personDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdatePerson", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@NationalNo", personDTO.NationalNo);
                        Command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);


                        if (personDTO.SecondName != "" && personDTO.SecondName != null)
                            Command.Parameters.AddWithValue("@SecondName", personDTO.SecondName);
                        else
                            Command.Parameters.AddWithValue("@SecondName", DBNull.Value);

                        if (personDTO.ThirdName != "" && personDTO.ThirdName != null)
                            Command.Parameters.AddWithValue("@ThirdName", personDTO.ThirdName);
                        else
                            Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

                        Command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                        Command.Parameters.AddWithValue("@Email", personDTO.Email);

                        Command.Parameters.AddWithValue("@Phone", personDTO.Phone);
                        Command.Parameters.AddWithValue("@Address", personDTO.Address);
                        Command.Parameters.AddWithValue("@Gender", personDTO.Gender);
                        Command.Parameters.AddWithValue("@DateOfBirth", personDTO.DateOfBirth);
                        Command.Parameters.AddWithValue("@NationalityCountryID", personDTO.NationalityCountryID);


                        if (personDTO.ImagePath != "" && personDTO.ImagePath != null)
                            Command.Parameters.AddWithValue("@ImagePath", personDTO.ImagePath);
                        else
                            Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);


                        Command.Parameters.AddWithValue("@PersonID", personDTO.PersonID);


                        RowsEffected = Command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                RowsEffected = 0;
            }
            return RowsEffected > 0;
        }

        public static bool DeletePerson(int PersonID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeletePerson", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@PersonID", PersonID);

                        RowsEffected = Command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                RowsEffected = 0;
            }
            return RowsEffected > 0;

        }

        public static List<PeopleListDTO> GetAllPeople()
        {
            List<PeopleListDTO> PeopleLists = new List<PeopleListDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllPeople", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                PeopleLists.Add
                                    (
                                       new PeopleListDTO
                                       (
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetString(Reader.GetOrdinal("NationalNo")),
                                        Reader.GetByte(Reader.GetOrdinal("Gender")),
                                        Reader.GetString(Reader.GetOrdinal("GenderCaption")),
                                        Reader.GetString(Reader.GetOrdinal("FirstName")),
                                        Reader.IsDBNull(Reader.GetOrdinal("SecondName")) ? "" : Reader.GetString(Reader.GetOrdinal("SecondName")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ThirdName")) ? "" : Reader.GetString(Reader.GetOrdinal("ThirdName")),
                                        Reader.GetString(Reader.GetOrdinal("LastName")),

                                        Reader.GetString(Reader.GetOrdinal("Email")),
                                        Reader.GetString(Reader.GetOrdinal("Phone")),
                                        Reader.GetString(Reader.GetOrdinal("Address")),
                                        Reader.GetDateTime(Reader.GetOrdinal("DateOfBirth")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ImagePath")) ? "" : Reader.GetString(Reader.GetOrdinal("ImagePath")),
                                        Reader.GetInt32(Reader.GetOrdinal("NationalityCountryID")),
                                        Reader.GetString(Reader.GetOrdinal("CountryName"))
                                       )
                                    );
                            }
                               
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);

            }
            return PeopleLists;

        }

        public static bool IsPersonExistByID(int PersonID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsPersonExistByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@PersonID", PersonID);

                        SqlParameter IsFoundParam = new SqlParameter("@IsFound", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        Command.Parameters.Add(IsFoundParam);

                        Command.ExecuteNonQuery();
                        IsFound = ((int)IsFoundParam.Value == 1);
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                IsFound = false;
            }
            return IsFound;
        }

        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsPersonExistByNationalNo", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        SqlParameter IsFoundParam = new SqlParameter("@IsFound", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        Command.Parameters.Add(IsFoundParam);

                        Command.ExecuteNonQuery();
                        IsFound = ((int)IsFoundParam.Value==1);
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                IsFound = false;
            }
            return IsFound;
        }
    }
}
