
namespace ConquerLoader.Forms
{
    partial class Plugins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plugins));
            this.btnExample = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // btnExample
            // 
            this.btnExample.AutoSize = true;
            this.btnExample.Location = new System.Drawing.Point(23, 76);
            this.btnExample.Name = "btnExample";
            this.btnExample.Size = new System.Drawing.Size(161, 50);
            this.btnExample.TabIndex = 0;
            this.btnExample.Text = "demoBtn";
            this.btnExample.UseSelectable = true;
            this.btnExample.Visible = false;
            // 
            // Plugins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 441);
            this.Controls.Add(this.btnExample);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Plugins";
            this.Resizable = false;
            this.Text = "Plugins";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Plugins_FormClosing);
            this.Load += new System.EventHandler(this.Plugins_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnExample;
    }
}