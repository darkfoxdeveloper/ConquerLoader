using System.Collections.Generic;
using System.ComponentModel;

namespace ConquerLoader.Models
{
    public class LoaderConfig
    {
        public BindingList<ServerConfiguration> Servers { get; set; }
        public ServerConfiguration DefaultServer { get; set; }
        public bool DebugMode { get; set; }
        public bool CloseOnFinish { get; set; }
        public bool HighResolution { get; set; }
        public bool FullScreen { get; set; }
        public string Title { get; set; }

        public LoaderConfig()
        {
            if (Servers == null)
            {
                Servers = new BindingList<ServerConfiguration>();
            }
        }
    }
}
