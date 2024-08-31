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
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class ApplicationDTO
    {
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        


        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }
        

        public DateTime LastStatusDate { get; set; }

        public int ApplicationStatus { get; set; }
       
       
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public ApplicationDTO(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID, DateTime lastStatusDate, 
            int applicationStatus, float paidFees, int createdByUserID)
        {
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            LastStatusDate = lastStatusDate;
            ApplicationStatus = applicationStatus;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
        }
    }


    public class clsApplicationData
    {
        public static int AddNewApplication(ApplicationDTO applicationDTO)
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

                        Command.Parameters.AddWithValue("@ApplicantPersonID", applicationDTO.ApplicantPersonID);
                        Command.Parameters.AddWithValue("@ApplicationDate", applicationDTO.ApplicationDate);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", applicationDTO.ApplicationTypeID);
                        Command.Parameters.AddWithValue("@ApplicationStatus", applicationDTO.ApplicationStatus);
                        Command.Parameters.AddWithValue("@LastStatusDate", applicationDTO.LastStatusDate);
                        Command.Parameters.AddWithValue("@PaidFees", applicationDTO.PaidFees);
                        Command.Parameters.AddWithValue("@CreatedByUserID", applicationDTO.CreatedByUserID);

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

        public static ApplicationDTO GetApplicationInfoByID(int ApplicationID)
        {
            ApplicationDTO applicationDTO ;
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
                                applicationDTO = new ApplicationDTO(
                                       Reader.GetInt32(Reader.GetOrdinal("ApplicationID")),
                                       Reader.GetInt32(Reader.GetOrdinal("ApplicantPersonID")),
                                       Reader.GetDateTime(Reader.GetOrdinal("ApplicationDate")),
                                       Reader.GetInt32(Reader.GetOrdinal("ApplicationTypeID")),
                                       Reader.GetDateTime(Reader.GetOrdinal("LastStatusDate")),
                                       Reader.GetByte(Reader.GetOrdinal("ApplicationStatus")),
                                       (float)Reader.GetDecimal(Reader.GetOrdinal("PaidFees")),
                                       Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID"))
                                       
                                    );
                            }
                            else
                                applicationDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                applicationDTO = null;
            }
            return applicationDTO;
        }


        public static bool UpdateApplication(ApplicationDTO applicationDTO)
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

                        Command.Parameters.AddWithValue("@ApplicationID", applicationDTO.ApplicationID);
                        Command.Parameters.AddWithValue("@ApplicantPersonID", applicationDTO.ApplicantPersonID);
                        Command.Parameters.AddWithValue("@ApplicationDate", applicationDTO.ApplicationDate);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", applicationDTO.ApplicationTypeID);
                        Command.Parameters.AddWithValue("@ApplicationStatus", applicationDTO.ApplicationStatus);
                        Command.Parameters.AddWithValue("@LastStatusDate", applicationDTO.LastStatusDate);
                        Command.Parameters.AddWithValue("@PaidFees", applicationDTO.PaidFees);
                        Command.Parameters.AddWithValue("@CreatedByUserID", applicationDTO.CreatedByUserID);


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


        public static List<ApplicationDTO> GetAllApplications()
        {
            List<ApplicationDTO> ApplicationsList = new List<ApplicationDTO> ();
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
                            while(Reader.Read())
                            {
                                ApplicationsList.Add(new ApplicationDTO(
                                       Reader.GetInt32(Reader.GetOrdinal("ApplicationID")),
                                       Reader.GetInt32(Reader.GetOrdinal("ApplicantPersonID")),
                                       Reader.GetDateTime(Reader.GetOrdinal("ApplicationDate")),
                                       Reader.GetInt32(Reader.GetOrdinal("ApplicationTypeID")),
                                       Reader.GetDateTime(Reader.GetOrdinal("LastStatusDate")),
                                       Reader.GetByte(Reader.GetOrdinal("ApplicationStatus")),
                                       (float)Reader.GetDecimal(Reader.GetOrdinal("PaidFees")),
                                       Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID"))

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
            return ApplicationsList;
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
