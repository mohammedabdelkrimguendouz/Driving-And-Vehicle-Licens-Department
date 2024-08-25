using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public class clsLicenseClassData
    {
        public static bool GetLicenseClassInfoByID(int LicenseClassID, ref string ClassName, ref string ClassDescription,ref byte MinimumAllowedAge,ref byte DefaultValidityLength,ref float ClassFees)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetLicenseClassInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;
                                ClassName = (string)Reader["ClassName"];
                                ClassDescription = (string)Reader["ClassDescription"];
                                MinimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                                DefaultValidityLength = (byte)Reader["DefaultValidityLength"];
                                ClassFees = Convert.ToSingle(Reader["ClassFees"]);
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

        public static bool GetLicenseClassInfoByClassName(string ClassName,ref int LicenseClassID, ref string ClassDescription, ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetLicenseClassInfoByClassName", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ClassName", ClassName);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;
                                LicenseClassID = (int)Reader["LicenseClassID"];
                                ClassDescription = (string)Reader["ClassDescription"];
                                MinimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                                DefaultValidityLength = (byte)Reader["DefaultValidityLength"];
                                ClassFees = Convert.ToSingle(Reader["ClassFees"]);
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

        public static bool UpdateLicenseClass(int LicenseClassID,  string ClassName,  string ClassDescription, short MinimumAllowedAge,short DefaultValidityLength, float ClassFees)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateLicenseClass", Connection))
                    {

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ClassName", ClassName);
                        Command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
                        Command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
                        Command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
                        Command.Parameters.AddWithValue("@ClassFees", ClassFees);
                        Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


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

        public static int AddNewLicenseClass( string ClassName, string ClassDescription, short MinimumAllowedAge, short DefaultValidityLength, float ClassFees)
        {
            int LicenseClassID = -1; ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewLicenseClass", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ClassName", ClassName);
                        Command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
                        Command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
                        Command.Parameters.AddWithValue("@ClassFees", ClassFees);
                        Command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);

                        SqlParameter outputIdParam = new SqlParameter("@NewLicenseClassID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        LicenseClassID = (int)outputIdParam.Value;


                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                LicenseClassID = -1;
            }
            return LicenseClassID;
        }

        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllLicenseClasses", Connection))
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
