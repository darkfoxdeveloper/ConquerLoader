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
            this.btnConnections = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.Black;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.ForeColor = System.Drawing.Color.White;
            this.logBox.Location = new System.Drawing.Point(33, 135);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(727, 295);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(658, 98);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(102, 31);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test Connected";
            this.btnTest.UseSelectable = true;
            this.btnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // btnConnections
            // 
            this.btnConnections.Location = new System.Drawing.Point(550, 98);
            this.btnConnections.Name = "btnConnections";
            this.btnConnections.Size = new System.Drawing.Size(102, 31);
            this.btnConnections.TabIndex = 2;
            this.btnConnections.Text = "Connections?";
            this.btnConnections.UseSelectable = true;
            this.btnConnections.Click += new System.EventHandler(this.BtnConnections_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnConnections);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.logBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "CLServer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logBox;
        private MetroFramework.Controls.MetroButton btnTest;
        private MetroFramework.Controls.MetroButton btnConnections;
    }
}

