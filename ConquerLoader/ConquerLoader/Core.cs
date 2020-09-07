using ConquerLoader.Models;
using System;
using System.IO;
using System.Net.Sockets;

namespace ConquerLoader
{
    public static class Core
    {
        public static DebugWriter DebugWritter = new DebugWriter("conquerloader.log");
        public static LoaderConfig GetLoaderConfig()
        {
            LoaderConfig lConfig = null;
            if (File.Exists("config.json"))
            {
                lConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<LoaderConfig>(File.ReadAllText("config.json"));
            }
            return lConfig;
        }
        public static void SaveLoaderConfig(LoaderConfig LoaderConfig)
        {
            File.WriteAllText("config.json", Newtonsoft.Json.JsonConvert.SerializeObject(LoaderConfig, Newtonsoft.Json.Formatting.Indented));
        }
        public static bool ServerAvailable(string Host, uint Port)
        {
            bool Online = false;
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect(Host, (int)Port);
                    Online = true;
                }
                catch (Exception)
                {
                    Online = false;
                }
            }
            return Online;
        }
    }
}
