using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsSettingData
    {
        public static byte GetDefaultValidityLengthForAnInternationalLicense()
        {
            byte DefaultValidityLengthForAnInternationalLicense = 0 ;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_GetDefaultValidityLengthForAnInternationalLicense", Connection))
                    {
                        Command.CommandType = System.Data.CommandType.StoredProcedure;
                        Object Result = Command.ExecuteScalar();
                        if (Result != null)
                            byte.TryParse(Result.ToString(), out DefaultValidityLengthForAnInternationalLicense);

                    }
                }
            }

            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                DefaultValidityLengthForAnInternationalLicense = 0;
            }

            return DefaultValidityLengthForAnInternationalLicense;
        }
    }
}
