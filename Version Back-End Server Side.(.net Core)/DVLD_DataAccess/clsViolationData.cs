using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class ViolationDTO
    {
        public int ViolationID { get; set; }
        public string ViolationTitle { get; set; }

        public string ViolationDescription { get; set; }
        public float FineFees { get; set; }

        public ViolationDTO(int violationID, string violationTitle, string violationDescription, float fineFees)
        {
            ViolationID = violationID;
            ViolationTitle = violationTitle;
            ViolationDescription = violationDescription;
            FineFees = fineFees;
        }
    }


    public class clsViolationData
    {
         public static ViolationDTO GetViolationInfoByID(int ViolationID)
         {
            ViolationDTO violationDTO ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    
                    using(SqlCommand Command = new SqlCommand("SP_GetViolationInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ViolationID", ViolationID);
                        using(SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                violationDTO = new ViolationDTO(
                                        Reader.GetInt32(Reader.GetOrdinal("ViolationID")),
                                        

                                        Reader.GetString(Reader.GetOrdinal("ViolationTitle")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ViolationDescription"))?"": Reader.GetString(Reader.GetOrdinal("ViolationDescription")),
                                        (float)Reader.GetDecimal(Reader.GetOrdinal("FineFees"))
                                    );
                            }
                            else
                                violationDTO = null;
                             
                        }
                    }
                }
            }
            catch(Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}",EventLogEntryType.Error);
                violationDTO = null;
            }
            return violationDTO;
          
        }

        public static ViolationDTO GetViolationInfoByTitle(string ViolationTitle)
        {
            ViolationDTO violationDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    
                    using (SqlCommand Command = new SqlCommand("SP_GetViolationInfoByTitle", Connection))
                    {
                        Command.CommandType=CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ViolationTitle", ViolationTitle);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                violationDTO = new ViolationDTO(
                                        Reader.GetInt32(Reader.GetOrdinal("ViolationID")),


                                        Reader.GetString(Reader.GetOrdinal("ViolationTitle")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ViolationDescription")) ? "" : Reader.GetString(Reader.GetOrdinal("ViolationDescription")),
                                       (float)Reader.GetDecimal(Reader.GetOrdinal("FineFees"))
                                    );
                            }
                            else
                                violationDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                violationDTO = null;
            }
            return violationDTO;
        }
        public static bool UpdateViolation(ViolationDTO violationDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    
                    using (SqlCommand Command = new SqlCommand("SP_UpdateViolation", Connection))
                    {

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ViolationID", violationDTO.ViolationID);
                        if (violationDTO.ViolationDescription != null && violationDTO.ViolationDescription != "")
                            Command.Parameters.AddWithValue("@ViolationDescription", violationDTO.ViolationDescription);
                        else
                            Command.Parameters.AddWithValue("@ViolationDescription", DBNull.Value);
                        Command.Parameters.AddWithValue("@ViolationTitle", violationDTO.ViolationTitle);
                        Command.Parameters.AddWithValue("@FineFees", violationDTO.FineFees);


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

        public static int AddNewViolation(ViolationDTO violationDTO)
        {
            int ViolationID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewViolation", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        
                        Command.Parameters.AddWithValue("@ViolationTitle", violationDTO.ViolationTitle);

                        if(violationDTO.ViolationDescription != null && violationDTO.ViolationDescription!="")
                            Command.Parameters.AddWithValue("@ViolationDescription", violationDTO.ViolationDescription);
                        else
                            Command.Parameters.AddWithValue("@ViolationDescription", DBNull.Value);


                        Command.Parameters.AddWithValue("@FineFees", violationDTO.FineFees);

                        SqlParameter outputIdParam = new SqlParameter("@NewViolationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        ViolationID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                ViolationID = -1;
            }
            return ViolationID;
        }

        public static List<ViolationDTO> GetAllViolations()
        {
            List<ViolationDTO> ViolationList = new List<ViolationDTO> ();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllViolations", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                ViolationList.Add(new ViolationDTO(
                                        Reader.GetInt32(Reader.GetOrdinal("ViolationID")),
                                        Reader.GetString(Reader.GetOrdinal("ViolationTitle")),
                                        Reader.IsDBNull(Reader.GetOrdinal("ViolationDescription")) ? "" : Reader.GetString(Reader.GetOrdinal("ViolationDescription")),
                                        (float)Reader.GetDecimal(Reader.GetOrdinal("FineFees"))
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
            return ViolationList;
        }
       
        public static bool IsViolationExistByViolationID(int ViolationID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsViolationExistByViolationID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ViolationID", ViolationID);

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

        public static bool IsViolationExistByViolationTitle(string ViolationTitle)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_IsViolationExistByViolationTitle", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ViolationTitle", ViolationTitle);
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

    }
}
