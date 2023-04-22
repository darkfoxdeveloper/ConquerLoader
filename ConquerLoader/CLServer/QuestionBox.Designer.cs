namespace CLServer
{
    partial class QuestionBox
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
            this.btnOk = new MetroFramework.Controls.MetroButton();
            this.lblQuestion = new MetroFramework.Controls.MetroLabel();
            this.tbxAnswer = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(28, 198);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(442, 50);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseSelectable = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.ForeColor = System.Drawing.Color.DarkGray;
            this.lblQuestion.Location = new System.Drawing.Point(28, 90);
            this.lblQuestion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(80, 19);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "questionText";
            // 
            // tbxAnswer
            // 
            // 
            // 
            // 
            this.tbxAnswer.CustomButton.Image = null;
            this.tbxAnswer.CustomButton.Location = new System.Drawing.Point(264, 2);
            this.tbxAnswer.CustomButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxAnswer.CustomButton.Name = "";
            this.tbxAnswer.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbxAnswer.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbxAnswer.CustomButton.TabIndex = 1;
            this.tbxAnswer.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbxAnswer.CustomButton.UseSelectable = true;
            this.tbxAnswer.CustomButton.Visible = false;
            this.tbxAnswer.Lines = new string[0];
            this.tbxAnswer.Location = new System.Drawing.Point(28, 144);
            this.tbxAnswer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxAnswer.MaxLength = 32767;
            this.tbxAnswer.Name = "tbxAnswer";
            this.tbxAnswer.PasswordChar = '\0';
            this.tbxAnswer.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxAnswer.SelectedText = "";
            this.tbxAnswer.SelectionLength = 0;
            this.tbxAnswer.SelectionStart = 0;
            this.tbxAnswer.ShortcutsEnabled = true;
            this.tbxAnswer.Size = new System.Drawing.Size(442, 38);
            this.tbxAnswer.TabIndex = 2;
            this.tbxAnswer.UseSelectable = true;
            this.tbxAnswer.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbxAnswer.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbxAnswer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbxAnswer_KeyPress);
            // 
            // QuestionBox
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 264);
            this.Controls.Add(this.tbxAnswer);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "QuestionBox";
            this.Padding = new System.Windows.Forms.Padding(17, 58, 17, 19);
            this.Text = "QuestionBox";
            this.Load += new System.EventHandler(this.QuestionBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnOk;
        private MetroFramework.Controls.MetroLabel lblQuestion;
        private MetroFramework.Controls.MetroTextBox tbxAnswer;
    }
}