using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public class clsTestData
    {
         public static int AddNewTest(int TestAppointmentID,
            bool TestResult, string Notes, int CreatedByUserID)
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

                        if (Notes != "" && Notes!=null)
                            Command.Parameters.AddWithValue("@Notes", Notes);
                        else
                            Command.Parameters.AddWithValue("@Notes", DBNull.Value);

                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        Command.Parameters.AddWithValue("@TestResult", TestResult);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
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

        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID,
            ref bool TestResult, ref string Notes,ref int CreatedByUserID)
        {
            bool IsFound = false;
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
                                IsFound = true;
                                TestAppointmentID = (int)Reader["TestAppointmentID"];
                                TestResult = (bool)Reader["TestResult"];
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                                 Notes = (Reader["Notes"] == DBNull.Value) ? "" : (string)Reader["Notes"];
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


        public static bool UpdateTest(int TestID,  int TestAppointmentID,
             bool TestResult, string Notes,  int CreatedByUserID)
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

                        if (Notes != "" && Notes != null)
                            Command.Parameters.AddWithValue("@Notes", Notes);
                        else
                            Command.Parameters.AddWithValue("@Notes", DBNull.Value);
                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        Command.Parameters.AddWithValue("@TestResult", TestResult);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
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

                        PassedTestCount = (byte)PassedTestCountParam.Value;
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


        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
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

       
    }
}
