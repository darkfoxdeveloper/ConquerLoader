using CLCore;
using CLCore.Models;
using ConquerLoader.CLCore;
using ConquerLoader.Models;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
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
        public string HookINI = "CLHook.ini";
        public string HookDLL = "CLHook.dll";
        public bool AllStarted = false;
        public Main()
        {
            InitializeComponent();
            this.Resizable = false;
            this.Theme = MetroFramework.MetroThemeStyle.Light;
            LoaderConfig = Core.GetLoaderConfig();
            LoaderEvents.LauncherLoaded += LoaderEvents_LauncherLoaded;
            LoaderEvents.ConquerLaunched += LoaderEvents_ConquerLaunched;
            LoaderEvents.LauncherExit += LoaderEvents_LauncherExit;
            Constants.ClientPath = Directory.GetCurrentDirectory();
            if (LoaderConfig != null) Constants.CloseOnFinish = LoaderConfig.CloseOnFinish;
            Constants.MainWorker = worker;
            Core.LoadAvailablePlugins();
            Core.LoadRemotePlugins();
            Core.InitPlugins();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            if (LoaderConfig == null)
            {
                Settings s = new Settings();
                s.ShowDialog(this);
            }
            else
            {
                LoadConfigInForm();
            }
            RefreshServerList();

            AllStarted = true;
            LoaderEvents.LauncherLoadedStartEvent();
        }

        //  The NotifyIcon object
        private void TrayMinimizerForm_Resize(object sender, EventArgs e)
        {
            if (Constants.HideInTrayOnFinish)
            {
                noty.Text = ProductName;
                noty.Icon = Properties.Resources.ConquerLoaderLogo;

                if (FormWindowState.Minimized == this.WindowState)
                {
                    noty.Visible = true;
                    Hide();
                    noty.BalloonTipTitle = $"{ProductName} {ProductVersion}";
                    noty.BalloonTipText = "The best loader for ConquerOnline";
                    noty.ShowBalloonTip(1000);
                }
                else if (FormWindowState.Normal == this.WindowState)
                {
                    noty.Visible = false;
                }
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bool Exists = CurrentConquerProcess != null;
            if (Exists)
            {
                CurrentConquerProcess.Kill();
                if (Process.GetProcessesByName(CurrentConquerProcess.ProcessName).Count() > 0)
                {
                    Environment.Exit(0);
                }
            }
        }

        private void LoaderEvents_LauncherLoaded()
        {
            Core.LogWritter.Write("Event LauncherLoaded Fired!");
        }

        private void LoaderEvents_ConquerLaunched(List<Parameter> parameters)
        {
            string parametersTxt = "";
            foreach (Parameter p in parameters)
            {
                parametersTxt += $"Id={p.Id };Value={p.Value}";
            }
            Core.LogWritter.Write($"Event ConquerLaunched Fired! Parameters: {parametersTxt}");
        }

        private void LoaderEvents_LauncherExit(List<Parameter> parameters)
        {
            try { Core.LogWritter.Write("Event LauncherExit Fired!"); } catch(Exception) {}
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
            Constants.LicenseKey = LoaderConfig.LicenseKey;
            foreach (ServerConfiguration server in LoaderConfig.Servers)
            {
                cbxServers.Items.Add(server.ServerName);
            }
            RefreshServerList();
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
            try
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
                    if (LoaderConfig.ServernameChange)
                    {
                        if (SelectedServer.ServerNameMemoryAddress != null && SelectedServer.ServerNameMemoryAddress.Length >= 8) // Detect valid memory address
                        {
                            // Nothing to do. Using the specified custom memory address 
                        }
                        else
                        {
                            // Set default recommended for each range of versions
                            if (SelectedServer.ServerVersion < 5600)
                            {
                                SelectedServer.ServerNameMemoryAddress = "0x005726DC";
                            }
                            if (SelectedServer.ServerVersion >= 5600)
                            {
                                SelectedServer.ServerNameMemoryAddress = "0x0097FAB8";
                            }
                            if (SelectedServer.ServerVersion >= 5700)
                            {
                                SelectedServer.ServerNameMemoryAddress = "0x009BEA00";
                            }
                            if (SelectedServer.ServerVersion >= 6000)
                            {
                                SelectedServer.ServerNameMemoryAddress = "0x00A56348";
                            }
                            if (SelectedServer.ServerVersion >= 6609)
                            {
                                SelectedServer.ServerNameMemoryAddress = "0x00CE8180";
                            }
                            if (SelectedServer.ServerVersion >= 6617)
                            {
                                SelectedServer.ServerNameMemoryAddress = "0x00CD7240";
                            }
                        }
                    }
                    else
                    {
                        SelectedServer.ServerNameMemoryAddress = "0";
                    }
                    // Create first the config used by DLL
                    File.WriteAllText(HookINI, "[CLHook]"
                        + Environment.NewLine + "HOST=" + SelectedServer.LoginHost
                        + Environment.NewLine + "GAMEHOST=" + SelectedServer.GameHost
                        + Environment.NewLine + "PORT=" + SelectedServer.LoginPort
                        + Environment.NewLine + "GAMEPORT=" + SelectedServer.GamePort
                        + Environment.NewLine + "SERVERNAME=" + SelectedServer.ServerName
                        + Environment.NewLine + "ENABLE_HOSTNAME=" + (SelectedServer.EnableHostName ? "1" : "0")
                        + Environment.NewLine + "HOSTNAME=" + SelectedServer.Hostname
                        + Environment.NewLine + "SERVER_VERSION=" + SelectedServer.ServerVersion
                        + Environment.NewLine + "SERVERNAME_MEMORYADDRESS=" + SelectedServer.ServerNameMemoryAddress
                        + Environment.NewLine + "DISABLE_AUTOFIX_FLASH=" + (LoaderConfig.DisableAutoFixFlash ? "1" : "0")
                        );
                    Core.LogWritter.Write("Created the Hook Configuration");
                    // Modify Setup of client
                    string SetupIniPath = Path.Combine(Directory.GetCurrentDirectory(), "ini", "GameSetup.ini");
                    IniManager parser = new IniManager(SetupIniPath, "ScreenMode");
                    parser.Write("ScreenMode", "FullScrType", LoaderConfig.FullScreen ? "0" : "1");
                    if (LoaderConfig.HighResolution)
                    {
                        parser.Write("ScreenMode", "ScrWidth", "1024");
                        parser.Write("ScreenMode", "ScrHeight", "768");
                        /*
                         * ScreenModeRecord
                         *  0 = 800x600, windowed
                            1 = 800x600, full-screen
                            2 = 1024x768, windowed
                            3 = 1024x768, full-screen
                         * */
                        parser.Write("ScreenMode", "ScreenModeRecord", LoaderConfig.FullScreen ? "3" : "2");
                    }
                    else
                    {
                        parser.Write("ScreenMode", "ScrWidth", "800");
                        parser.Write("ScreenMode", "ScrHeight", "600");
                        /*
                         * ScreenModeRecord
                         *  0 = 800x600, windowed
                            1 = 800x600, full-screen
                            2 = 1024x768, windowed
                            3 = 1024x768, full-screen
                         * */
                        parser.Write("ScreenMode", "ScreenModeRecord", LoaderConfig.FullScreen ? "1" : "0");
                    }
                    worker.RunWorkerAsync();
                }
            } catch(Exception ex)
            {
                Core.LogWritter.Write("Error found: " + ex);
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
                    string OutputCopyDll = Path.Combine(Application.StartupPath, "Env_DX8", HookINI);
                    if (File.Exists(OutputCopyDll))
                    {
                        File.Delete(OutputCopyDll);
                    }
                    File.Copy(Path.Combine(Application.StartupPath, HookINI), OutputCopyDll);
                }
                if (SelectedServer.UseDirectX9)
                {
                    if (File.Exists(CheckPathEnvDX9))
                    {
                        Core.LogWritter.Write("Found Env_DX9 Folder. Setting the folder for run executable...");
                        PathToConquerExe = CheckPathEnvDX9;
                        WorkingDir = Path.GetDirectoryName(PathToConquerExe);
                        string OutputCopyDll = Path.Combine(Application.StartupPath, "Env_DX9", HookINI);
                        if (File.Exists(OutputCopyDll))
                        {
                            File.Delete(OutputCopyDll);
                        }
                        File.Copy(Path.Combine(Application.StartupPath, HookINI), OutputCopyDll);
                    }
                }
                Process conquerProc = Process.Start(new ProcessStartInfo() { FileName = PathToConquerExe, WorkingDirectory = WorkingDir, Arguments = "blacknull" });
                if (conquerProc != null)
                {
                    Core.LogWritter.Write("Process launched!");
                    worker.ReportProgress(10);
                    CurrentConquerProcess = conquerProc;
                    int ConquerOpened = Process.GetProcessesByName(CurrentConquerProcess.ProcessName).Count();
                    Core.LogWritter.Write($"CLServer Enabled: {LoaderConfig.CLServer} . Processes of Conquer opened: {ConquerOpened} (Only connect if have less or equal to 1)");
                    if (LoaderConfig.CLServer && ConquerOpened <= 1) // Only if not have other Conquer.exe opened
                    {
                        Core.LogWritter.Write("Connecting to CLServer");
                        // Try connect to CLServer
                        try
                        {
                            SocketSystem.CurrentSocketClient = new CLClient(SelectedServer.LoginHost, 8000);
                            Core.LogWritter.Write(string.Format("CLClient connected at CLServer with port {0}.", 8000));
                        }
                        catch (Exception ex) // Prevent break process of loader
                        {
                            Console.WriteLine("Cannot connect to CLServer: {0}", ex);
                        }
                    }
                    LoaderEvents.ConquerLaunchedStartEvent(new List<Parameter>
                    {
                        new Parameter() { Id = "ConquerProcessId", Value = CurrentConquerProcess.Id.ToString() },
                        new Parameter() { Id = "GameServerIP", Value = SelectedServer.GameHost }
                    });
                    Core.LogWritter.Write("Injecting DLL...");
                    worker.ReportProgress(20);
                    if (SelectedServer.ServerVersion >= 6187)
                    {
                        conquerProc.WaitForInputIdle(35000);
                    }
                    if (!Injector.StartInjection(Application.StartupPath + @"\" + HookDLL, (uint)conquerProc.Id, worker))
                    {
                        Core.LogWritter.Write("Injection failed!");
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
                if (Constants.CloseOnFinish)
                {
                    LoaderEvents.LauncherExitStartEvent(new List<Parameter>() { new Parameter() { Id = "CLOSE_MESSAGE", Value = "Finished" } });
                    Environment.Exit(0);
                }
                if (Constants.HideInTrayOnFinish)
                {
                    if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() > 0)
                    {
                        LoaderEvents.LauncherExitStartEvent(new List<Parameter>() { new Parameter() { Id = "CLOSE_MESSAGE", Value = "Finished" } });
                        Environment.Exit(0);
                    } else
                    {
                        WindowState = FormWindowState.Minimized;
                    }
                }
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
