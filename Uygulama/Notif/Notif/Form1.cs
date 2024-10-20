using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Notif
{


    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public Form1(string text1, string text2, string text3, string text4)
        {
            InitializeComponent();

            // Update labels with extracted text
            label1Text.Text = text1;
            label2Text.Text = text2;
            label3Text.Text = text3;
            label4.Text = text4;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1Text.BackColor = ColorTranslator.FromHtml("#e3eff8");
            label1Text.ForeColor = ColorTranslator.FromHtml("#ed6839");

            label2Text.BackColor = ColorTranslator.FromHtml("#e3eff8");
            label3Text.BackColor = ColorTranslator.FromHtml("#e3eff8");
            label4.BackColor = ColorTranslator.FromHtml("#e3eff8");
            TopMost = true;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
