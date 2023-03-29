using System;
using System.Windows.Forms;

namespace Updater
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
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
