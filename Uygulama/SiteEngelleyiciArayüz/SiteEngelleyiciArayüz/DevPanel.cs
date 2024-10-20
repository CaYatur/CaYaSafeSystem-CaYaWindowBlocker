using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SiteEngelleyiciArayüz
{
    public partial class DevPanel : Form
    {
        System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["Form1"];
        System.Windows.Forms.Form l = System.Windows.Forms.Application.OpenForms["Lock"];
        public DevPanel()
        {
            InitializeComponent();
        }
        public static event EventHandler RequestEventCA;
        public static event EventHandler RequestEventLOG;
        public static event EventHandler RequestEventLOAD;
        public static event EventHandler RequestEventUN;
        public static event EventHandler RequestEventOut;
        public static event EventHandler RequestEventUpdate;
        private void DevPanel_Load(object sender, EventArgs e)
        {
            //label1.Text = ((Form1)f).Text;
            RequestEventCA?.Invoke(this, EventArgs.Empty);

            TopMost = true;
            TopMost = false;
            TopMost = true;
            TopMost = false;
            TopMost = true;



            string logDirectory = @"C:\ProgramData\PVGCC";

            List<string> logFiles = Directory.GetFiles(logDirectory, "*_Log.txt").ToList();

            if (logFiles.Count == 0)
            {
                MessageBox.Show("Geçerli bir log dosyası bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Dictionary<string, int> windowCounts = new Dictionary<string, int>();

            foreach (string logFile in logFiles)
            {
                using (StreamReader reader = new StreamReader(logFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 3)
                        {
                            string window = parts[1].Trim();
                            if (!windowCounts.ContainsKey(window))
                                windowCounts[window] = 1;
                            else
                                windowCounts[window]++;
                        }
                    }
                }
            }

            // Tüm pencereleri sayılarıyla birlikte ListBox'a ekleyin
            foreach (var pair in windowCounts.OrderByDescending(pair => pair.Value))
            {
                listBox1.Items.Add($"{pair.Key}: {pair.Value} kez");
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Lock lc = new Lock();
            //string variableValue = lc.txtval;
            //lc.txtval = "CaYa";
            //MessageBox.Show("One Time Activation Code: " + variableValue);
            //console.Text = variableValue;
            //console.Text = variableValue;
            string _OneTimeCode = Lock.OneTimeCode;
            console.Text = _OneTimeCode;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            RequestEventLOG?.Invoke(this, EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RequestEventLOAD?.Invoke(this, EventArgs.Empty);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe");
            await Task.Delay(1000);
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RequestEventUN?.Invoke(this, EventArgs.Empty);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RequestEventOut?.Invoke(this, EventArgs.Empty);
            Close();
        }


        public static string text1;
        public static string text2;
        public static string text3;


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            text1 = textBox1.Text;
            RequestEventUpdate?.Invoke(this, EventArgs.Empty);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            text2 = textBox2.Text;
            RequestEventUpdate?.Invoke(this, EventArgs.Empty);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            text3 = textBox3.Text;
            RequestEventUpdate?.Invoke(this, EventArgs.Empty);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RequestEventUpdate?.Invoke(this, EventArgs.Empty);
        }
    }
}
