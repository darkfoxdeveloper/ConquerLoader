﻿using CLCore.Models;
using ConquerLoader.Forms;
using MetroFramework.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ConquerLoader
{
    public partial class Settings : MetroFramework.Forms.MetroForm
    {
        public LoaderConfig CurrentLoaderConfig = null;
        private Wizard WizardForm = null;
        private Wizard WizardEditForm = null;
        private Plugins PluginsForm = null;
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
                tglDisableAutoFixFlash.Checked = CurrentLoaderConfig.DisableAutoFixFlash;
                tglCLServer.Checked = CurrentLoaderConfig.CLServer;
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

        private void TglDisableAutoFixFlash_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLoaderConfig.DisableAutoFixFlash = (sender as MetroToggle).Checked;
        }

        private void TglCLServer_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLoaderConfig.CLServer = (sender as MetroToggle).Checked;
        }

        private void BtnWizard_Click(object sender, EventArgs e)
        {
            if (WizardForm == null)
            {
                WizardForm = new Wizard();
            }
            WizardForm.ShowDialog();
            CurrentLoaderConfig = Core.GetLoaderConfig();
            gridViewSettings.DataSource = CurrentLoaderConfig.Servers;
        }

        private void BtnPlugins_Click(object sender, EventArgs e)
        {
            if (PluginsForm == null)
            {
                PluginsForm = new Plugins();
            }
            PluginsForm.ShowDialog();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (gridViewSettings.CurrentRow != null)
            {
                if (WizardEditForm != null)
                {
                    WizardEditForm.Dispose();
                }
                if (Core.GetLoaderConfig().Servers.Count > gridViewSettings.CurrentRow.Index)
                {
                    WizardEditForm = new Wizard(gridViewSettings.CurrentRow.Index);
                    WizardEditForm.ShowDialog();
                    CurrentLoaderConfig = Core.GetLoaderConfig();
                    gridViewSettings.DataSource = CurrentLoaderConfig.Servers;
                }
            }
        }
    }
}
