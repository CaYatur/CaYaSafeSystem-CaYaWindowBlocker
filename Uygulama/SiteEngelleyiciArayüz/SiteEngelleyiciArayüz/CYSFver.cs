using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteEngelleyiciArayüz
{
    public partial class CYSFver : Form
    {
        int DevMode = 0;
        int DevModeEnable = 0;
        private bool DevModeEnabledAttempt = false;
        private bool DevModeEnabled = false;
        public CYSFver()
        {
            InitializeComponent();
        }

        private void CYSFver_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (DevMode == 5)
            {
                DevModeEnabledAttempt = true;
            }
            else if (DevModeEnable == 6 && DevMode == 10)
            {
                DevModeEnabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DevMode += 1;
            //label2.Text = DevMode.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (DevModeEnabledAttempt == true)
            {
                DevModeEnable += 1;
                //label3.Text = DevModeEnable.ToString();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DevPanel dp = new DevPanel();
            //dp.Show();
            if (DevModeEnabled == true)
            {
                dp.Show();
            }
        }
    }
}
