using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
namespace DVLD_DataAccess
{
    public class TestAppointmentDTO
    {
       

        public int TestAppointmentID { get; set; }

        public int TestTypeID { get; set; }
        
        public int LocalDrivingLicenseApplicationID { get; set; }
       

        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
       

        public bool IsLocked { get; set; }
        public int? RetakeTestApplicationID { get; set; }

        public TestAppointmentDTO(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID,
            DateTime appointmentDate, float paidFees, int createdByUserID,bool isLocked, int? retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
        }
    }
    public class TestAppointmentsListDTO
    {


        public int TestAppointmentID { get; set; }

       public string TestTypeTitle { get; set; }

        public int LocalDrivingLicenseApplicationID { get; set; }


        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public string  ClassName { get; set; }


        public bool IsLocked { get; set; }
        public string FullName { get; set; }

        public TestAppointmentsListDTO(int testAppointmentID, string testTypeTitle, int localDrivingLicenseApplicationID, DateTime appointmentDate, 
            float paidFees, string className, bool isLocked, string fullName)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeTitle = testTypeTitle;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            ClassName = className;
            IsLocked = isLocked;
            FullName = fullName;
        }
    }
    public class ApplicationTestAppointmentsDTO
    {


        public int TestAppointmentID { get; set; }

        


        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
       

        public bool IsLocked { get; set; }

        public ApplicationTestAppointmentsDTO(int testAppointmentID, DateTime appointmentDate, 
            float paidFees, bool isLocked)
        {
            TestAppointmentID = testAppointmentID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            IsLocked = isLocked;
        }
    }


    public class clsTestAppointmentData
    {

        public static int AddNewTestAppointment(TestAppointmentDTO testAppointmentDTO)
        {
            int TestAppointmentID = -1; ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewTestAppointment", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;


                        if (testAppointmentDTO.RetakeTestApplicationID == null)
                             Command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                        else
                             Command.Parameters.AddWithValue("@RetakeTestApplicationID", testAppointmentDTO.RetakeTestApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", testAppointmentDTO.TestTypeID);
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", testAppointmentDTO.LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@AppointmentDate", testAppointmentDTO.AppointmentDate);
                        Command.Parameters.AddWithValue("@PaidFees", testAppointmentDTO.PaidFees);
                        Command.Parameters.AddWithValue("@IsLocked", testAppointmentDTO.IsLocked);
                        Command.Parameters.AddWithValue("@CreatedByUserID", testAppointmentDTO.CreatedByUserID);

                        SqlParameter outputIdParam = new SqlParameter("@NewTestAppointmentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        TestAppointmentID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                TestAppointmentID = -1;
            }
            return TestAppointmentID;
        }

        public static TestAppointmentDTO GetTestAppointmentInfoByID(int TestAppointmentID)
        {
            TestAppointmentDTO testAppointmentDTO ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetTestAppointmentInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                testAppointmentDTO = new TestAppointmentDTO(
                                Reader.GetInt32(Reader.GetOrdinal("TestAppointmentID")),
                                Reader.GetInt32(Reader.GetOrdinal("TestTypeID")),
                                Reader.GetInt32(Reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                Reader.GetDateTime(Reader.GetOrdinal("AppointmentDate")),
                                (float)Reader.GetDecimal(Reader.GetOrdinal("PaidFees")),
                                Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                Reader.GetBoolean(Reader.GetOrdinal("IsLocked")),
                                Reader.IsDBNull(Reader.GetOrdinal("RetakeTestApplicationID"))?null: Reader.GetInt32(Reader.GetOrdinal("RetakeTestApplicationID"))
                                );
                            }
                            else
                                testAppointmentDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                testAppointmentDTO = null;
            }

            return testAppointmentDTO;
        }

        public static TestAppointmentDTO GetLastTestAppointment(int LocalDrivingLicenseApplicationID,  byte TestTypeID)
        {
            TestAppointmentDTO testAppointmentDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetLastTestAppointment", Connection))
                    {
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                testAppointmentDTO = new TestAppointmentDTO(
                                Reader.GetInt32(Reader.GetOrdinal("TestAppointmentID")),
                                Reader.GetInt32(Reader.GetOrdinal("TestTypeID")),
                                Reader.GetInt32(Reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                Reader.GetDateTime(Reader.GetOrdinal("AppointmentDate")),
                                (float)Reader.GetDecimal(Reader.GetOrdinal("PaidFees")),
                                Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                Reader.GetBoolean(Reader.GetOrdinal("IsLocked")),
                                Reader.IsDBNull(Reader.GetOrdinal("RetakeTestApplicationID")) ? null : Reader.GetInt32(Reader.GetOrdinal("RetakeTestApplicationID"))
                                );
                            }
                            else
                                testAppointmentDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                 testAppointmentDTO = null;
            }

            return testAppointmentDTO;
        }

        public static bool UpdateTestAppointment(TestAppointmentDTO testAppointmentDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateTestAppointment", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        if (testAppointmentDTO.RetakeTestApplicationID == null)
                            Command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                        else
                            Command.Parameters.AddWithValue("@RetakeTestApplicationID", testAppointmentDTO.RetakeTestApplicationID);
                        Command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentDTO.TestAppointmentID);
                        Command.Parameters.AddWithValue("@TestTypeID", testAppointmentDTO.TestTypeID);
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", testAppointmentDTO.LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@AppointmentDate", testAppointmentDTO.AppointmentDate);
                        Command.Parameters.AddWithValue("@PaidFees", testAppointmentDTO.PaidFees);
                        Command.Parameters.AddWithValue("@IsLocked", testAppointmentDTO.IsLocked);
                        Command.Parameters.AddWithValue("@CreatedByUserID", testAppointmentDTO.CreatedByUserID);


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

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteTestAppointment", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static List<ApplicationTestAppointmentsDTO> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            List<ApplicationTestAppointmentsDTO> ApplicationTestAppointmentsList = new List<ApplicationTestAppointmentsDTO> ();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetApplicationTestAppointmentsPerTestType", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                ApplicationTestAppointmentsList.Add(new ApplicationTestAppointmentsDTO(
                                Reader.GetInt32(Reader.GetOrdinal("TestAppointmentID")),
                                Reader.GetDateTime(Reader.GetOrdinal("AppointmentDate")),
                                (float)Reader.GetDecimal(Reader.GetOrdinal("PaidFees")),
                                Reader.GetBoolean(Reader.GetOrdinal("IsLocked"))
                               
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

            return ApplicationTestAppointmentsList;
        }
        public static List<TestAppointmentsListDTO> GetAllTestAppointments()
        {
            List<TestAppointmentsListDTO> TestAppointmentsList = new List<TestAppointmentsListDTO> ();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllTestAppointments", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                TestAppointmentsList.Add(new TestAppointmentsListDTO(
                                Reader.GetInt32(Reader.GetOrdinal("TestAppointmentID")),
                                Reader.GetString(Reader.GetOrdinal("TestTypeTitle")),
                                Reader.GetInt32(Reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                Reader.GetDateTime(Reader.GetOrdinal("AppointmentDate")),
                                (float)Reader.GetDecimal(Reader.GetOrdinal("PaidFees")),
                                Reader.GetString(Reader.GetOrdinal("ClassName")),
                                Reader.GetBoolean(Reader.GetOrdinal("IsLocked")),
                                Reader.GetString(Reader.GetOrdinal("FullName"))
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

            return TestAppointmentsList;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetTestID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        
                        SqlParameter TestIdParam = new SqlParameter("@TestID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(TestIdParam);


                        Command.ExecuteNonQuery();

                        TestID = (int)TestIdParam.Value;


                    }
                }
            }

            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                TestID = -1;
            }

            return TestID;
        }

        
    }
}
