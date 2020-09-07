namespace ConquerLoader
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnStart = new MetroFramework.Controls.MetroButton();
            this.pBar = new MetroFramework.Controls.MetroProgressBar();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.cbxServers = new MetroFramework.Controls.MetroComboBox();
            this.btnLogModules = new MetroFramework.Controls.MetroButton();
            this.serverStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(196, 127);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(206, 30);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START";
            this.btnStart.UseSelectable = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(25, 99);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(376, 23);
            this.pBar.TabIndex = 2;
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
            // 
            // cbxServers
            // 
            this.cbxServers.FormattingEnabled = true;
            this.cbxServers.ItemHeight = 23;
            this.cbxServers.Location = new System.Drawing.Point(25, 127);
            this.cbxServers.Name = "cbxServers";
            this.cbxServers.Size = new System.Drawing.Size(165, 29);
            this.cbxServers.TabIndex = 3;
            this.cbxServers.UseSelectable = true;
            this.cbxServers.SelectedIndexChanged += new System.EventHandler(this.CbxServers_SelectedIndexChanged);
            // 
            // btnLogModules
            // 
            this.btnLogModules.ForeColor = System.Drawing.Color.White;
            this.btnLogModules.Location = new System.Drawing.Point(297, 62);
            this.btnLogModules.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogModules.Name = "btnLogModules";
            this.btnLogModules.Size = new System.Drawing.Size(105, 24);
            this.btnLogModules.TabIndex = 4;
            this.btnLogModules.Text = "LOG MODULES";
            this.btnLogModules.UseSelectable = true;
            this.btnLogModules.Click += new System.EventHandler(this.BtnLogModules_Click);
            // 
            // serverStatus
            // 
            this.serverStatus.AutoSize = true;
            this.serverStatus.Location = new System.Drawing.Point(22, 62);
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.Size = new System.Drawing.Size(10, 13);
            this.serverStatus.TabIndex = 5;
            this.serverStatus.Text = "-";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 169);
            this.Controls.Add(this.serverStatus);
            this.Controls.Add(this.btnLogModules);
            this.Controls.Add(this.cbxServers);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(13, 60, 13, 13);
            this.Text = "ConquerLoader by DaRkFoxDeveloper";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnStart;
        private MetroFramework.Controls.MetroProgressBar pBar;
        private System.ComponentModel.BackgroundWorker worker;
        private MetroFramework.Controls.MetroComboBox cbxServers;
        private MetroFramework.Controls.MetroButton btnLogModules;
        private System.Windows.Forms.Label serverStatus;
    }
}

