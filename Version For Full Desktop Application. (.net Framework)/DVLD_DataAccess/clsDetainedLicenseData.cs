using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseData
    {
         public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate, float FineFees,int ViolationID,
            int CreatedByUserID)
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

                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        Command.Parameters.AddWithValue("@ViolationID", ViolationID);
                        Command.Parameters.AddWithValue("@DetainDate", DetainDate);
                        Command.Parameters.AddWithValue("@FineFees", FineFees);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool GetDetainedLicenseInfoByID(int DetainID, ref int LicenseID,ref DateTime DetainDate,
            ref float FineFees,ref int ViolationID,
            ref int CreatedByUserID, ref bool IsReleased,ref  DateTime ReleaseDate,ref
            int ReleasedByUserID,ref int ReleaseApplicationID)
        {
            bool IsFound = false;
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
                                IsFound = true;

                                LicenseID = (int)Reader["LicenseID"];
                                DetainDate = (DateTime)Reader["DetainDate"];
                                FineFees = Convert.ToSingle(Reader["FineFees"]);
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                                IsReleased =(bool)Reader["IsReleased"];
                                ViolationID= (int)Reader["ViolationID"];
                                ReleaseDate = Reader["ReleaseDate"]==DBNull.Value ?DateTime.MaxValue :(DateTime) Reader["ReleaseDate"] ;
                                ReleasedByUserID = Reader["ReleasedByUserID"]==DBNull.Value?-1:(int)Reader["ReleasedByUserID"];
                                ReleaseApplicationID= Reader["ReleaseApplicationID"] == DBNull.Value ? -1 : (int)Reader["ReleaseApplicationID"];
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

        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID, ref  int DetainID, ref DateTime DetainDate,
            ref float FineFees,ref int ViolationID,
            ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref
            int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
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
                                 IsFound = true;

                                 DetainID = (int)Reader["DetainID"];
                                 DetainDate = (DateTime)Reader["DetainDate"];
                                 FineFees = Convert.ToSingle(Reader["FineFees"]);
                                 CreatedByUserID = (int)Reader["CreatedByUserID"];
                                 IsReleased = (bool)Reader["IsReleased"];
                                 ViolationID= (int)Reader["ViolationID"];
                                 ReleaseDate = Reader["ReleaseDate"] == DBNull.Value ? DateTime.MaxValue : (DateTime)Reader["ReleaseDate"];
                                 ReleasedByUserID = Reader["ReleasedByUserID"] == DBNull.Value ? -1 : (int)Reader["ReleasedByUserID"];
                                 ReleaseApplicationID = Reader["ReleaseApplicationID"] == DBNull.Value ? -1 : (int)Reader["ReleaseApplicationID"];
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


        public static bool UpdateDetainedLicense(int DetainID,int LicenseID, DateTime DetainDate, float FineFees,int ViolationID,
            int CreatedByUserID)
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

                        Command.Parameters.AddWithValue("@DetainID", DetainID);
                        Command.Parameters.AddWithValue("@ViolationID", ViolationID);
                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        Command.Parameters.AddWithValue("@DetainDate", DetainDate);
                        Command.Parameters.AddWithValue("@FineFees", FineFees);
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

        public static bool ReleaseDetainedLicense(int DetainID, 
            int ReleasedByUserID,  int ReleaseApplicationID)
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
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
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
