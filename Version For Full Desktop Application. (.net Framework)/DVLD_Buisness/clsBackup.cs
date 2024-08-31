using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsBackup
    {
       
        static public bool CreateBackup(string FilePath)
        {
            return clsBackupData.CreateBackup(FilePath);
        }
        static public bool RestoreBackup(string FilePath)
        {
            return clsBackupData.RestoreBackup(FilePath);
        }

    }
}
