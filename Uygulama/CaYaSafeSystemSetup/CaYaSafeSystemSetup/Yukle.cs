using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System.ServiceProcess;
using NetFwTypeLib;
using System.Diagnostics; // System.Diagnostics ad alanını ekleyin


namespace CaYaSafeSystemSetup
{
    public partial class Yukle : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

        enum SymbolicLink
        {
            File = 0,
            Directory = 1
        }

        static void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                CopyDirectory(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
        }

        public Yukle()
        {
            InitializeComponent();
        }

        private async void Yukle_Load(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(500);

            progressBar1.Maximum = 100;

            // ProgressBar'ın ilerlemesini sıfırla
            progressBar1.Value = 0;

            // Bazı işlemleri simüle etmek için döngü kullan
            //for (int i = 0; i <= 100; i++)
            //{
            //    // İşlem sırasında bekleme süresi eklemek için Thread.Sleep kullanılabilir
            //    // System.Threading.Thread.Sleep(50);
            //    //await System.Threading.Tasks.Task.Delay(50);

            //    // ProgressBar'ı güncelle
            //    progressBar1.Value = i;

            //    // ProgressBar'ın güncellenmesini sağlamak için yenileme işlemi yap
            //    progressBar1.Refresh();

            //    // Yapılacak işlemi buraya yazabilirsiniz
            //    // Örneğin, dosya kopyalama, veritabanı işlemleri, vb.

            //    programfilesX86();
            //    programData();
            //    SysWOW64();
            //    AppData();

            //    // Görevi zamanlayıcıya ekle
            //    await System.Threading.Tasks.Task.Delay(50);
            //    label3.Text = "CaYaSafeSystem 'cyRUN.exe' Otomatik başlatma için yükleniyor...";
            //    await System.Threading.Tasks.Task.Delay(10);
            //    AddTaskToScheduler(@"C:\Program Files (x86)\CaYaSafe\cyRUN\cyRUN.exe", "cyRUN");
            //    await System.Threading.Tasks.Task.Delay(50);
            //    label3.Text = "CaYaSafeSystem 'ServerStartt.exe' Otomatik başlatma için yükleniyor...";
            //    await System.Threading.Tasks.Task.Delay(10);
            //    AddTaskToScheduler(@"C:\Program Files (x86)\CaYaSafe\ServerStartt.exe", "ServerCY");
            //    await System.Threading.Tasks.Task.Delay(50);
            //    label3.Text = "CaYaSafeSystem 'AntiEX.exe' Otomatik başlatma için yükleniyor...";
            //    await System.Threading.Tasks.Task.Delay(10);
            //    AddTaskToScheduler(@"C:\Program Files (x86)\CaYaSafe\AntiEX\AntiEX.exe", "AntiEX");
            //    await System.Threading.Tasks.Task.Delay(50);
            //    label3.Text = "CaYaSafeSystem 'CheckUnAuth.exe' Otomatik başlatma için yükleniyor...";
            //    await System.Threading.Tasks.Task.Delay(10);
            //    AddTaskToScheduler(@"C:\Program Files (x86)\CaYaSafe\CheckUnAuth\CheckUnAuth.exe", "CheckUnAuth");
            //    await System.Threading.Tasks.Task.Delay(50);
            //    label3.Text = "CaYaSafeSystem 'CpgXR.exe' Otomatik başlatma için yükleniyor...";
            //    await System.Threading.Tasks.Task.Delay(10);
            //    AddTaskToScheduler(@"C:\Windows\SysWOW64\CpgX\CpgXR\CpgXR.exe", "CpgXR");
            //    await System.Threading.Tasks.Task.Delay(50);
            //    Console.WriteLine("Görev başarıyla zamanlayıcıya eklendi.");
            //    await System.Threading.Tasks.Task.Delay(50);
            //    // Yeni bir servis oluşturmak için:
            //    label3.Text = "CaYaSafeSystem Hizmetleri yükleniyor...";
            //    await System.Threading.Tasks.Task.Delay(10);
            //    CreateService("CYRS", "CaYaSafeServices", @"C:\Windows\SysWOW64\CYRSServices\CYRS.exe");

            //}


            int totalSteps = 11; // Toplam adım sayısı
            int currentStep = 0; // Başlangıç adımı

            void UpdateProgressBar()
            {
                currentStep++;
                double progressPercentage = (double)currentStep / totalSteps * 100;
                progressBar1.Value = (int)progressPercentage;
            }







            programfilesX86();
            UpdateProgressBar(); // ProgressBar'u güncelle
            programData();
            UpdateProgressBar(); // ProgressBar'u güncelle
            SysWOW64();
            UpdateProgressBar(); // ProgressBar'u güncelle
            AppData();
            UpdateProgressBar(); // ProgressBar'u güncelle

            // Görevi zamanlayıcıya ekle
            await System.Threading.Tasks.Task.Delay(50);
            label3.Text = "CaYaSafeSystem 'cyRUN.exe' Otomatik başlatma için yükleniyor...";
            await System.Threading.Tasks.Task.Delay(10);
            AddTaskToScheduler(@"C:\Program Files (x86)\CaYaSafe\cyRUN\cyRUN.exe", "cyRUN");
            UpdateProgressBar(); // ProgressBar'u güncelle
            await System.Threading.Tasks.Task.Delay(50);
            label3.Text = "CaYaSafeSystem 'ServerStartt.exe' Otomatik başlatma için yükleniyor...";
            await System.Threading.Tasks.Task.Delay(10);
            AddTaskToScheduler(@"C:\Program Files (x86)\CaYaSafe\ServerStartt.exe", "ServerCY");
            UpdateProgressBar(); // ProgressBar'u güncelle
            await System.Threading.Tasks.Task.Delay(50);
            label3.Text = "CaYaSafeSystem 'AntiEX.exe' Otomatik başlatma için yükleniyor...";
            await System.Threading.Tasks.Task.Delay(10);
            AddTaskToScheduler(@"C:\Program Files (x86)\CaYaSafe\AntiEX\AntiEX.exe", "AntiEX");
            UpdateProgressBar(); // ProgressBar'u güncelle
            await System.Threading.Tasks.Task.Delay(50);
            label3.Text = "CaYaSafeSystem 'CheckUnAuth.exe' Otomatik başlatma için yükleniyor...";
            await System.Threading.Tasks.Task.Delay(10);
            AddTaskToScheduler(@"C:\Program Files (x86)\CheckUnAuth\CheckUnAuth.exe", "CheckUnAuth");
            UpdateProgressBar(); // ProgressBar'u güncelle
            await System.Threading.Tasks.Task.Delay(50);
            label3.Text = "CaYaSafeSystem 'CpgXR.exe' Otomatik başlatma için yükleniyor...";
            await System.Threading.Tasks.Task.Delay(10);
            AddTaskToScheduler(@"C:\Windows\SysWOW64\CpgX\CpgXR\CpgXR.exe", "CpgXR");
            UpdateProgressBar(); // ProgressBar'u güncelle
            await System.Threading.Tasks.Task.Delay(50);
            Console.WriteLine("Görev başarıyla zamanlayıcıya eklendi.");
            await System.Threading.Tasks.Task.Delay(50);
            // Yeni bir servis oluşturmak için:
            label3.Text = "CaYaSafeSystem Hizmetleri yükleniyor...";
            await System.Threading.Tasks.Task.Delay(10);
            CreateService("CYRS", "CaYaSafeServices", @"C:\Windows\SysWOW64\CYRSServices\CYRS.exe");
            UpdateProgressBar(); // ProgressBar'u güncelle
            await System.Threading.Tasks.Task.Delay(50);

            label3.Text = "Güvenlik duvarına ekleniyor...";
            await System.Threading.Tasks.Task.Delay(10);
            string ruleName = "CaYaSafeServer"; // Kural adı
            string exePath = @"C:\Program Files (x86)\CaYaSafe\server.exe"; // Uygulama yolunu belirtin
            SetFirewallException(ruleName, exePath);
            UpdateProgressBar(); // ProgressBar'u güncelle






            if (progressBar1.Value == 100)
            {
                //İşlem gerçekleştir
                label3.Text = "Kurulum işlemi tamamlandı!";
                await System.Threading.Tasks.Task.Delay(1000);

                Hide();

                SafeModeEnable sme = new SafeModeEnable();
                sme.Show();
            }
        }

        private void Yukle_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("Yükleme esnasına yükleyici kapatılamaz.");
        }

        static void SetFirewallException(string ruleName, string exePath)
        {

            string script = $@"netsh advfirewall firewall show rule name=""{ruleName}"" > nul & if errorlevel 1 (netsh advfirewall firewall add rule name=""{ruleName}"" dir=in action=allow program=""{exePath}"" enable=yes) else (netsh advfirewall firewall set rule name=""{ruleName}"" new program=""{exePath}"")";

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + script;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }




        private async void programfilesX86()
        {
            label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)' Konumuna Yükleniyor...";
            await System.Threading.Tasks.Task.Delay(50);

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sourceFolder = Path.Combine(currentDirectory, "ProgramFiles");

            //string sourceFolder = @"C:\Path\To\Your\Source\Folder\deneme";


            string targetFolder = @"C:\Program Files (x86)\";

            try
            {
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                // Klasör içeriğini kopyala
                DirectoryInfo sourceDir = new DirectoryInfo(sourceFolder);
                DirectoryInfo targetDir = new DirectoryInfo(targetFolder);

                CopyDirectory(sourceDir, targetDir);

                // Create symbolic link
                if (CreateSymbolicLink(targetFolder, sourceFolder, SymbolicLink.Directory))
                {
                    Console.WriteLine("Klasör başarıyla oluşturuldu ve simge bağlantısı kuruldu.");
                }
                else
                {
                    Console.WriteLine("Klasör oluşturulamadı veya simge bağlantısı kurulamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }


        }

        private async void programData()
        {
            label3.Text = "CaYaSafeSystem 'ProgramData' Konumuna Yükleniyor...";
            await System.Threading.Tasks.Task.Delay(50);

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sourceFolder = Path.Combine(currentDirectory, "ProgramData");

            //string sourceFolder = @"C:\Path\To\Your\Source\Folder\deneme";


            string targetFolder = @"C:\ProgramData\";

            try
            {
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                // Klasör içeriğini kopyala
                DirectoryInfo sourceDir = new DirectoryInfo(sourceFolder);
                DirectoryInfo targetDir = new DirectoryInfo(targetFolder);

                CopyDirectory(sourceDir, targetDir);

                // Create symbolic link
                if (CreateSymbolicLink(targetFolder, sourceFolder, SymbolicLink.Directory))
                {
                    Console.WriteLine("Klasör başarıyla oluşturuldu ve simge bağlantısı kuruldu.");
                }
                else
                {
                    Console.WriteLine("Klasör oluşturulamadı veya simge bağlantısı kurulamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }


        }


        private async void SysWOW64()
        {
            label3.Text = "CaYaSafeSystem 'SysWOW64' Konumuna Yükleniyor...";
            await System.Threading.Tasks.Task.Delay(50);

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sourceFolder = Path.Combine(currentDirectory, "SysWOW64");

            //string sourceFolder = @"C:\Path\To\Your\Source\Folder\deneme";


            string targetFolder = @"C:\Windows\SysWOW64\";

            try
            {
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                // Klasör içeriğini kopyala
                DirectoryInfo sourceDir = new DirectoryInfo(sourceFolder);
                DirectoryInfo targetDir = new DirectoryInfo(targetFolder);

                CopyDirectory(sourceDir, targetDir);

                // Create symbolic link
                if (CreateSymbolicLink(targetFolder, sourceFolder, SymbolicLink.Directory))
                {
                    Console.WriteLine("Klasör başarıyla oluşturuldu ve simge bağlantısı kuruldu.");
                }
                else
                {
                    Console.WriteLine("Klasör oluşturulamadı veya simge bağlantısı kurulamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }


        }



        private async void AppData()
        {
            label3.Text = "CaYaSafeSystem 'AppData' Konumuna Yükleniyor...";
            await System.Threading.Tasks.Task.Delay(50);

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sourceFolder = Path.Combine(currentDirectory, "AppData");

            //string sourceFolder = @"C:\Path\To\Your\Source\Folder\deneme";


            string targetFolder = @"C:\Users\Default\AppData\Roaming\";

            try
            {
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                // Klasör içeriğini kopyala
                DirectoryInfo sourceDir = new DirectoryInfo(sourceFolder);
                DirectoryInfo targetDir = new DirectoryInfo(targetFolder);

                CopyDirectory(sourceDir, targetDir);

                // Create symbolic link
                if (CreateSymbolicLink(targetFolder, sourceFolder, SymbolicLink.Directory))
                {
                    Console.WriteLine("Klasör başarıyla oluşturuldu ve simge bağlantısı kuruldu.");
                }
                else
                {
                    Console.WriteLine("Klasör oluşturulamadı veya simge bağlantısı kurulamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }


        }



        static void AddTaskToScheduler(string programPath, string taskName)
        {
            // Görev Zamanlayıcısı oluşturucuyu başlat
            using (TaskService taskService = new TaskService())
            {
                // Yeni bir görev oluştur
                TaskDefinition taskDefinition = taskService.NewTask();

                // Görevin çalışması şu süreyi aşarsa görevi durdur
                taskDefinition.Settings.ExecutionTimeLimit = TimeSpan.Zero;

                // Zamanlanan başlangıç kaçırılırsa en kısa sürede çalıştır
                taskDefinition.Settings.StartWhenAvailable = false;

                // Yönetici ayrıcalıklarını etkinleştir
                taskDefinition.Principal.RunLevel = TaskRunLevel.Highest;

                // Çalışan görev istendiğinde sona ermezse, görevi durmaya zorla
                taskDefinition.Settings.AllowHardTerminate = false;

                // Görevi yalnızca bilgisayar AC gücündeyken başlat
                taskDefinition.Settings.DisallowStartIfOnBatteries = false;

                // Bilgisayar pil gücüne geçerse görevi durdur
                taskDefinition.Settings.StopIfGoingOnBatteries = false;

                // Görevi tetikleyen oturum başlangıcı ve AC gücü değişiklikleri
                taskDefinition.Triggers.Add(new LogonTrigger());
                taskDefinition.Triggers.Add(new SessionStateChangeTrigger() { StateChange = TaskSessionStateChangeType.SessionUnlock });

                // Görevin ne zaman başlayacağını belirle (örneğin, şu an)
                DateTime startTime = DateTime.Now;
                taskDefinition.Triggers.Add(new TimeTrigger(startTime));

                // Görevin hangi işlemi yapacağını belirle
                //string arguments = "/yourArgument"; // Programa isteğe bağlı argümanlar ekleyebilirsiniz
                //taskDefinition.Actions.Add(new ExecAction(programPath, arguments, null));
                taskDefinition.Actions.Add(new ExecAction(programPath, null));

                // Görevi zamanlayıcıya ekle
                taskService.RootFolder.RegisterTaskDefinition(taskName, taskDefinition,
                    //TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
                    TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken, null);
            }


        }




        static void CreateService(string serviceName, string displayName, string executablePath)
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                try
                {

                    // Servis parametreleriyle birlikte oluşturulur
                    string createCmd = $"create {serviceName} binPath= \"{executablePath}\" DisplayName= \"{displayName}\"";
                    ExecuteCommand("sc", createCmd);

                    Console.WriteLine($"'{serviceName}' adlı servis başarıyla oluşturuldu.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Servis oluşturma hatası: {ex.Message}");
                }
            }
        }

        static void DeleteService(string serviceName)
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                try
                {
                    // Servis varsa durdur
                    if (ServiceExists(serviceName))
                    {
                        sc.Stop();
                    }

                    // Servisi sil
                    ExecuteCommand("sc", $"delete {serviceName}");

                    Console.WriteLine($"'{serviceName}' adlı servis başarıyla silindi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Servis silme hatası: {ex.Message}");
                }
            }
        }

        static bool ServiceExists(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        static void ExecuteCommand(string command, string arguments)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            process.Close();
        }
























    }

}
