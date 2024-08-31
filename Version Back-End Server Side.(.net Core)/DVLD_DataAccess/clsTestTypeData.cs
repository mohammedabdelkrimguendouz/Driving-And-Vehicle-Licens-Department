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
    public class TestTypeDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public float Fees { get; set; }

        public TestTypeDTO(int iD, string title, string description, float fees)
        {
            ID = iD;
            Title = title;
            Description = description;
            Fees = fees;
        }
    }
    public class clsTestTypeData
    {
        public static TestTypeDTO GetTestTypeInfoByID(int TestTypeID)
        {
            TestTypeDTO testTypeDTO;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetTestTypeInfoByID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                testTypeDTO = new TestTypeDTO(
                                        Reader.GetInt32(Reader.GetOrdinal("TestTypeID")),
                                        Reader.GetString(Reader.GetOrdinal("TestTypeTitle")),
                                        Reader.IsDBNull(Reader.GetOrdinal("TestTypeDescription"))?"":Reader.GetString(Reader.GetOrdinal("TestTypeDescription")),
                                        (float)Reader.GetDecimal(Reader.GetOrdinal("TestTypeFees"))
                                        );
                            }
                            else
                                testTypeDTO = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                testTypeDTO = null;
            }

            return testTypeDTO;
        }

        public static bool UpdateTestType(TestTypeDTO testTypeDTO)
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_UpdateTestType", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@TestTypeID", testTypeDTO.ID);
                        if (testTypeDTO.Description != null && testTypeDTO.Description != "")
                            Command.Parameters.AddWithValue("@TestTypeDescription", testTypeDTO.Description);
                        else
                            Command.Parameters.AddWithValue("@TestTypeDescription", DBNull.Value);
                        Command.Parameters.AddWithValue("@TestTypeTitle", testTypeDTO.Title);
                        Command.Parameters.AddWithValue("@TestTypeFees", testTypeDTO.Fees);


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

        public static int AddNewTestType(TestTypeDTO testTypeDTO)
        {
            int TestTypeID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_AddNewTestType", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        if(testTypeDTO.Description!=null&&testTypeDTO.Description!="")
                            Command.Parameters.AddWithValue("@TestTypeDescription", testTypeDTO.Description);
                        else
                            Command.Parameters.AddWithValue("@TestTypeDescription",DBNull.Value);



                        Command.Parameters.AddWithValue("@TestTypeTitle", testTypeDTO.Title);
                        Command.Parameters.AddWithValue("@TestTypeFees", testTypeDTO.Fees);
                        SqlParameter outputIdParam = new SqlParameter("@NewTestTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);


                        Command.ExecuteNonQuery();

                        TestTypeID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                TestTypeID = -1;
            }
            return TestTypeID;
        }

        public static List<TestTypeDTO>  GetAllTestTypes()
        {
            List<TestTypeDTO> TestTypeList = new List<TestTypeDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();

                    using (SqlCommand Command = new SqlCommand("SP_GetAllTestTypes", Connection))
                    {
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                TestTypeList.Add(new TestTypeDTO(
                                        Reader.GetInt32(Reader.GetOrdinal("TestTypeID")),
                                        Reader.GetString(Reader.GetOrdinal("TestTypeTitle")),
                                        Reader.IsDBNull(Reader.GetOrdinal("TestTypeDescription")) ? "" : Reader.GetString(Reader.GetOrdinal("TestTypeDescription")),
                                        (float)Reader.GetDecimal(Reader.GetOrdinal("TestTypeFees"))
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
            return TestTypeList;
        }
    }
}
