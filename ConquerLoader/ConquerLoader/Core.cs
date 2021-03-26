﻿using CLCore;
using CLCore.Models;
using ConquerLoader.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ConquerLoader
{
    public static class Core
    {
        public static LogWritter LogWritter = new LogWritter("conquerloader.log");
        public static string ConfigJsonPath = "config.json";
        public static bool UseEncryptedConfig = false;
        public static List<TextTranslation> TextTranslations = new List<TextTranslation>();
        public static LoaderConfig GetLoaderConfig()
        {
            LoaderConfig lConfig = null;
            if (File.Exists(ConfigJsonPath + ".lock"))
            {
                UseEncryptedConfig = true;
                lConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<LoaderConfig>(ConfigFilesEncryption.AESEncription.DecryptString(Constants.LockConfigurationKey, File.ReadAllText(ConfigJsonPath + ".lock")));
            } else
            {
                if (File.Exists(ConfigJsonPath))
                {
                    lConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<LoaderConfig>(File.ReadAllText(ConfigJsonPath));
                }
            }
            if (lConfig != null)
            {
                DetectLang(lConfig);
            }
            return lConfig;
        }
        public static void DetectLang(LoaderConfig lConfig)
        {
            if (lConfig.Lang != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(lConfig.Lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lConfig.Lang);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
            switch (Thread.CurrentThread.CurrentCulture.Name)
            {
                case "es":
                    {
                        TextTranslations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TextTranslation>>(Encoding.UTF8.GetString(Properties.Resources.lang_es));
                        break;
                    }
                case "en":
                    {
                        TextTranslations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TextTranslation>>(Encoding.UTF8.GetString(Properties.Resources.lang_en));
                        break;
                    }
                case "pt":
                    {
                        TextTranslations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TextTranslation>>(Encoding.UTF8.GetString(Properties.Resources.lang_pt));
                        break;
                    }
            }
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
            if (UseEncryptedConfig)
            {
                File.WriteAllText(ConfigJsonPath + ".lock", ConfigFilesEncryption.AESEncription.EncryptString(Constants.LockConfigurationKey, Newtonsoft.Json.JsonConvert.SerializeObject(LoaderConfig, Newtonsoft.Json.Formatting.Indented)));
            } else
            {
                File.WriteAllText(ConfigJsonPath, Newtonsoft.Json.JsonConvert.SerializeObject(LoaderConfig, Newtonsoft.Json.Formatting.Indented));
            }
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
                    TextTranslation str = TextTranslations.Where(x => x.Id == c.Name).FirstOrDefault();
                    if (str != null && str.Text.Length > 0)
                    {
                        c.Text = str.Text;
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
