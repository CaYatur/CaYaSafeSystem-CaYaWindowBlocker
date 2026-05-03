using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Authentication;
using MongoDB.Driver.Core.Clusters;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core;
using ZXing;
using ZXing.Common;
using System.Drawing;
using ZXing.QrCode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Runtime.InteropServices;
using MongoDB.Driver.Core.Configuration;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using MongoDB.Driver.Core.Clusters;
using System.Security.Cryptography.X509Certificates;
using System.Runtime;
using System.Net.Security;
using System.Threading;
using Newtonsoft.Json;
using System.Net.WebSockets;
using SharpCompress.Common;
using static System.Windows.Forms.LinkLabel;


namespace SiteEngelleyiciArayüz
{



    public partial class Lock : Form
    {

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

        private static Kilit instance2;

        public static Kilit Instance2
        {
            get
            {
                if (instance2 == null || instance2.IsDisposed)
                {
                    instance2 = new Kilit();
                }
                return instance2;
            }
        }


        private static FRTN instance3;

        public static FRTN Instance3
        {
            get
            {
                if (instance3 == null || instance3.IsDisposed)
                {
                    instance3 = new FRTN();
                }
                return instance3;
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


        private string macAddress;


        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        private bool stopChecking = false;
        private bool alreadyConnected = false;
        private bool OneRun = true;
        private bool Showed = false;



        private readonly HttpListener _listener;



        

        public Lock()
        {
            InitializeComponent();
            StartupFormDesign();
            TopMost = false;
            DevPanel.RequestEventCA += HandleForm2RequestCA;
            DevPanel.RequestEventLOG += HandleForm2RequestLOG;
            DevPanel.RequestEventLOAD += HandleForm2RequestLOAD;
            DevPanel.RequestEventUN += HandleForm2RequestUN;
            DevPanel.RequestEventOut += HandleForm2RequestOut;
            //textBox1.Visible = false;


            //bool isRunningSVR = true;

            //string exeFilePath = @"C:\Program Files (x86)\CaYaSafe\server.exe";
            //Process exeProcess = new Process();
            //exeProcess.StartInfo.FileName = exeFilePath;
            //exeProcess.StartInfo.UseShellExecute = false;
            //exeProcess.StartInfo.CreateNoWindow = true; // Eğer kullanıcı arayüzü olmayan bir konsol uygulamasıysa
            //exeProcess.StartInfo.RedirectStandardOutput = true;

            //exeProcess.Start();

            //Thread bgThread = new Thread(() =>
            //{
            //    while (isRunningSVR && !exeProcess.StandardOutput.EndOfStream)
            //    {
            //        string outputLine = exeProcess.StandardOutput.ReadLine();
            //        // Exe dosyasının konsola yazdığı çıktıyı kontrol etme
            //        if (outputLine.Contains("LockSystemBypass"))
            //        {
            //            // Belirli bir şey yazıldığında C# form uygulamanızda bir işlem gerçekleştirin
            //            // Örneğin: Bir fonksiyonu çağırabilir veya bir işlemi başlatabilirsiniz
            //            this.Invoke((MethodInvoker)delegate
            //            {
            //                StartupLocalHost();
            //            });

            //            Console.WriteLine("Exe dosyası belirli bir şey yazdı!");
            //            // Exe dosyasını kapatma
            //            exeProcess.Kill();
            //            isRunningSVR = false;

            //            // Buraya işlem yapılacak kodu ekleyin
            //        }
            //    }
            //});
            //bgThread.IsBackground = true;
            //bgThread.Start();




            /////////////////////////////DOSYA OLUŞTURMNA SİSTEMİİİİİİİİİİİİİİİİİİİİİİİİİİİİİİİİ
            //string programDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            //string filePath = Path.Combine(programDataFolder, "verifyc.txt");

            //if (!File.Exists(filePath))
            //{
            //    // Dosya yoksa oluştur
            //    try
            //    {
            //        using (StreamWriter sw = File.CreateText(filePath))
            //        {
            //            sw.WriteLine("Bu dosya ProgramData dizininde oluşturuldu.");
            //        }
            //        Console.WriteLine("Dosya oluşturuldu: " + filePath);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Dosya oluşturulurken bir hata oluştu: " + ex.Message);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Dosya zaten var: " + filePath);
            //}

            /////////////////////////////DOSYA OLUŞTURMNA SİSTEMİİİİİİİİİİİİİİİİİİİİİİİİİİİİİİİİYUKARI!!!!!!!!!!!!
            ///
            ///AŞAĞIII!!!

            string programDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string filePath = Path.Combine(programDataFolder, "verifyc.txt");

            // Eğer dosya varsa sil
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Console.WriteLine("Mevcut dosya silindi: " + filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Dosya silinirken bir hata oluştu: " + ex.Message);
                    return; // Hata durumunda işlemi sonlandır
                }
            }

            // Yeni dosya oluştur
            try
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("This computer using CaYaSafeSystem!");
                }
                Console.WriteLine("Yeni dosya oluşturuldu: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dosya oluşturulurken bir hata oluştu: " + ex.Message);
            }


            //WebAutoLogin();

            ///////////DisplayLocalIPv4Address();

            // MongoDB bağlantı adresi - AppSettings.cs içindeki sabitten okunur
            string connectionString = AppSettings.MongoDbConnectionString;

            //GC.Collect();
            //Thread Mongo = new Thread(() =>
            //{

            //});
            //Mongo.IsBackground = true;
            //Mongo.Start();

            try
            {
                MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);


                // Sertifika kullanımını etkinleştir
                //settings.UseTls = true;

                // Sertifika dosyasının yolu
                //settings.SslSettings = new SslSettings
                //{
                //    EnabledSslProtocols = SslProtocols.Tls12,
                //    CheckCertificateRevocation = false,
                //    ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                //    {
                //        // Sertifika doğrulama işlemlerini gerçekleştirin
                //        // Eğer sertifika doğrulama hatası olursa bile bağlantıya izin ver
                //        return errors == SslPolicyErrors.None || true;
                //    }
                //};

                //settings.SslSettings.CheckCertificateRevocation = false;


                // Bağlantı zaman aşımı süresini 10 saniye olarak ayarla
                //settings.ConnectTimeout = TimeSpan.FromSeconds(10);

                // MongoClient oluştur
                client = new MongoClient(settings);

                // Veritabanı ve collection adları
                string databaseName = "CaYa";
                string collectionName = "CaYa";

                // MongoDB veritabanına bağlan
                database = client.GetDatabase(databaseName);

                // MongoDB collection'ına erişim
                collection = database.GetCollection<BsonDocument>(collectionName);

            }
            catch (Exception ex)
            {
                // Hata ayıklama mesajını göster
                MessageBox.Show($"Bağlantı hatası: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }




            pictureBox17.Visible = false;
            pictureBox7.Visible = false;

            TopMost = true;
            macAddress = GetLocalIPAddress();

            //Task.Run(() => CheckDataPeriodically());
            CaYaStartup();
            this.KeyPreview = true; // Klavye olaylarını form üzerinde kullanılabilir kıl

            //this.KeyDown += Lock_KeyDown; // KeyDown olayına abone ol
            label8.Text = string.Empty;
            ServerSystem();


        }

        private async void HandleForm2RequestCA(object sender, EventArgs e)
        {
            // Form2'den gelen isteği işleyin.
            // Bu metot, Form2'de bir buton tıklandığında çağrılacak.
            //MessageBox.Show("Form2'den istek alındı ve işlem gerçekleştirildi.");

            GC.Collect();
            // "developermode" yazıldığında yapılacak işlemler
            //MessageBox.Show("Developer Mode Aktif!");
            Developer.Visible = true;
            DeveloperModeActivated = true; // Developer mode'u aç
            label8.Text = ""; // Label'ı sıfırla
            Connection.Visible = true;
            label3.Visible = true;
            label2.Visible = true;
            preview.Text = "Lütfen Bekleyiniz.";
            CodeBox = false;
            await Task.Delay(10);
            preview.Text = "Geliştirici Modu Aktif.";
            CodeBox = false;
            GC.Collect();
        }

        private async void HandleForm2RequestLOG(object sender, EventArgs e)
        {
            GC.Collect();
            DeleteIP();
            Opening();
            await Task.Delay(200);
            await CaYaSystem14();
        }
        private async void HandleForm2RequestLOAD(object sender, EventArgs e)
        {
            Opening();
        }
        private async void HandleForm2RequestUN(object sender, EventArgs e)
        {
            GC.Collect();
            //FRTN.Instance3.Show();
            DeleteIP();

            button2.Visible = false;
            //Hide();
            //Form1.Instance4.Hide();
            label17.Text = "İzinsiz Giriş Yapılmaya Çalışıldı!";





            // Dosyanın oluşturulacağı dizin
            string dizin = @"C:\ProgramData"; // Kullanıcı adınıza göre değiştirin

            // Dosyanın adı
            string dosyaAdi = "UNAUTH";

            // Dosyanın tam yolu
            string dosyaYolu = Path.Combine(dizin, dosyaAdi);

            // Dosyayı oluştur
            try
            {
                // FileStream kullanarak dosyayı oluştur
                using (FileStream fs = File.Create(dosyaYolu))
                {
                    Console.WriteLine("Dosya oluşturuldu: " + dosyaYolu);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dosya oluşturulurken bir hata oluştu: " + ex.Message);
            }

            //await Task.Delay(550);
            //await Task.Delay(10000);
            //Environment.Exit(1);
            MessageBox.Show("İZİNSİZ GİRİŞ İŞLEMİ BAŞARISIZ! BİLGİSAYAR KİTLENDİ!");
        }
        private async void HandleForm2RequestOut(object sender, EventArgs e)
        {

            GC.Collect();
            label8.Text = "";
            Connection.Text = "Logout For DeveloperMode";
            Connection.ForeColor = Color.OrangeRed;
            await Task.Delay(5000);
            Connection.Text = "CaYaSafe Hizmetlerine Bağlı Olup Olunmadığı Bilinmiyor.";
            Connection.ForeColor = Color.Gray;
            //pictureBox2.Visible = false;
            Developer.Visible = false;
            DeveloperModeActivated = false;

        }

        private async void ServerSystem()
        {
            GC.Collect();
            Thread bgThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);


                    string filePath = @"C:\ProgramData\verifyc.txt";
                    string[] lines = null;

                    try
                    {
                        // Dosyayı oku
                        lines = File.ReadAllLines(filePath);

                        for (int i = 0; i < lines.Length; i++)
                        {
                            string line = lines[i];

                            // "verified" içeren satırları kontrol et
                            if (line.Contains("verified"))
                            {
                                // Satırdan tarih bilgisini çıkar
                                string dateString = line.Split('(')[1].Split(')')[0].Trim();
                                DateTime verifiedTime = DateTime.ParseExact(dateString, "yyyy.MM.dd.HH.mm", null);

                                // Şu anki zamanı al
                                DateTime currentTime = DateTime.Now;

                                // Zaman farkını kontrol et (2 dakikadan az ise)
                                TimeSpan timeDifference = currentTime - verifiedTime;
                                if (timeDifference.TotalMinutes < 2)
                                {
                                    // Zamanı geçmiş ise satırı sil
                                    lines[i] = string.Empty;

                                    this.Invoke((MethodInvoker)async delegate
                                    {
                                        StartupLocalHost();
                                    });
                                }
                            }
                        }

                        // Geçerli olmayan satırları temizle
                        lines = Array.FindAll(lines, s => !string.IsNullOrEmpty(s));

                        // Dosyayı güncelle
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

        //private HttpListener listener;

        //private async void WebAutoLogin()
        //{
        //    listener = new HttpListener();
        //    listener.Prefixes.Add("http://*:4728/"); // Dinlenecek adresi belirtme (80 portu)
        //    listener.Start();
        //    Console.WriteLine("Listening...");

        //    await ListenAsync();
        //}




        //private async Task ListenAsync()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            // İstek geldiğinde işleme alma
        //            HttpListenerContext context = await listener.GetContextAsync();
        //            Task.Run(() =>
        //            {
        //                try
        //                {
        //                    // İstek bilgilerini alıyoruz
        //                    HttpListenerRequest request = context.Request;
        //                    HttpListenerResponse response = context.Response;
        //                    string url = request.Url.AbsolutePath;

        //                    // Belirli bir URL'ye yönlendirme yapıldı mı kontrol ediyoruz
        //                    if (url == "/cysfsy")
        //                    {
        //                        // Yönlendirme yapıldıysa bir işlem gerçekleştir
        //                        // Örneğin: Bir TextBox kontrolünün görünürlüğünü değiştirebiliriz
        //                        this.Invoke((MethodInvoker)delegate
        //                        {
        //                            StartupLocalHost();
        //                        });
        //                    }

        //                    // Başarılı yanıt gönderme
        //                    string responseString = "<html><body>AutoLoginSystem CaYaSafe</body></html>";
        //                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        //                    response.ContentLength64 = buffer.Length;
        //                    System.IO.Stream output = response.OutputStream;
        //                    output.Write(buffer, 0, buffer.Length);
        //                    output.Close();
        //                }
        //                catch (Exception ex)
        //                {
        //                    this.Invoke((MethodInvoker)delegate
        //                    {
        //                        // Hata durumunda da TextBox kontrolünde hata mesajı gösterebiliriz
        //                        textBox1.AppendText("Hata: " + ex.Message + "\n");
        //                    });
        //                }
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Invoke((MethodInvoker)delegate
        //        {
        //            // Hata durumunda da TextBox kontrolünde hata mesajı gösterebiliriz
        //            textBox1.AppendText("Hata: " + ex.Message + "\n");
        //        });
        //    }
        //}

        private async void StartupLocalHost()
        {
            GC.Collect();
            Opening();
            await Task.Delay(100);
            DeleteIP();
            await Task.Delay(1000);
            CaYaSystem14();
        }





        private const int MinWidth = 750;
        private const int MinHeight = 450;
        private const int MaxWidth = 750;
        private const int MaxHeight = 450;

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






        private void CheckAndPerformAction()
        {
            // MongoDB'den veriyi kontrol et
            var filter = Builders<BsonDocument>.Filter.Eq("ipAddress", "example_ip_address");
            var document = collection.Find(filter).FirstOrDefault();

            if (document != null)
            {
                bool isConnected = document["isConnected"].AsBoolean;

                if (isConnected)
                {
                    MessageBox.Show("Bağlantı başarılı. İşlem gerçekleştiriliyor.");

                    // Burada istediğiniz işlemi gerçekleştirin, örneğin Not Defteri açma
                    System.Diagnostics.Process.Start("notepad.exe");
                }
                else
                {
                    MessageBox.Show("Bağlantı başarısız.");
                }
            }
            else
            {
                MessageBox.Show("Belirtilen IP adresi bulunamadı.");
            }
        }


        private string Developermode = "CYDEVELOPERMODE";
        private string LoginBypass = "BYPASSLOGIN";
        private string CMDMode = "CMD";
        private bool DeveloperModeActivated = false;

        private async void Lock_KeyDown(object sender, KeyEventArgs e)
        {
            label8.Text += e.KeyCode.ToString();

            if (DeveloperModeActivated)
            {

                // Eğer developer mode zaten aktifse başka bir işlem gerçekleştir
                if (label8.Text == LoginBypass)
                {
                    GC.Collect();
                    label8.Text = "";
                    // "bypasslogin" yazıldığında yapılacak işlemler
                    //MessageBox.Show("Bypass Login Aktif!");
                    //MessageBox.Show("Bypass Login Aktif!");
                    pictureBox2.Visible = true;
                    Connection.Text = "Login For Developer System";
                    Connection.ForeColor = Color.Gold;
                    DeleteIP();
                    Opening();
                    await Task.Delay(5000);
                    CaYaSystem14();
                }
                else if (label8.Text == CMDMode)
                {
                    GC.Collect();
                    label8.Text = "";
                    Process.Start("cmd.exe");
                }
                else if (label8.Text == "SHOWCODE")
                {
                    GC.Collect();
                    label8.Text = "";
                    Connection.Text = "This Is One Time Activation Code: " + randomCode;
                    Connection.ForeColor = Color.OrangeRed;
                }
                else if (label8.Text == "EXPLORER")
                {
                    GC.Collect();
                    label8.Text = "";
                    Connection.Text = "Explorer.exe Has Been Activated";
                    Connection.ForeColor = Color.OrangeRed;
                    Process.Start("explorer.exe");
                }
                else if (label8.Text == "CLOSE")
                {
                    GC.Collect();
                    label8.Text = "";
                    Connection.Text = "Logout For DeveloperMode";
                    Connection.ForeColor = Color.OrangeRed;
                    await Task.Delay(5000);
                    Connection.Text = "CaYaSafe Hizmetlerine Bağlı Olup Olunmadığı Bilinmiyor.";
                    Connection.ForeColor = Color.Gray;
                    //pictureBox2.Visible = false;
                    Developer.Visible = false;
                    DeveloperModeActivated = false;
                }
                else if (label8.Text == "EXIT")
                {
                    GC.Collect();
                    label8.Text = "";
                    Process.Start("explorer.exe");
                    Connection.Text = "Closing This Program";
                    Connection.ForeColor = Color.OrangeRed;
                    Application.Exit();
                }
                else if (label8.Text == "FORTNITE")
                {
                    GC.Collect();
                    label8.Text = "";
                    FRTN.Instance3.Show();
                }
                else if (label8.Text == "FIVEFORTNITE")
                {
                    GC.Collect();
                    label8.Text = "";

                    for (int i = 0; i < 5; i++)
                    {
                        FRTN FRTN2 = new FRTN();
                        FRTN2.Show();
                    }
                }
                else if (label8.Text == "LOADING")
                {
                    GC.Collect();
                    label8.Text = "";

                    Opening();
                }
                else if (label8.Text == "SHOWCOMMAND")
                {
                    GC.Collect();
                    label8.Text = "";

                    label6.Visible = true;
                    label1.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;
                    label12.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    label15.Visible = true;
                    label16.Visible = true;

                    await Task.Delay(30000);

                    label6.Visible = false;
                    label1.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;
                    label12.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                }



                // Diğer işlemleri buraya ekleyebilirsiniz
                //DeveloperModeActivated = false; // Developer mode'u kapat
                //label8.Text = ""; // Label'ı sıfırla
            }
            else
            {
                if (label8.Text == Developermode)
                {
                    GC.Collect();
                    // "developermode" yazıldığında yapılacak işlemler
                    //MessageBox.Show("Developer Mode Aktif!");
                    Developer.Visible = true;
                    DeveloperModeActivated = true; // Developer mode'u aç
                    label8.Text = ""; // Label'ı sıfırla
                    Connection.Visible = true;
                    label3.Visible = true;
                    label2.Visible = true;
                    preview.Text = "Lütfen Bekleyiniz.";
                    CodeBox = false;
                    await Task.Delay(10);
                    preview.Text = "Geliştirici Modu Aktif.";
                    CodeBox = false;
                    GC.Collect();
                }
            }
        }

        private void Developer_Click(object sender, EventArgs e)
        {
            label8.Text = "";
            GC.Collect();
        }



        private async void CaYaStartup()
        {
            DeleteIP();
            await Task.Delay(200);

            string ipAddress = GetLocalIPAddress();
            label7.Text = ipAddress;
            //label7.Text = "Uzaktan Bu Cihazı Aç: ";
            //label1.Text = ipAddress;

            await Task.Delay(100);
            CheckDataPeriodically();
            await Task.Delay(50);
            GenerateRandomCode();
            await Task.Delay(20);
            //label6.Visible = false; 
            await DisplayQRCode();
            GC.Collect();








        }



        private async void CheckDataPeriodically()
        {
            while (!stopChecking)
            {
                try
                {
                    string ipAddress = GetLocalIPAddress();
                    var filter = Builders<BsonDocument>.Filter.Eq("ipAddress", ipAddress);
                    var document = await collection.Find(filter).FirstOrDefaultAsync();

                    if (document != null)
                    {
                        bool isConnected = document["isConnected"].AsBoolean;

                        if (isConnected && !alreadyConnected)
                        {
                            alreadyConnected = true;

                            Process[] processes = Process.GetProcessesByName("explorer");
                            if (processes.Length == 0)
                            {

                                // Yapılacak işlem sadece bir kez gerçekleştirilecek
                                //pictureBox2.Visible = true;
                                GC.Collect();
                                pictureBox17.Visible = true;
                                Connection.Text = "CaYaSafe Hizmetlerine Başarılı Bir Şekilde Bağlanıldı!";
                                Connection.ForeColor = Color.Green;
                                Opening();
                                await Task.Delay(100);
                                DeleteIP();
                                await Task.Delay(1000);
                                CaYaSystem14();

                            }

                        }
                        else if (!isConnected)
                        {
                            if (!isConnected && !Showed)
                            {
                                Showed = true;
                                //pictureBox2.Visible = true;
                                pictureBox17.Visible = true;
                                Connection.Text = "CaYaSafe Hizmetlerine Başarılı Bir Şekilde Bağlanıldı!";
                                Connection.ForeColor = Color.Green;
                                GC.Collect();
                            }


                            // DoNotConnectedOperation();
                        }

                        if (string.IsNullOrEmpty(preview.Text))
                        {
                            preview.Text = "Şifrenizi Girin";
                            CodeBox = false;
                            GC.Collect();
                            // label1 boşsa burada yapılacak işlemleri ekleyin
                            // Örneğin:
                            // MessageBox.Show("Label boş!");
                            // veya başka bir işlem gerçekleştirin
                        }
                        else
                        {
                            // label1 boş değilse burada yapılacak işlemleri ekleyin
                            // Örneğin:
                            // MessageBox.Show("Label dolu: " + label1.Text);
                            // veya başka bir işlem gerçekleştirin
                        }


                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda gerekli işlemleri burada yapabilirsiniz
                    // MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                await Task.Delay(100); // Belirli bir süre bekleyerek tekrar kontrol et
            }
        }

        private async void Opening()
        {

            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            if (form1 != null)
            {
                // Form1'i minimize et
                //form1.WindowState = FormWindowState.Minimized;

                form1.ShowInTaskbar = false;
                //form1.Dispose();
                GC.WaitForPendingFinalizers();
                form1.WindowState = FormWindowState.Minimized;
                form1.Visible = false;
            }

            GC.Collect();
            pictureBox8.Visible = true;
            Logining.Visible = true;
            pictureBox16.Visible = false;
            pictureBox17.Visible = false;

            QRCodeLogOut();
            ManuelLogout();
            Lock.Instance.Show();
            Lock.Instance.TopMost = true;



            pictureBox1.Visible = false;
            Connection.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            //label4.Visible = false;
            //button1.Visible = false;
            button2.Visible = false;
            pictureBox7.Visible = false;
            Number0.Visible = false;
            Number1.Visible = false;
            Number2.Visible = false;
            Number3.Visible = false;
            Number4.Visible = false;
            Number5.Visible = false;
            Number6.Visible = false;
            Number7.Visible = false;
            Number8.Visible = false;
            Number9.Visible = false;
            Clear.Visible = false;
            preview.Visible = false;
            //label6.Visible = false;

            ////Show
            ///
            //label5.Visible = true;
            GC.Collect();
        }

        private async void DeleteIP()
        {
            string ipAddressToDelete = GetLocalIPAddress();
            await DeleteByIpAddress(ipAddressToDelete);

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







        private void DoConnectedOperation()
        {
            DeleteDocumentByLocalIpAddress().Wait();
        }



        private void DoNotConnectedOperation()
        {
            // IP bağlı değilken yapılacak işlemler burada
            Console.WriteLine("IP Bağlı Değil!");
        }

        // Diğer metotlar ve olaylar burada...

        private async Task DeleteDocumentByLocalIpAddress()
        {
            string localIpAddress = GetLocalIPAddress();

            var filter = Builders<BsonDocument>.Filter.Eq("ipAddress", localIpAddress);
            var result = await collection.DeleteOneAsync(filter);

            if (result.DeletedCount > 0)
            {
                Console.WriteLine($"Belge başarıyla silindi: {localIpAddress}");
            }
            else
            {
                Console.WriteLine($"Belge bulunamadı: {localIpAddress}");
            }
        }


        private string CodeForDisconnect = "1234";
        private bool Disconnect = false;

        private async void preview_TextChanged(object sender, EventArgs e)
        {
            string enteredPassword = preview.Text;
            string enteredQRCode = preview.Text;

            string ipAddress = GetLocalIPAddress();

            if (enteredQRCode == randomCode)
            {
                //DeleteIP();
                //Opening();

                ////DialogResult result = MessageBox.Show("İŞLEM BAŞARILI!", "CaYa©", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //await Task.Delay(500);

                ////ShowInTaskbar = false;
                ////WindowState = FormWindowState.Minimized;

                //CaYaSystem14();
            }
            else if (enteredQRCode == CodeForDisconnect)
            {
                button4.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                if (Disconnect == false)
                {
                    button4.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                    button4.Visible = true;
                }
                else
                {
                    button4.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                    button4.Visible = true;
                    button4.Text = "Hizmetler Tekrar Etkinleştirilemez.";
                    //Enabled = false;
                }
            }
            else
            {
                button4.Visible = false;
                //MessageBox.Show("HATA! Yanlış QR Kod veya Şifre");
            }
        }



        private string CaYa = "CY14CaYa.CaYatur67"; // Doğru şifre
        private string randomCode;
        private string FakerandomCode;
        private string Fake2randomCode;
        private string Fake3randomCode;
        private string Fake4randomCode;



        private void GenerateRandomCode()
        {
            Random random = new Random();
            randomCode = random.Next(100000, 999999).ToString();
            FakerandomCode = random.Next(100000, 999999).ToString();
            Fake2randomCode = random.Next(1000, 9999).ToString();
            Fake3randomCode = random.Next(1000, 99999).ToString();
            Fake4randomCode = random.Next(1000, 99999).ToString();
        }




        public static string OneTimeCode;


        private async Task DisplayQRCode()
        {
            string ipAddress = GetLocalIPAddress(); // PC'nin IP adresini al
            string ipAddressv4 = await GetLocalIPv4Async();
            //string ipAddressv4 = await GetIPv4Address();
            string transformedCode = TransformCode(randomCode);

            OneTimeCode = randomCode;

            label2.Text = transformedCode;

            label18.Text = ipAddressv4;

            string qrData = $"IpAddress:{ipAddress} Ipv4Address:{ipAddressv4} UnkownSerial:{FakerandomCode}-{Fake2randomCode}-{Fake3randomCode}-{Fake4randomCode}  Encrypt Code: {transformedCode}  Decrypt: {FakerandomCode}";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // PictureBox boyutuna göre QR kodunu yeniden boyutlandır
            int qrCodeSize = Math.Min(pictureBox1.Width, pictureBox1.Height);
            Bitmap qrCodeImage = await Task.Run(() => qrCode.GetGraphic(5));
            qrCodeImage = await Task.Run(() => ResizeImage(qrCodeImage, qrCodeSize, qrCodeSize));

            pictureBox1.Image = qrCodeImage;

            await AddToDatabase(ipAddress); // IP adresini veritabanına ekle
        }


        // Resmi belirli boyutlara göre yeniden boyutlandıran yöntem
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



        private string TransformCode(string code)
        {
            // Sayıları harf kombinasyonlarına dönüştür
            string transformedCode = "";
            foreach (char digit in code)
            {
                switch (digit)
                {
                    case '1':
                        transformedCode += "a";
                        break;
                    case '2':
                        transformedCode += "y";
                        break;
                    case '3':
                        transformedCode += "d";
                        break;
                    case '4':
                        transformedCode += "s";
                        break;
                    case '5':
                        transformedCode += "k";
                        break;
                    case '6':
                        transformedCode += "p";
                        break;
                    case '7':
                        transformedCode += "z";
                        break;
                    case '8':
                        transformedCode += "x";
                        break;
                    case '9':
                        transformedCode += "w";
                        break;
                    case '0':
                        transformedCode += "e";
                        break;
                    default:
                        transformedCode += digit;
                        break;
                }
            }
            return transformedCode;
        }



        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            Authenticate();
        }

        private void preview_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Handle the Enter key press
                e.IsInputKey = true; // Mark the event as handled
                GC.Collect();
                Authenticate(); // Call the Authenticate method
            }
        }







        private async void Authenticate()
        {

            //ClientWebSocket webSocket = new ClientWebSocket();
            //CancellationToken cancellationToken = new CancellationToken();

            //await webSocket.ConnectAsync(new Uri(AppSettings.TwoFactorAuthServerUrl), cancellationToken);
            //// Bağlantı başarıyla yapıldı

            //// Server'dan mesaj alınması
            //byte[] receiveBuffer = new byte[1024];
            //var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), cancellationToken);

            //string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
            ////MessageBox.Show($"Sunucudan gelen mesaj: {receivedMessage}");

            //// JSON mesajın içinden sadece password kısmını al
            //dynamic json = JsonConvert.DeserializeObject(receivedMessage);
            //string password = json.password;

            string enteredPassword = preview.Text;
            string enteredQRCode = preview.Text;
            string password = null; // password'u tanımlıyoruz, ancak henüz değeri yok

            string ipAddress = GetLocalIPAddress();

            Uri uri = new Uri(AppSettings.TwoFactorAuthServerUrl);
            ClientWebSocket webSocket = new ClientWebSocket();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            try
            {
                // Bağlantı başlat
                await webSocket.ConnectAsync(uri, cancellationToken);

                // Bağlantı başarılı bir şekilde kuruldu, mesajı al
                byte[] receiveBuffer = new byte[1024];
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), cancellationToken);

                string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                //MessageBox.Show($"Sunucudan gelen mesaj: {receivedMessage}");

                // JSON mesajın içinden sadece password kısmını al
                dynamic json = JsonConvert.DeserializeObject(receivedMessage);
                password = json.password; // password'u burada atıyoruz
            }
            catch (OperationCanceledException)
            {
                // İşlem zaman aşımına uğradı
                // Zaman aşımıyla ilgili uygun işlemleri gerçekleştirin
            }
            catch (Exception ex)
            {
                // Diğer hata durumları için genel bir hata işleme mekanizması
                // Hatanın türüne ve duruma göre uygun işlemleri gerçekleştirin
            }
            finally
            {
                // Temizlik işlemleri
               // if (webSocket.State == WebSocketState.Open)
               //     await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
               // webSocket.Dispose();
            }



            if (enteredPassword == CaYa || enteredQRCode == randomCode || password == preview.Text)
            {
                GC.Collect();
                DeleteIP();
                //DialogResult result = MessageBox.Show("İŞLEM BAŞARILI!", "CaYa©", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Opening();
                await Task.Delay(200);

                //ShowInTaskbar = false;
                //WindowState = FormWindowState.Minimized;
                if (webSocket.State == WebSocketState.Open)
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationToken);
                webSocket.Dispose();

                await CaYaSystem14();
            }
            else if (enteredQRCode == FakerandomCode || enteredQRCode == Fake2randomCode || enteredQRCode == Fake3randomCode || enteredQRCode == Fake4randomCode)
            {
                GC.Collect();
                //FRTN.Instance3.Show();
                DeleteIP();

                button2.Visible = false;
                //Hide();
                //Form1.Instance4.Hide();
                label17.Text = "İzinsiz Giriş Yapılmaya Çalışıldı!";





                // Dosyanın oluşturulacağı dizin
                string dizin = @"C:\ProgramData"; // Kullanıcı adınıza göre değiştirin

                // Dosyanın adı
                string dosyaAdi = "UNAUTH";

                // Dosyanın tam yolu
                string dosyaYolu = Path.Combine(dizin, dosyaAdi);

                // Dosyayı oluştur
                try
                {
                    // FileStream kullanarak dosyayı oluştur
                    using (FileStream fs = File.Create(dosyaYolu))
                    {
                        Console.WriteLine("Dosya oluşturuldu: " + dosyaYolu);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Dosya oluşturulurken bir hata oluştu: " + e.Message);
                }

                //await Task.Delay(550);
                //await Task.Delay(10000);
                //Environment.Exit(1);
                MessageBox.Show("İZİNSİZ GİRİŞ İŞLEMİ BAŞARISIZ! BİLGİSAYAR KİTLENDİ!");
            }
            else
            {
                MessageBox.Show("HATA! Yanlış QR Kod veya Şifre");
            }
        }

        private async void UpdateMongoDB(string ipAddress)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("ipAddress", ipAddress);
            var update = Builders<BsonDocument>.Update.Set("isConnected", true);
            await collection.UpdateOneAsync(filter, update);

            Console.WriteLine("MongoDB güncellendi.");
        }
        private async Task AddToDatabase(string ipAddress)
        {
            try
            {
                if (collection != null)
                {
                    var document = new BsonDocument
                    {
                        { "ipAddress", ipAddress },
                        { "isConnected", false }
                    };

                    await collection.InsertOneAsync(document);
                    //MessageBox.Show("Veri MongoDB'ye eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Console.WriteLine("Hata: collection değişkeni null.");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void SaveInitialData()
        {
            string ipAddress = GetLocalIPAddress();

            var document = new BsonDocument
            {
                { "ipAddress", ipAddress },
                { "isConnected", false }
            };

            await collection.InsertOneAsync(document);

            Console.WriteLine("İlk veri MongoDB'ye eklendi.");
        }



        private async void CMD()
        {
            string enteredPassword = preview.Text;
            string enteredQRCode = preview.Text;

            if (enteredPassword == CaYa || enteredQRCode == randomCode)
            {
                DeleteIP();
                DialogResult result = MessageBox.Show("İŞLEM BAŞARILI! CMD BAŞLATILIYOR...", "CaYa©", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("cmd");
                GC.Collect();
                //form1.Visible = false;
                //Visible = false;
            }
            else if (enteredQRCode == FakerandomCode || enteredQRCode == Fake2randomCode || enteredQRCode == Fake3randomCode || enteredQRCode == Fake4randomCode)
            {
                GC.Collect();
                //FRTN.Instance3.Show();
                //DeleteIP();
                //MessageBox.Show("İZİNSİZ GİRİŞ İŞLEMİ BAŞARISIZ! BİLGİSAYAR KİTLENDİ!");


                //Visible = false;
                //await Task.Delay(10000);
                //Environment.Exit(1);

                //FRTN.Instance3.Show();
                DeleteIP();
                MessageBox.Show("İZİNSİZ GİRİŞ İŞLEMİ BAŞARISIZ! BİLGİSAYAR KİTLENDİ!");
                button2.Visible = false;
                //Hide();
                //Form1.Instance4.Hide();
                label17.Text = "İzinsiz Giriş Yapılmaya Çalışıldı! CMD";

                //await Task.Delay(550);
                //await Task.Delay(10000);
                //Environment.Exit(1);
            }
            else
            {
                MessageBox.Show("HATA! Yanlış QR Kod veya Şifre");
            }
        }




        private void DisplayLocalIPv4Address()
        {
            // Tüm ağ arabirimlerini al
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            // IPv4 adresleri için döngü
            foreach (NetworkInterface adapter in nics)
            {
                // Yalnızca etkin ve IPv4 destekleyen arabirimleri kontrol et
                if (adapter.OperationalStatus == OperationalStatus.Up && adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    // Arabirime ait tüm IP adreslerini al
                    var ipProps = adapter.GetIPProperties();
                    var addresses = ipProps.UnicastAddresses.Where(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                    // İlk IPv4 adresini al ve yazdır
                    var ipv4Address = addresses.FirstOrDefault()?.Address.ToString();
                    if (ipv4Address != null)
                    {
                        //label1.Text = "IPv4 Adres: " + ipv4Address;

                        return;
                    }
                }
            }

            // Hiçbir IPv4 adresi bulunamadıysa
            //label1.Text = "IPv4 Adresi Bulunamadı";
        }


        //private string GetLocalIPv4Address()
        //{
        //    // Tüm ağ arabirimlerini al
        //    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

        //    // IPv4 adresleri için döngü
        //    foreach (NetworkInterface adapter in nics)
        //    {
        //        // Yalnızca etkin ve IPv4 destekleyen arabirimleri kontrol et
        //        if (adapter.OperationalStatus == OperationalStatus.Up && adapter.Supports(NetworkInterfaceComponent.IPv4))
        //        {
        //            // Arabirime ait tüm IP adreslerini al
        //            var ipProps = adapter.GetIPProperties();
        //            var addresses = ipProps.UnicastAddresses.Where(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        //            // İlk IPv4 adresini al ve döndür
        //            var ipv4Address = addresses.FirstOrDefault()?.Address.ToString();
        //            if (ipv4Address != null)
        //            {
        //                return ipv4Address;
        //            }
        //        }
        //    }

        //    // Hiçbir IPv4 adresi bulunamadıysa
        //    return "IPv4 Adresi Bulunamadı";


        //}


        //static IPAddress GetIPv4Address()
        //{
        //    // Host adını al
        //    string hostName = Dns.GetHostName();

        //    // IP adreslerini al
        //    IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

        //    // IPv4 adreslerini filtrele
        //    foreach (IPAddress ipv4Address in ipAddresses)
        //    {
        //        if (ipv4Address.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return ipv4Address;
        //        }
        //    }

        //    // IPv4 adresi bulunamadı
        //    return null;
        //}


        static async Task<string> GetLocalIPv4Async()
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
                    ipAddress = await reader.ReadToEndAsync();
                }
            }

            return ipAddress.Trim();
        }


        static async Task<string> GetIPv4Address()
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



        //////////////MACADRESSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
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


        /// /GetLocalMACAddress()













        // Diğer metotlar ve olaylar burada...











        private async Task CaYaSystem14()
        {
            if (OneRun)
            {
                OneRun = false;
                Process.Start("explorer.exe");

                await Task.Delay(2050); // Toplam 2050 milisaniye bekleyecek

                Locking();
            }
        }

        private async Task Locking()
        {
            Visible = false;
            WindowState = FormWindowState.Minimized;

            await Task.Delay(1100); // Toplam 1100 milisaniye bekleyecek

            Kilit.Instance2.Show();

            await Task.Delay(1500); // Toplam 1500 milisaniye bekleyecek

            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            if (form1 != null)
            {
                form1.ShowInTaskbar = false;
                form1.WindowState = FormWindowState.Minimized;
                form1.Visible = false;
            }
        }


        // /////////ÖNCEKİ BENİM YAZDIĞIM KOD::::
        //private async void CaYaSystem14()
        //{
        //    if (OneRun)
        //    {
        //        OneRun = false;
        //        Process.Start("explorer.exe");

        //        await Task.Delay(1500);


        //        await Task.Delay(550);


        //        GC.WaitForPendingFinalizers();
        //        await Task.Delay(100);
        //        Locking();
        //    }

        //}

        //private async void Locking()
        //{
        //    Visible = false;
        //    WindowState = FormWindowState.Minimized;
        //    //Dispose();

        //    // Form2'yi yok etmek için


        //    await Task.Delay(100);
        //    Kilit.Instance2.Show();


        //    await Task.Delay(1000);
        //    // Form2 içinde Form1'e erişim sağlayın
        //    Form1 form1 = Application.OpenForms["Form1"] as Form1;
        //    if (form1 != null)
        //    {
        //        // Form1'i minimize et
        //        //form1.WindowState = FormWindowState.Minimized;

        //        form1.ShowInTaskbar = false;
        //        //form1.Dispose();
        //        GC.WaitForPendingFinalizers();
        //        form1.WindowState = FormWindowState.Minimized;
        //        form1.Visible = false;
        //    }
        //    await Task.Delay(500);

        //    //Dispose();






        //    //Kilit K = new Kilit();
        //    //K.ShowDialog();
        //    //this.Close();

        //}



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Lock_Load(object sender, EventArgs e)
        {

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

        private async void button2_Click(object sender, EventArgs e)
        {
            Form1.Instance4.Visible = true;
            //DeleteIP();
            //await Task.Delay(100);
            Lock.Instance.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }




        private bool CodeBox = false;

        private void SayiEkle(int sayi)
        {
            // Eğer textbox boşsa sadece sayıyı ekle
            if (string.IsNullOrEmpty(preview.Text))
            {
                preview.Text = sayi.ToString();
            }
            else
            {
                if (!CodeBox)
                {
                    CodeBox = true;
                    preview.Text = "";
                }

                // TextBox'taki mevcut sayıyı al
                string mevcutSayi = preview.Text;
                string previewtext = preview.Text;

                // Yeni sayıyı mevcut sayının sonuna ekle
                //textBox1.Text = mevcutSayi + sayi.ToString();
                preview.Text = mevcutSayi + sayi.ToString();
            }
        }



        private void Number0_Click(object sender, EventArgs e)
        {
            SayiEkle(0);
        }

        private void Number1_Click(object sender, EventArgs e)
        {
            SayiEkle(1);
        }

        private void Number2_Click(object sender, EventArgs e)
        {
            SayiEkle(2);
        }

        private void Number3_Click(object sender, EventArgs e)
        {
            SayiEkle(3);
        }

        private void Number4_Click(object sender, EventArgs e)
        {
            SayiEkle(4);
        }

        private void Number5_Click(object sender, EventArgs e)
        {
            SayiEkle(5);
        }

        private void Number6_Click(object sender, EventArgs e)
        {
            SayiEkle(6);
        }

        private void Number7_Click(object sender, EventArgs e)
        {
            SayiEkle(7);
        }

        private void Number8_Click(object sender, EventArgs e)
        {
            SayiEkle(8);
        }

        private void Number9_Click(object sender, EventArgs e)
        {
            SayiEkle(9);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            preview.ResetText();
            textBox1.ResetText();
            CodeBox = false;
            preview.Text = "Şifrenizi Girin";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CMD();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            stopChecking = true;
            DeleteIP();
            Connection.Text = "CaYaSafe Hizmet Bağlantısı Kapatıldı!";
            Connection.ForeColor = Color.Red;
            pictureBox17.Visible = false;
            button4.Visible = false;

            if (Disconnect == true)
            {
                //button4.Text = "Hizmetler Tekrar Etkinleştirilemez.";
                //Enabled = false;
                button4.Visible = true;
            }
            else
            {
                //button4.Text = "Hizmetler Tekrar Etkinleştirilemez.";
                //Enabled = false;
            }
            Disconnect = true;
        }

        private async void Lock_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeleteIP();
            await Task.Delay(250);
        }

        private async void Lock_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteIP();
            await Task.Delay(250);
        }

        private void preview_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }








        private void StartupFormDesign()
        {

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            preview.BackColor = ColorTranslator.FromHtml("#CCCCCC");
            Number0.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number1.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number2.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number3.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number4.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number5.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number6.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number7.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number8.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Number9.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Clear.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            pictureBox4.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            pictureBox3.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            pictureBox19.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            label7.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            pictureBox18.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            pictureBox17.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            pictureBox16.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            pictureBox2.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            button2.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            pictureBox7.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Logining.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            Logining.ForeColor = ColorTranslator.FromHtml("#29577a");
            pictureBox8.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            label17.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            button4.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            panel1.BackColor = ColorTranslator.FromHtml("#f2f2f2");





            pictureBox19.Visible = false;

            label17.Visible = true; //sertifika doğrulama
            button2.Visible = true;
            pictureBox7.Visible = false;
            Number0.Visible = false;
            Number1.Visible = false;
            Number2.Visible = false;
            Number3.Visible = false;
            Number4.Visible = false;
            Number5.Visible = false;
            Number6.Visible = false;
            Number7.Visible = false;
            Number8.Visible = false;
            Number9.Visible = false;
            Clear.Visible = false;
            preview.Visible = false;
            pictureBox18.Visible = false;
            pictureBox4.Visible = false;
        }

        private void ManuelLogin()
        {
            QRCodeLogOut();

            pictureBox19.Visible = true;

            pictureBox4.Visible = true;
            //button2.Visible = true;
            pictureBox7.Visible = true;
            Number0.Visible = true;
            Number1.Visible = true;
            Number2.Visible = true;
            Number3.Visible = true;
            Number4.Visible = true;
            Number5.Visible = true;
            Number6.Visible = true;
            Number7.Visible = true;
            Number8.Visible = true;
            Number9.Visible = true;
            Clear.Visible = true;
            preview.Visible = true;
            pictureBox18.Visible = true;
            pictureBox4.Visible = true;
        }

        private void ManuelLogout()
        {
            pictureBox19.Visible = false;

            //button2.Visible = true;
            pictureBox7.Visible = false;
            Number0.Visible = false;
            Number1.Visible = false;
            Number2.Visible = false;
            Number3.Visible = false;
            Number4.Visible = false;
            Number5.Visible = false;
            Number6.Visible = false;
            Number7.Visible = false;
            Number8.Visible = false;
            Number9.Visible = false;
            Clear.Visible = false;
            preview.Visible = false;
            pictureBox18.Visible = false;
            pictureBox4.Visible = false;
        }

        private void QRCodeLogin()
        {
            ManuelLogout();

            label17.Visible = true;
            label18.Visible = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            label7.Visible = true;
            pictureBox3.Visible = true;
        }

        private void QRCodeLogOut()
        {
            label17.Visible = false;
            label18.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            label7.Visible = false;
            pictureBox3.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ManuelLogin();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            QRCodeLogin();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

            if (!CodeBox)
            {
                CodeBox = true;
                preview.Text = "";
            }
            // TextBox'ta yazılanları Label'a ekle


            this.preview.Text += this.textBox1.Text;



            // TextBox'ı temizle
            //this.textBox1.Text = "";

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {



            if (e.KeyCode == Keys.Back && this.preview.Text.Length > 0)
            {
                this.preview.Text = this.preview.Text.Substring(0, this.preview.Text.Length - 1);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Authenticate();
            }
            else
            {

            }

            // TextBox'ı temizle
            this.textBox1.Text = "";
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void preview_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }








    }
}