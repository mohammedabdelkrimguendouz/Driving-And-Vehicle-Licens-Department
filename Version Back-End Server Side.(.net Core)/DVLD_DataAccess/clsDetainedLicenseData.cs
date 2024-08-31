using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DVLD_DataAccess
{

    public class DetainedLicenseDTO
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }

        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }

        public int ViolationID { get; set; }
        
        public int CreatedByUserID { get; set; }
       
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
       
        public int? ReleaseApplicationID { get; set; }

        public DetainedLicenseDTO(int detainID, int licenseID, DateTime detainDate, float fineFees, 
            int violationID, int createdByUserID, bool isReleased, DateTime? releaseDate,
            int? releasedByUserID, int? releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            ViolationID = violationID;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
        }
    }

    public class DetainedLicensesListDTO
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }

        public DateTime DetainDate { get; set; }
        public string ViolationTitle { get; set; }
        public float FineFees { get; set; }

        
        public string NationalNo {  get; set; }
      

        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        
        public string FullName {  get; set; }
        public int? ReleaseApplicationID { get; set; }

        public DetainedLicensesListDTO(int detainID, int licenseID, DateTime detainDate, string violationTitle,
            float fineFees, string nationalNo, bool isReleased,
            DateTime? releaseDate, string fullName, int? releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            ViolationTitle = violationTitle;
            FineFees = fineFees;
            NationalNo = nationalNo;
            
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            FullName = fullName;
            ReleaseApplicationID = releaseApplicationID;
        }
    }
    public class clsDetainedLicenseData
    {
         public static int AddNewDetainedLicense(DetainedLicenseDTO detainedLicenseDTO)
        {
            int DetainID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewDetainedLicense", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseID", detainedLicenseDTO.LicenseID);
                        Command.Parameters.AddWithValue("@ViolationID", detainedLicenseDTO.ViolationID);
                        Command.Parameters.AddWithValue("@DetainDate", detainedLicenseDTO.DetainDate);
                        Command.Parameters.AddWithValue("@FineFees", detainedLicenseDTO.FineFees);
                        Command.Parameters.AddWithValue("@CreatedByUserID", detainedLicenseDTO.CreatedByUserID);

                        SqlParameter outputIdParam = new SqlParameter("@NewDetainID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        DetainID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                DetainID = -1;
            }
            return DetainID;
        }

        public static DetainedLicenseDTO GetDetainedLicenseInfoByID(int DetainID)
        {
            DetainedLicenseDTO  detainedLicenseDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetDetainedLicenseInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@DetainID", DetainID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                           

                            if (Reader.Read())
                            {
                                detainedLicenseDTO = new DetainedLicenseDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("DetainID")),
                                     Reader.GetInt32(Reader.GetOrdinal("LicenseID")),
                                     Reader.GetDateTime(Reader.GetOrdinal("DetainDate")),
                                     (float)Reader.GetDecimal(Reader.GetOrdinal("FineFees")),
                                     Reader.GetInt32(Reader.GetOrdinal("ViolationID")),
                                     Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                     Reader.GetBoolean(Reader.GetOrdinal("IsReleased")),
                                     Reader.IsDBNull(Reader.GetOrdinal("ReleaseDate")) ?null: Reader.GetDateTime(Reader.GetOrdinal("ReleaseDate")),
                                     Reader.IsDBNull(Reader.GetOrdinal("ReleasedByUserID")) ? null : Reader.GetInt32(Reader.GetOrdinal("ReleasedByUserID")),
                                     Reader.IsDBNull(Reader.GetOrdinal("ReleaseApplicationID")) ? null : Reader.GetInt32(Reader.GetOrdinal("ReleaseApplicationID"))
                                    );
                            }
                            else
                                detainedLicenseDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                detainedLicenseDTO = null;
            }
            return detainedLicenseDTO;
        }

        public static DetainedLicenseDTO GetDetainedLicenseInfoByLicenseID(int LicenseID)
        {
            DetainedLicenseDTO detainedLicenseDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetDetainedLicenseInfoByLicenseID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                detainedLicenseDTO = new DetainedLicenseDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("DetainID")),
                                     Reader.GetInt32(Reader.GetOrdinal("LicenseID")),
                                     Reader.GetDateTime(Reader.GetOrdinal("DetainDate")),
                                     (float)Reader.GetDecimal(Reader.GetOrdinal("FineFees")),
                                     Reader.GetInt32(Reader.GetOrdinal("ViolationID")),
                                     Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                     Reader.GetBoolean(Reader.GetOrdinal("IsReleased")),
                                    Reader.IsDBNull(Reader.GetOrdinal("ReleaseDate")) ? null : Reader.GetDateTime(Reader.GetOrdinal("ReleaseDate")),
                                     Reader.IsDBNull(Reader.GetOrdinal("ReleasedByUserID")) ? null : Reader.GetInt32(Reader.GetOrdinal("ReleasedByUserID")),
                                     Reader.IsDBNull(Reader.GetOrdinal("ReleaseApplicationID")) ? null : Reader.GetInt32(Reader.GetOrdinal("ReleaseApplicationID"))
                                    );
                            }
                            else
                                detainedLicenseDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                detainedLicenseDTO = null;
            }
            return detainedLicenseDTO;
        }


        public static bool UpdateDetainedLicense(DetainedLicenseDTO detainedLicenseDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateDetainedLicense", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@DetainID", detainedLicenseDTO.DetainID);
                        Command.Parameters.AddWithValue("@ViolationID", detainedLicenseDTO.ViolationID);
                        Command.Parameters.AddWithValue("@LicenseID", detainedLicenseDTO.LicenseID);
                        Command.Parameters.AddWithValue("@DetainDate", detainedLicenseDTO.DetainDate);
                        Command.Parameters.AddWithValue("@FineFees", detainedLicenseDTO.FineFees);
                        Command.Parameters.AddWithValue("@CreatedByUserID", detainedLicenseDTO.CreatedByUserID);


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

        

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsLicenseDetained", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);
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

        public static bool ReleaseDetainedLicense(int DetainID,int ReleasedByUserID, int ReleaseApplicationID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_ReleaseDetainedLicense", Connection))
                    {

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@DetainID", DetainID);
                        Command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                        Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                        Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);


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
        public static List<DetainedLicensesListDTO> GetAllDetainedLicenses()
        {
            List<DetainedLicensesListDTO> DetainedLicenseList = new List<DetainedLicensesListDTO> ();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllDetainedLicenses", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                DetainedLicenseList.Add(new DetainedLicensesListDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("DetainID")),
                                     Reader.GetInt32(Reader.GetOrdinal("LicenseID")),
                                     Reader.GetDateTime(Reader.GetOrdinal("DetainDate")),
                                     Reader.GetString(Reader.GetOrdinal("ViolationTitle")),
                                     (float)Reader.GetDecimal(Reader.GetOrdinal("FineFees")),
                                     
                                     Reader.GetString(Reader.GetOrdinal("NationalNo")),
                                     Reader.GetBoolean(Reader.GetOrdinal("IsReleased")),
                                     Reader.IsDBNull(Reader.GetOrdinal("ReleaseDate")) ? null : Reader.GetDateTime(Reader.GetOrdinal("ReleaseDate")),
                                     Reader.GetString(Reader.GetOrdinal("FullName")),
                                    Reader.IsDBNull(Reader.GetOrdinal("ReleaseApplicationID")) ? null : Reader.GetInt32(Reader.GetOrdinal("ReleaseApplicationID"))
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
            return DetainedLicenseList;
        }
    }
}
