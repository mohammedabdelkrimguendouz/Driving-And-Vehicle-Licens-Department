using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Data.Common;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus,DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1; ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewApplication", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                        Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                        Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                        SqlParameter outputIdParam = new SqlParameter("@NewApplicationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        ApplicationID = (int)outputIdParam.Value;

                    }
                }
            }

            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                ApplicationID = -1;
            }
            return ApplicationID;
        }

        public static bool GetApplicationInfoByID(int ApplicationID,ref int ApplicantPersonID,ref DateTime ApplicationDate,ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate,ref float PaidFees, ref int CreatedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetApplicationInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;
                                ApplicantPersonID = (int)Reader["ApplicantPersonID"];
                                ApplicationDate = (DateTime)Reader["ApplicationDate"];
                                ApplicationTypeID = (int)Reader["ApplicationTypeID"];
                                ApplicationStatus = (byte)Reader["ApplicationStatus"];
                                LastStatusDate = (DateTime)Reader["LastStatusDate"];
                                PaidFees = Convert.ToSingle(Reader["PaidFees"]);
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


        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, 
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateApplication", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                        Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                        Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


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

        public static bool DeleteApplication(int ApplicationID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteApplication", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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


        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllApplications", Connection))
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

        public static bool UpdateStatus(int ApplicationID, byte NewStatus)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateStatus", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);


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

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsApplicationExist", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static int GetActiveApplicationID(int ApplicantPersonID,int ApplicationTypeID)
        {
            int ActiveApplicationID = -1; ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_GetActiveApplicationID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        Object Result = Command.ExecuteScalar();
                        SqlParameter outputActiveApplicationIDParam = new SqlParameter("@ActiveApplicationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputActiveApplicationIDParam);


                        Command.ExecuteNonQuery();

                        ActiveApplicationID =(outputActiveApplicationIDParam.Value==DBNull.Value)?-1: (int)outputActiveApplicationIDParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                ActiveApplicationID = -1;
            }
            return ActiveApplicationID;
        }

        public static bool DeosPersonHaveActiverApplication(int ApplicantPersonID, int ApplicationTypeID)
        {
            return GetActiveApplicationID(ApplicantPersonID, ApplicationTypeID) != -1;
        }

        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int ApplicationTypeID,int LicenseClassID)
        {
            int ActiveApplicationID = -1; ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetActiveApplicationIDForLicenseClass", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                        Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                        SqlParameter outputActiveApplicationIDParam = new SqlParameter("@ActiveApplicationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputActiveApplicationIDParam);


                        Command.ExecuteNonQuery();

                        ActiveApplicationID = (outputActiveApplicationIDParam.Value == DBNull.Value) ? -1 : (int)outputActiveApplicationIDParam.Value;

                    }
                }
            }

            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                ActiveApplicationID = -1;
            }
            return ActiveApplicationID;
        }
    }
}
