using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Updater
{
    internal static class VersionCheck
    {
        private static int version = -1;
        private static string gitApi = "https://api.github.com/repos/SplitScreen-Me/splitscreenme-nucleus/";
        //private static string gitApi = "https://api.github.com/repos/Mikou27/splitscreenme-nucleus/";//custom url for testing purpose
        public static bool installerMode = false;
        public static string modeText;
        private static string releaseTag;

        public static string CheckAppUpdate()
        {
            try
            {
                HttpClient http = new HttpClient();
                string response = http.Get(gitApi + "tags");

                if (response == null)
                {
                    return string.Empty;
                }

                string releaseTag = response.Substring(10, 6);//avoid using "Newtonsoft.Json.Linq" not ideal but avoid error if the file has been updated

                string getCurrentVersion = releaseTag.Substring(releaseTag.IndexOf("v") + 1).Trim('"', ' ').Replace(".", "");

                int version = int.Parse(getCurrentVersion);

                if (version > GetVersion())///Update available
                {
                    if (!installerMode)
                    {
                        modeText = $"Nucleus Co-op {releaseTag} is available.\r\nDo you want to download and install it?";
                    }

                    return releaseTag;
                }
                else///No update available
                {
                    if (!installerMode)
                    {
                        Process[] processes = Process.GetProcessesByName("NucleusCoop");

                        if (processes.Length == 0)//Starting updater manually
                            MessageBox.Show("No Update available", "Nucleus Co-op", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    return string.Empty;
                }

                //return "test"; //code a supprimer/decommenter si decommenté plus haut^ 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private static int GetVersion()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, $"readme.txt")))
            {
                StreamReader content = new StreamReader(Path.Combine(Application.StartupPath, $"readme.txt"));

                string[] text = content.ReadLine().Split(' ');
                version = int.Parse(text[text.Length - 1].Replace(".", ""));
                content.Dispose();
                return version;
            }
            else
            {
                installerMode = true;
                modeText = $@"Start Nucleus Co-op {releaseTag} Download?";
                return -1;
            }         
        }
    }
}
