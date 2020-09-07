using System.Collections.Generic;

namespace ConquerLoader.Models
{
    public class LoaderConfig
    {
        public List<ServerConfiguration> Servers { get; set; }
        public ServerConfiguration DefaultServer { get; set; }
        public bool DebugMode { get; set; }

        public LoaderConfig()
        {
            if (Servers == null)
            {
                Servers = new List<ServerConfiguration>();
            }
        }
    }
}
