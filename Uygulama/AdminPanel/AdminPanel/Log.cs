using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminPanel
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        private void Log_Load(object sender, EventArgs e)
        {
            LoadDataFromFile();
        }

        private void LoadDataFromFile()
        {
            // Dosyadan verileri yükler ve ListBox'a ekler
            try
            {
                string filePath = "data.txt";
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        listBox1.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verileri yüklerken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            foreach (var device in listBox1.SelectedItems)
            {
                string[] deviceParts = device.ToString().Split('(');
                string ip = deviceParts[0].Trim(); // IP'yi al
                string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                string requestUrl = $"http://{ip}:4728/cysfsylog";

                // Web tarayıcısını başlat
                Process.Start(new ProcessStartInfo
                {
                    FileName = requestUrl,
                    UseShellExecute = true
                });

            }
        }
    }
}
