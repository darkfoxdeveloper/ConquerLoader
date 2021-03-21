
namespace ConquerLoader.Forms
{
    partial class ServerDatManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerDatManager));
            this.lstServers = new System.Windows.Forms.ListBox();
            this.lstGroups = new System.Windows.Forms.ListBox();
            this.btnClose = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // lstServers
            // 
            this.lstServers.FormattingEnabled = true;
            this.lstServers.ItemHeight = 20;
            this.lstServers.Location = new System.Drawing.Point(34, 112);
            this.lstServers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstServers.Name = "lstServers";
            this.lstServers.Size = new System.Drawing.Size(310, 384);
            this.lstServers.TabIndex = 0;
            this.lstServers.SelectedIndexChanged += new System.EventHandler(this.LstServers_SelectedIndexChanged);
            // 
            // lstGroups
            // 
            this.lstGroups.FormattingEnabled = true;
            this.lstGroups.ItemHeight = 20;
            this.lstGroups.Location = new System.Drawing.Point(356, 112);
            this.lstGroups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstGroups.Name = "lstGroups";
            this.lstGroups.Size = new System.Drawing.Size(310, 384);
            this.lstGroups.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(34, 508);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(633, 52);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseSelectable = true;
            this.btnClose.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ServerDatManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 569);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstGroups);
            this.Controls.Add(this.lstServers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "ServerDatManager";
            this.Padding = new System.Windows.Forms.Padding(30, 92, 30, 31);
            this.Resizable = false;
            this.Text = "Server.dat Viewer";
            this.Load += new System.EventHandler(this.ServerDatManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstServers;
        private System.Windows.Forms.ListBox lstGroups;
        private MetroFramework.Controls.MetroButton btnClose;
    }
}