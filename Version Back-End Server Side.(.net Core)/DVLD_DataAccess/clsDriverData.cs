using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccess
{

    public class DriverDTO
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
       
        public int CreatedByUserID { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DriverDTO(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
        }
    }

    public class DriversListDTO
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }

        public string NationalNo {  get; set; }
        public string FullName {  get; set; }

        public DateTime CreatedDate { get; set; }

        public int NumberOfActiveLicenses {  get; set; }

        public DriversListDTO(int driverID, int personID, string nationalNo, string fullName,
            DateTime createdDate, int numberOfActiveLicenses)
        {
            DriverID = driverID;
            PersonID = personID;
            NationalNo = nationalNo;
            FullName = fullName;
            CreatedDate = createdDate;
            NumberOfActiveLicenses = numberOfActiveLicenses;
        }
    }

    public class clsDriverData
    {
        public static DriverDTO GetDriverInfoByID(int DriverID)
        {
            DriverDTO driverDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetDriverInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@DriverID", DriverID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                driverDTO = new DriverDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("DriverID")),
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                        Reader.GetDateTime(Reader.GetOrdinal("CreatedDate"))
                                    );
                            }
                            else
                                driverDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                driverDTO = null;
            }

            return driverDTO;
        }

        public static DriverDTO GetDriverInfoByPersonID(int PersonID)
        {
            DriverDTO driverDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetDriverInfoByPersonID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                driverDTO = new DriverDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("DriverID")),
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetInt32(Reader.GetOrdinal("CreatedByUserID")),
                                        Reader.GetDateTime(Reader.GetOrdinal("CreatedDate"))
                                    );
                            }
                            else
                                driverDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                 driverDTO = null;
            }

            return driverDTO;
        }

        public static bool UpdateDriver(DriverDTO driverDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateDriver", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@PersonID", driverDTO.PersonID);
                        Command.Parameters.AddWithValue("@CreatedByUserID", driverDTO.CreatedByUserID);
                        Command.Parameters.AddWithValue("@DriverID", driverDTO.DriverID);
                        Command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

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

        public static int AddNewDriver(DriverDTO driverDTO)
        {
            int DriverID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewDriver", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@PersonID", driverDTO.PersonID);
                        Command.Parameters.AddWithValue("@CreatedByUserID", driverDTO.CreatedByUserID);
                        Command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                        SqlParameter outputIdParam = new SqlParameter("@NewDriverID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        DriverID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                DriverID = -1;
            }
            return DriverID;
        }

        public static List<DriversListDTO> GetAllDriveres()
        {
            List<DriversListDTO> DriversList = new List<DriversListDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetAllDriveres", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                DriversList.Add(new DriversListDTO
                                    (
                                        Reader.GetInt32(Reader.GetOrdinal("DriverID")),
                                        Reader.GetInt32(Reader.GetOrdinal("PersonID")),
                                        Reader.GetString(Reader.GetOrdinal("NationalNo")),
                                        Reader.GetString(Reader.GetOrdinal("FullName")),
                                        Reader.GetDateTime(Reader.GetOrdinal("CreatedDate")),
                                        Reader.GetInt32(Reader.GetOrdinal("NumberOfActiveLicenses"))
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
            return DriversList;
        }
    }
}
