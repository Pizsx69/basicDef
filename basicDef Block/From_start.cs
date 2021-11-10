using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicDef_Block
{
    static class From_start
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (IsAdministrator() == false)
            {             // Restart program and run as admin
                ProcessStartInfo info = new ProcessStartInfo("basicDef Block.exe");
                info.UseShellExecute = true;
                info.Verb = "runas";
                //info.Arguments = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "boot.ini");
                Process.Start(info);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_Application());
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
