using CLCore.Models;
using ConquerLoader.Forms;
using MetroFramework.Controls;
using System;
using System.IO;
using System.Windows.Forms;

namespace ConquerLoader
{
    public partial class Settings : MetroFramework.Forms.MetroForm
    {
        public LoaderConfig CurrentLoaderConfig = null;
        private Wizard WizardForm = null;
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
                tglHighResolution.Checked = CurrentLoaderConfig.HighResolution;
                tglFullscreen.Checked = CurrentLoaderConfig.FullScreen;
                tglServerNameChange.Checked = CurrentLoaderConfig.ServernameChange;
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

        private void TglHighResolution_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLoaderConfig.HighResolution = (sender as MetroToggle).Checked;
        }

        private void TglFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLoaderConfig.FullScreen = (sender as MetroToggle).Checked;
        }

        private void TglServerNameChange_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLoaderConfig.ServernameChange = (sender as MetroToggle).Checked;
        }

        private void BtnAdvancedMode_Click(object sender, EventArgs e)
        {
            if (gridViewSettings.ReadOnly)
            {
                gridViewSettings.ReadOnly = false;
                btnAdvancedMode.Text = "Simple";
                MetroFramework.MetroMessageBox.Show(this, $"Now can edit in the grid directly! This is a feature for Advanced Users.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                gridViewSettings.ReadOnly = true;
                btnAdvancedMode.Text = "Advanced";
            }
        }

        private void BtnWizard_Click(object sender, EventArgs e)
        {
            // TODO new wizard mode
            if (WizardForm == null)
            {
                WizardForm = new Wizard();
            }
            DialogResult dRes = WizardForm.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                CurrentLoaderConfig = Core.GetLoaderConfig();
                gridViewSettings.DataSource = CurrentLoaderConfig.Servers;
            }
        }
    }
}
