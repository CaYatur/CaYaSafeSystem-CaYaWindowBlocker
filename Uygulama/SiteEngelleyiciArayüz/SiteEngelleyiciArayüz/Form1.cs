using Microsoft.VisualBasic.Devices;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SiteEngelleyiciArayüz
{
    public partial class Form1 : Form
    {



        private static Lock instance;

        public static Lock Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new Lock();
                }
                return instance;
            }
        }

        private static Form1 instance4;
        public static Form1 Instance4
        {
            get
            {
                if (instance4 == null || instance4.IsDisposed)
                {
                    instance4 = new Form1();
                }
                return instance4;
            }
        }

        private async Task DeleteByIpAddress(string ipAddress)
        {
            try
            {
                if (collection != null)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("ipAddress", ipAddress);
                    var result = await collection.DeleteManyAsync(filter);

                    if (result.DeletedCount > 0)
                    {
                        Console.WriteLine($"{ipAddress} IP adresine sahip veri başarıyla silindi.");
                    }
                    else
                    {
                        Console.WriteLine($"{ipAddress} IP adresine sahip veri bulunamadı.");
                    }
                }
                else
                {
                    Console.WriteLine("Hata: collection değişkeni null.");
                }
            }
            catch (Exception ex)
            {
                // Hata sadece kaydedilir, program devam eder
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }
        private async void DeleteIP()
        {
            string ipAddressToDelete = GetLocalIPAddress();
            await DeleteByIpAddress(ipAddressToDelete);

        }
        private string GetLocalIPAddress()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var networkInterface in networkInterfaces)
            {
                if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    return networkInterface.GetPhysicalAddress().ToString();
                }
            }

            // MAC adresi bulunamazsa varsayılan bir değer kullanabilirsiniz
            return "00-00-00-00-00-00";
        }

        private void EndProcess(string processName)
        {
            try
            {
                // Process sınıfı kullanarak taskkill komutunu çalıştır
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

                    // Başarıyla sonlandırıldıysa
                    //if (process.ExitCode == 0)
                    //{
                    //    MessageBox.Show($"Süreç '{processName}' başarıyla sonlandırıldı.");
                    //}
                    //else
                    //{
                    //    MessageBox.Show($"Süreç sonlandırılamadı. Çıkış kodu: {process.ExitCode}");
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void RunUac()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    Verb = "runas", // Uygulamayı yönetici olarak başlat
                    UseShellExecute = true
                };

                Process.Start(startInfo);
                Application.Exit(); // Şu anki uygulamayı kapat
            }
            catch (Exception ex)
            {
                MessageBox.Show("UYARI! İşinize Devam Etmek İçin Onaylamanız Gerekmektedir.");
            }
        }

        private bool IsRunAsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void CloseFormCaYa()
        {
            if (IsRunAsAdmin())
            {
                DisableTaskManager Background = new DisableTaskManager();
                Background.ShowDialog();
                this.Close();
            }
        }


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

        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        private async void CYStartupDB()
        {
            // MongoDB bağlantı adresi - AppSettings.cs içindeki sabitten okunur
            //string connectionString = AppSettings.MongoDbConnectionString;

            await Task.Delay(1200);
            //DeleteIP();
        }


        public Form1()
        {
            InitializeComponent();

            Process currentProcess = Process.GetCurrentProcess();
            string currentProcessName = currentProcess.ProcessName;

            Process[] processes = Process.GetProcessesByName(currentProcessName);

            if (processes.Length > 1)
            {
                //MessageBox.Show("Uygulama zaten çalışıyor!");
                Environment.Exit(1);
                return;
            }

            TopMost = false;
            TopMost = true;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            // Panelin köşelerini yuvarla
            PanelColor.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, PanelColor.Width, PanelColor.Height, 5, 5));

            button2.BackColor = ColorTranslator.FromHtml("#e74c3c");
            button2.ForeColor = ColorTranslator.FromHtml("#ffffff");

            button3.BackColor = ColorTranslator.FromHtml("#3498db");
            //Lock LK = new Lock();
            //LK.Show();
            //LK.Visible = false; // veya LK.Hide();

            Lock.Instance.Show();
            Lock.Instance.Visible = false;

        }





        private void label1_Click(object sender, EventArgs e)
        {
            info info = new info();
            info.ShowDialog();
        }




        //static string programNameOrPath = "node.exe";




        private async void button1_Click(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            {
                RunUac();
                return; // İşlemi sonlandır
            }

            CYStartupDB();
            //DeleteIP();
            button3.Enabled = false;
            button1.Enabled = false;

            //Lock LK = new Lock();
            //LK.Close();
            DeleteIP();
            // Başka bir programı kapatma işlemi
            //Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(programNameOrPath));




            button1.Text = "Hatanı Anladın Birdaha Yapmayacaksın '5'";
            await Task.Delay(1000); // 1 saniye beklet

            button1.Text = "Hatanı Anladın Birdaha Yapmayacaksın '4'";
            await Task.Delay(1000);

            button1.Text = "Hatanı Anladın Birdaha Yapmayacaksın '3'";
            await Task.Delay(1000);

            button1.Text = "Hatanı Anladın Birdaha Yapmayacaksın '2'";
            await Task.Delay(1000);

            button1.Text = "Hatanı Anladın Birdaha Yapmayacaksın '1'";
            await Task.Delay(1000);

            button1.Text = "Hatanı Anladın Birdaha Yapmayacaksın '0'";
            await Task.Delay(1000);

            button1.Text = "Başarılı!";
            await Task.Delay(1300);
            Process.Start("explorer.exe");
            await Task.Delay(300);

            Environment.Exit(1);
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            CYStartupDB();
            await Task.Delay(100);
            button2.Text = "Kapatılıyor 3";
            await Task.Delay(1500);
            button2.Text = "Kapatılıyor 2";
            await Task.Delay(1500);
            button2.Text = "Kapatılıyor 1";
            await Task.Delay(1500);
            button2.Text = "Kapatılıyor 0";
            await Task.Delay(500);
            button2.Text = "Başarıyla Kapatıldı!";
            Process.Start("shutdown", "/s /f /t 0");
        }

        private void CloseSpecificForm()
        {
            // Kapatmak istediğiniz formun adını belirtin (Form1 örneği)
            string formNameToClose = "Lock";

            // İlgili formu arayın ve kapatın
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == formNameToClose)
                {
                    form.Close();
                    break; // Form bulunduğunda döngüden çıkın
                }
            }
        }


        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            CYSFver cs = new CYSFver();
            cs.ShowDialog();
            ///////
            //Visible = false;

            //Lock.Instance.Show();
            //Lock.Instance.TopMost = true;
            ///////








            //Lock LK = new Lock();
            //LK.ShowDialog();
            //LK.Visible = true;


            //Lock LK = new Lock();
            //LK.ShowDialog();
            //LK.Visible = true;

            //Lock.Instance.Show();
            //Lock.Instance.TopMost = true;

            //Lock.Instance.Dispose();
            //CloseSpecificForm();
            //await Task.Delay(1000);
            //Lock.Instance.Show();
            //Lock.Instance.TopMost = true;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
            {
                //DialogResult result = MessageBox.Show("Bu Bilgisayar KİTLENMİŞTİR!", "CaYa©", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Alt+F4 tuş kombinasyonunu engelle
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string filePath = "C:\\Program Files (x86)\\CaYaLab\\CaYaLabOffline\\CaYaLabOffline.exe"; // Açmak istediğiniz dosyanın yolu

        private async void Form1_Load(object sender, EventArgs e)
        {
            DevPanel.RequestEventUpdate += HandleForm2RequestUpdate;

            //CloseFormCaYa();

            if (IsWifiAvailable())
            {
                // Wifi bağlantısı mevcut, belirli işlemleri gerçekleştir.
            }
            else
            {
                //Process.Start(filePath);
            }

            await Task.Delay(250);

            EndProcess("chrome");
            EndProcess("explorer");
            EndProcess("cmd");

        }


        private async void HandleForm2RequestUpdate(object sender, EventArgs e)
        {

            string _Text1 = DevPanel.text1;
            label1.Text = _Text1;

            string _Text2 = DevPanel.text2;
            label2.Text = _Text2;

            string _Text3 = DevPanel.text3;
            label3.Text = _Text3;

        }


        private void label5_Click(object sender, EventArgs e)
        {
            info info = new info();
            info.ShowDialog();
        }





        public static bool IsWifiAvailable()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    return true; // En az bir wifi bağlantısı mevcut.
                }
            }

            return false; // Wifi bağlantısı yok.
        }

        private void label6_Click(object sender, EventArgs e)
        {

            string url = "https://yenilikler.cayatur.repl.co/";

            // Web tarayıcısını başlat
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

        }


        int r = 255, g = 0, b = 0;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.PanelColor.BackColor = Color.FromArgb(r, g, b);

            if (r > 0 && b == 0)
            {
                r--;
                g++;
            }
            if (g > 0 && r == 0)
            {
                g--;
                b++;
            }
            if (b > 0 && g == 0)
            {
                b--;
                r++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Visible = false;
            //Lock LK = new Lock();
            //LK.ShowDialog();
            //LK.Visible = true;
            Lock.Instance.Show();
            Lock.Instance.TopMost = true;
        }
    }
}