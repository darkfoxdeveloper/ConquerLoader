using ConquerLoader.Models;
using MetroFramework.Controls;
using System;
using System.IO;
using System.Windows.Forms;

namespace ConquerLoader
{
    public partial class Settings : MetroFramework.Forms.MetroForm
    {
        public LoaderConfig CurrentLoaderConfig = null;
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Resizable = false;
            if (!File.Exists(Core.ConfigJsonPath))
            {
                LoaderConfig lc = new LoaderConfig();
                Core.SaveLoaderConfig(lc);
                MetroFramework.MetroMessageBox.Show(this, "Cannot load config.json. Creating one... Restart App!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            } else
            {
                CurrentLoaderConfig = Core.GetLoaderConfig();
                tglDebugMode.Checked = CurrentLoaderConfig.DebugMode;
                tglCloseOnFinish.Checked = CurrentLoaderConfig.CloseOnFinish;
                tbxTitle.Text = CurrentLoaderConfig.Title;
                gridViewSettings.DataSource = CurrentLoaderConfig.Servers;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Core.SaveLoaderConfig(CurrentLoaderConfig);
            this.Close();
        }

        private void TglDebugMode_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLoaderConfig.DebugMode = (sender as MetroToggle).Checked;
        }

        private void TglCloseOnFinish_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLoaderConfig.CloseOnFinish = (sender as MetroToggle).Checked;
        }

        private void TbxTitle_TextChanged(object sender, EventArgs e)
        {
            CurrentLoaderConfig.Title = (sender as MetroTextBox).Text;
        }
    }
}
