using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftBackup
{
    public partial class Form1 : Form
    {
        private string dirPath;
        private string minecraftFolder;

        protected void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public Form1()
        {
            string pathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string minecraftFolder = Path.Combine(pathAppData, ".minecraft");
            this.minecraftFolder = minecraftFolder;
            if (!Directory.Exists(minecraftFolder))
            {
                Application.Exit();
            }
            string backupDirPath = Path.Combine(pathAppData, ".mcbackup");
            this.dirPath = backupDirPath;
            InitializeComponent();
            if (!Directory.Exists(backupDirPath))
            {
                Directory.CreateDirectory(backupDirPath);
            }
            else
            {
                button5.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 23;
            string versionsDirectory = Path.Combine(dirPath, "jar");
            if (!Directory.Exists(versionsDirectory))
            {
                Directory.CreateDirectory(versionsDirectory);
            }
            progressBar1.Value = 34;
            string mcVersionDirectory = Path.Combine(minecraftFolder, "versions");
            string versionDir = Path.Combine(mcVersionDirectory, "1.8.9");
            string theJar = Path.Combine(versionDir, "1.8.9.jar");
            string jsonFile = Path.Combine(versionDir, "1.8.9.json");
            progressBar1.Value = 50;
            if (!Directory.Exists(versionDir))
            {
                Application.Exit();
            }
            progressBar1.Value = 80;
            string futureJarLoc = Path.Combine(versionsDirectory, "1.8.9.jar");
            string futureJsonLoc = Path.Combine(versionsDirectory, "1.8.9.json");
            File.Copy(theJar, futureJarLoc, true);
            File.Copy(jsonFile, futureJsonLoc, true);
            progressBar1.Value = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 70;
            string savesDirectory = Path.Combine(minecraftFolder, "saves");
            string backupSavesDirectory = Path.Combine(dirPath, "saves");
            if (!Directory.Exists(savesDirectory))
            {
                Application.Exit();
            }
            if (!Directory.Exists(backupSavesDirectory))
            {
                Directory.CreateDirectory(backupSavesDirectory);
            }
            DirectoryCopy(savesDirectory, backupSavesDirectory, true);
            progressBar1.Value = 100;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 70;
            string resourcePackDirectory = Path.Combine(minecraftFolder, "resourcepacks");
            string backupResourcePackDirectory = Path.Combine(dirPath, "resourcepacks");
            if (!Directory.Exists(resourcePackDirectory))
            {
                Application.Exit();
            }
            if (!Directory.Exists(backupResourcePackDirectory))
            {
                Directory.CreateDirectory(backupResourcePackDirectory);
            }
            DirectoryCopy(resourcePackDirectory, backupResourcePackDirectory, true);
            progressBar1.Value = 100;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 80;
            string serversDatOri = Path.Combine(minecraftFolder, "servers.dat");
            string serversDatBackup = Path.Combine(dirPath, "servers.dat");
            if (!File.Exists(serversDatOri))
            {
                Application.Exit();
            }
            File.Copy(serversDatOri, serversDatBackup);
            progressBar1.Value = 100;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 90;
            if (!Directory.Exists(dirPath))
            {
                Application.Exit();
            }
            DirectoryCopy(dirPath, minecraftFolder, true);
            progressBar1.Value = 100;
        }
    }
}
