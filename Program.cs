using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Updater
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName("Updater").Length > 1)
            {
                return;
            }

            if (VersionCheck.CheckAppUpdate() == string.Empty)
            {
                return;
            }
         
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Updater(VersionCheck.CheckAppUpdate()));
        }
    }
}
