using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Updater
{
    internal static class VersionCheck
    {
        private static string version = string.Empty;
        //private static string gitApi = "https://api.github.com/repos/SplitScreen-Me/splitscreenme-nucleus/";
        private static string gitApi = "https://api.github.com/repos/Mikou27/splitscreenme-nucleus/";
        public static bool installerMode = false;
        public static string modeText;
        private static string releaseTag;

        public static string CheckAppUpdate()
        {
            HttpClient http = new HttpClient();
            string response = http.Get(gitApi + "tags");

            if (response == null)
            {
                return string.Empty;
            }

            JArray versions = JArray.Parse(response);

            releaseTag = versions[0]["name"].ToString();
            string currentVersion = releaseTag.Substring(releaseTag.IndexOf("v") + 1).Trim('"', ' ');

            if (currentVersion != GetVersion())///Update available
            {
                if (!installerMode)
                {
                    modeText = $"Nucleus Co-op {releaseTag} is available.\r\nDo you want to download and install it?";
                }

                return releaseTag;
            }
            else///No update available
            {
                //if (!installerMode)
                //{
                //    Process[] processes = Process.GetProcessesByName("NucleusCoop");

                //    if (processes.Length == 0)
                //        MessageBox.Show("No Updated available", "Nucleus Co-op", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

                return string.Empty;
            }
           // return "test"; //code a supprimer/decommenter si decommenté plus haut^ 
        }

        private static string GetVersion()
        {
            //if (!File.Exists(Application.StartupPath + "\\NucleusCoop.exe"))//If used as installer. 
            //{
            //    installerMode = true;
            //    modeText = $@"Start Nucleus Co-op {releaseTag} Download?";
            //    return string.Empty;
            //}

            try { 

                StreamReader content = new StreamReader(Path.Combine(Application.StartupPath, $"readme.txt"));

                string[] text = content.ReadLine().Split(' ');
                version = text[text.Length - 1];
                content.Dispose();
            }
            catch 
            {
                installerMode = true;
                modeText = $@"Start Nucleus Co-op {releaseTag} Download?";}

                return version;
           }
    }
}
