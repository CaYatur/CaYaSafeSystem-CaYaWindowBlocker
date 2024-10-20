using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace CaYaSafeSystemSetup
{
    public partial class verify12 : Form
    {

        private static verify12 _instance;
        private bool close = false;
        public static event EventHandler RequestEvent;

        public static verify12 Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new verify12();
                }
                return _instance;
            }
        }


        public verify12()
        {
            InitializeComponent();
        }

        public event EventHandler SomethingHappened;


        private void verify12_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close == false)
            {
                e.Cancel = true;
            }
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Registry anahtarını aç
            RegistryKey key = Registry.LocalMachine.CreateSubKey("Software\\CaYaSafeSystem");
            // Değeri oku
            string readValue = key.GetValue("SFMD") as string;
            if (readValue == textBox1.Text)
            {
                label3.Visible = true;

                RequestEvent?.Invoke(this, EventArgs.Empty);

                close = true;
                await Task.Delay(1000);
                Close();
            }
            key.Close();
        }

        private void verify12_Load(object sender, EventArgs e)
        {
            TopMost = true;
            TopMost = false;
            TopMost = true;
            TopMost = true;
            TopMost = false;
            TopMost = true;
            TopMost = true;
        }
    }
}
