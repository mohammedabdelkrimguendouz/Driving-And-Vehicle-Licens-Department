using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public class clsInternationalLicenseData
    {
        public static int AddNewInternationalLicense(int ApplicationID, int DriverID,
           int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive,
           byte IssueReason,int CreatedByUserID)
        {
            int InternationalLicenseID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewInternationalLicense", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        Command.Parameters.AddWithValue("@IsActive", IsActive);
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@DriverID", DriverID);
                        Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                        Command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                        Command.Parameters.AddWithValue("@IssueReason", IssueReason);

                        SqlParameter outputIdParam = new SqlParameter("@NewInternationalLicenseID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        InternationalLicenseID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                InternationalLicenseID = -1;
            }
            return InternationalLicenseID;
        }

        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID,ref int ApplicationID,ref int DriverID,
           ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate,ref DateTime ExpirationDate,ref bool IsActive,ref byte IssueReason,
           ref int CreatedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetInternationalLicenseInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;
                                ApplicationID = (int)Reader["ApplicationID"];
                                DriverID = (int)Reader["DriverID"];
                                IssuedUsingLocalLicenseID = (int)Reader["IssuedUsingLocalLicenseID"];
                                IssueDate = (DateTime)Reader["IssueDate"];
                                ExpirationDate = (DateTime)Reader["ExpirationDate"];
                                IsActive = (bool)Reader["IsActive"];
                                IssueReason = (byte)Reader["IssueReason"];
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
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


        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
           int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive,byte IssueReason,
           int CreatedByUserID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateInternationalLicense", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        Command.Parameters.AddWithValue("@IssueReason", IssueReason);
                        Command.Parameters.AddWithValue("@IsActive", IsActive);
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@DriverID", DriverID);
                        Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                        Command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);


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


        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetDriverInternationalLicenses", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@DriverID", DriverID);
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

        public static int  GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int ActiveInternationalLicenseID = -1; ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetActiveInternationalLicenseIDByDriverID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@DriverID", DriverID);

                        SqlParameter outputActiveInternationalLicenseIDParam = new SqlParameter("@ActiveInternationalLicenseID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputActiveInternationalLicenseIDParam);


                        Command.ExecuteNonQuery();

                        ActiveInternationalLicenseID =(outputActiveInternationalLicenseIDParam.Value==DBNull.Value)?-1:(int)outputActiveInternationalLicenseIDParam.Value;


                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                ActiveInternationalLicenseID = -1;
            }

            return ActiveInternationalLicenseID;
        }
        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllInternationalLicenses", Connection))
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

        static public bool DeactivateInternationalLicense(int InternationalLicenseID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeactivateInternationalLicense", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
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

    }
}
