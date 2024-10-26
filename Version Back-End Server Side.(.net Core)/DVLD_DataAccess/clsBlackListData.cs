using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class BlackListDTO
    {
        public long ID { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public BlackListDTO(long ID, string Token, DateTime ExpiryDate)
        {
            this.ID = ID;
            this.Token = Token;
            this.ExpiryDate = ExpiryDate;
        }

    }
    public class clsBlackListData
    {
        public static long AddNewBlackList(BlackListDTO blacklistDTO)
        {
            long ID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewBlackList", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@Token", blacklistDTO.Token);
                        Command.Parameters.AddWithValue("@ExpiryDate", blacklistDTO.ExpiryDate);


                        SqlParameter outputIdParam = new SqlParameter("ID", SqlDbType.BigInt)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        ID = (long)outputIdParam.Value;

                    }
                }

            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                ID = -1;
            }
            return ID;
        }

        public static bool UpdateBlackList(BlackListDTO blacklistDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateBlackList", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ID", blacklistDTO.ID);
                        Command.Parameters.AddWithValue("@Token", blacklistDTO.Token);
                        Command.Parameters.AddWithValue("@ExpiryDate", blacklistDTO.ExpiryDate);


                        SqlParameter RowsEffectedParam = new SqlParameter("@RowsEffected", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        Command.Parameters.Add(RowsEffectedParam);

                        Command.ExecuteNonQuery();
                        RowsEffected = ((int)RowsEffectedParam.Value);


                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                RowsEffected = 0;
            }

            return (RowsEffected == 1);
        }

        public static bool DeleteBlackList(long ID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteBlackList", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ID", ID);


                        SqlParameter RowsEffectedParam = new SqlParameter("@RowsEffected", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        Command.Parameters.Add(RowsEffectedParam);

                        Command.ExecuteNonQuery();
                        RowsEffected = ((int)RowsEffectedParam.Value);

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                RowsEffected = 0;
            }

            return RowsEffected == 1;
        }

        public static List<BlackListDTO> GetAllBlackList()
        {
            List<BlackListDTO> ListBlackList = new List<BlackListDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllBlackList", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                ListBlackList.Add
                                   (
                                      new BlackListDTO
                                      (
                                         Reader.GetInt64(Reader.GetOrdinal("ID")),
Reader.GetString(Reader.GetOrdinal("Token")),
Reader.GetDateTime(Reader.GetOrdinal("ExpiryDate"))
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

            return ListBlackList;
        }

        public static bool IsBlackListExistByID(long ID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsBlackListExistByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ID", ID);
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
        public static async Task<bool> IsTokenExistAsync(string token)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsTokenExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Token", token);
                        SqlParameter isFoundParam = new SqlParameter("@IsFound", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(isFoundParam);

                        await command.ExecuteNonQueryAsync();
                        isFound = ((int)isFoundParam.Value == 1);
                    }
                }
            }
            catch (Exception ex)
            {
              
                clsEventLogData.WriteEvent($" Message : {ex.Message} \n\n Source : {ex.Source} \n\n Target Site :  {ex.TargetSite} \n\n Stack Trace :  {ex.StackTrace}", EventLogEntryType.Error);
                isFound = false;
            }
            return isFound;
        }


        public static BlackListDTO GetBlackListInfoByID(long ID)
        {
            BlackListDTO blacklistDTO = null;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetBlackListInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {

                                blacklistDTO = new BlackListDTO(Reader.GetInt64(Reader.GetOrdinal("ID")),
Reader.GetString(Reader.GetOrdinal("Token")),
Reader.GetDateTime(Reader.GetOrdinal("ExpiryDate"))
                                 );

                            }
                            else
                                blacklistDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                blacklistDTO = null;
            }
            return blacklistDTO;

        }
    }
}
