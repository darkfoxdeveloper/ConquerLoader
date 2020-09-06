using ConquerLoader.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using static ConquerLoader.Core;

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
                // For testing
                LoaderConfig l = new LoaderConfig();
                l.Servers.Add(new ServerConfiguration() { ExecutableName = "Conquer.exe", LoginHost = "127.0.0.1", GameHost = "127.0.0.1", GamePort= 5816, LoginPort = 9958, ServerName = "ConquerLoaderIsHere"});
                l.DefaultServer = l.Servers.FirstOrDefault();
                File.WriteAllText("config.json", Newtonsoft.Json.JsonConvert.SerializeObject(l, Newtonsoft.Json.Formatting.Indented));
            } else
            {
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
                worker.RunWorkerAsync();
            }
        }

        private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // TODO Validación de IPS que pueden usar el Loader para que asi aun que se venda el codigo fuente habra una libreria que controlara quien puede o no usar el loader. La libreria en question tendra un codigo necesario para que funcione el loader
            // Do start conquer and inject

            //Directory.SetCurrentDirectory(@"C:\Users\DaRkFoxDeveloper\Desktop\5065");
            //Process conquerProc = Process.Start(new ProcessStartInfo() { FileName = @"C:\Users\DaRkFoxDeveloper\Desktop\5065" + @"\" + SelectedServer.ExecutableName, Arguments = "blacknull" });
            Process conquerProc = Process.Start(new ProcessStartInfo() { FileName = Application.StartupPath + @"\" + SelectedServer.ExecutableName, Arguments = "blacknull" });
            if (conquerProc != null)
            {
                CurrentConquerProcess = conquerProc;
                DllInjector.GetInstance.Inject(conquerProc.ProcessName, Application.StartupPath + @"\" + HookDLL);
                //if (!InjectDLL(CurrentConquerProcess.Handle, Application.StartupPath + @"\" + HookDLL, worker))
                //{
                //    MetroFramework.MetroMessageBox.Show(this, $"[{SelectedServer.ServerName}] Cannot inject " + HookDLL, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            else
            {
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
            string t = "";
            foreach (ProcessModule m in CurrentConquerProcess.Modules)
            {
                t += "ModuleName:" + m.ModuleName + Environment.NewLine + "FileName:" + m.FileName + Environment.NewLine;
            }
            File.WriteAllText("modules.log", t);
        }
    }
}
