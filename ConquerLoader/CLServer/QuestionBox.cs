namespace CLServer
{
    public partial class QuestionBox : MetroFramework.Forms.MetroForm
    {
        public string Answer = "";
        public QuestionBox()
        {
            InitializeComponent();
        }
        public QuestionBox(string Title, string Question)
        {
            InitializeComponent();
            this.Name = Title;
            this.lblQuestion.Text = Question;
        }

        private void TbxAnswer_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            Answer = this.tbxAnswer.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
