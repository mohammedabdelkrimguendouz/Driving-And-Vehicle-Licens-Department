using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsSetting
    {
        public static byte GetDefaultValidityLengthForAnInternationalLicense()
        {
            return clsSettingData.GetDefaultValidityLengthForAnInternationalLicense();
        }
    }
}
