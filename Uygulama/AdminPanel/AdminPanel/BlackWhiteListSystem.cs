using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdminPanel
{
    public partial class BlackWhiteListSystem : Form
    {
        public BlackWhiteListSystem()
        {
            InitializeComponent();
        }

        private void BlackWhiteListSystem_Load(object sender, EventArgs e)
        {
            LoadDataFromFile();
            LoadDataFromFileW();
            LoadDataFromFileB();
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




        private async void whitelistadd()
        {
            string endpoint = string.IsNullOrWhiteSpace(textBox1.Text) ? "" : textBox1.Text;




            if (string.IsNullOrWhiteSpace(endpoint))
            {
                MessageBox.Show("Lütfen bir endpoint girin.");
                return;
            }

            // ListBox'a yeni bir öğe ekler ve verileri dosyaya kaydeder
            string newItem = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(newItem))
            {
                listBox2.Items.Add(newItem);
                SaveDataToFileW();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen bir öğe girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkBox1.Checked)
            {
                foreach (var device in listBox1.Items)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/whitelist/add/{endpoint}/";

                    //await SendRequestAsync(requestUrl, deviceName);
                    SendRequestAsync(requestUrl, deviceName);
                }
            }
            else
            {
                foreach (var device in listBox1.SelectedItems)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/whitelist/add/{endpoint}/";

                    await SendRequestAsync(requestUrl, deviceName);
                }
            }
        }


        private async void whitelistdelete()
        {
            string endpoint = listBox2.SelectedItem.ToString();


            //if (string.IsNullOrWhiteSpace(endpoint))
            //{
            //    MessageBox.Show("Lütfen bir endpoint girin.");
            //    return;
            //}

            if (checkBox1.Checked)
            {
                foreach (var device in listBox1.Items)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/whitelist/delete/{endpoint}/";

                    //await SendRequestAsync(requestUrl, deviceName);
                    SendRequestAsync(requestUrl, deviceName);
                }
            }
            else
            {
                foreach (var device in listBox1.SelectedItems)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/whitelist/delete/{endpoint}/";

                    await SendRequestAsync(requestUrl, deviceName);
                }
            }

            try
            {
                if (listBox2.SelectedItem != null)
                {
                    listBox2.Items.Remove(listBox2.SelectedItem); // Seçili öğeyi kaldır
                    SaveDataToFileW(); // Güncellenmiş listeyi dosyaya kaydet
                }
                else
                {
                    MessageBox.Show("Lütfen bir öğe seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriyi kaldırırken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void blacklistadd()
        {
            string endpoint = string.IsNullOrWhiteSpace(textBox2.Text) ? "" : textBox2.Text;


            if (string.IsNullOrWhiteSpace(endpoint))
            {
                MessageBox.Show("Lütfen bir endpoint girin.");
                return;
            }


            // ListBox'a yeni bir öğe ekler ve verileri dosyaya kaydeder
            string newItem = textBox2.Text.Trim();
            if (!string.IsNullOrEmpty(newItem))
            {
                listBox3.Items.Add(newItem);
                SaveDataToFileB();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen bir öğe girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (checkBox1.Checked)
            {
                foreach (var device in listBox1.Items)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/blacklist/add/{endpoint}/";

                    //await SendRequestAsync(requestUrl, deviceName);
                    SendRequestAsync(requestUrl, deviceName);
                }
            }
            else
            {
                foreach (var device in listBox1.SelectedItems)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/blacklist/add/{endpoint}/";

                    await SendRequestAsync(requestUrl, deviceName);
                }
            }
        }


        private async void blacklistdelete()
        {
            string endpoint = listBox3.SelectedItem.ToString();


            //if (string.IsNullOrWhiteSpace(endpoint))
            //{
            //    MessageBox.Show("Lütfen bir endpoint girin.");
            //    return;
            //}

            if (checkBox1.Checked)
            {
                foreach (var device in listBox1.Items)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/blacklist/delete/{endpoint}/";

                    //await SendRequestAsync(requestUrl, deviceName);
                    SendRequestAsync(requestUrl, deviceName);
                }
            }
            else
            {
                foreach (var device in listBox1.SelectedItems)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adını al
                    string requestUrl = $"http://{ip}:4728/blacklist/delete/{endpoint}/";

                    await SendRequestAsync(requestUrl, deviceName);
                }
            }



            try
            {
                if (listBox3.SelectedItem != null)
                {
                    listBox3.Items.Remove(listBox3.SelectedItem); // Seçili öğeyi kaldır
                    SaveDataToFileB(); // Güncellenmiş listeyi dosyaya kaydet
                }
                else
                {
                    MessageBox.Show("Lütfen bir öğe seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriyi kaldırırken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //label5.Text = ($"{deviceName} cihazından gelen yanıt: {responseBody}");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"{deviceName} cihazına istek gönderilirken bir hata oluştu: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            whitelistadd();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            whitelistdelete();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            blacklistadd();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            blacklistdelete();
        }










        private void SaveDataToFileW()
        {
            // ListBox'taki tüm öğeleri dosyaya kaydeder
            try
            {
                string filePath = "dataW.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var item in listBox2.Items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verileri kaydederken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataFromFileW()
        {
            // Dosyadan verileri yükler ve ListBox'a ekler
            try
            {
                string filePath = "dataW.txt";
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        listBox2.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verileri yüklerken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void SaveDataToFileB()
        {
            // ListBox'taki tüm öğeleri dosyaya kaydeder
            try
            {
                string filePath = "dataB.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var item in listBox3.Items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verileri kaydederken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataFromFileB()
        {
            // Dosyadan verileri yükler ve ListBox'a ekler
            try
            {
                string filePath = "dataB.txt";
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        listBox3.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verileri yüklerken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
