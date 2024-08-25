using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public class clsApplicationTypeData
    {
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID, ref string ApplicationTypeTitle,ref float ApplicationFees)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetApplicationTypeInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                       

                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;
                                ApplicationTypeTitle = (string)Reader["ApplicationTypeTitle"];
                                ApplicationFees = Convert.ToSingle(Reader["ApplicationFees"]);
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

        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateApplicationType", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        Command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
                        Command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);


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

        public static int AddNewApplicationType(string ApplicationTypeTitle, float ApplicationFees)
        {
            int ApplicationTypeID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewApplicationType", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
                        Command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

                        SqlParameter outputIdParam = new SqlParameter("@NewApplicationTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        ApplicationTypeID = (int)outputIdParam.Value;

                    }
                }
            }

            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                ApplicationTypeID = -1;
            }
            return ApplicationTypeID;
        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllApplicationTypes", Connection))
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
