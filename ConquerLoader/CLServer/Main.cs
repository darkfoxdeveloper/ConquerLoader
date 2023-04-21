using CLCore;
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
            CLServerConfig.APIBaseUri = "https://localhost:7198/api";
            _CLServer = new CLCore.CLServer("2d32a164-fcd5-486b-b43f-005a78274caf", true);
            _CLServer.DetectedNotAllowedIP += CLServer_DetectedNotAllowedIP;
            _CLServer.TcpServer.ClientConnected += Server_ClientConnected;
            _CLServer.TcpServer.ClientDisconnected += Server_ClientDisconnected;
            Invoke(new MethodInvoker(() =>
            {
                logBox.AppendText(string.Format($"CLServer is started in Port {CLServerConfig.ServerPort}") + Environment.NewLine);
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
            QuestionBox qBox = new QuestionBox("Check connection by IP", "Specify the IPAddress for check if are online");
            if (qBox.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(_CLServer.CheckConnectionByIP(qBox.Answer) ? $"{qBox.Answer} Is Connected" : $"{qBox.Answer} Is NOT Connected");
            }
        }

        private void BtnConnections_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_CLServer.Connections.Count+"");
        }

        private void BtnDummyConnections_Click(object sender, EventArgs e)
        {
            _CLServer.Connections.Add(new CLCore.ConnectionEvent() { IPAddress = "127.0.0.1" });
            _CLServer.Connections.Add(new CLCore.ConnectionEvent() { IPAddress = "192.168.1.1" });
            if (!_CLServer.Demo())
            {
                MessageBox.Show("API Not available.");
            }
        }
    }
}
