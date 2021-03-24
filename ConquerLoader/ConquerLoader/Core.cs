using CLCore;
using CLCore.Models;
using ConquerLoader.Properties;
using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ConquerLoader
{
    public static class Core
    {
        public static LogWritter LogWritter = new LogWritter("conquerloader.log");
        public static string ConfigJsonPath = "config.json";
        public static LoaderConfig GetLoaderConfig()
        {
            LoaderConfig lConfig = null;
            if (File.Exists(ConfigJsonPath))
            {
                lConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<LoaderConfig>(File.ReadAllText(ConfigJsonPath));
            }
            return lConfig;
        }

        internal static void LoadAvailablePlugins()
        {
            try
            {
                PluginLoader loader = new PluginLoader();
                loader.LoadPlugins();
                LogWritter.Write("Loaded " + PluginLoader.Plugins.Count + " plugins.");
            }
            catch (Exception e)
            {
                LogWritter.Write(string.Format("Plugins couldn't be loaded: {0}", e.Message));
                Environment.Exit(0);
            }
        }

        public static void LoadRemotePlugins()
        {
            // Remote plugins
            try
            {
                PluginLoader loader = new PluginLoader();
                int loaded = loader.LoadPluginsFromAPI(GetLoaderConfig());
                Core.LogWritter.Write("Loaded " + loaded + " remote plugins.");
            }
            catch (Exception ex)
            {
                Core.LogWritter.Write("Error remote plugins init: " + ex.ToString());
            }
        }

        public static void InitPlugins()
        {
            foreach (IPlugin plugin in PluginLoader.Plugins)
            {
                plugin.Init();
                LogWritter.Write("Init plugin: " + plugin.Name + ".");
            }
        }

        public static void SaveLoaderConfig(LoaderConfig LoaderConfig)
        {
            File.WriteAllText(ConfigJsonPath, Newtonsoft.Json.JsonConvert.SerializeObject(LoaderConfig, Newtonsoft.Json.Formatting.Indented));
        }
        public static bool ServerAvailable(string Host, uint Port)
        {
            var result = false;
            using (var client = new TcpClient())
            {
                try
                {
                    client.ReceiveTimeout = 1 * 1000;
                    client.SendTimeout = 1 * 1000;
                    var asyncResult = client.BeginConnect(Host, (int)Port, null, null);
                    var waitHandle = asyncResult.AsyncWaitHandle;
                    try
                    {
                        if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(0.6), false))
                        {
                            // wait handle didn't came back in time
                            client.Close();
                        }
                        else
                        {
                            // The result was positiv
                            result = client.Connected;
                            // ensure the ending-call
                            client.EndConnect(asyncResult);
                        }
                    }
                    catch(Exception)
                    {
                    }
                    finally
                    {
                        // Ensure to close the wait handle.
                        waitHandle.Close();
                    }
                }
                catch(Exception)
                {
                }
            }
            return result;
        }

        public static void LoadControlTranslations(Control.ControlCollection Controls)
        {
            foreach (Control c in Controls)
            {
                if (c is MetroFramework.Controls.MetroLabel || c is MetroFramework.Controls.MetroButton)
                {
                    string str = Strings.ResourceManager.GetString(c.Name);
                    if (str != null && str.Length > 0)
                    {
                        c.Text = str;
                    }
                    else
                    {
                        if (c.Text != "-")
                        {
                            c.Text = c.Name;
                        }
                    }
                }

                if (c.HasChildren)
                {
                    LoadControlTranslations(c.Controls);
                }
            }
        }
    }
}
