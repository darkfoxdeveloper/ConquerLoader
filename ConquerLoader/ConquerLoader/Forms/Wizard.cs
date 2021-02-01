using CLCore.Models;
using System.IO;

namespace ConquerLoader.Forms
{
    public partial class Wizard : MetroFramework.Forms.MetroForm
    {
        public Wizard()
        {
            InitializeComponent();
        }

        private void Wizard_Load(object sender, System.EventArgs e)
        {
            string VersionDatFilename = Path.Combine(Directory.GetCurrentDirectory(), "Version.dat");
            string[] VersionDatLines = File.ReadAllLines(VersionDatFilename);
            int RealVersion = 0;
            foreach (string str in VersionDatLines)
            {
                int nVersion = int.Parse(str.Replace("#", ""));
                if (nVersion > 4000)
                {
                    RealVersion = nVersion;
                }
            }
            tbxVersion.Text = RealVersion.ToString();
            tbxConquerExe.Text = "Conquer.exe"; // Default Conquer Executable
            tbxLoginPort.Text = "9958";
            tbxGamePort.Text = "5816";
        }

        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            LoaderConfig lConfig = Core.GetLoaderConfig();
            lConfig.Servers.Add(new ServerConfiguration()
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
            Core.SaveLoaderConfig(lConfig);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Wizard_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            // Prevent the form from closing.
            e.Cancel = true;

            // Hide it instead.
            this.Hide();
        }
    }
}
