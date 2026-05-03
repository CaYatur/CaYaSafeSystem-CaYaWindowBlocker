using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace AdminPanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LoadDataFromFile();
            checkpc();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Log nf = new Log();
            nf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Notif nf = new Notif();
            nf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ListBox'a yeni bir öðe ekler ve verileri dosyaya kaydeder
            string newItem = txtNewItem.Text.Trim();
            if (!string.IsNullOrEmpty(newItem))
            {
                listBox1.Items.Add(newItem);
                SaveDataToFile();
                txtNewItem.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen bir öðe girin.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                MessageBox.Show("Verileri yüklerken bir hata oluþtu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDataToFile()
        {
            // ListBox'taki tüm öðeleri dosyaya kaydeder
            try
            {
                string filePath = "data.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var item in listBox1.Items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verileri kaydederken bir hata oluþtu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BlackWhiteListSystem bw = new BlackWhiteListSystem();
            bw.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }






        private int successfulRequests = 0;
        private int failedRequests = 0;

        private void button5_Click(object sender, EventArgs e)
        {
            checkpc();
        }

        private void checkpc()
        {
            // Baþarýlý ve baþarýsýz istek sayýlarýný sýfýrla
            successfulRequests = 0;
            failedRequests = 0;

            foreach (var device in listBox1.Items)
            {
                string[] deviceParts = device.ToString().Split('(');
                string ip = deviceParts[0].Trim(); // IP'yi al
                string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adýný al
                string requestUrl = $"http://{ip}:4728/Check";

                //await SendRequestAsync(requestUrl, deviceName);
                SendRequestAsync(requestUrl, deviceName);
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
                    //MessageBox.Show($"{deviceName} cihazýndan gelen yanýt: {responseBody}");
                    label5.Text = ($"{deviceName} cihazýndan gelen yanýt: {responseBody}");

                    // Baþarýlý istek sayýsýný arttýr
                    successfulRequests++;
                }
            }
            catch (HttpRequestException ex)
            {
                //MessageBox.Show($"{deviceName} cihazýna istek gönderilirken bir hata oluþtu: {ex.Message}");

                // Baþarýsýz istek sayýsýný arttýr
                failedRequests++;
            }

            // Ýstek sayýlarýný güncelle
            UpdateRequestStatistics();
        }

        private void UpdateRequestStatistics()
        {
            // Baþarýlý ve baþarýsýz istek sayýlarýný label5'te göster
            label5.Text = $"Baþarýlý Ýstekler: {successfulRequests}, Baþarýsýz Ýstekler: {failedRequests}";
        }

        private async void button6_Click(object sender, EventArgs e)
        {


            DialogResult result = MessageBox.Show("Açýk olan tüm bilgisayarlarý kapatmak istiyormusunuz?", "CaYaSafeSystem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kullanýcýnýn seçimine göre programý kapat
            if (result == DialogResult.Yes)
            {

                foreach (var device in listBox1.Items)
                {
                    string[] deviceParts = device.ToString().Split('(');
                    string ip = deviceParts[0].Trim(); // IP'yi al
                    string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adýný al
                    string requestUrl = $"http://{ip}:4728/shutdown";

                    //await SendRequestAsync(requestUrl, deviceName);
                    SendRequestAsync(requestUrl, deviceName);
                }

            }




            //DialogResult result = MessageBox.Show("Programý kapatmak istiyor musunuz?", "CaYaSafeSystem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //// Kullanýcýnýn seçimine göre programý kapat
            //if (result == DialogResult.Yes)
            //{
            //    EndProcess("cyRUN");
            //    EndProcess("svpgcc");
            //    EndProcess("AutoOpenCY");
            //    EndProcess("cyRUN");
            //    EndProcess("svpgcc");
            //    EndProcess("AutoOpenCY");
            //    EndProcess("CYRS");
            //    EndProcess("server");
            //    EndProcess("CaYaSafe32");
            //    EndProcess("log");
            //    EndProcess("CaYaLabOffline");
            //    EndProcess("CaYa");
            //    EndProcess("CaYaUnkown");
            //    EndProcess("AntiEX.exe");
            //    EndProcess("CaYaExtraSafe");
            //    EndProcess("CpgXR");
            //    EndProcess("CpgX");
            //    MessageBox.Show("Hizmetler KAPATILDI!");
            //}
        }

        private void EndProcess(string processName)
        {
            try
            {
                // Process sýnýfý kullanarak taskkill komutunu çalýþtýr
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = $"/im {processName}.exe /f",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();

                    // Baþarýyla sonlandýrýldýysa
                    //if (process.ExitCode == 0)
                    //{
                    //    MessageBox.Show($"Süreç '{processName}' baþarýyla sonlandýrýldý.");
                    //}
                    //else
                    //{
                    //    MessageBox.Show($"Süreç sonlandýrýlamadý. Çýkýþ kodu: {process.ExitCode}");
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    listBox1.Items.Remove(listBox1.SelectedItem); // Seçili öðeyi kaldýr
                    SaveDataToFile(); // Güncellenmiþ listeyi dosyaya kaydet
                }
                else
                {
                    MessageBox.Show("Lütfen bir öðe seçin.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriyi kaldýrýrken bir hata oluþtu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    foreach (var device in listBox1.SelectedItems)
                    {
                        string[] deviceParts = device.ToString().Split('(');
                        string ip = deviceParts[0].Trim(); // IP'yi al
                        string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adýný al
                        string requestUrl = $"http://{ip}:4728/watch";

                        // Web tarayýcýsýný baþlat
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = requestUrl,
                            UseShellExecute = true
                        });

                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir öðe seçin.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekran izlemeye çalýþýrken bir hata oluþtu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    foreach (var device in listBox1.SelectedItems)
                    {
                        string[] deviceParts = device.ToString().Split('(');
                        string ip = deviceParts[0].Trim(); // IP'yi al
                        string deviceName = deviceParts[1].Replace(")", "").Trim(); // Cihaz adýný al
                        string requestUrl = $"http://{ip}:4728/filesystem";

                        // Web tarayýcýsýný baþlat
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = requestUrl,
                            UseShellExecute = true
                        });

                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir öðe seçin.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekran izlemeye çalýþýrken bir hata oluþtu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}