using System.Collections.Generic;

namespace CLCore.Models
{
    public class ServerDatGroup
    {
        public string GroupName { get; set; }
        public string GroupIcon { get; set; }
        public virtual List<ServerConfiguration> Servers { get; set; }

        public ServerDatGroup()
        {
            if (Servers == null)
            {
                Servers = new List<ServerConfiguration>();
            }
        }

        override public string ToString()
        {
            return $"{GroupName}, Icon: {GroupIcon}";
        }
    }
}
