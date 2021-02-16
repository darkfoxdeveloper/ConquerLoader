namespace ConquerLoader
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.tglDebugMode = new MetroFramework.Controls.MetroToggle();
            this.tglCloseOnFinish = new MetroFramework.Controls.MetroToggle();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.lblDisableAutoFixFlash = new MetroFramework.Controls.MetroLabel();
            this.tglDisableAutoFixFlash = new MetroFramework.Controls.MetroToggle();
            this.btnPlugins = new MetroFramework.Controls.MetroButton();
            this.lblCLServer = new MetroFramework.Controls.MetroLabel();
            this.tglCLServer = new MetroFramework.Controls.MetroToggle();
            this.btnWizard = new MetroFramework.Controls.MetroButton();
            this.btnEdit = new MetroFramework.Controls.MetroButton();
            this.lblServerNameChange = new MetroFramework.Controls.MetroLabel();
            this.tglServerNameChange = new MetroFramework.Controls.MetroToggle();
            this.lblFullscreen = new MetroFramework.Controls.MetroLabel();
            this.tglFullscreen = new MetroFramework.Controls.MetroToggle();
            this.lblHighResolution = new MetroFramework.Controls.MetroLabel();
            this.tglHighResolution = new MetroFramework.Controls.MetroToggle();
            this.gridViewSettings = new System.Windows.Forms.DataGridView();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.lblTitle = new MetroFramework.Controls.MetroLabel();
            this.tbxTitle = new MetroFramework.Controls.MetroTextBox();
            this.lblCloseOnFinish = new MetroFramework.Controls.MetroLabel();
            this.lblDebugMode = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // tglDebugMode
            // 
            this.tglDebugMode.AutoSize = true;
            this.tglDebugMode.Location = new System.Drawing.Point(20, 41);
            this.tglDebugMode.Margin = new System.Windows.Forms.Padding(2);
            this.tglDebugMode.Name = "tglDebugMode";
            this.tglDebugMode.Size = new System.Drawing.Size(80, 17);
            this.tglDebugMode.TabIndex = 0;
            this.tglDebugMode.Text = "Off";
            this.tglDebugMode.UseSelectable = true;
            this.tglDebugMode.CheckedChanged += new System.EventHandler(this.TglDebugMode_CheckedChanged);
            // 
            // tglCloseOnFinish
            // 
            this.tglCloseOnFinish.AutoSize = true;
            this.tglCloseOnFinish.Location = new System.Drawing.Point(147, 41);
            this.tglCloseOnFinish.Margin = new System.Windows.Forms.Padding(2);
            this.tglCloseOnFinish.Name = "tglCloseOnFinish";
            this.tglCloseOnFinish.Size = new System.Drawing.Size(80, 17);
            this.tglCloseOnFinish.TabIndex = 1;
            this.tglCloseOnFinish.Text = "Off";
            this.tglCloseOnFinish.UseSelectable = true;
            this.tglCloseOnFinish.CheckedChanged += new System.EventHandler(this.TglCloseOnFinish_CheckedChanged);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.lblDisableAutoFixFlash);
            this.metroPanel1.Controls.Add(this.tglDisableAutoFixFlash);
            this.metroPanel1.Controls.Add(this.btnPlugins);
            this.metroPanel1.Controls.Add(this.lblCLServer);
            this.metroPanel1.Controls.Add(this.tglCLServer);
            this.metroPanel1.Controls.Add(this.btnWizard);
            this.metroPanel1.Controls.Add(this.btnEdit);
            this.metroPanel1.Controls.Add(this.lblServerNameChange);
            this.metroPanel1.Controls.Add(this.tglServerNameChange);
            this.metroPanel1.Controls.Add(this.lblFullscreen);
            this.metroPanel1.Controls.Add(this.tglFullscreen);
            this.metroPanel1.Controls.Add(this.lblHighResolution);
            this.metroPanel1.Controls.Add(this.tglHighResolution);
            this.metroPanel1.Controls.Add(this.gridViewSettings);
            this.metroPanel1.Controls.Add(this.btnSave);
            this.metroPanel1.Controls.Add(this.lblTitle);
            this.metroPanel1.Controls.Add(this.tbxTitle);
            this.metroPanel1.Controls.Add(this.lblCloseOnFinish);
            this.metroPanel1.Controls.Add(this.lblDebugMode);
            this.metroPanel1.Controls.Add(this.tglCloseOnFinish);
            this.metroPanel1.Controls.Add(this.tglDebugMode);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(16, 69);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1044, 402);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // lblDisableAutoFixFlash
            // 
            this.lblDisableAutoFixFlash.AutoSize = true;
            this.lblDisableAutoFixFlash.Location = new System.Drawing.Point(714, 19);
            this.lblDisableAutoFixFlash.Name = "lblDisableAutoFixFlash";
            this.lblDisableAutoFixFlash.Size = new System.Drawing.Size(132, 19);
            this.lblDisableAutoFixFlash.TabIndex = 21;
            this.lblDisableAutoFixFlash.Text = "Disable AutoFix Flash";
            // 
            // tglDisableAutoFixFlash
            // 
            this.tglDisableAutoFixFlash.AutoSize = true;
            this.tglDisableAutoFixFlash.Location = new System.Drawing.Point(714, 41);
            this.tglDisableAutoFixFlash.Margin = new System.Windows.Forms.Padding(2);
            this.tglDisableAutoFixFlash.Name = "tglDisableAutoFixFlash";
            this.tglDisableAutoFixFlash.Size = new System.Drawing.Size(80, 17);
            this.tglDisableAutoFixFlash.TabIndex = 20;
            this.tglDisableAutoFixFlash.Text = "Off";
            this.tglDisableAutoFixFlash.UseSelectable = true;
            this.tglDisableAutoFixFlash.CheckedChanged += new System.EventHandler(this.TglDisableAutoFixFlash_CheckedChanged);
            // 
            // btnPlugins
            // 
            this.btnPlugins.Location = new System.Drawing.Point(714, 99);
            this.btnPlugins.Name = "btnPlugins";
            this.btnPlugins.Size = new System.Drawing.Size(75, 30);
            this.btnPlugins.TabIndex = 19;
            this.btnPlugins.Text = "Plugins";
            this.btnPlugins.UseSelectable = true;
            this.btnPlugins.Click += new System.EventHandler(this.BtnPlugins_Click);
            // 
            // lblCLServer
            // 
            this.lblCLServer.AutoSize = true;
            this.lblCLServer.Location = new System.Drawing.Point(881, 19);
            this.lblCLServer.Name = "lblCLServer";
            this.lblCLServer.Size = new System.Drawing.Size(150, 19);
            this.lblCLServer.TabIndex = 18;
            this.lblCLServer.Text = "CLServer (Experimental)";
            this.lblCLServer.Visible = false;
            // 
            // tglCLServer
            // 
            this.tglCLServer.AutoSize = true;
            this.tglCLServer.Location = new System.Drawing.Point(881, 41);
            this.tglCLServer.Margin = new System.Windows.Forms.Padding(2);
            this.tglCLServer.Name = "tglCLServer";
            this.tglCLServer.Size = new System.Drawing.Size(80, 17);
            this.tglCLServer.TabIndex = 17;
            this.tglCLServer.Text = "Off";
            this.tglCLServer.UseSelectable = true;
            this.tglCLServer.Visible = false;
            this.tglCLServer.CheckedChanged += new System.EventHandler(this.TglCLServer_CheckedChanged);
            // 
            // btnWizard
            // 
            this.btnWizard.Location = new System.Drawing.Point(875, 99);
            this.btnWizard.Name = "btnWizard";
            this.btnWizard.Size = new System.Drawing.Size(75, 30);
            this.btnWizard.TabIndex = 16;
            this.btnWizard.Text = "New +";
            this.btnWizard.UseSelectable = true;
            this.btnWizard.Click += new System.EventHandler(this.BtnWizard_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(794, 99);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 30);
            this.btnEdit.TabIndex = 15;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseSelectable = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // lblServerNameChange
            // 
            this.lblServerNameChange.AutoSize = true;
            this.lblServerNameChange.Location = new System.Drawing.Point(543, 19);
            this.lblServerNameChange.Name = "lblServerNameChange";
            this.lblServerNameChange.Size = new System.Drawing.Size(132, 19);
            this.lblServerNameChange.TabIndex = 14;
            this.lblServerNameChange.Text = "ServerName Change";
            // 
            // tglServerNameChange
            // 
            this.tglServerNameChange.AutoSize = true;
            this.tglServerNameChange.Location = new System.Drawing.Point(543, 41);
            this.tglServerNameChange.Margin = new System.Windows.Forms.Padding(2);
            this.tglServerNameChange.Name = "tglServerNameChange";
            this.tglServerNameChange.Size = new System.Drawing.Size(80, 17);
            this.tglServerNameChange.TabIndex = 13;
            this.tglServerNameChange.Text = "Off";
            this.tglServerNameChange.UseSelectable = true;
            this.tglServerNameChange.CheckedChanged += new System.EventHandler(this.TglServerNameChange_CheckedChanged);
            // 
            // lblFullscreen
            // 
            this.lblFullscreen.AutoSize = true;
            this.lblFullscreen.Location = new System.Drawing.Point(428, 19);
            this.lblFullscreen.Name = "lblFullscreen";
            this.lblFullscreen.Size = new System.Drawing.Size(68, 19);
            this.lblFullscreen.TabIndex = 12;
            this.lblFullscreen.Text = "FullScreen";
            // 
            // tglFullscreen
            // 
            this.tglFullscreen.AutoSize = true;
            this.tglFullscreen.Location = new System.Drawing.Point(420, 41);
            this.tglFullscreen.Margin = new System.Windows.Forms.Padding(2);
            this.tglFullscreen.Name = "tglFullscreen";
            this.tglFullscreen.Size = new System.Drawing.Size(80, 17);
            this.tglFullscreen.TabIndex = 11;
            this.tglFullscreen.Text = "Off";
            this.tglFullscreen.UseSelectable = true;
            this.tglFullscreen.CheckedChanged += new System.EventHandler(this.TglFullscreen_CheckedChanged);
            // 
            // lblHighResolution
            // 
            this.lblHighResolution.AutoSize = true;
            this.lblHighResolution.Location = new System.Drawing.Point(273, 19);
            this.lblHighResolution.Name = "lblHighResolution";
            this.lblHighResolution.Size = new System.Drawing.Size(139, 19);
            this.lblHighResolution.TabIndex = 10;
            this.lblHighResolution.Text = "High Resolution Mode";
            // 
            // tglHighResolution
            // 
            this.tglHighResolution.AutoSize = true;
            this.tglHighResolution.Location = new System.Drawing.Point(285, 41);
            this.tglHighResolution.Margin = new System.Windows.Forms.Padding(2);
            this.tglHighResolution.Name = "tglHighResolution";
            this.tglHighResolution.Size = new System.Drawing.Size(80, 17);
            this.tglHighResolution.TabIndex = 9;
            this.tglHighResolution.Text = "Off";
            this.tglHighResolution.UseSelectable = true;
            this.tglHighResolution.CheckedChanged += new System.EventHandler(this.TglHighResolution_CheckedChanged);
            // 
            // gridViewSettings
            // 
            this.gridViewSettings.AllowUserToOrderColumns = true;
            this.gridViewSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewSettings.Location = new System.Drawing.Point(20, 141);
            this.gridViewSettings.Name = "gridViewSettings";
            this.gridViewSettings.RowHeadersWidth = 62;
            this.gridViewSettings.Size = new System.Drawing.Size(1011, 244);
            this.gridViewSettings.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(956, 99);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseSelectable = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(21, 99);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(92, 19);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Title in Loader";
            // 
            // tbxTitle
            // 
            // 
            // 
            // 
            this.tbxTitle.CustomButton.Image = null;
            this.tbxTitle.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.tbxTitle.CustomButton.Name = "";
            this.tbxTitle.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbxTitle.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbxTitle.CustomButton.TabIndex = 1;
            this.tbxTitle.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbxTitle.CustomButton.UseSelectable = true;
            this.tbxTitle.CustomButton.Visible = false;
            this.tbxTitle.Lines = new string[0];
            this.tbxTitle.Location = new System.Drawing.Point(129, 99);
            this.tbxTitle.MaxLength = 32767;
            this.tbxTitle.Name = "tbxTitle";
            this.tbxTitle.PasswordChar = '\0';
            this.tbxTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxTitle.SelectedText = "";
            this.tbxTitle.SelectionLength = 0;
            this.tbxTitle.SelectionStart = 0;
            this.tbxTitle.ShortcutsEnabled = true;
            this.tbxTitle.Size = new System.Drawing.Size(191, 23);
            this.tbxTitle.TabIndex = 5;
            this.tbxTitle.UseSelectable = true;
            this.tbxTitle.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbxTitle.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbxTitle.TextChanged += new System.EventHandler(this.TbxTitle_TextChanged);
            // 
            // lblCloseOnFinish
            // 
            this.lblCloseOnFinish.AutoSize = true;
            this.lblCloseOnFinish.Location = new System.Drawing.Point(147, 19);
            this.lblCloseOnFinish.Name = "lblCloseOnFinish";
            this.lblCloseOnFinish.Size = new System.Drawing.Size(96, 19);
            this.lblCloseOnFinish.TabIndex = 4;
            this.lblCloseOnFinish.Text = "Close on Finish";
            // 
            // lblDebugMode
            // 
            this.lblDebugMode.AutoSize = true;
            this.lblDebugMode.Location = new System.Drawing.Point(20, 19);
            this.lblDebugMode.Name = "lblDebugMode";
            this.lblDebugMode.Size = new System.Drawing.Size(87, 19);
            this.lblDebugMode.TabIndex = 3;
            this.lblDebugMode.Text = "Debug Mode";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 487);
            this.Controls.Add(this.metroPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(13, 60, 13, 13);
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroToggle tglDebugMode;
        private MetroFramework.Controls.MetroToggle tglCloseOnFinish;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel lblDebugMode;
        private MetroFramework.Controls.MetroLabel lblCloseOnFinish;
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroTextBox tbxTitle;
        private MetroFramework.Controls.MetroButton btnSave;
        private System.Windows.Forms.DataGridView gridViewSettings;
        private MetroFramework.Controls.MetroLabel lblHighResolution;
        private MetroFramework.Controls.MetroToggle tglHighResolution;
        private MetroFramework.Controls.MetroLabel lblFullscreen;
        private MetroFramework.Controls.MetroToggle tglFullscreen;
        private MetroFramework.Controls.MetroLabel lblServerNameChange;
        private MetroFramework.Controls.MetroToggle tglServerNameChange;
        private MetroFramework.Controls.MetroButton btnEdit;
        private MetroFramework.Controls.MetroButton btnWizard;
        private MetroFramework.Controls.MetroLabel lblCLServer;
        private MetroFramework.Controls.MetroToggle tglCLServer;
        private MetroFramework.Controls.MetroButton btnPlugins;
        private MetroFramework.Controls.MetroLabel lblDisableAutoFixFlash;
        private MetroFramework.Controls.MetroToggle tglDisableAutoFixFlash;
    }
}