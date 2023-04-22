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
            this.Text = Title;
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

        private void QuestionBox_Load(object sender, System.EventArgs e)
        {
            this.Resizable = false;
        }
    }
}
