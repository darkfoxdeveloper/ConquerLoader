using ConquerLoader.CLCore;
using System;
using System.Windows.Forms;

namespace CLServer
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public SocketServer Server = null;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Resizable = false;

            // Setup a socket server
            SocketServer Server = new SocketServer(5000);
            Server.Server.ClientConnected += Server_ClientConnected;
            Server.Server.ClientDisconnected += Server_ClientDisconnected;
            Server.Server.Delimiter = 0x13;
            Server.Server.DelimiterDataReceived += (sendr, msg) =>
            {
                // Implement some actions for commands /autoclick_detected
                if (msg.MessageString.StartsWith("/"))
                {
                    string[] parameters = msg.MessageString.TrimStart('/').Split(' ');
                    switch (parameters[0])
                    {
                        case "autoclick_detected":
                            Invoke(new MethodInvoker(() =>
                            {
                                logBox.AppendText($"Autoclick detected in client: {msg.TcpClient.Client.GetHashCode()}" + Environment.NewLine);
                            }));
                            break;
                        default:
                            Invoke(new MethodInvoker(() =>
                            {
                                logBox.AppendText($"Command not found sended from client: {msg.MessageString}" + Environment.NewLine);
                            }));
                            break;
                    }
                } else
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        logBox.AppendText($"Client talk: {msg.MessageString}" + Environment.NewLine);
                    }));
                }
            };
        }

        private void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {
            Invoke(new MethodInvoker(() =>
            {
                logBox.AppendText($"Client {e.Client.GetHashCode()} disconnected." + Environment.NewLine);
            }));
        }

        private void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
            Invoke(new MethodInvoker(() =>
            {
                logBox.AppendText($"Client {e.Client.GetHashCode()} connected." + Environment.NewLine);
            }));
        }
    }
}
