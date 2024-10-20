using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaYaSafeSystemSetup
{
    public partial class Kaldırma : Form
    {
        public Kaldırma()
        {
            InitializeComponent();
        }

        private void Kaldırma_Load(object sender, EventArgs e)
        {

        }

        private void Kaldırma_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
