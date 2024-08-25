using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Common;

namespace DVLD_DataAccess
{
    public class clsViolationData
    {
         public static bool GetViolationInfoByID(int ViolationID, ref string ViolationTitle, ref string ViolationDescription, ref float FineFees)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    string query = "SELECT * FROM Violations WHERE ViolationID = @ViolationID";
                    using(SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ViolationID", ViolationID);
                        using(SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;
                                ViolationDescription = (string)Reader["ViolationDescription"];
                                ViolationTitle = (string)Reader["ViolationTitle"];
                                FineFees = Convert.ToSingle(Reader["FineFees"]);
                            }
                            else
                                IsFound = false;
                             
                        }
                    }
                }
            }
            catch(Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}",EventLogEntryType.Error);
                IsFound = false;
            }
            return IsFound;
          
        }

        public static bool GetViolationInfoByTitle(string ViolationTitle, ref int ViolationID,  ref string ViolationDescription, ref float FineFees)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    string query = "SELECT * FROM Violations WHERE ViolationTitle = @ViolationTitle";
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ViolationTitle", ViolationTitle);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;
                                ViolationDescription = (string)Reader["ViolationDescription"];
                                ViolationID = (int)Reader["ViolationID"];
                                FineFees = Convert.ToSingle(Reader["FineFees"]);
                            }
                            else
                                IsFound = false;
                        }
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
        public static bool UpdateViolation(int ViolationID, string ViolationTitle, string ViolationDescription, float FineFees)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    string query = @"Update Violations 
                            set ViolationTitle=@ViolationTitle,
                                FineFees=@FineFees,
                                ViolationDescription=@ViolationDescription
                              WHERE ViolationID = @ViolationID";
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ViolationID", ViolationID);
                        Command.Parameters.AddWithValue("@ViolationDescription", ViolationDescription);
                        Command.Parameters.AddWithValue("@ViolationTitle", ViolationTitle);
                        Command.Parameters.AddWithValue("@FineFees", FineFees);


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

        public static int AddNewViolation(string ViolationTitle, string ViolationDescription, float FineFees)
        {
            int ViolationID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    
                    using (SqlCommand Command = new SqlCommand("SP_AddNewViolation", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ViolationDescription", ViolationDescription);
                        Command.Parameters.AddWithValue("@ViolationTitle", ViolationTitle);
                        Command.Parameters.AddWithValue("@FineFees", FineFees);

                        SqlParameter outputIdParam = new SqlParameter("@NewViolationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        ViolationID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                ViolationID = -1;
            }
            return ViolationID ;  
        }

        public static DataTable GetAllViolations()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllViolations", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                                dt.Load(Reader);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
               
            }
            return dt;
        }
       
        public static bool IsViolationExistByViolationID(int ViolationID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsViolationExistByViolationID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ViolationID", ViolationID);

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

        public static bool IsViolationExistByViolationTitle(string ViolationTitle)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsViolationExistByViolationTitle", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ViolationTitle", ViolationTitle);
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
