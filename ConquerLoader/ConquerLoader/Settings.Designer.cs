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
            this.tglDebugMode = new MetroFramework.Controls.MetroToggle();
            this.tglCloseOnFinish = new MetroFramework.Controls.MetroToggle();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.lblTitle = new MetroFramework.Controls.MetroLabel();
            this.tbxTitle = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lblDebugMode = new MetroFramework.Controls.MetroLabel();
            this.gridViewSettings = new System.Windows.Forms.DataGridView();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // tglDebugMode
            // 
            this.tglDebugMode.AutoSize = true;
            this.tglDebugMode.Location = new System.Drawing.Point(112, 19);
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
            this.tglCloseOnFinish.Location = new System.Drawing.Point(405, 19);
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
            this.metroPanel1.Controls.Add(this.gridViewSettings);
            this.metroPanel1.Controls.Add(this.btnSave);
            this.metroPanel1.Controls.Add(this.lblTitle);
            this.metroPanel1.Controls.Add(this.tbxTitle);
            this.metroPanel1.Controls.Add(this.metroLabel2);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.Controls.Add(this.lblDebugMode);
            this.metroPanel1.Controls.Add(this.tglCloseOnFinish);
            this.metroPanel1.Controls.Add(this.tglDebugMode);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(16, 69);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(824, 402);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(739, 92);
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
            this.lblTitle.Location = new System.Drawing.Point(546, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(33, 19);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Title";
            // 
            // tbxTitle
            // 
            // 
            // 
            // 
            this.tbxTitle.CustomButton.Image = null;
            this.tbxTitle.CustomButton.Location = new System.Drawing.Point(75, 1);
            this.tbxTitle.CustomButton.Name = "";
            this.tbxTitle.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbxTitle.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbxTitle.CustomButton.TabIndex = 1;
            this.tbxTitle.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbxTitle.CustomButton.UseSelectable = true;
            this.tbxTitle.CustomButton.Visible = false;
            this.tbxTitle.Lines = new string[0];
            this.tbxTitle.Location = new System.Drawing.Point(610, 19);
            this.tbxTitle.MaxLength = 32767;
            this.tbxTitle.Name = "tbxTitle";
            this.tbxTitle.PasswordChar = '\0';
            this.tbxTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxTitle.SelectedText = "";
            this.tbxTitle.SelectionLength = 0;
            this.tbxTitle.SelectionStart = 0;
            this.tbxTitle.ShortcutsEnabled = true;
            this.tbxTitle.Size = new System.Drawing.Size(199, 23);
            this.tbxTitle.TabIndex = 5;
            this.tbxTitle.UseSelectable = true;
            this.tbxTitle.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbxTitle.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbxTitle.TextChanged += new System.EventHandler(this.TbxTitle_TextChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(295, 17);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(96, 19);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "Close on Finish";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(291, 17);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(96, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Close on Finish";
            // 
            // lblDebugMode
            // 
            this.lblDebugMode.AutoSize = true;
            this.lblDebugMode.Location = new System.Drawing.Point(20, 17);
            this.lblDebugMode.Name = "lblDebugMode";
            this.lblDebugMode.Size = new System.Drawing.Size(87, 19);
            this.lblDebugMode.TabIndex = 3;
            this.lblDebugMode.Text = "Debug Mode";
            // 
            // gridViewSettings
            // 
            this.gridViewSettings.AllowUserToOrderColumns = true;
            this.gridViewSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewSettings.Location = new System.Drawing.Point(20, 141);
            this.gridViewSettings.Name = "gridViewSettings";
            this.gridViewSettings.Size = new System.Drawing.Size(789, 244);
            this.gridViewSettings.TabIndex = 8;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 487);
            this.Controls.Add(this.metroPanel1);
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
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroTextBox tbxTitle;
        private MetroFramework.Controls.MetroButton btnSave;
        private System.Windows.Forms.DataGridView gridViewSettings;
    }
}