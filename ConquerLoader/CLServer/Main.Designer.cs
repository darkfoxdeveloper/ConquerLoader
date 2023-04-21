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
            this.btnDummyConnections = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.Black;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.ForeColor = System.Drawing.Color.White;
            this.logBox.Location = new System.Drawing.Point(44, 169);
            this.logBox.Margin = new System.Windows.Forms.Padding(4);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(969, 369);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(768, 122);
            this.btnTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(245, 39);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Is Connected?";
            this.btnTest.UseSelectable = true;
            this.btnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // btnConnections
            // 
            this.btnConnections.Location = new System.Drawing.Point(423, 122);
            this.btnConnections.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnections.Name = "btnConnections";
            this.btnConnections.Size = new System.Drawing.Size(218, 39);
            this.btnConnections.TabIndex = 2;
            this.btnConnections.Text = "N Connections";
            this.btnConnections.UseSelectable = true;
            this.btnConnections.Click += new System.EventHandler(this.BtnConnections_Click);
            // 
            // btnDummyConnections
            // 
            this.btnDummyConnections.Location = new System.Drawing.Point(44, 122);
            this.btnDummyConnections.Margin = new System.Windows.Forms.Padding(4);
            this.btnDummyConnections.Name = "btnDummyConnections";
            this.btnDummyConnections.Size = new System.Drawing.Size(371, 39);
            this.btnDummyConnections.TabIndex = 3;
            this.btnDummyConnections.Text = "Add Dummy Connections";
            this.btnDummyConnections.UseSelectable = true;
            this.btnDummyConnections.Click += new System.EventHandler(this.BtnDummyConnections_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.btnDummyConnections);
            this.Controls.Add(this.btnConnections);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.logBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(27, 75, 27, 25);
            this.Text = "CLServer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logBox;
        private MetroFramework.Controls.MetroButton btnTest;
        private MetroFramework.Controls.MetroButton btnConnections;
        private MetroFramework.Controls.MetroButton btnDummyConnections;
    }
}

