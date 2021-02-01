using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace CLCore
{
    public class CLServer
    {
        private SimpleTcpServer TcpServer { get; set; }
        public List<TcpClient> Connections { get; set; }
        public delegate void NotAllowedIP(string IPAddress);
        public event NotAllowedIP DetectedNotAllowedIP;
        public CLServer(uint Port)
        {
            TcpServer = new SimpleTcpServer().Start((int)Port);
            Connections = new List<TcpClient>();
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
            ConsoleColor bColor = Console.BackgroundColor;
            ConsoleColor fColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Format("[CLServer] is started in Port {0}.", Port));
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor;
        }

        private void CLServer_DetectedNotAllowedIP(string IPAddress)
        {
            ConsoleColor bColor = Console.BackgroundColor;
            ConsoleColor fColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("Detected a IP {0} not connected to CLServer. Warning!", IPAddress));
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor;
        }

        private void Server_ClientConnected(object sender, TcpClient e)
        {
            Connections.Add(e);
            Console.WriteLine(e.Client.RemoteEndPoint.ToString() + " has connected!");
        }
        private void Server_ClientDisconnected(object sender, TcpClient e)
        {
            Connections.Remove(e);
            Console.WriteLine(e.Client.RemoteEndPoint.ToString() + " has disconnected!");
        }
        public bool CheckConnectionByIP(string IPAddress)
        {
            return Connections.Where(x => ((IPEndPoint)x.Client.RemoteEndPoint).Address.ToString() == IPAddress).Count() > 0;
        }
        public void WatchClientIP(string IPAddress)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += (sender, e) => WatchClientIP_Watcher(IPAddress);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }
        private void WatchClientIP_Watcher(string IPAddress)
        {
            bool Connected = Connections.Where(x => ((IPEndPoint)x.Client.RemoteEndPoint).Address.ToString() == IPAddress).Count() > 0;
            if (!Connected)
            {
                DetectedNotAllowedIP(IPAddress);
            }
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
    }

    public static class SocketSystem
    {
        public static CLClient CurrentSocketClient = null;
    }
}
