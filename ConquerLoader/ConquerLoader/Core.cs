using ConquerLoader.Models;
using System.IO;

namespace ConquerLoader
{
    public static class Core
    {
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
    }
}
