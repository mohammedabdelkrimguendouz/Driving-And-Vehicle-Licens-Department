using DVLD.Applications;
using DVLD.Applications.Application_Types;
using DVLD.Applications.Local_Driving_License_Applications;
using DVLD.Applications.Replacement_for_Damaged_or_Lost_Licenses;
using DVLD.Global;
using DVLD.Licenses;
using DVLD.Licenses.Detaind_Licenses;
using DVLD.People;
using DVLD.Tests;
using DVLD.Tests.TestTypes;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new frmLogin());
          //Application.Run(new frmTest());
          //Application.Run(new frmListDetainedLicenses());
        }
    }
}
