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
    public class clsTestAppointmentData
    {

        public static int AddNewTestAppointment(byte TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked,int RetakeTestApplicationID)
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


                        if (RetakeTestApplicationID==-1)
                             Command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                        else
                             Command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        Command.Parameters.AddWithValue("@IsLocked", IsLocked);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool GetTestAppointmentInfoByID(int TestAppointmentID ,ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID,ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsFound = false;
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
                                IsFound = true;
                                LocalDrivingLicenseApplicationID = (int)Reader["LocalDrivingLicenseApplicationID"];
                                TestTypeID = (int)Reader["TestTypeID"];
                                AppointmentDate = (DateTime)Reader["AppointmentDate"];
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                                PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                                IsLocked = (bool)Reader["IsLocked"];
                                RetakeTestApplicationID=(Reader["RetakeTestApplicationID"]==DBNull.Value)?-1:(int)Reader["RetakeTestApplicationID"];
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

        public static bool GetLastTestAppointment(int LocalDrivingLicenseApplicationID,  byte TestTypeID,ref  int TestAppointmentID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsFound = false;
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
                                IsFound = true;
                                TestAppointmentID = (int)Reader["TestAppointmentID"];
                                AppointmentDate = (DateTime)Reader["AppointmentDate"];
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                                PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                                IsLocked = (bool)Reader["IsLocked"];
                                RetakeTestApplicationID=(Reader["RetakeTestApplicationID"]==DBNull.Value)?-1:(int)Reader["RetakeTestApplicationID"];
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

        public static bool UpdateTestAppointment(int TestAppointmentID,byte TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
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

                        if (RetakeTestApplicationID == -1)
                            Command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                        else
                            Command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        Command.Parameters.AddWithValue("@IsLocked", IsLocked);
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

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            DataTable dt = new DataTable();
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
        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
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
