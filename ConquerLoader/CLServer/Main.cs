using System;
using System.Windows.Forms;

namespace CLServer
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public CLCore.CLServer _CLServer = null;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Resizable = false;

            // Setup a socket server
            _CLServer = new CLCore.CLServer("0967ddad-1f18-44cf-9ed6-26242b8595ed", 8000);
            _CLServer.DetectedNotAllowedIP += CLServer_DetectedNotAllowedIP;
            _CLServer.TcpServer.ClientConnected += Server_ClientConnected;
            _CLServer.TcpServer.ClientDisconnected += Server_ClientDisconnected;
            Invoke(new MethodInvoker(() =>
            {
                logBox.AppendText(string.Format("CLServer is started in Port 8000") + Environment.NewLine);
            }));
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

        private void CLServer_DetectedNotAllowedIP(string IPAddress)
        {
            Invoke(new MethodInvoker(() =>
            {
                logBox.AppendText(string.Format("Detected a IP {0} not connected to CLServer. Warning!", IPAddress) + Environment.NewLine);
            }));
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            CLCore.CLServer sTest = new CLCore.CLServer("SKSK-B6FC-JFOQ-7DTQ");
            MessageBox.Show(sTest.CheckConnectionByIP("127.0.0.1")+"");
        }

        private void BtnConnections_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_CLServer.Connections.Count+"");
        }
    }
}
