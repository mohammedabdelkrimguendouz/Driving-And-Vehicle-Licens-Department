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
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{

    public class LicenseDTO
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
       
        public int DriverID { get; set; }
        
        public int LicenseClass { get; set; }
        
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        

        public int CreatedByUserID { get; set; }

        public LicenseDTO(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate,
            DateTime expirationDate, string notes, float paidFees, bool isActive, 
            byte issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
        }
    }

    public class DriverLocalLicensesDTO
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }

        public string ClassName {  get; set; }
        public DateTime IssueDate {  get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public DriverLocalLicensesDTO(int licenseID, int applicationID, string className, DateTime issueDate,
            DateTime expirationDate, bool isActive)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            ClassName = className;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
        }
    }

    public class clsLicenseData
    {
         public static int AddNewLicense(LicenseDTO licenseDTO)
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

                        Command.Parameters.AddWithValue("@CreatedByUserID", licenseDTO.CreatedByUserID);
                        Command.Parameters.AddWithValue("@IssueReason", licenseDTO.IssueReason);
                        Command.Parameters.AddWithValue("@PaidFees", licenseDTO.PaidFees);
                        Command.Parameters.AddWithValue("@IsActive", licenseDTO.IsActive);
                        Command.Parameters.AddWithValue("@ApplicationID", licenseDTO.ApplicationID);
                        Command.Parameters.AddWithValue("@DriverID", licenseDTO.DriverID);
                        Command.Parameters.AddWithValue("@LicenseClass", licenseDTO.LicenseClass);
                        Command.Parameters.AddWithValue("@IssueDate", licenseDTO.IssueDate);
                        Command.Parameters.AddWithValue("@ExpirationDate", licenseDTO.ExpirationDate);
                        if(licenseDTO.Notes != "" && licenseDTO.Notes != null)
                            Command.Parameters.AddWithValue("@Notes", licenseDTO.Notes);
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

        public static LicenseDTO GetLicenseInfoByID(int LicenseID)
        {
            LicenseDTO licenseDTO ;
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
                                licenseDTO = new LicenseDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LicenseID")),
                                     Reader.GetInt32(Reader.GetOrdinal("ApplicationID")),
                                      Reader.GetInt32(Reader.GetOrdinal("DriverID")),
                                       Reader.GetInt32(Reader.GetOrdinal("LicenseClass")),
                                        Reader.GetDateTime(Reader.GetOrdinal("IssueDate")),
                                         Reader.GetDateTime(Reader.GetOrdinal("ExpirationDate")),
                                         Reader.IsDBNull(Reader.GetOrdinal("Notes")) ? "" : Reader.GetString(Reader.GetOrdinal("Notes")),
                                           (float)Reader.GetDecimal(Reader.GetOrdinal("PaidFees")),
                                            Reader.GetBoolean(Reader.GetOrdinal("IsActive")),
                                        Reader.GetByte(Reader.GetOrdinal("IssueReason")),
                                        Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID"))

                                    );
                            }
                            else
                                licenseDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                licenseDTO = null;
            }
            return licenseDTO;
        }


        public static bool UpdateLicense(LicenseDTO licenseDTO)
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

                        Command.Parameters.AddWithValue("@LicenseID", licenseDTO.LicenseID);
                        Command.Parameters.AddWithValue("@CreatedByUserID", licenseDTO.CreatedByUserID);
                        Command.Parameters.AddWithValue("@IssueReason", licenseDTO.IssueReason);
                        Command.Parameters.AddWithValue("@PaidFees", licenseDTO.PaidFees);
                        Command.Parameters.AddWithValue("@IsActive", licenseDTO.IsActive);
                        Command.Parameters.AddWithValue("@ApplicationID", licenseDTO.ApplicationID);
                        Command.Parameters.AddWithValue("@DriverID", licenseDTO.DriverID);
                        Command.Parameters.AddWithValue("@LicenseClass", licenseDTO.LicenseClass);
                        Command.Parameters.AddWithValue("@IssueDate", licenseDTO.IssueDate);
                        Command.Parameters.AddWithValue("@ExpirationDate", licenseDTO.ExpirationDate);
                        if (licenseDTO.Notes != "" && licenseDTO.Notes != null)
                            Command.Parameters.AddWithValue("@Notes", licenseDTO.Notes);
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

        public static List<DriverLocalLicensesDTO> GetDriverLocalLicenses(int DriverID)
        {
            List<DriverLocalLicensesDTO> DriverLocalLicensesList = new List<DriverLocalLicensesDTO> ();
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
                            while(Reader.Read())
                            {
                                DriverLocalLicensesList.Add(new DriverLocalLicensesDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LicenseID")),
                                     Reader.GetInt32(Reader.GetOrdinal("ApplicationID")),
                                      Reader.GetString(Reader.GetOrdinal("ClassName")),
                                       
                                        Reader.GetDateTime(Reader.GetOrdinal("IssueDate")),
                                         Reader.GetDateTime(Reader.GetOrdinal("ExpirationDate")),
                                        
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

            return DriverLocalLicensesList;
  
        }
        

        public static List<LicenseDTO> GetAllLicenses()
        {
            List<LicenseDTO> LicensesList = new List<LicenseDTO>();
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
                            while (Reader.Read())
                            {
                                LicensesList.Add(new LicenseDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LicenseID")),
                                     Reader.GetInt32(Reader.GetOrdinal("ApplicationID")),
                                      Reader.GetInt32(Reader.GetOrdinal("DriverID")),
                                       Reader.GetInt32(Reader.GetOrdinal("LicenseClass")),
                                        Reader.GetDateTime(Reader.GetOrdinal("IssueDate")),
                                         Reader.GetDateTime(Reader.GetOrdinal("ExpirationDate")),
                                          Reader.IsDBNull(Reader.GetOrdinal("Notes"))?"":Reader.GetString(Reader.GetOrdinal("Notes")),
                                           (float)Reader.GetDecimal(Reader.GetOrdinal("PaidFees")),
                                            Reader.GetBoolean(Reader.GetOrdinal("IsActive")),
                                        Reader.GetByte(Reader.GetOrdinal("IssueReason")),
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

            return LicensesList;
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
            int LicenseID = -1;
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
