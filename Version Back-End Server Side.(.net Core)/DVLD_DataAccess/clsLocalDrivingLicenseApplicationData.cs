using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{

    public class LocalDrivingLicenseApplicationDTO
    {
        public int LocalDrivingLicenseApplicationID { get; set; }

        public int LicenseClassID { get; set; }

        public int ApplicationID { get; set; }

        public LocalDrivingLicenseApplicationDTO(int localDrivingLicenseApplicationID, int licenseClassID, int applicationID)
        {
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            LicenseClassID = licenseClassID;
            ApplicationID = applicationID;
        }
    }

    public class LocalDrivingLicenseApplicationsListDTO
    {
        public int LocalDrivingLicenseApplicationID { get; set; }

        public string ClassName { get; set; }

        public string NationalNo {  get; set; }

        public string FullName {  get; set; }

        public DateTime ApplicationDate { get; set; }

        public int PassedTestCount { get; set; }

        public string Status { get; set; }

        public LocalDrivingLicenseApplicationsListDTO(int localDrivingLicenseApplicationID, string className, string nationalNo, 
            string fullName, DateTime applicationDate, int passedTestCount, string status)
        {
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            ClassName = className;
            NationalNo = nationalNo;
            FullName = fullName;
            ApplicationDate = applicationDate;
            PassedTestCount = passedTestCount;
            Status = status;
        }
    }
    public class clsLocalDrivingLicenseApplicationData
    {
        public static int AddNewLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationDTO localDrivingLicenseApplicationDTO)
        {
            int LocalDrivingLicenseApplicationID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewLocalDrivingLicenseApplication", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicationID", localDrivingLicenseApplicationDTO.ApplicationID);
                        Command.Parameters.AddWithValue("@LicenseClassID", localDrivingLicenseApplicationDTO.LicenseClassID);
                        SqlParameter outputIdParam = new SqlParameter("@NewLocalDrivingLicenseApplicationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        LocalDrivingLicenseApplicationID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                LocalDrivingLicenseApplicationID = -1;
            }
            return LocalDrivingLicenseApplicationID;
        }

        public static LocalDrivingLicenseApplicationDTO GetLocalDrivingLicenseApplicationInfoByID(int LocalDrivingLicenseApplicationID)
        {
            LocalDrivingLicenseApplicationDTO localDrivingLicenseApplicationDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetLocalDrivingLicenseApplicationInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                localDrivingLicenseApplicationDTO = new LocalDrivingLicenseApplicationDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                    Reader.GetInt32(Reader.GetOrdinal("LicenseClassID")),
                                    Reader.GetInt32(Reader.GetOrdinal("ApplicationID"))
                                    );
                            }
                            else
                                localDrivingLicenseApplicationDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                localDrivingLicenseApplicationDTO = null;
            }
            return localDrivingLicenseApplicationDTO;
        }

        public static LocalDrivingLicenseApplicationDTO GetLocalDrivingLicenseApplicationInfoByApplicationID( int ApplicationID)
        {
            LocalDrivingLicenseApplicationDTO localDrivingLicenseApplicationDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetLocalDrivingLicenseApplicationInfoByApplicationID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                localDrivingLicenseApplicationDTO = new LocalDrivingLicenseApplicationDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                    Reader.GetInt32(Reader.GetOrdinal("LicenseClassID")),
                                    Reader.GetInt32(Reader.GetOrdinal("ApplicationID"))
                                    );
                            }
                            else
                                localDrivingLicenseApplicationDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                localDrivingLicenseApplicationDTO = null;
            }
            return localDrivingLicenseApplicationDTO;
        }


        public static bool UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationDTO localDrivingLicenseApplicationDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateLocalDrivingLicenseApplication", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationDTO.LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@ApplicationID", localDrivingLicenseApplicationDTO.ApplicationID);
                        Command.Parameters.AddWithValue("@LicenseClassID", localDrivingLicenseApplicationDTO.LicenseClassID);


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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();

                    using (SqlCommand Command = new SqlCommand("SP_DeleteLocalDrivingLicenseApplication", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        
        public static bool DoesPassedTestType(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DoesPassedTestType", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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

        public static bool DeosAttendTestType(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeosAttendTestType", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static int GetTotalTrialsPerTest(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            int TotalTrialsPerTest = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetTotalTrialsPerTest", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                        SqlParameter TotalTrialsPerTestParam = new SqlParameter("@TotalTrialsPerTest", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(TotalTrialsPerTestParam);


                        Command.ExecuteNonQuery();

                        TotalTrialsPerTest = (int)TotalTrialsPerTestParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                TotalTrialsPerTest = 0;
            }
            return TotalTrialsPerTest;
        }

        public static List<LocalDrivingLicenseApplicationsListDTO> GetAllLocalDrivingLicenseApplications()
        {
            List<LocalDrivingLicenseApplicationsListDTO> LocalDrivingLicenseApplicationsList = new List<LocalDrivingLicenseApplicationsListDTO> ();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllLocalDrivingLicenseApplications", Connection))
                    {
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                LocalDrivingLicenseApplicationsList.Add(new LocalDrivingLicenseApplicationsListDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                    Reader.GetString(Reader.GetOrdinal("ClassName")),
                                    Reader.GetString(Reader.GetOrdinal("NationalNo")),
                                    Reader.GetString(Reader.GetOrdinal("FullName")),
                                    Reader.GetDateTime(Reader.GetOrdinal("ApplicationDate")),
                                    Reader.GetInt32(Reader.GetOrdinal("PassedTestCount")),
                                    Reader.GetString(Reader.GetOrdinal("Status"))
                                    
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
            return LocalDrivingLicenseApplicationsList;
        }

        public static bool IsThereAnActiveScheduleTest(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsThereAnActiveScheduleTest", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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
