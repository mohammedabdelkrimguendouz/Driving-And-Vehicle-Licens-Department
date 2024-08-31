using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{

    public class UserDTO
    {
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public UserDTO(int personID, int userID, string userName, string password, bool isActive)
        {
            PersonID = personID;
            UserID = userID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }
    }

    public class UsersListDTO
    {
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }

        public UsersListDTO(int personID, int userID, string userName, string fullName, bool isActive)
        {
            PersonID = personID;
            UserID = userID;
            UserName = userName;
            FullName = fullName;
            IsActive = isActive;
        }
    }
    public class clsUserData
    {
        public static int AddNewUser(UserDTO userDTO)
        {
            int UserID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewUser", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                        Command.Parameters.AddWithValue("@UserName", userDTO.UserName);
                        Command.Parameters.AddWithValue("@Password", userDTO.Password);
                        Command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);

                        SqlParameter outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        UserID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                UserID = -1;
            }
            return UserID;
        }

        public static UserDTO GetUserInfoByID(int UserID)
        {
            UserDTO userDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetUserInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                userDTO = new UserDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetInt32(Reader.GetOrdinal("UserID")),
                              
                                        Reader.GetString(Reader.GetOrdinal("UserName")),
                                        Reader.GetString(Reader.GetOrdinal("Password")),
                                        Reader.GetBoolean(Reader.GetOrdinal("IsActive"))
                                    );

                            }
                            else
                                userDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                userDTO = null;
            }
            return userDTO;
            
        }

        public static UserDTO GetUserInfoByPersonID( int PersonID)
        {
            UserDTO userDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetUserInfoByPersonID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                userDTO = new UserDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetInt32(Reader.GetOrdinal("UserID")),

                                        Reader.GetString(Reader.GetOrdinal("UserName")),
                                        Reader.GetString(Reader.GetOrdinal("Password")),
                                        Reader.GetBoolean(Reader.GetOrdinal("IsActive"))
                                    );
                            }
                            else
                                userDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                userDTO = null;
            }
            return userDTO;
            
        }

        public static UserDTO GetUserInfoByUserNameAndPassword(string UserName, string Password)
        {
            UserDTO userDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetUserInfoByUserNameAndPassword", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@UserName", UserName);
                        Command.Parameters.AddWithValue("@Password", Password);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                userDTO = new UserDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetInt32(Reader.GetOrdinal("UserID")),

                                        Reader.GetString(Reader.GetOrdinal("UserName")),
                                        Reader.GetString(Reader.GetOrdinal("Password")),
                                        Reader.GetBoolean(Reader.GetOrdinal("IsActive"))
                                    );
                            }
                            else
                                userDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                userDTO = null;
            }
            return userDTO;
        }
        public static UserDTO GetUserInfoByUserName(string UserName)
        {
            UserDTO userDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetUserInfoByUserName", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@UserName", UserName);

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                userDTO = new UserDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetInt32(Reader.GetOrdinal("UserID")),

                                        Reader.GetString(Reader.GetOrdinal("UserName")),
                                        Reader.GetString(Reader.GetOrdinal("Password")),
                                        Reader.GetBoolean(Reader.GetOrdinal("IsActive"))
                                    );
                            }
                            else
                                userDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                userDTO = null;
            }
            return userDTO;
        }

        public static bool UpdateUser(UserDTO userDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateUser", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@UserID", userDTO.UserID);
                        Command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                        Command.Parameters.AddWithValue("@UserName", userDTO.UserName);
                        Command.Parameters.AddWithValue("@Password", userDTO.Password);
                        Command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);


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

        public static bool DeleteUser(int UserID)
        {

            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteUser", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@UserID", UserID);

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

        public static List<UsersListDTO> GetAllUsers()
        {
            List<UsersListDTO> UsersList = new List<UsersListDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllUsers", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                UsersList.Add(new UsersListDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetInt32(Reader.GetOrdinal("UserID")),

                                        Reader.GetString(Reader.GetOrdinal("UserName")),
                                        Reader.GetString(Reader.GetOrdinal("FullName")),
                                        Reader.GetBoolean(Reader.GetOrdinal("IsActive"))
                                    ));
                            }
                                
                        }


                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
            
            }

            return UsersList;
        }

        
        public static bool IsUserExistByUserID(int UserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsUserExistByUserID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@UserID", UserID);
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

        public static bool IsUserExistByUserName(string UserName)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsUserExistByUserName", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@UserName", UserName);

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
        
        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsUserExistForPersonID", Connection))
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
        
       
    }
}
