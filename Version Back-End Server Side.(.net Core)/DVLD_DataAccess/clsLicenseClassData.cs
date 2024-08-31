using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace DVLD_DataAccess
{

    public class LicenseClassDTO
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte DefaultValidityLength { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public float ClassFees { get; set; }

        public LicenseClassDTO(int licenseClassID, string className, string classDescription, 
            byte defaultValidityLength, byte minimumAllowedAge, float classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            DefaultValidityLength = defaultValidityLength;
            MinimumAllowedAge = minimumAllowedAge;
            ClassFees = classFees;
        }
    }


    public class clsLicenseClassData
    {
        public static LicenseClassDTO GetLicenseClassInfoByID(int LicenseClassID)
        {
            LicenseClassDTO licenseClassDTO ;
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
                                licenseClassDTO = new LicenseClassDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LicenseClassID")),
                                     Reader.GetString(Reader.GetOrdinal("ClassName")),
                                     Reader.IsDBNull(Reader.GetOrdinal("ClassDescription"))?"":Reader.GetString(Reader.GetOrdinal("ClassDescription")),
                                       Reader.GetByte(Reader.GetOrdinal("DefaultValidityLength")),
                                        Reader.GetByte(Reader.GetOrdinal("MinimumAllowedAge")),
                                        (float)Reader.GetDecimal(Reader.GetOrdinal("ClassFees"))
                                    );
                            }
                            else
                                licenseClassDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                licenseClassDTO = null;
            }
            return licenseClassDTO;
        }

        public static LicenseClassDTO GetLicenseClassInfoByClassName(string ClassName)
        {
            LicenseClassDTO licenseClassDTO ;
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
                                licenseClassDTO = new LicenseClassDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LicenseClassID")),
                                     Reader.GetString(Reader.GetOrdinal("ClassName")),
                                      Reader.IsDBNull(Reader.GetOrdinal("ClassDescription")) ? "" : Reader.GetString(Reader.GetOrdinal("ClassDescription")),
                                       Reader.GetByte(Reader.GetOrdinal("DefaultValidityLength")),
                                        Reader.GetByte(Reader.GetOrdinal("MinimumAllowedAge")),
                                       (float)Reader.GetDecimal(Reader.GetOrdinal("ClassFees"))
                                    );
                            }
                            else
                                licenseClassDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                licenseClassDTO = null;
            }
            return licenseClassDTO;
        }

        public static bool UpdateLicenseClass(LicenseClassDTO licenseClassDTO)
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

                        Command.Parameters.AddWithValue("@ClassName", licenseClassDTO.ClassName);

                        if(licenseClassDTO.ClassDescription != null && licenseClassDTO.ClassDescription != "")
                            Command.Parameters.AddWithValue("@ClassDescription", licenseClassDTO.ClassDescription);
                        else
                            Command.Parameters.AddWithValue("@ClassDescription", DBNull.Value);

                        Command.Parameters.AddWithValue("@MinimumAllowedAge", licenseClassDTO.MinimumAllowedAge);
                        Command.Parameters.AddWithValue("@DefaultValidityLength", licenseClassDTO.DefaultValidityLength);
                        Command.Parameters.AddWithValue("@ClassFees", licenseClassDTO.ClassFees);
                        Command.Parameters.AddWithValue("@LicenseClassID", licenseClassDTO.LicenseClassID);


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

        public static int AddNewLicenseClass(LicenseClassDTO licenseClassDTO)
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

                        Command.Parameters.AddWithValue("@ClassName", licenseClassDTO.ClassName);
                        if (licenseClassDTO.ClassDescription != null && licenseClassDTO.ClassDescription != "")
                            Command.Parameters.AddWithValue("@ClassDescription", licenseClassDTO.ClassDescription);
                        else
                            Command.Parameters.AddWithValue("@ClassDescription", DBNull.Value);
                        Command.Parameters.AddWithValue("@MinimumAllowedAge", licenseClassDTO.MinimumAllowedAge);
                        Command.Parameters.AddWithValue("@ClassFees", licenseClassDTO.ClassFees);
                        Command.Parameters.AddWithValue("@DefaultValidityLength", licenseClassDTO.DefaultValidityLength);

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

        public static List<LicenseClassDTO> GetAllLicenseClasses()
        {
            List<LicenseClassDTO> LicenseClassesList = new List<LicenseClassDTO> ();
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
                            while(Reader.Read())
                            {
                                LicenseClassesList.Add(new LicenseClassDTO(
                                    Reader.GetInt32(Reader.GetOrdinal("LicenseClassID")),
                                     Reader.GetString(Reader.GetOrdinal("ClassName")),
                                     Reader.IsDBNull(Reader.GetOrdinal("ClassDescription")) ? "" : Reader.GetString(Reader.GetOrdinal("ClassDescription")),
                                       Reader.GetByte(Reader.GetOrdinal("DefaultValidityLength")),
                                        Reader.GetByte(Reader.GetOrdinal("MinimumAllowedAge")),
                                        (float)Reader.GetDecimal(Reader.GetOrdinal("ClassFees"))
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
            return LicenseClassesList;
        }
    }
}
