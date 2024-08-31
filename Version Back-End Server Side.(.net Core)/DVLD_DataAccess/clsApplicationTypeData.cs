using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class ApplicationTypeDTO
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

        public ApplicationTypeDTO(int applicationTypeID, string applicationTypeTitle, float applicationFees)
        {
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationFees = applicationFees;
        }
    }
    public class clsApplicationTypeData
    {
        public static ApplicationTypeDTO GetApplicationTypeInfoByID(int ApplicationTypeID)
        {
            ApplicationTypeDTO applicationTypeDTO;
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
                                applicationTypeDTO = new ApplicationTypeDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("ApplicationTypeID")),
                                     Reader.GetString(Reader.GetOrdinal("ApplicationTypeTitle")),
                                      (float)Reader.GetDecimal(Reader.GetOrdinal("ApplicationFees"))
                                    );
                            }
                            else
                                applicationTypeDTO = null; ;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                applicationTypeDTO = null;
            }
            return applicationTypeDTO;
        }

        public static bool UpdateApplicationType(ApplicationTypeDTO applicationTypeDTO)
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

                        Command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeDTO.ApplicationTypeID);
                        Command.Parameters.AddWithValue("@ApplicationTypeTitle", applicationTypeDTO.ApplicationTypeTitle);
                        Command.Parameters.AddWithValue("@ApplicationFees", applicationTypeDTO.ApplicationFees);


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

        public static int AddNewApplicationType(ApplicationTypeDTO applicationTypeDTO)
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

                        Command.Parameters.AddWithValue("@ApplicationTypeTitle", applicationTypeDTO.ApplicationTypeTitle);
                        Command.Parameters.AddWithValue("@ApplicationFees", applicationTypeDTO.ApplicationFees);

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

        public static List<ApplicationTypeDTO> GetAllApplicationTypes()
        {
            List<ApplicationTypeDTO>  ApplicationTypeList = new List<ApplicationTypeDTO>();
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
                            while(Reader.Read())
                            {
                                ApplicationTypeList.Add(new ApplicationTypeDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("ApplicationTypeID")),
                                     Reader.GetString(Reader.GetOrdinal("ApplicationTypeTitle")),
                                      (float)Reader.GetDecimal(Reader.GetOrdinal("ApplicationFees"))
                                    )
                                    );
                            }
                        }


                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
               
            }
            return ApplicationTypeList;
        }

    }
}
