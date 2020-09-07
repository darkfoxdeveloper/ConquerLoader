using ConquerLoader.Models;
using System;
using System.Diagnostics;
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
        public Main()
        {
            InitializeComponent();
            this.Resizable = false;
            this.Theme = MetroFramework.MetroThemeStyle.Light;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoaderConfig = Core.GetLoaderConfig();
            if (LoaderConfig == null)
            {
                MetroFramework.MetroMessageBox.Show(this, "Cannot load config.json", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
                // TODO Create a new gui for manage this config.json and show in this case
            } else
            {
                Core.DebugWritter.Write("Loaded config.json");
                btnLogModules.Enabled = LoaderConfig.DebugMode;
                btnLogModules.Visible = LoaderConfig.DebugMode;
                foreach (ServerConfiguration server in LoaderConfig.Servers)
                {
                    cbxServers.Items.Add(server.ServerName);
                }
                if (LoaderConfig.Servers.Count > 0)
                {
                    cbxServers.SelectedItem = cbxServers.Items[0];
                    foreach (object s in cbxServers.Items)
                    {
                        if (s.Equals(LoaderConfig.DefaultServer.ServerName))
                        {
                            cbxServers.SelectedItem = s;
                        }
                    }
                    
                }
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
                Core.DebugWritter.Write("Created the Hook Configuration");
                worker.RunWorkerAsync();
            }
        }

        private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Do start conquer and inject
            Core.DebugWritter.Write("Launching " + SelectedServer.ExecutableName + "...");
            Process conquerProc = Process.Start(new ProcessStartInfo() { FileName = Application.StartupPath + @"\" + SelectedServer.ExecutableName, Arguments = "blacknull" });
            if (conquerProc != null)
            {
                Core.DebugWritter.Write("Process launched!");
                CurrentConquerProcess = conquerProc;
                DllInjector.GetInstance.worker = worker;
                Core.DebugWritter.Write("Injecting DLL...");
                if (DllInjector.GetInstance.Inject(conquerProc.ProcessName, Application.StartupPath + @"\" + HookDLL) != DllInjectionResult.Success)
                {
                    MetroFramework.MetroMessageBox.Show(this, $"[{SelectedServer.ServerName}] Cannot inject " + HookDLL, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else
                {
                    Core.DebugWritter.Write("Injected successfully!");
                }
            }
            else
            {
                Core.DebugWritter.Write("Cannot launch " + SelectedServer.ExecutableName + "");
                MetroFramework.MetroMessageBox.Show(this, $"[{SelectedServer.ServerName}] Cannot start {SelectedServer.ExecutableName}", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Worker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.pBar.Value = e.ProgressPercentage;
            if (this.pBar.Value >= 100)
            {
                //Environment.Exit(0);
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
            Core.DebugWritter.Write(t);
        }
    }
}
