using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class InternationalLicensesListDTO
    {
        public int InternationalLicenseID { get; set; }

        public int ApplicationID { get; set; }
        public int DriverID { get; set; }

        public int IssuedUsingLocalLicenseID { get; set; }



        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public InternationalLicensesListDTO(int internationalLicenseID, int applicationID, int driverID, 
            int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive)
        {
            InternationalLicenseID = internationalLicenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
        }
    }


    public class InternationalLicenseDTO
    {
        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        
        public int IssuedUsingLocalLicenseID { get; set; }



        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

       
        public byte IssueReason { get; set; }

        public int CreatedByUserID { get; set; }

        public int ApplicationID {  get; set; }

        public InternationalLicenseDTO(int internationalLicenseID, int driverID, int issuedUsingLocalLicenseID, 
            DateTime issueDate, DateTime expirationDate, bool isActive, byte issueReason, 
            int createdByUserID, int applicationID)
        {
            InternationalLicenseID = internationalLicenseID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            ApplicationID = applicationID;
        }
    }
    public class clsInternationalLicenseData
    {
        public static int AddNewInternationalLicense(InternationalLicenseDTO internationalLicenseDTO)
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

                        Command.Parameters.AddWithValue("@CreatedByUserID", internationalLicenseDTO.CreatedByUserID);
                        Command.Parameters.AddWithValue("@IsActive", internationalLicenseDTO.IsActive);
                        Command.Parameters.AddWithValue("@ApplicationID", internationalLicenseDTO.ApplicationID);
                        Command.Parameters.AddWithValue("@DriverID", internationalLicenseDTO.DriverID);
                        Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", internationalLicenseDTO.IssuedUsingLocalLicenseID);
                        Command.Parameters.AddWithValue("@IssueDate", internationalLicenseDTO.IssueDate);
                        Command.Parameters.AddWithValue("@ExpirationDate", internationalLicenseDTO.ExpirationDate);
                        Command.Parameters.AddWithValue("@IssueReason", internationalLicenseDTO.IssueReason);

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

        public static InternationalLicenseDTO GetInternationalLicenseInfoByID(int InternationalLicenseID)
        {
            InternationalLicenseDTO internationalLicenseDTO;
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
                                internationalLicenseDTO=new InternationalLicenseDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("InternationalLicenseID")),
                                    Reader.GetInt32(Reader.GetOrdinal("DriverID")),
                                    Reader.GetInt32(Reader.GetOrdinal("IssuedUsingLocalLicenseID")),
                                    Reader.GetDateTime(Reader.GetOrdinal("IssueDate")),
                                    Reader.GetDateTime(Reader.GetOrdinal("ExpirationDate")),
                                    Reader.GetBoolean(Reader.GetOrdinal("IsActive")),
                                    Reader.GetByte(Reader.GetOrdinal("IssueReason")),
                                    Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                    Reader.GetInt32(Reader.GetOrdinal("ApplicationID"))
                                    );
                            }
                            else
                                internationalLicenseDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                internationalLicenseDTO = null;
            }

            return internationalLicenseDTO;
        }


        public static bool UpdateInternationalLicense(InternationalLicenseDTO internationalLicenseDTO)
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

                        Command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseDTO.InternationalLicenseID);
                        Command.Parameters.AddWithValue("@CreatedByUserID", internationalLicenseDTO.CreatedByUserID);
                        Command.Parameters.AddWithValue("@IssueReason", internationalLicenseDTO.IssueReason);
                        Command.Parameters.AddWithValue("@IsActive", internationalLicenseDTO.IsActive);
                        Command.Parameters.AddWithValue("@ApplicationID", internationalLicenseDTO.ApplicationID);
                        Command.Parameters.AddWithValue("@DriverID", internationalLicenseDTO.DriverID);
                        Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", internationalLicenseDTO.IssuedUsingLocalLicenseID);
                        Command.Parameters.AddWithValue("@IssueDate", internationalLicenseDTO.IssueDate);
                        Command.Parameters.AddWithValue("@ExpirationDate", internationalLicenseDTO.ExpirationDate);


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


        public static List<InternationalLicensesListDTO> GetDriverInternationalLicenses(int DriverID)
        {
            List<InternationalLicensesListDTO> DriverInternationalLicensesList = new List<InternationalLicensesListDTO>();
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
                            while (Reader.Read())
                            {
                                DriverInternationalLicensesList.Add(new InternationalLicensesListDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("InternationalLicenseID")),
                                     Reader.GetInt32(Reader.GetOrdinal("ApplicationID")),
                                    Reader.GetInt32(Reader.GetOrdinal("DriverID")),
                                    Reader.GetInt32(Reader.GetOrdinal("IssuedUsingLocalLicenseID")),
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

            return DriverInternationalLicensesList;
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
        public static List<InternationalLicensesListDTO> GetAllInternationalLicenses()
        {
            List<InternationalLicensesListDTO> internationalLicensesList = new List<InternationalLicensesListDTO> ();
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
                            while(Reader.Read())
                            {
                                internationalLicensesList.Add(new InternationalLicensesListDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("InternationalLicenseID")),
                                     Reader.GetInt32(Reader.GetOrdinal("ApplicationID")),
                                    Reader.GetInt32(Reader.GetOrdinal("DriverID")),
                                    Reader.GetInt32(Reader.GetOrdinal("IssuedUsingLocalLicenseID")),
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

            return internationalLicensesList;
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
