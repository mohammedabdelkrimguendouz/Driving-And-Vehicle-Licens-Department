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
    public class TestDTO
    {
        public int TestID { get; set; }


        public int TestAppointmentID { get; set; }
        

        public string Notes { get; set; }

        public int CreatedByUserID { get; set; }
       

        public bool TestResult { get; set; }

        public TestDTO(int testID, int testAppointmentID, string notes, int createdByUserID, bool testResult)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            Notes = notes;
            CreatedByUserID = createdByUserID;
            TestResult = testResult;
        }
    }

    public class clsTestData
    {
         public static int AddNewTest(TestDTO testDTO)
         {

            int TestID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewTest", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        if (testDTO.Notes != "" && testDTO.Notes != null)
                            Command.Parameters.AddWithValue("@Notes", testDTO.Notes);
                        else
                            Command.Parameters.AddWithValue("@Notes", DBNull.Value);

                        Command.Parameters.AddWithValue("@TestAppointmentID", testDTO.TestAppointmentID);
                        Command.Parameters.AddWithValue("@TestResult", testDTO.TestResult);
                        Command.Parameters.AddWithValue("@CreatedByUserID", testDTO.CreatedByUserID);
                        SqlParameter outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        TestID = (int)outputIdParam.Value;

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

        public static TestDTO GetTestInfoByID(int TestID)
        {
            TestDTO testDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetTestInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@TestID", TestID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                testDTO = new TestDTO(

                                Reader.GetInt32(Reader.GetOrdinal("TestID")),
                                Reader.GetInt32(Reader.GetOrdinal("TestAppointmentID")),
                                Reader.IsDBNull(Reader.GetOrdinal("Notes"))?"": Reader.GetString(Reader.GetOrdinal("Notes")),
                                Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                Reader.GetBoolean(Reader.GetOrdinal("TestResult"))
                                );
                                
                            }
                            else
                                testDTO=null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                testDTO = null;
            }
            return testDTO;
        }


        public static bool UpdateTest(TestDTO testDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateTest", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        if (testDTO.Notes != "" && testDTO.Notes != null)
                            Command.Parameters.AddWithValue("@Notes", testDTO.Notes);
                        else
                            Command.Parameters.AddWithValue("@Notes", DBNull.Value);
                        Command.Parameters.AddWithValue("@TestAppointmentID", testDTO.TestAppointmentID);
                        Command.Parameters.AddWithValue("@TestResult", testDTO.TestResult);
                        Command.Parameters.AddWithValue("@CreatedByUserID", testDTO.CreatedByUserID);
                        Command.Parameters.AddWithValue("@TestID", testDTO.TestID);


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

        public static bool DeleteTest(int TestID)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteTest", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@TestID", TestID);

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

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0 ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetPassedTestCount", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        SqlParameter PassedTestCountParam = new SqlParameter("@PassedTestCount", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(PassedTestCountParam);


                        Command.ExecuteNonQuery();

                        PassedTestCount =Convert.ToByte(PassedTestCountParam.Value);
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                PassedTestCount = 0;
            }
            return PassedTestCount;

        
        }


        public static List<TestDTO> GetAllTests()
        {
            List<TestDTO> TestsList = new List<TestDTO> ();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllTests", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                TestsList.Add(new TestDTO(

                                Reader.GetInt32(Reader.GetOrdinal("TestID")),
                                Reader.GetInt32(Reader.GetOrdinal("TestAppointmentID")),
                                Reader.IsDBNull(Reader.GetOrdinal("Notes")) ? "" : Reader.GetString(Reader.GetOrdinal("Notes")),
                                Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                Reader.GetBoolean(Reader.GetOrdinal("TestResult"))));
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                
            }
            return TestsList;

            
        }

       
    }
}
