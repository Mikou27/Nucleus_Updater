using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Updater
{
    public partial class Updater : Form
    {
        private string version;
        private WebClient webClient;
        public static string destinationPath = Application.StartupPath;
        private bool finished = false;

        public Updater(string newVersion)
        {
            InitializeComponent();
            version = newVersion;
            label.Text = VersionCheck.modeText;
            label.Location = new Point(Width / 2 - label.Width / 2, label.Location.Y);
            TopMost = true;  
        }

        private void btn_yes_MouseClick(object sender, MouseEventArgs e)
        {
            if (finished)
            {
                Invoke(new Action(delegate
                {
                    Close();
                }));

                return;
            }

            DownloadReleaseZip();

            btn_yes.Visible = false;
            btn_no.Visible = false;
            prog_DownloadBar.Visible = true;
            Process[] processes = Process.GetProcessesByName("NucleusCoop");

            foreach (Process NucleusCoop in processes)
            {
                NucleusCoop.Kill();
            }
        }

        private void DownloadReleaseZip()
        {
            DeleteTemp();

            if (!Directory.Exists(Path.Combine(destinationPath, @"Temp")))
            {
                Directory.CreateDirectory((Path.Combine(destinationPath, @"Temp")));
            }

            if (Directory.Exists(Path.Combine(destinationPath, @"Temp")))///Will download and extract in the previously created "Temp" folder.
            {
                using (webClient = new WebClient())
                {
                    webClient.DownloadProgressChanged += wc_DownloadProgressChanged;
                    webClient.DownloadFileAsync(
                    //new System.Uri($@"https://github.com/SplitScreen-Me/splitscreenme-nucleus/releases/download/{version}/NucleusApp.zip"),
                     new System.Uri($@"https://github.com/Mikou27/splitscreenme-nucleus/releases/download/{version}/NucleusApp.zip"),
                    Path.Combine(destinationPath, @"Temp\\NucleusApp.zip"));
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                }
            }
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Invoke(new Action(delegate
                {
                    prog_DownloadBar.Value = 0;
                    label.Text = $"Extract and install Nucleus {version}";
                    label.Location = new Point(Width / 2 - label.Width / 2, label.Location.Y);

                }));

                bool isValidZip = ZipFile.CheckZip(Path.Combine(destinationPath, @"Temp\NucleusApp.zip"));
                if (isValidZip)
                {
                    ZipFile zip = new ZipFile(Path.Combine(destinationPath, @"Temp\NucleusApp.zip"));
                    zip.Password = "nucleus";
                    zip.ExtractAll(Path.Combine(destinationPath, @"Temp"));
                    zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                    zip.Dispose();
                }
                else
                {
                    MessageBox.Show("Zip file doesn't exist or is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                List<string> currentFiles = new List<string>(Directory.GetFileSystemEntries(destinationPath, "*", SearchOption.AllDirectories));
                List<string> updateFiles = new List<string>(Directory.GetFileSystemEntries(Path.Combine(destinationPath, @"Temp"), "*", SearchOption.AllDirectories));

                List<string> currentFilesNames = new List<string>();

                foreach (string current in currentFiles)
                {
                    string[] fileName = current.Split('\\');
                    int index = fileName.Length - 1;
                    currentFilesNames.Add(fileName[index]);
                }

                int count = 0;
                List<string> newFilesCheck = new List<string>();

                foreach (string update in updateFiles)
                {
                    if (update.Contains("NucleusApp.zip"))
                    {
                        continue;
                    }

                    if (File.Exists(update))//check if it's a file and not a folder path
                    {
                        string[] fileName = update.Split('\\');
                        int index = fileName.Length - 1;
                        string updatefileName = fileName[index];

                        for (int i = 0; i < currentFiles.Count; i++)
                        {
                            if (currentFiles[i].Contains("Temp") || currentFiles[i].Contains("content") ||
                                currentFiles[i].Contains("debug-log.txt") || currentFiles[i].Contains("theme") ||
                                currentFiles[i].Contains("Updater.exe") || currentFiles[i].Contains("games profiles"))
                            {
                                continue;
                            }

                            string currentFileName = currentFilesNames[i];

                            newFilesCheck.Add(currentFileName);//clean paths list(no "Temp"/"content")
                            if (updatefileName == currentFileName)
                            {
                                if (currentFileName == "Settings.ini")
                                {
                                    UpdateSettingsInI(currentFiles[i], update);
                                    continue;
                                }

                                byte[] toCompareUpdate = File.ReadAllBytes(update);
                                byte[] toCompareCurrent = File.ReadAllBytes(currentFiles[i]);

                                if (toCompareUpdate.Length != toCompareCurrent.Length || update.Contains("readme.txt"))//Force readme.txt update if the length of the new one is equal.
                                {
                                    try
                                    {
                                        File.Delete(currentFiles[i]);
                                        File.Copy(update, currentFiles[i], true);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(string.Format("{0}: {1}", ex.ToString(), ex.Message) + " \n " + update + "\n " + currentFiles[i], "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        continue;
                                    }
                                }
                            }
                        }

                        //check if the update zip contains new files
                        if (!newFilesCheck.Contains(updatefileName))
                        {
                            string newFilePath = destinationPath + update.Substring(update.IndexOf("\\Temp") + 5);//build destination path(install root + new file path) 
                            string filePathNoFileName = newFilePath.Remove(newFilePath.IndexOf(updatefileName));//remove file name from path(keep folder path only in order to create the new required folders)

                            if (!Directory.Exists(filePathNoFileName))
                            {
                                Directory.CreateDirectory(filePathNoFileName);
                            }

                            Console.WriteLine(newFilePath);
                            File.Copy(update, newFilePath, true);
                        }
                        count++;
                    }

                    Invoke(new Action(delegate { prog_DownloadBar.Value = (count*100)/updateFiles.Count; }));
                }              

                Invoke(new Action(delegate
                {
                    DeleteTemp();
                    prog_DownloadBar.Dispose();
                    finished = true;

                    btn_yes.Visible = true;
                    btn_yes.Text = "Exit";
                    btn_yes.Location = new Point(Width / 2 - btn_yes.Width / 2, Height / 2 - btn_yes.Height / 2);

                    label.Text = "Installation conpleted!";
                    label.Location = new Point(Width / 2 - label.Width / 2, label.Location.Y);
                }));
            });
        }

        private void UpdateSettingsInI(string currentIniPath, string updateIniPath)
        {
            //Current settings.ini
            Dictionary<string, List<string>> currentIniFields = new Dictionary<string, List<string>>();
            string[] CurrentIniContent = File.ReadAllLines(currentIniPath);
            string currentField = "";

            foreach (string field in CurrentIniContent)
            {
                if (field.StartsWith("["))
                {
                    currentIniFields.Add(field, new List<string>());
                    currentField = field;
                }
                else
                {
                    currentIniFields[currentField].Add(field);
                }
            }

            //Update settings.ini
            Dictionary<string, List<string>> updateIniFields = new Dictionary<string, List<string>>();
            string[] UpdateIniContent = File.ReadAllLines(updateIniPath);
            string updateField = "";

            foreach (string field in UpdateIniContent)
            {
                if (field.StartsWith("["))
                {
                    updateIniFields.Add(field, new List<string>());
                    updateField = field;
                }
                else
                {
                    updateIniFields[updateField].Add(field);
                }
            }

            //Will now compare the content of ini files
            List<string> currentOptions = new List<string>();

            foreach (KeyValuePair<string, List<string>> option in updateIniFields)
            {
                if (!currentIniFields.Keys.Contains(option.Key))
                {
                    currentIniFields.Add(option.Key, option.Value);
                }
                else if (currentIniFields.Keys.Contains(option.Key))
                {
                    foreach (string updateValue in updateIniFields[option.Key])
                    {
                        string[] updateOptionName = updateValue.Split('=');

                        foreach (string s in currentIniFields[option.Key])
                        {
                            string[] currentOptionName = s.Split('=');
                            currentOptions.Add(currentOptionName[0]);
                        }

                        if (!currentOptions.Contains(updateOptionName[0]))
                        {
                            currentIniFields[option.Key].Add(updateValue);
                        }
                    }
                }
            }

            //Will now write the updated ini file
            List<string> finalIniContent = new List<string>();

            foreach (KeyValuePair<string, List<string>> option in currentIniFields)
            {
                finalIniContent.Add(option.Key);
                foreach (string val in option.Value)
                {
                    finalIniContent.Add(val);
                }
            }

            File.WriteAllLines(Path.Combine(destinationPath, @"Settings.ini"), finalIniContent);
            // File.WriteAllLines(Path.Combine(destinationPath, @"test.ini"), finalIniContent);
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            prog_DownloadBar.Value = e.ProgressPercentage;
            label.Text = e.ProgressPercentage + "%";
            label.Location = new Point((prog_DownloadBar.Location.X + (prog_DownloadBar.Width / 2)) - label.Width / 2, label.Location.Y);
        }

        private void btn_no_MouseClick(object sender, MouseEventArgs e)
        {
            DeleteTemp();
            Close();
        }

        private void DeleteTemp()
        {
            if (destinationPath == null || destinationPath == string.Empty)
            {
                return;
            }

            try
            {
                if (webClient != null)
                {
                    webClient.CancelAsync();
                }

                Directory.Delete(Path.Combine(destinationPath, @"Temp"), true);
            }
            catch /*(Exception ex)*/
            {
                //MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Updater_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeleteTemp();
        }

        private void label_changelog_Click(object sender, EventArgs e)
        {
            Process.Start($@"https://github.com/SplitScreen-Me/splitscreenme-nucleus/releases/tag/{version}");
        }
    }
}
