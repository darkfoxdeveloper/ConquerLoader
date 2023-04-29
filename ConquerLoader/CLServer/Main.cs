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
                if (qBox.Text.Length > 0 && uint.TryParse(qBox.Text, out uint IP))
                {
                    MessageBox.Show(_CLServer.CheckConnectionByIP(IP.ToString()) ? $"{IP} Is Connected" : $"{IP} Is NOT Connected");
                } else
                {
                    MessageBox.Show("No specified valid IP", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void logBox_TextChanged(object sender, EventArgs e)
        {
            lblNumberConnections.Text = $"{_CLServer.Connections.Count} connections";
        }
    }
}
