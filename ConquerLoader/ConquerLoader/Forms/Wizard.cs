using CLCore;
using CLCore.Models;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ConquerLoader
{
    public partial class Wizard : MetroFramework.Forms.MetroForm
    {
        private int _IndexSelectedServer = -1;
        private ServerConfiguration _SelectedServer = null;
        private LoaderConfig _LoaderConfig = null;
        public Wizard()
        {
            InitializeComponent();
            _LoaderConfig = Core.GetLoaderConfig();
        }
        public Wizard(int IndexSelectedServer)
        {
            InitializeComponent();
            _IndexSelectedServer = IndexSelectedServer;
            _LoaderConfig = Core.GetLoaderConfig();
        }

        private void Wizard_Load(object sender, System.EventArgs e)
        {
            if (_IndexSelectedServer >= 0)
            {
                _SelectedServer = _LoaderConfig.Servers[_IndexSelectedServer];
                Text = "Edit Server";
                btnAdd.Text = "Edit in List";
                tbxServerName.Text = _SelectedServer.ServerName;
                tbxIP.Text = _SelectedServer.LoginHost;
                tbxConquerExe.Text = _SelectedServer.ExecutableName;
                tbxLoginPort.Text = _SelectedServer.LoginPort.ToString();
                tbxGamePort.Text = _SelectedServer.GamePort.ToString();
                tbxGroupIcon.Text = _SelectedServer.FlashIcon;
                tbxVersion.Text = _SelectedServer.ServerVersion.ToString();
                tglUseDX9.Checked = _SelectedServer.UseDirectX9;
            } else
            {
                string VersionDatFilename = Path.Combine(Directory.GetCurrentDirectory(), "Version.dat");
                string[] VersionDatLines = new string[] { };
                if (File.Exists("Version.dat"))
                {
                    VersionDatLines = File.ReadAllLines(VersionDatFilename);
                }
                int RealVersion = 0;
                foreach (string str in VersionDatLines)
                {
                    if (!str.StartsWith("#"))
                    {
                        int nVersion = int.Parse(str);
                        if (nVersion > 4000)
                        {
                            RealVersion = nVersion;
                        }
                    }
                }
                tbxVersion.Text = RealVersion.ToString();
                tbxConquerExe.Text = "Conquer.exe"; // Default Conquer Executable
                tbxLoginPort.Text = "9958";
                tbxGamePort.Text = "5816";
            }
        }

        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            if (_IndexSelectedServer >= 0)
            {
                _SelectedServer.ServerName = tbxServerName.Text;
                _SelectedServer.LoginHost = tbxIP.Text;
                _SelectedServer.GameHost = tbxIP.Text;
                _SelectedServer.ServerVersion = uint.Parse(tbxVersion.Text);
                _SelectedServer.ExecutableName = tbxConquerExe.Text;
                _SelectedServer.LoginPort = uint.Parse(tbxLoginPort.Text);
                _SelectedServer.GamePort = uint.Parse(tbxGamePort.Text);
                _SelectedServer.FlashIcon = tbxGroupIcon.Text;
                _SelectedServer.UseDirectX9 = tglUseDX9.Checked;
            }
            else
            {
                _LoaderConfig.Servers.Add(new ServerConfiguration()
                {
                    GameHost = tbxIP.Text,
                    LoginHost = tbxIP.Text,
                    ExecutableName = tbxConquerExe.Text,
                    ServerName = tbxServerName.Text,
                    UseDirectX9 = tglUseDX9.Checked,
                    ServerVersion = uint.Parse(tbxVersion.Text),
                    LoginPort = 9958,
                    GamePort = 5816
                });
            }
            Core.SaveLoaderConfig(_LoaderConfig);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Wizard_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            // Prevent the form from closing.
            e.Cancel = true;

            // Hide it instead.
            this.Hide();
        }

        private void TbxVersion_TextChanged(object sender, System.EventArgs e)
        {
            bool isNumber = int.TryParse(tbxVersion.Text, out _);
            if (isNumber)
            {
                if (tbxVersion.Text.Length > 0 && int.Parse(tbxVersion.Text) >= 6000)
                {
                    tbxGamePort.Text = "5816";
                    tbxGroupIcon.Visible = true;
                    lblGroupIcon.Visible = true;
                    btnHelpGroupIcon.Visible = true;
                }
                else
                {
                    tbxGroupIcon.Visible = false;
                    lblGroupIcon.Visible = false;
                    btnHelpGroupIcon.Visible = false;
                }
            }
        }

        private void ValidateNumber_Leave(object sender, System.EventArgs e)
        {
            MetroFramework.Controls.MetroTextBox tbx = (MetroFramework.Controls.MetroTextBox)sender;
            bool isNumber = int.TryParse(tbx.Text, out int n);
            if (!isNumber)
            {
                tbx.Text = "0";
                if (tbx.Name == "tbxLoginPort")
                {
                    tbx.Text = "9958";
                }
                if (tbx.Name == "tbxGamePort")
                {
                    tbx.Text = "5816";
                }
            }
        }

        private void TbxIP_Leave(object sender, System.EventArgs e)
        {
            MetroFramework.Controls.MetroTextBox tbx = (MetroFramework.Controls.MetroTextBox)sender;
            bool validIP = IPAddress.TryParse(tbx.Text, out IPAddress IP);
            if (!validIP)
            {
                tbx.Text = "";
                lblHelpIP.Text = "Please, write a valid IP Address.";
            }
        }

        private void BtnHelpGroupIcon_Click(object sender, System.EventArgs e)
        {
            string p = Path.Combine(Constants.ClientPath, "data", "main", "flash");
            string AvailableGroupsNameInClient = "";
            if (Directory.Exists(p))
            {
                foreach (string s in Directory.GetDirectories(p, "Group*"))
                {
                    string dir = Path.GetFileName(s);
                    foreach(string f in Directory.GetFiles(s))
                    {
                        AvailableGroupsNameInClient += dir + "/" + Path.GetFileName(f) + System.Environment.NewLine;
                    }
                }
                MessageBox.Show(AvailableGroupsNameInClient, "Available Group Names", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Any found", "Not detected swf files!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
