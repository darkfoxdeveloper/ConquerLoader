namespace ConquerLoader.Models
{
    public class ServerConfiguration
    {
        public string ServerName { get; set; }
        public uint ServerVersion { get; set; }
        public string LoginHost { get; set; }
        public string GameHost { get; set; }
        public uint LoginPort { get; set; }
        public uint GamePort { get; set; }
        public string ExecutableName { get; set; }
        public bool EnableHostName { get; set; }
        public string Hostname { get; set; }
    }
}
