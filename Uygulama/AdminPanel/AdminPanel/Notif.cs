using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace AdminPanel
{
    public partial class Notif : Form
    {

        private const string FilePath = "data.txt";
        private List<string> devices = new List<string>();

        public Notif()
        {
            InitializeComponent();


            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";

            label1.BackColor = ColorTranslator.FromHtml("#e3eff8");
            label1.ForeColor = ColorTranslator.FromHtml("#ed6839");

            label2.BackColor = ColorTranslator.FromHtml("#e3eff8");
            label3.BackColor = ColorTranslator.FromHtml("#e3eff8");
            label4.BackColor = ColorTranslator.FromHtml("#e3eff8");
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
                        listBoxDevices.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verileri yüklerken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label2.Text = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label3.Text = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label4.Text = textBox4.Text;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string endpoint = string.IsNullOrWhiteSpace(textBox1.Text) ? "ㅤ" : textBox1.Text;
            string endpoint2 = string.IsNullOrWhiteSpace(textBox2.Text) ? "ㅤ" : textBox2.Text;
            string endpoint3 = string.IsNullOrWhiteSpace(textBox3.Text) ? "ㅤ" : textBox3.Text;
            string endpoint4 = string.IsNullOrWhiteSpace(textBox4.Text) ? "ㅤ" : textBox4.Text;

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                MessageBox.Show("Lütfen bir endpoint girin.");
                return;
            }

            if (checkBox1.Checked)
            {
                foreach (var device in listBoxDevices.Items)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/warn/{endpoint}/{endpoint2}/{endpoint3}/{endpoint4}";

                    //await SendRequestAsync(requestUrl, deviceName);
                    SendRequestAsync(requestUrl, deviceName);
                }
            }
            else
            {
                foreach (var device in listBoxDevices.SelectedItems)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/warn/{endpoint}/{endpoint2}/{endpoint3}/{endpoint4}";

                    await SendRequestAsync(requestUrl, deviceName);
                }
            }
        }






        private async Task SendRequestAsync(string requestUrl, string deviceName)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show($"{deviceName} cihazından gelen yanıt: {responseBody}");
                    label5.Text = ($"{deviceName} cihazından gelen yanıt: {responseBody}");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"{deviceName} cihazına istek gönderilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
