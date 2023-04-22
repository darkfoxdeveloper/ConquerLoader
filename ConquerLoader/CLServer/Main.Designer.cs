namespace CLServer
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
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.btnTest = new MetroFramework.Controls.MetroButton();
            this.lblNumberConnections = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.Black;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.ForeColor = System.Drawing.Color.White;
            this.logBox.Location = new System.Drawing.Point(23, 122);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(727, 295);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "";
            this.logBox.TextChanged += new System.EventHandler(this.logBox_TextChanged);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(535, 63);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(215, 53);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Check connection by IP";
            this.btnTest.UseSelectable = true;
            this.btnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // lblNumberConnections
            // 
            this.lblNumberConnections.AutoSize = true;
            this.lblNumberConnections.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblNumberConnections.Location = new System.Drawing.Point(23, 420);
            this.lblNumberConnections.Name = "lblNumberConnections";
            this.lblNumberConnections.Size = new System.Drawing.Size(88, 19);
            this.lblNumberConnections.TabIndex = 3;
            this.lblNumberConnections.Text = "0 connections";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblNumberConnections);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.logBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "CLServer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logBox;
        private MetroFramework.Controls.MetroButton btnTest;
        private MetroFramework.Controls.MetroLabel lblNumberConnections;
    }
}

