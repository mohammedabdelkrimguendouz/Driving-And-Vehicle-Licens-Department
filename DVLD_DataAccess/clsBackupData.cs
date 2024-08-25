using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsBackupData
    {
        static public bool CreateBackup(string FilePath)
        {
            bool Successed = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_CreateBackup", Connection))
                    {
                        Command.CommandType = System.Data.CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@FilePath", FilePath);

                        Command.ExecuteNonQuery();
                        Successed = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                Successed = false;
            }
            return Successed;
        }

        static public bool RestoreBackup(string FilePath)
        {
            bool Successed = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_RestoreBackup", Connection))
                    {
                        Command.CommandType = System.Data.CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@FilePath", FilePath);

                        Command.ExecuteNonQuery();
                        Successed = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                Successed = false;
            }
            return Successed;
        }
    }
}
