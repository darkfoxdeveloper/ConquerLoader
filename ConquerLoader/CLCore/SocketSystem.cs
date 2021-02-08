using SimpleTCP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace CLCore
{
    public class ConnectionEvent
    {
        public string IPAddress { get; set; }
        public System.Timers.ElapsedEventHandler Event { get; set; }
    }
    public class CLServer
    {
        public SimpleTcpServer TcpServer { get; set; }
        public List<ConnectionEvent> Connections { get; set; }
        public delegate void NotAllowedIP(string IPAddress);
        public event NotAllowedIP DetectedNotAllowedIP;
        private System.Timers.Timer WatcherTimer { get; set; }
        private Models.LogWritter LogWritter { get; set; }
        private string API_BASE_URL = "https://conquerloader.com/api/v1/167635d839c027b0c965cfa0f995dc43$";
        private string API_GET_CONNECTIONS = "";
        private string API_SET_CONNECTIONS = "";
        private string API_HAVE_CONNECTIONS = "";
        private string API_LICENSE_KEY = "";
        /// <summary>
        /// Launch CLServer
        /// </summary>
        /// <param name="LicenseKey"></param>
        public CLServer(string LicenseKey)
        {
            API_LICENSE_KEY = LicenseKey;
            Init();
            if (HaveConnectionsFromAPI())
            {
                GetConnectionsFromAPI();
            }
        }
        /// <summary>
        /// Launch and Start CLServer
        /// </summary>
        /// <param name="LicenseKey">License key of Loader</param>
        /// <param name="Port">Port for use</param>
        public CLServer(string LicenseKey, uint Port)
        {
            API_LICENSE_KEY = LicenseKey;
            Init();
            SetConnectionsToAPI(true);
            TcpServer = new SimpleTcpServer().Start((int)Port);
            DetectedNotAllowedIP += CLServer_DetectedNotAllowedIP;

            // Setup a socket server
            TcpServer.ClientConnected += Server_ClientConnected;
            TcpServer.ClientDisconnected += Server_ClientDisconnected;
            TcpServer.Delimiter = 0x13;
            //Server.TcpServer.DelimiterDataReceived += (sendr, msg) =>
            //{
            //    if (msg.MessageString.StartsWith("/"))
            //        {
            //            string[] parameters = msg.MessageString.TrimStart('/').Split(' ');
            //            switch (parameters[0])
            //            {
            //                case "autoclick_detected":
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //        }
            //};
        }
        private void Init()
        {
            Constants.LicenseKey = API_LICENSE_KEY;
            API_HAVE_CONNECTIONS = $"{API_BASE_URL}/HaveConnections/{Constants.LicenseKey}";
            API_GET_CONNECTIONS = $"{API_BASE_URL}/GetConnections/{Constants.LicenseKey}";
            API_SET_CONNECTIONS = $"{API_BASE_URL}/SetConnections/{Constants.LicenseKey}";
            Connections = new List<ConnectionEvent>();
            LogWritter = new Models.LogWritter("CLServer.log");
        }

        private bool HaveConnectionsFromAPI()
        {
            return HttpRequests.GET(API_HAVE_CONNECTIONS).IsSuccessStatusCode;
        }

        private List<ConnectionEvent> GetConnectionsFromAPI()
        {
            string result = HttpRequests.GET(API_GET_CONNECTIONS).Content.ReadAsStringAsync().Result;
            List<string> IPList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result);
            foreach(string IP in IPList)
            {
                Connections.Add(new ConnectionEvent() { IPAddress = IP });
            }
            return Connections;
        }

        private bool SetConnectionsToAPI(bool Clear = false)
        {
            List<string> IPs = new List<string>();
            if (!Clear)
            {
                foreach (ConnectionEvent ce in Connections)
                {
                    IPs.Add(ce.IPAddress);
                }
            }
            LogWritter.Write(string.Format("IPs for API: {0}", IPs.Count));
            return HttpRequests.POST(API_SET_CONNECTIONS, Newtonsoft.Json.JsonConvert.SerializeObject(IPs, Newtonsoft.Json.Formatting.Indented)).IsSuccessStatusCode;
        }

        private void CLServer_DetectedNotAllowedIP(string IPAddress)
        {
            LogWritter.Write(string.Format("Detected a IP {0} not connected to CLServer. Warning!", IPAddress));
        }

        private void Server_ClientConnected(object sender, TcpClient e)
        {
            string ClientAddressIP = ((IPEndPoint)e.Client.RemoteEndPoint).Address.ToString();
            if (Connections.Where(x => x.IPAddress == ClientAddressIP).Count() <= 0)
            {
                Connections.Add(new ConnectionEvent() { IPAddress = ClientAddressIP });
            }
            SetConnectionsToAPI();
            LogWritter.Write(string.Format("{0} Connections sended to API", Connections.Count()));
        }
        private void Server_ClientDisconnected(object sender, TcpClient e)
        {
            string ClientAddressIP = ((IPEndPoint)e.Client.RemoteEndPoint).Address.ToString();
            ConnectionEvent cEv = Connections.Where(x => x.IPAddress.Equals(ClientAddressIP)).FirstOrDefault();
            if (cEv != null)
            {
                Connections.Remove(cEv);
            }
            SetConnectionsToAPI();
            LogWritter.Write(string.Format("{0} Connections sended to API", Connections.Count()));
        }
        public bool CheckConnectionByIP(string IPAddress)
        {
            if (HaveConnectionsFromAPI())
            {
                GetConnectionsFromAPI();
            }
            LogWritter.Write(string.Format("{0} Connections <= DEBUG", Connections.Count()));
            return Connections.Where(x => x.IPAddress == IPAddress).Count() > 0;
        }
        public ConnectionEvent GetConnectionEventByIP(string IPAddress)
        {
            return Connections.Where(x => x.IPAddress == IPAddress).FirstOrDefault();
        }
        public void WatchClientIP(string IPAddress)
        {
            if (WatcherTimer == null)
            {
                WatcherTimer = new System.Timers.Timer
                {
                    Interval = 10000,
                    Enabled = true
                };
            }
            ConnectionEvent cEv = GetConnectionEventByIP(IPAddress);
            System.Timers.ElapsedEventHandler eventForHandle = new System.Timers.ElapsedEventHandler((s, e) => WatchClientIP_Watcher(IPAddress));
            if (eventForHandle != null && cEv != null)
            {
                cEv.Event = eventForHandle;
                WatcherTimer.Elapsed += eventForHandle;
            }
        }
        private void UnwatchClientIP(string IPAddress)
        {
            if (WatcherTimer != null)
            {
                ConnectionEvent cEv = GetConnectionEventByIP(IPAddress);
                Connections.Remove(cEv);
                WatcherTimer.Elapsed -= cEv.Event;
            }
        }
        private void WatchClientIP_Watcher(string IPAddress)
        {
            bool Connected = CheckConnectionByIP(IPAddress);
            LogWritter.Write(string.Format("Valid Connection: {0}", Connected));
            if (!Connected)
            {
                DetectedNotAllowedIP(IPAddress);
                UnwatchClientIP(IPAddress);
            }
        }
        private void SyncroConnections()
        {
            GetConnectionsFromAPI();
        }
    }
    public class CLClient
    {
        public SimpleTcpClient Client = null;
        public CLClient(string IP, uint Port)
        {
            Client = new SimpleTcpClient();
            Client.Connect(IP, (int)Port);
        }

        public void Send(string content, uint seconds = 3)
        {
            Client.WriteLineAndGetReply(content, TimeSpan.FromSeconds(seconds));
        }

        public Message SendAndGetMessage(string content, uint seconds = 3)
        {
            return Client.WriteLineAndGetReply(content, TimeSpan.FromSeconds(seconds));
        }

        public void Disconnect()
        {
            Client.Disconnect();
            Client.Dispose();
        }
    }

    public static class SocketSystem
    {
        public static CLClient CurrentSocketClient = null;
    }
}
