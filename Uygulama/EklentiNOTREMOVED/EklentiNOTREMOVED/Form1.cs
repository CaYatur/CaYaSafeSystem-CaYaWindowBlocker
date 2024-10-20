namespace EklentiNOTREMOVED
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ekle ek = new ekle();
            ek.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kaldýr kl = new kaldýr();
            kl.Show();
        }
    }
}