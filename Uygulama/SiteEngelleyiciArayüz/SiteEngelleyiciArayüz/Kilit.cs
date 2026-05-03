using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using System.ServiceProcess;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.Timers;
using Microsoft.Toolkit.Uwp.Notifications;
using Amazon.Auth.AccessControlPolicy;
using Microsoft.VisualBasic.ApplicationServices;
using System.Threading;


namespace SiteEngelleyiciArayüz
{
    public partial class Kilit : Form
    {

        private static settings instance;

        public static settings Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new settings();
                }
                return instance;
            }
        }


        private static Kilit instance2;

        public static Kilit Instance2
        {
            get
            {
                if (instance2 == null || instance.IsDisposed)
                {
                    instance2 = new Kilit();
                }
                return instance2;
            }
        }


        bool mousedown;


        private void Kilit_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
        }

        private void Kilit_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                int mousex = MousePosition.X - 110;
                int mousey = MousePosition.Y - 7;
                this.SetDesktopLocation(mousex, mousey);
            }

            // İşlem yapılacak kodlar

            Rectangle screenBounds = Screen.FromControl(this).Bounds; // Formun bulunduğu ekranın boyutunu alıyoruz
            int edgeProximityThreshold = -50; // Kenara olan mesafe eşiği (pixel cinsinden)

            int leftDistance = this.Left; // Sol kenara olan mesafe
            int topDistance = this.Top; // Üst kenara olan mesafe
            int rightDistance = screenBounds.Width - (this.Left + this.Width); // Sağ kenara olan mesafe
            int bottomDistance = screenBounds.Height - (this.Top + this.Height); // Alt kenara olan mesafe
            // Eğer formun herhangi bir köşesi ekranın kenarına yeterince yakınsa işlem gerçekleştir
            if (leftDistance < edgeProximityThreshold ||
                topDistance < edgeProximityThreshold ||
                rightDistance < edgeProximityThreshold ||
                bottomDistance < edgeProximityThreshold)
            {
                //MessageBox.Show("Form ekranın bir kenarına çok yakın!");

                SetFormLocationToBottomRight();
                GC.Collect();
            }
            
        }

        private void Kilit_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }



        static string programNameOrPath = "CaYaSafe32.exe";
        static string programNameOrPath2 = "cyRUN.exe";
        static string programNameOrPath3 = "CaYaExtraSafe.exe";
        static string programNameOrPath4 = "AntiEX.exe";

        // Başka bir programı kapatma işlemi
        Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(programNameOrPath));
        Process[] processes2 = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(programNameOrPath2));
        Process[] processes3 = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(programNameOrPath3));
        Process[] processes4 = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(programNameOrPath4));

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






        public static event EventHandler RequestEvent2;
        
        public Kilit()
        {

            InitializeComponent();

            this.ControlBox = false;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            // Panelin köşelerini yuvarla
            //PanelColor.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, PanelColor.Width, PanelColor.Height, 5, 5));
            SetFormLocationToBottomRight();

            //settings sett = new settings();
            //sett.Show();
            TopMost = true;
            settings.Instance.Show();
            settings.Instance.Hide();



            // Form2'den gelen olaya abone olun.
            settings.RequestEvent += HandleForm2Request;
            //settings.RequestEvent2 += HandleForm3Request;



            GC.Collect();
            

            //EdgeBugRemoval();

            //notifyIcon1.Text = "Teas";
            //notifyIcon1.Visible = true;
            //notifyIcon1.BalloonTipTitle = "CaYaSafe";
            //notifyIcon1.BalloonTipText = "Kilit açıldı!";
            //notifyIcon1.ShowBalloonTip(100);
        }


        private async void EdgeBugRemoval()
        {
            //Thread bgThread = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        // İlgili işlemler
            //        this.Invoke((MethodInvoker)delegate
            //        {
            //            // İşlem yapılacak kodlar

            //            Rectangle screenBounds = Screen.FromControl(this).Bounds; // Formun bulunduğu ekranın boyutunu alıyoruz
            //            int edgeProximityThreshold = -50; // Kenara olan mesafe eşiği (pixel cinsinden)

            //            int leftDistance = this.Left; // Sol kenara olan mesafe
            //            int topDistance = this.Top; // Üst kenara olan mesafe
            //            int rightDistance = screenBounds.Width - (this.Left + this.Width); // Sağ kenara olan mesafe
            //            int bottomDistance = screenBounds.Height - (this.Top + this.Height); // Alt kenara olan mesafe
            //            GC.Collect();
            //            // Eğer formun herhangi bir köşesi ekranın kenarına yeterince yakınsa işlem gerçekleştir
            //            if (leftDistance < edgeProximityThreshold ||
            //                topDistance < edgeProximityThreshold ||
            //                rightDistance < edgeProximityThreshold ||
            //                bottomDistance < edgeProximityThreshold)
            //            {
            //                //MessageBox.Show("Form ekranın bir kenarına çok yakın!");

            //                SetFormLocationToBottomRight();
            //                GC.Collect();
            //            }
            //        });

            //        Thread.Sleep(5000); // 5 saniye bekletme
            //        GC.Collect();
            //    }

            //});

            //await Task.Delay(5000);

            //bgThread.IsBackground = true;
            //bgThread.Start();
        }








        private const int MinWidth = 225;
        private const int MinHeight = 300;
        private const int MaxWidth = 225;
        private const int MaxHeight = 300;

        // WM_SIZE mesajını ele almak için override edilmiş method
        protected override void WndProc(ref Message m)
        {
            const int WM_SIZE = 0x0005;

            if (m.Msg == WM_SIZE)
            {
                // Yeni boyutu al
                int newWidth = (int)(m.LParam.ToInt64() & 0xFFFF);
                int newHeight = (int)(m.LParam.ToInt64() >> 16);

                // Minimum genişlik kontrolü
                if (newWidth < MinWidth)
                {
                    m.LParam = (IntPtr)((newHeight << 16) | MinWidth);
                }

                // Minimum yükseklik kontrolü
                if (newHeight < MinHeight)
                {
                    m.LParam = (IntPtr)((MinHeight << 16) | newWidth);
                }

                // Maksimum genişlik kontrolü
                if (newWidth > MaxWidth)
                {
                    m.LParam = (IntPtr)((newHeight << 16) | MaxWidth);
                }

                // Maksimum yükseklik kontrolü
                if (newHeight > MaxHeight)
                {
                    m.LParam = (IntPtr)((MaxHeight << 16) | newWidth);
                }
            }

            // Ana sınıfın WndProc metodunu çağır
            base.WndProc(ref m);
        }


        

        

        private void HandleForm2Request(object sender, EventArgs e)
        {
            // Form2'den gelen isteği işleyin.
            // Bu metot, Form2'de bir buton tıklandığında çağrılacak.
            //MessageBox.Show("Form2'den istek alındı ve işlem gerçekleştirildi.");

            KontrolEtVeIslemYap();
        }


        private void SetFormLocationToBottomRight()
        {
            // Ekranın sağ alt köşesinin koordinatlarını al
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            // Formun boyutunu ve konumunu ayarla
            //this.Width = 164; // Formun genişliğini isteğinize göre ayarlayabilirsiniz
            //this.Height = 260; // Formun yüksekliğini isteğinize göre ayarlayabilirsiniz

            int formX = screenWidth - this.Width;
            int formY = screenHeight - this.Height;

            this.Location = new System.Drawing.Point(formX, formY);
        }


        string filePath = "C:\\Program Files (x86)\\CaYaSafe\\CaYaSafe32\\CaYaSafe32.exe"; // Açmak istediğiniz dosyanın yolu
        string filePath2 = "C:\\Program Files (x86)\\CaYaSafe\\cyRUN\\cyRUN.exe"; // Açmak istediğiniz dosyanın yolu
        string filePath3 = "C:\\Program Files (x86)\\CaYaSafe\\CaYaExtraSafe\\CaYaExtraSafe.exe"; // Açmak istediğiniz dosyanın yolu
        string filePath4 = "C:\\Program Files (x86)\\CaYaSafe\\AntiEX\\AntiEX.exe";

        private async void button1_Click(object sender, EventArgs e)
        {

            try
            {
                StopService("CYRS");
                Process.Start(filePath4);
                //Process.Start(filePath);
                //Process.Start(filePath2);
            }
            catch (Exception ex)
            {
                // Dosya açma hatası
                Console.WriteLine("Kilitleme Hatası! Bilgisayarı Lütfen Yeniden Başlatın Hata Kodu: " + ex.Message);
            }
            Visible = false;
            await Task.Delay(50000);


            //Environment.Exit(1);

            //Application.Exit();
            //Close();
        }









        // Form2 içinde Form1'e erişim sağlayın
        Form1 form1 = Application.OpenForms["Form1"] as Form1;
        Lock locking = Application.OpenForms["locking"] as Lock;
        private async void Kilit_Load(object sender, EventArgs e)
        {


            StartService("CYRS");

            //foreach (Process process in processes2)
            //{
            //    process.CloseMainWindow(); // Programın ana penceresini kapatmaya çalışır
            //    process.WaitForExit(100); // Programın kapanmasını 5 saniye boyunca bekler
            //    if (!process.HasExited) // Eğer hala çalışıyorsa
            //    {
            //        process.Kill(); // Zorla kapat
            //    }
            //}


            foreach (Process process in processes)
            {
                process.CloseMainWindow(); // Programın ana penceresini kapatmaya çalışır
                process.WaitForExit(100); // Programın kapanmasını 5 saniye boyunca bekler
                if (!process.HasExited) // Eğer hala çalışıyorsa
                {
                    process.Kill(); // Zorla kapat
                }
            }
            foreach (Process process in processes3)
            {
                process.CloseMainWindow(); // Programın ana penceresini kapatmaya çalışır
                process.WaitForExit(100); // Programın kapanmasını 5 saniye boyunca bekler
                if (!process.HasExited) // Eğer hala çalışıyorsa
                {
                    process.Kill(); // Zorla kapat
                }
            }

            foreach (Process process in processes4)
            {
                process.CloseMainWindow(); // Programın ana penceresini kapatmaya çalışır
                process.WaitForExit(100); // Programın kapanmasını 5 saniye boyunca bekler
                if (!process.HasExited) // Eğer hala çalışıyorsa
                {
                    process.Kill(); // Zorla kapat
                }
            }

            if (form1 != null)
            {
                form1.TopMost = false;
                //form1.Visible = false;
                form1.Hide();

                //await Task.Delay(1000);
                //form1.Close();
            }
            if (locking != null)
            {
                locking.TopMost = false;
                //locking.Visible = false;
                locking.Hide();
                //await Task.Delay(1000);
                //locking.Close();
            }


            KontrolEtVeIslemYap();

            // Klasörün bulunduğu dizin
            string klasorYolu = @"C:\Program Files (x86)\CaYa\CaYaWindowBlocker";

            // Eski ve yeni klasör adları
            string eskiKlasorAdi = "AAeklenti";
            string yeniKlasorAdi = "unknown";

            // Eski klasör yolunu oluştur
            string eskiKlasorYolu = Path.Combine(klasorYolu, eskiKlasorAdi);

            try
            {
                // Klasör adını değiştir
                Directory.Move(eskiKlasorYolu, Path.Combine(klasorYolu, yeniKlasorAdi));
                Console.WriteLine("Klasör adı başarıyla değiştirildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Klasör adı değiştirilirken bir hata oluştu: " + ex.Message);
            }


        }




        static void StartService(string serviceName)
        {
            ServiceController serviceController = new ServiceController(serviceName);

            if (serviceController.Status == ServiceControllerStatus.Stopped)
            {
                try
                {
                    serviceController.Start();
                    serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                    Console.WriteLine("Service started successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error starting service: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Service is already running.");
            }
        }

        static void StopService(string serviceName)
        {
            ServiceController serviceController = new ServiceController(serviceName);

            try
            {
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    serviceController.Stop();
                    serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                    //MessageBox.Show("Service stopped successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //MessageBox.Show("Service is not running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping service: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ShowSettingsForm()
        {
            // Eğer settings formu zaten açıksa
            if (Application.OpenForms.OfType<settings>().Any())
            {
                // Formu öne çıkar
                settings.Instance.BringToFront();
                settings.Instance.TopMost = true;
                settings.Instance.TopMost = false;
            }
            else
            {
                // Formu aç
                settings.Instance.Show();
            }
        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            //settings.Instance.Show();
            //settings.Instance.TopMost = true;
            //await Task.Delay(500);
            //settings.Instance.TopMost = false;
            //ShowSettingsForm();
            //TopMost = false;
            settings.Instance.Show();
            //settings.Instance.BringToFront();
            settings.Instance.TopMost = true;
            settings.Instance.TopMost = false;
            settings.Instance.Enabled = true;

            RequestEvent2?.Invoke(this, EventArgs.Empty);

            await Task.Delay(5000);
            //TopMost = true;
        }


        private async void pictureBox12_Click(object sender, EventArgs e)
        {
            settings.Instance.Show();
            settings.Instance.TopMost = true;
            settings.Instance.TopMost = false;
            settings.Instance.Enabled = true;

            RequestEvent2?.Invoke(this, EventArgs.Empty);

            await Task.Delay(5000);
        }


        private async void pictureBox13_Click(object sender, EventArgs e)
        {
            settings.Instance.Show();
            settings.Instance.TopMost = true;
            settings.Instance.TopMost = false;
            settings.Instance.Enabled = true;

            RequestEvent2?.Invoke(this, EventArgs.Empty);

            await Task.Delay(5000);
        }



        private void Kilit_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private async void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                //Process.Start(filePath);
                //Process.Start(filePath2);
                StopService("CYRS");

                // Klasörün bulunduğu dizin
                string klasorYolu = @"C:\Program Files (x86)\CaYa\CaYaWindowBlocker";

                // Eski ve yeni klasör adları
                string eskiKlasorAdi = "unknown";
                string yeniKlasorAdi = "AAeklenti";

                // Eski klasör yolunu oluştur
                string eskiKlasorYolu = Path.Combine(klasorYolu, eskiKlasorAdi);

                try
                {
                    // Klasör adını değiştir
                    Directory.Move(eskiKlasorYolu, Path.Combine(klasorYolu, yeniKlasorAdi));
                    Console.WriteLine("Klasör adı başarıyla değiştirildi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Klasör adı değiştirilirken bir hata oluştu: " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                // Dosya açma hatası
                Console.WriteLine("Kilitleme Hatası! Bilgisayarı Lütfen Yeniden Başlatın Hata Kodu: " + ex.Message);
            }
            Visible = false;
            await Task.Delay(5000);


            Environment.Exit(1);
        }












        private void pictureBox11_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private async void pictureBox8_Click(object sender, EventArgs e)
        {
            try
            {
                //Process.Start(filePath);
                //Process.Start(filePath2);
                StopService("CYRS");

                // Klasörün bulunduğu dizin
                string klasorYolu = @"C:\Program Files (x86)\CaYa\CaYaWindowBlocker";

                // Eski ve yeni klasör adları
                string eskiKlasorAdi = "unknown";
                string yeniKlasorAdi = "AAeklenti";

                // Eski klasör yolunu oluştur
                string eskiKlasorYolu = Path.Combine(klasorYolu, eskiKlasorAdi);

                try
                {
                    // Klasör adını değiştir
                    Directory.Move(eskiKlasorYolu, Path.Combine(klasorYolu, yeniKlasorAdi));
                    Console.WriteLine("Klasör adı başarıyla değiştirildi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Klasör adı değiştirilirken bir hata oluştu: " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                // Dosya açma hatası
                Console.WriteLine("Kilitleme Hatası! Bilgisayarı Lütfen Yeniden Başlatın Hata Kodu: " + ex.Message);
            }
            Visible = false;
            await Task.Delay(5000);


            Environment.Exit(1);
        }




        private void pictureBox16_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private async void pictureBox15_Click(object sender, EventArgs e)
        {
            try
            {
                //Process.Start(filePath);
                //Process.Start(filePath2);
                StopService("CYRS");

                // Klasörün bulunduğu dizin
                string klasorYolu = @"C:\Program Files (x86)\CaYa\CaYaWindowBlocker";

                // Eski ve yeni klasör adları
                string eskiKlasorAdi = "unknown";
                string yeniKlasorAdi = "AAeklenti";

                // Eski klasör yolunu oluştur
                string eskiKlasorYolu = Path.Combine(klasorYolu, eskiKlasorAdi);

                try
                {
                    // Klasör adını değiştir
                    Directory.Move(eskiKlasorYolu, Path.Combine(klasorYolu, yeniKlasorAdi));
                    Console.WriteLine("Klasör adı başarıyla değiştirildi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Klasör adı değiştirilirken bir hata oluştu: " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                // Dosya açma hatası
                Console.WriteLine("Kilitleme Hatası! Bilgisayarı Lütfen Yeniden Başlatın Hata Kodu: " + ex.Message);
            }
            Visible = false;
            await Task.Delay(5000);


            Environment.Exit(1);
        }

        




        private const string themeDosyaAdi = "theme.txt";
        private void ThemeOnR()
        {

            pictureBox6.Visible = true;
            pictureBox7.Visible = true;
            pictureBox18.Visible = true;
            pictureBox3.Visible = true; //PC
            pictureBox5.Visible = true;
            pictureBox4.Visible = true;

        }


        private void ThemeOnB()
        {
            
            pictureBox11.Visible = true;
            pictureBox22.Visible = true;
            pictureBox12.Visible = true; //PC
            pictureBox8.Visible = true;
            pictureBox9.Visible = true;
            pictureBox10.Visible = true;





            pictureBox8.BackColor = ColorTranslator.FromHtml("#191919");
            pictureBox9.BackColor = ColorTranslator.FromHtml("#191919");
            pictureBox10.BackColor = ColorTranslator.FromHtml("#191919");
            pictureBox11.BackColor = ColorTranslator.FromHtml("#191919");
            pictureBox12.BackColor = ColorTranslator.FromHtml("#191919");
            pictureBox22.BackColor = ColorTranslator.FromHtml("#191919");
        }


        private void ThemeOnW()
        {
            pictureBox17.Visible = true;
            pictureBox16.Visible = true;
            pictureBox15.Visible = true;
            pictureBox14.Visible = true;
            pictureBox13.Visible = true;
            pictureBox21.Visible = true;

            pictureBox17.BackColor = ColorTranslator.FromHtml("#e6e6e6");
            pictureBox16.BackColor = ColorTranslator.FromHtml("#e6e6e6");
            pictureBox15.BackColor = ColorTranslator.FromHtml("#e6e6e6");
            pictureBox14.BackColor = ColorTranslator.FromHtml("#e6e6e6");
            pictureBox13.BackColor = ColorTranslator.FromHtml("#e6e6e6");
            pictureBox21.BackColor = ColorTranslator.FromHtml("#e6e6e6");
        }










        private void KontrolEtVeIslemYap()
        {
            // Dosyadan tema değerini oku
            if (File.Exists(themeDosyaAdi))
            {
                string temaDegeriStr = File.ReadAllText(themeDosyaAdi);

                // Tema değerini int'e çevir
                if (int.TryParse(temaDegeriStr, out int temaDegeri))
                {
                    // Tema değerine göre ilgili işlemi gerçekleştir
                    switch (temaDegeri)
                    {
                        case 1:
                            ThemeOn1();
                            break;
                        case 2:
                            ThemeOn2();
                            break;
                        case 3:
                            ThemeOn3();
                            break;
                        default:
                            // Bilinmeyen tema değeri, varsayılan işlemi gerçekleştir
                            ThemeOn1();
                            break;
                    }
                }
                else
                {
                    // Geçersiz tema değeri, varsayılan işlemi gerçekleştir
                    ThemeOn1();
                }
            }
            else
            {
                // Dosya bulunamadı, varsayılan işlemi gerçekleştir
                ThemeOn1();
            }
        }


        private void YazThemeDosyasi(int temaDegeri)
        {
            // Tema dosyasına belirtilen değeri yaz
            try
            {
                using (StreamWriter writer = new StreamWriter(themeDosyaAdi))
                {
                    writer.Write(temaDegeri);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tema dosyasına yazılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ThemeOn1()
        {
            ThemeOff2();
            ThemeOff3();

            ThemeOnR();

        }
        private void ThemeOff1()
        {

            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox3.Visible = false; //PC
            pictureBox5.Visible = false;
            pictureBox4.Visible = false;
            pictureBox18.Visible = false;

        }

        private async void ThemeOn2()
        {
            ThemeOff1();
            ThemeOff3();

            ThemeOnB();

        }



        private void ThemeOff2()
        {

            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false; //PC
            pictureBox22.Visible = false;

        }


        private async void ThemeOn3()
        {
            ThemeOff1();
            ThemeOff2();

            ThemeOnW();

        }

        private void ThemeOff3()
        {

            pictureBox17.Visible = false;
            pictureBox16.Visible = false;
            pictureBox15.Visible = false;
            pictureBox14.Visible = false;
            pictureBox13.Visible = false;
            pictureBox21.Visible = false;

        }


    }
}
