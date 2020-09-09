using ConquerLoader.Models;
using MetroFramework.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ConquerLoader
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public LoaderConfig LoaderConfig = null;
        public ServerConfiguration SelectedServer = null;
        public Process CurrentConquerProcess = null;
        public string HookINI = "OpenConquerHook.ini";
        public string HookDLL = "OpenConquerHook.dll";
        public bool AllStarted = false;
        public Main()
        {
            InitializeComponent();
            this.Resizable = false;
            this.Theme = MetroFramework.MetroThemeStyle.Light;
        }

        /*
         * TODO LIST
         * Change servername on new clients with encrypted Server.dat (Maybe the best option is find the String of first Server in Server.dat (Decrypting it with the tool in the oc forum) in memory and remplace this with the custom string of LoaderConfig.SelectedServer.ServerName
         * Create a page with wordpress and storefront theme for anounce the ConquerLoader with the domain conquerloader.com (Simple page only for now, in future have complements for sell and implement in this loader)
         * Premium Features (Control with IP of servername in some AccessList, json or via Database) with the features are payed for the user (User = Server IP)
         * */

        private void Main_Load(object sender, EventArgs e)
        {
            LoaderConfig = Core.GetLoaderConfig();
            if (LoaderConfig == null)
            {
                Settings s = new Settings();
                s.ShowDialog(this);
            } else
            {
                LoadConfigInForm();
            }
            RefreshServerList();
            AllStarted = true;
        }

        private void LoadConfigInForm()
        {
            cbxServers.Items.Clear();
            if (LoaderConfig.Title != null && LoaderConfig.Title.Length > 0)
            {
                Text = LoaderConfig.Title;
            }
            Core.LogWritter.Write("Loaded config.json");
            btnLogModules.Enabled = LoaderConfig.DebugMode;
            btnLogModules.Visible = LoaderConfig.DebugMode;
            foreach (ServerConfiguration server in LoaderConfig.Servers)
            {
                cbxServers.Items.Add(server.ServerName);
            }
        }

        private void RefreshServerList(bool CheckServerStatus = true)
        {
            if (LoaderConfig.Servers.Count > 0)
            {
                cbxServers.SelectedItem = cbxServers.Items[0];
                foreach (object s in cbxServers.Items)
                {
                    if (LoaderConfig.DefaultServer != null && s.Equals(LoaderConfig.DefaultServer.ServerName))
                    {
                        cbxServers.SelectedItem = s;
                        if (CheckServerStatus)
                        {
                            SetServerStatus();
                        }
                    }
                }
            }
        }

        private void SetServerStatus()
        {
            bool Online = Core.ServerAvailable(LoaderConfig.DefaultServer.LoginHost, LoaderConfig.DefaultServer.GamePort);
            serverStatus.Text = Online ? "ONLINE" : "OFFLINE";
            if (Online)
            {
                serverStatus.ForeColor = Color.DarkGreen;
            } else
            {
                serverStatus.ForeColor = Color.DarkRed;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (LoaderConfig == null) return;
            if (cbxServers.SelectedItem == null) return;
            SelectedServer = LoaderConfig.Servers.Where(x => x.ServerName == cbxServers.SelectedItem.ToString()).FirstOrDefault();
            if (SelectedServer != null)
            {
                // Save Last Selected
                SelectedServer = LoaderConfig.Servers.Where(x => x.ServerName == cbxServers.SelectedItem.ToString()).FirstOrDefault();
                LoaderConfig.DefaultServer = SelectedServer;
                Core.SaveLoaderConfig(LoaderConfig);
                if (File.Exists(HookINI))
                {
                    File.Delete(HookINI);
                }
                // Create first the config used by DLL
                File.WriteAllText(HookINI, "[OpenConquerHook]"
                    + Environment.NewLine + "HOST=" + SelectedServer.LoginHost
                    + Environment.NewLine + "GAMEHOST=" + SelectedServer.GameHost
                    + Environment.NewLine + "PORT=" + SelectedServer.LoginPort
                    + Environment.NewLine + "GAMEPORT=" + SelectedServer.GamePort
                    + Environment.NewLine + "SERVERNAME=" + SelectedServer.ServerName
                    + Environment.NewLine + "ENABLE_HOSTNAME=" + (SelectedServer.EnableHostName ? "1" : "0")
                    + Environment.NewLine + "HOSTNAME=" + SelectedServer.Hostname
                    + Environment.NewLine + "SERVER_VERSION=" + SelectedServer.ServerVersion
                    );
                Core.LogWritter.Write("Created the Hook Configuration");
                worker.RunWorkerAsync();
            }
        }

        private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Do start conquer and inject
            Core.LogWritter.Write("Launching " + SelectedServer.ExecutableName + "...");
            string PathToConquerExe = Path.Combine(Application.StartupPath, SelectedServer.ExecutableName);
            string WorkingDir = Path.GetDirectoryName(PathToConquerExe);
            if (File.Exists(PathToConquerExe))
            {
                string CheckPathEnvDX8 = Path.Combine(Application.StartupPath, "Env_DX8", SelectedServer.ExecutableName);
                string CheckPathEnvDX9 = Path.Combine(Application.StartupPath, "Env_DX9", SelectedServer.ExecutableName);
                Core.LogWritter.Write("Checking if existing path: " + CheckPathEnvDX8 + "...");
                if (File.Exists(CheckPathEnvDX8))
                {
                    Core.LogWritter.Write("Found Env_DX8 Folder. Setting the folder for run executable...");
                    PathToConquerExe = CheckPathEnvDX8;
                    WorkingDir = Path.GetDirectoryName(PathToConquerExe);
                }
                if (SelectedServer.UseDirectX9)
                {
                    if (File.Exists(CheckPathEnvDX9))
                    {
                        Core.LogWritter.Write("Found Env_DX9 Folder. Setting the folder for run executable...");
                        PathToConquerExe = CheckPathEnvDX9;
                        WorkingDir = Path.GetDirectoryName(PathToConquerExe);
                    }
                }
                Process conquerProc = Process.Start(new ProcessStartInfo() { FileName = PathToConquerExe, WorkingDirectory = WorkingDir, Arguments = "blacknull" });
                if (conquerProc != null)
                {
                    Core.LogWritter.Write("Process launched!");
                    CurrentConquerProcess = conquerProc;
                    DllInjector.GetInstance.worker = worker;
                    Core.LogWritter.Write("Injecting DLL...");
                    if (DllInjector.GetInstance.Inject((uint)conquerProc.Id, Application.StartupPath + @"\" + HookDLL) != DllInjectionResult.Success)
                    {
                        MetroFramework.MetroMessageBox.Show(this, $"[{SelectedServer.ServerName}] Cannot inject " + HookDLL, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Core.LogWritter.Write("Injected successfully!");
                    }
                }
                else
                {
                    Core.LogWritter.Write("Cannot launch " + SelectedServer.ExecutableName + "");
                    MetroFramework.MetroMessageBox.Show(this, $"[{SelectedServer.ServerName}] Cannot start {SelectedServer.ExecutableName}", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else
            {
                Core.LogWritter.Write("Path for Executable not found: " + PathToConquerExe);
            }
        }

        private void Worker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.pBar.Value = e.ProgressPercentage;
            if (this.pBar.Value >= 100)
            {
                if (LoaderConfig.CloseOnFinish) Environment.Exit(0);
            }
        }

        private void BtnLogModules_Click(object sender, EventArgs e)
        {
            if (CurrentConquerProcess == null) return;
            string t = "Modules on process: " + CurrentConquerProcess.ProcessName;
            foreach (ProcessModule m in CurrentConquerProcess.Modules)
            {
                t += "ModuleName:" + m.ModuleName + Environment.NewLine + "FileName:" + m.FileName + Environment.NewLine;
            }
            Core.LogWritter.Write(t);
        }

        private void CbxServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AllStarted)
            {
                foreach (object s in cbxServers.Items)
                {
                    MetroComboBox currentSender = sender as MetroComboBox;
                    if (s.Equals(currentSender.SelectedItem.ToString()))
                    {
                        cbxServers.SelectedItem = currentSender.SelectedItem.ToString();
                        LoaderConfig.DefaultServer = LoaderConfig.Servers.Where(x => x.ServerName == currentSender.SelectedItem.ToString()).FirstOrDefault();
                        SetServerStatus();
                        Core.SaveLoaderConfig(LoaderConfig);
                    }
                }
            }
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            s.ShowDialog(this);
            LoaderConfig = s.CurrentLoaderConfig;
            LoadConfigInForm();
        }
    }
}
