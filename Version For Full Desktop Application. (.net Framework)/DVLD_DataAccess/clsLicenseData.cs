using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {
         public static int AddNewLicense(int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes,float PaidFees,bool IsActive
            ,byte IssueReason,int CreatedByUserID)
        {
            int LicenseID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewLicense", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        Command.Parameters.AddWithValue("@IssueReason", IssueReason);
                        Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        Command.Parameters.AddWithValue("@IsActive", IsActive);
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@DriverID", DriverID);
                        Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                        Command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                        if(Notes!="" && Notes!=null)
                            Command.Parameters.AddWithValue("@Notes", Notes);
                        else
                            Command.Parameters.AddWithValue("@Notes", DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewLicenseID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        LicenseID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                LicenseID = -1;
            }
            return LicenseID;

        }

        public static bool GetLicenseInfoByID(int LicenseID,ref int ApplicationID, ref int DriverID,
            ref int LicenseClass,ref  DateTime IssueDate,ref DateTime ExpirationDate,ref string Notes,ref float PaidFees,ref bool IsActive
            , ref byte IssueReason,ref int CreatedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetLicenseInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;
                                ApplicationID = (int)Reader["ApplicationID"];
                                DriverID = (int)Reader["DriverID"];
                                LicenseClass = (int)Reader["LicenseClass"];
                                IssueDate = (DateTime)Reader["IssueDate"];
                                ExpirationDate = (DateTime)Reader["ExpirationDate"];
                                Notes = (Reader["Notes"]==DBNull.Value) ?"":(string)Reader["Notes"];
                                PaidFees = Convert.ToSingle(Reader["PaidFees"]);
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


        public static bool UpdateLicense(int LicenseID,int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive
            , byte IssueReason, int CreatedByUserID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateLicense", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        Command.Parameters.AddWithValue("@IssueReason", IssueReason);
                        Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        Command.Parameters.AddWithValue("@IsActive", IsActive);
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@DriverID", DriverID);
                        Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                        Command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                        if (Notes != "" && Notes != null)
                            Command.Parameters.AddWithValue("@Notes", Notes);
                        else
                            Command.Parameters.AddWithValue("@Notes", DBNull.Value);


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

       

        public static DataTable GetDriverLocalLicenses(int DriverID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetDriverLocalLicenses", Connection))
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
        

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllLicenses", Connection))
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

        static public bool DeactivateLicense(int LicenseID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeactivateLicense", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        
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

        public static int GetActiveLicenseIDByPersonID(int PersonID,int LicenseClassID)
        {
            int LicenseID = -1; ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetActiveLicenseIDByPersonID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        SqlParameter outputIdParam = new SqlParameter("@NewLicenseID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        LicenseID =(outputIdParam.Value==DBNull.Value)?-1:(int)outputIdParam.Value;


                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                LicenseID = -1;
            }

            return LicenseID;
        }


    }
}
