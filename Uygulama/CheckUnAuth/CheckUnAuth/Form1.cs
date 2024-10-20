using QRCoder;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using NativeWifi; // Ek olarak, bu kütüphaneyi yüklemeyi unutmayýn (nuget üzerinden)




namespace CheckUnAuth
{

    public partial class Form1 : Form
    {
        private WlanClient wlan;



        public Form1()
        {
            InitializeComponent();






            RunCmdCommand("Netsh WLAN show interfaces");




        }




        public static event EventHandler RequestEvent;
        public static event EventHandler RequestEvent2;
        private async void Form1_Load(object sender, EventArgs e)
        {
            EndProcess("explorer");
            DisplayQRCode();

            
            

            DTM dtm = new DTM();
            dtm.Show();




            string programDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string filePath = Path.Combine(programDataFolder, "verifyc.txt");

            if (!File.Exists(filePath))
            {
                // Dosya yoksa oluþtur
                try
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine("CaYaSafeSystemUNAUTH");
                    }
                    Console.WriteLine("Dosya oluþturuldu: " + filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Dosya oluþturulurken bir hata oluþtu: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Dosya zaten var: " + filePath);
            }


            Thread bgThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(600);


                    string filePath = @"C:\ProgramData\verifyc.txt";
                    string[] lines = null;

                    try
                    {
                        // Dosyayý oku
                        lines = File.ReadAllLines(filePath);

                        for (int i = 0; i < lines.Length; i++)
                        {
                            string line = lines[i];

                            // "verified" içeren satýrlarý kontrol et
                            if (line.Contains("verified"))
                            {
                                // Satýrdan tarih bilgisini çýkar
                                string dateString = line.Split('(')[1].Split(')')[0].Trim();
                                DateTime verifiedTime = DateTime.ParseExact(dateString, "yyyy.MM.dd.HH.mm", null);

                                // Þu anki zamaný al
                                DateTime currentTime = DateTime.Now;

                                // Zaman farkýný kontrol et (2 dakikadan fazla ise)
                                TimeSpan timeDifference = currentTime - verifiedTime;
                                if (timeDifference.TotalMinutes < 2)
                                {
                                    // Zamaný geçmiþ ise satýrý sil
                                    lines[i] = string.Empty;

                                    this.Invoke((MethodInvoker)async delegate
                                    {
                                        APROVED ap = new APROVED();
                                        ap.Show();
                                        Hide();
                                        label5.Text = "GÝRÝÞ YAPILIYOR LÜTFEN BEKLEYÝNÝZ...";
                                        RequestEvent?.Invoke(this, EventArgs.Empty);

                                        // Dosyanýn bulunduðu dizin
                                        string dizin = @"C:\ProgramData"; // Kullanýcý adýnýza göre deðiþtirin

                                        // Silinecek dosyanýn adý
                                        string dosyaAdi = "UNAUTH";

                                        // Dosyanýn tam yolu
                                        string dosyaYolu = Path.Combine(dizin, dosyaAdi);

                                        // Dosyayý sil
                                        try
                                        {
                                            if (File.Exists(dosyaYolu))
                                            {
                                                File.Delete(dosyaYolu);
                                                Console.WriteLine("Dosya silindi: " + dosyaYolu);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Dosya mevcut deðil: " + dosyaYolu);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Dosya silinirken bir hata oluþtu: " + ex.Message);
                                        }

                                        
                                        await Task.Delay(2000);
                                        Process.Start("explorer.exe");
                                        await Task.Delay(500);
                                        //Environment.Exit(1);
                                        //Close();
                                        RequestEvent2?.Invoke(this, EventArgs.Empty);
                                        Hide();
                                    });
                                }
                            }
                        }

                        // Geçerli olmayan satýrlarý temizle
                        lines = Array.FindAll(lines, s => !string.IsNullOrEmpty(s));

                        // Dosyayý güncelle
                        File.WriteAllLines(filePath, lines);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }




                }




            });
            bgThread.IsBackground = true;
            bgThread.Start();


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

        private void RunCmdCommand(string command)
        {
            try
            {
                // Process oluþturma ve ayarlama
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;

                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();

                // Komutu CMD'ye yazma
                StreamWriter streamWriter = process.StandardInput;
                streamWriter.WriteLine(command);
                streamWriter.Close();

                // CMD'den çýktýyý okuma
                StreamReader streamReader = process.StandardOutput;
                string output = streamReader.ReadToEnd();
                streamReader.Close();

                // SSID'yi bulma
                string ssid = FindSSID(output);

                // Formdaki label6'ya yazma
                SetLabelText(ssid);

                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private string FindSSID(string output)
        {
            // SSID'yi bulma
            string[] lines = output.Split('\n');
            foreach (string line in lines)
            {
                if (line.Contains("SSID"))
                {
                    string[] parts = line.Split(':');
                    if (parts.Length > 1)
                    {
                        return parts[1].Trim();
                    }
                }
            }
            return "SSID bulunamadý.";
        }

        private void SetLabelText(string text)
        {
            if (label6.InvokeRequired)
            {
                label6.Invoke(new MethodInvoker(delegate { label6.Text = text; }));
            }
            else
            {
                label6.Text = text;
            }
        }




        private string GetSsid(NetworkInterface networkInterface)
        {
            // Eðer kablosuz að baðlantýsý varsa, SSID bilgisini al
            if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                networkInterface.OperationalStatus == OperationalStatus.Up)
            {
                // Kablosuz að arayüzünü bulduk, þimdi SSID bilgisini almamýz gerekiyor.
                var wlanClient = new WlanClient(); // WlanClient sýnýfýný kullanmak için System.Net.NetworkInformation.Wlan kütüphanesini eklemeyi unutmayýn

                foreach (var wlanInterface in wlanClient.Interfaces)
                {
                    // Ýlgili kablosuz að arayüzünü bulduk
                    if (wlanInterface.InterfaceGuid.ToString() == networkInterface.Id)
                    {
                        // Ýlgili arayüzdeki að baðlantýsýný al
                        var wlanConnectionAttributes = wlanInterface.CurrentConnection;

                        // Eðer bir baðlantý varsa, SSID bilgisini döndür
                        if (wlanConnectionAttributes.isState == Wlan.WlanInterfaceState.Connected)
                        {
                            return Encoding.ASCII.GetString(wlanConnectionAttributes.wlanAssociationAttributes.dot11Ssid.SSID, 0, (int)wlanConnectionAttributes.wlanAssociationAttributes.dot11Ssid.SSIDLength);
                        }
                    }
                }
            }

            // Eðer kablosuz að baðlantýsý yoksa veya bir sorun oluþtuysa null döndür
            return null;
        }




        private async void DisplayQRCode()
        {
            string ipAddressv4 = GetIPv4Address();

            string qrData = $"Ipv4Address:{ipAddressv4}";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // PictureBox boyutuna göre QR kodunu yeniden boyutlandýr
            int qrCodeSize = Math.Min(pictureBox1.Width, pictureBox1.Height);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            qrCodeImage = ResizeImage(qrCodeImage, qrCodeSize, qrCodeSize);

            pictureBox1.Image = qrCodeImage;
        }



        // Resmi belirli boyutlara göre yeniden boyutlandýran yöntem
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }



        static string GetIPv4Address()
        {
            string ipAddress = string.Empty;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "powershell";
            psi.Arguments = "$defaultInterface = (Get-NetRoute | Where-Object { $_.DestinationPrefix -eq '0.0.0.0/0' }).InterfaceAlias; $ipAddress = (Get-NetIPAddress -InterfaceAlias $defaultInterface -AddressFamily IPv4).IPAddress; $ipAddress";
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.CreateNoWindow = true;

            using (Process process = Process.Start(psi))
            {
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    ipAddress = reader.ReadToEnd().Trim();
                }
            }

            return ipAddress;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
            {
                DialogResult result = MessageBox.Show("Bu Bilgisayar KÝTLENMÝÞTÝR!", "CaYa©", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Alt+F4 tuþ kombinasyonunu engelle
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}