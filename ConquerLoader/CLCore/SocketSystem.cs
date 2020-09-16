using SimpleTCP;
using System;

namespace ConquerLoader.CLCore
{
    public class SocketServer
    {
        public SimpleTcpServer Server = null;
        public SocketServer(uint Port)
        {
            Server = new SimpleTcpServer().Start((int)Port);
        }
    }
    public class SocketClient
    {
        public SimpleTcpClient Client = null;
        public SocketClient(string IP, uint Port)
        {
            Client = new SimpleTcpClient().Connect(IP, (int)Port);
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
}
