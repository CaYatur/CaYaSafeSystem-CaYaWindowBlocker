using System.Diagnostics;

namespace CaYaLabOffline
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe");
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Process.Start("shutdown", "/s /t 0"); // Bilgisayarý hemen kapatýr
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown", "/r /t 0"); // Bilgisayarý hemen yeniden baþlatýr
        }

    }
}