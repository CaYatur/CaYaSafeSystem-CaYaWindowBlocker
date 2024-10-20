using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.ServiceProcess;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Diagnostics;

namespace CaYaSafeSystemSetup
{
    public partial class Kaldir : Form
    {
        private bool Auth = false;
        private bool suss = false;

        public Kaldir()
        {
            InitializeComponent();

            verify12.RequestEvent += verify12_SomethingHappened;
        }

        private void verify12_SomethingHappened(object sender, EventArgs e)
        {
            // Olay tetiklendiğinde burası çalışır
            // İlgili işlemleri gerçekleştir
            Auth = true;
            //MessageBox.Show("HAh");
        }

        private async void Kaldir_Load(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(500);



            // Registry anahtarını aç
            RegistryKey key = Registry.LocalMachine.CreateSubKey("Software\\CaYaSafeSystem");
            // Değeri oku
            string readValue = key.GetValue("SFMD") as string;
            if (readValue != null)
            {
                key.Close();
                label3.Text = "Güvenlik nedeniyle 12 basamaklı kod gerekmektedir!";
                verify12 v12 = new verify12();
                v12.ShowDialog();
            }
            else
            {
                key.Close();

                suss = true;
            }


            if (Auth == true)
            {
                suss = true;
            }

            if (suss == true)
            {

                string dosyaYolu = "C:\\Program Files (x86)\\CaYa\\CaYaSafeSystem\\ver.txt";


                // Dosyayı silme işlemi
                if (File.Exists(dosyaYolu))
                {
                    File.Delete(dosyaYolu);
                    Console.WriteLine("Dosya silindi.");
                }
                else
                {
                    Console.WriteLine("Belirtilen dosya bulunamadı.");
                }




                progressBar1.Maximum = 100;

                // ProgressBar'ın ilerlemesini sıfırla
                progressBar1.Value = 0;

                //// Bazı işlemleri simüle etmek için döngü kullan
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


                //    // Belirtilen klasördeki tüm dosyaları sil
                //    label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)\\CaYa' Konumundan kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteFolder(@"C:\Program Files (x86)\CaYa");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)\\CaYaSafe' Konumundan kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteFolder(@"C:\Program Files (x86)\CaYaSafe");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)\\CaYaLab' Konumundan kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteFolder(@"C:\Program Files (x86)\CaYaLab");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)\\CheckUnAuth' Konumundan kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteFolder(@"C:\Program Files (x86)\CheckUnAuth");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'ProgramData\\svpgcc' Konumundan kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteFolder(@"C:\ProgramData\svpgcc");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'SysWOW64\\CYRSServices' Konumundan kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteFolder(@"C:\Windows\SysWOW64\CYRSServices");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'SysWOW64\\CpgX' Konumundan kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteFolder(@"C:\Windows\SysWOW64\CpgX");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'AppData\\Roaming\\FolNamC' Konumundan kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteFolder(@"C:\Users\Default\AppData\Roaming\FolNamC");
                //    await System.Threading.Tasks.Task.Delay(50);


                //    label3.Text = "CaYaSafeSystem 'cyRUN.exe' Otomatik başlatma kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteTaskFromScheduler("cyRUN");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'CpgXR.exe' Otomatik başlatma kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteTaskFromScheduler("CpgXR");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'ServerCY.exe' Otomatik başlatma kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteTaskFromScheduler("ServerCY");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'AntiEX.exe' Otomatik başlatma kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteTaskFromScheduler("AntiEX");
                //    await System.Threading.Tasks.Task.Delay(50);
                //    label3.Text = "CaYaSafeSystem 'CheckUnAuth.exe' Otomatik başlatma kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteTaskFromScheduler("CheckUnAuth");


                //    removesafemode();
                //    await System.Threading.Tasks.Task.Delay(50);
                //    // Bir servisi silmek için:
                //    label3.Text = "CaYaSafeSystem 'CYRS' Hizmeti kaldırılıyor...";
                //    await System.Threading.Tasks.Task.Delay(10);
                //    DeleteService("CYRS");


                //}

                int totalSteps = 17; // Toplam adım sayısı
                int currentStep = 0; // Başlangıç adımı

                void UpdateProgressBar()
                {
                    currentStep++;
                    double progressPercentage = (double)currentStep / totalSteps * 100;
                    progressBar1.Value = (int)progressPercentage;
                }




                EndProcess("CheckUnAuth");
                EndProcess("svpgcc");
                EndProcess("cyRUN");
                EndProcess("svpgcc");
                EndProcess("cyRUN");
                EndProcess("CaYaSafe32");
                EndProcess("CaYaExtraSafe");
                EndProcess("CpgXR");
                EndProcess("CpgX");
                EndProcess("log");
                EndProcess("log");
                EndProcess("CaYa");
                EndProcess("CaYaLabOffline");
                EndProcess("AntiEX");
                await System.Threading.Tasks.Task.Delay(10);
                EndProcess("CheckUnAuth");
                EndProcess("svpgcc");
                EndProcess("cyRUN");
                EndProcess("svpgcc");
                EndProcess("cyRUN");
                EndProcess("CaYaSafe32");
                EndProcess("CaYaExtraSafe");
                EndProcess("CpgXR");
                EndProcess("CpgX");
                EndProcess("log");
                EndProcess("log");
                EndProcess("CaYa");
                EndProcess("CaYaLabOffline");
                EndProcess("AntiEX");
                await System.Threading.Tasks.Task.Delay(500);
                EndProcess("CheckUnAuth");
                EndProcess("svpgcc");
                EndProcess("cyRUN");
                EndProcess("svpgcc");
                EndProcess("cyRUN");
                EndProcess("CaYaSafe32");
                EndProcess("CaYaExtraSafe");
                EndProcess("CpgXR");
                EndProcess("CpgX");
                EndProcess("log");
                EndProcess("log");
                EndProcess("CaYa");
                EndProcess("CaYaLabOffline");
                EndProcess("AntiEX");


                label3.Text = "CaYaSafeSystem 'SysWOW64\\CpgX' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\Windows\SysWOW64\CpgX");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)\\CaYa' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\Program Files (x86)\CaYa");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)\\CaYaSafe' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\Program Files (x86)\CaYaSafe");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)\\CaYaLab' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\Program Files (x86)\CaYaLab");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'ProgramFiles (x86)\\CheckUnAuth' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\Program Files (x86)\CheckUnAuth");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'ProgramData\\svpgcc' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\ProgramData\svpgcc");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'SysWOW64\\CYRSServices' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\Windows\SysWOW64\CYRSServices");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'AppData\\Roaming\\FolNamC' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\Users\Default\AppData\Roaming\FolNamC");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'AppData\\Roaming\\DisableSafeMode' Konumundan kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteFolder(@"C:\Users\Default\AppData\Roaming\DisableSafeMode");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);




                label3.Text = "CaYaSafeSystem 'cyRUN.exe' Otomatik başlatma kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteTaskFromScheduler("cyRUN");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'CpgXR.exe' Otomatik başlatma kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteTaskFromScheduler("CpgXR");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'ServerCY.exe' Otomatik başlatma kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteTaskFromScheduler("ServerCY");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'AntiEX.exe' Otomatik başlatma kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteTaskFromScheduler("AntiEX");
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeSystem 'CheckUnAuth.exe' Otomatik başlatma kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteTaskFromScheduler("CheckUnAuth");
                UpdateProgressBar(); // ProgressBar'u güncelle


                removesafemode();
                UpdateProgressBar(); // ProgressBar'u güncelle
                await System.Threading.Tasks.Task.Delay(50);
                // Bir servisi silmek için:
                label3.Text = "CaYaSafeSystem 'CYRS' Hizmeti kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                DeleteService("CYRS");
                UpdateProgressBar(); // ProgressBar'u güncelle

                await System.Threading.Tasks.Task.Delay(50);
                label3.Text = "CaYaSafeServer Güvenlik duvarından kaldırılıyor...";
                await System.Threading.Tasks.Task.Delay(10);
                RemoveFirewallException("CaYaSafeServer");
                UpdateProgressBar(); // ProgressBar'u güncelle













                if (progressBar1.Value == 100)
                {
                    //İşlem gerçekleştir
                    label3.Text = "CaYaSafeSystem Bu bilgisayardan başarılı bir şekilde kaldırılmıştır.";

                    System.Threading.Thread.Sleep(1000);

                    Hide();
                    //MessageBox.Show("Kaldırma İşlemi Tamamlandı!");
                    //Environment.Exit(0);
                    Kaldırma kl = new Kaldırma();
                    kl.Show();
                }













            }

            
        }


        static void RemoveFirewallException(string ruleName)
        {
            string script = $@"netsh advfirewall firewall delete rule name=""{ruleName}""";

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + script;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }


        private async void removesafemode()
        {
            label3.Text = "CaYaSafeSystem 'Kayıt Defteri' Konumundan kaldırılıyor...";
            await System.Threading.Tasks.Task.Delay(50);

            // Registry anahtarını aç
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\CaYaSafeSystem", true);

            // Değeri oku
            string readValue = key.GetValue("SFMD") as string;

            // Değerin varlığını kontrol et
            if (readValue != null)
            {

                // Değeri sil
                key.DeleteValue("SFMD");
                Console.WriteLine("Değer başarıyla silindi.");
                turnoff();


                // Registry anahtarını kapat
                key?.Close();
                //MessageBox.Show("Başarılı bir şekilde tekrar Güvenli Mod aktif edildi!");
            }
            else
            {


                // Registry anahtarını kapat
                key?.Close();

                Console.WriteLine("Belirtilen anahtar bulunamadı.");
                //MessageBox.Show("Değer bulunamadı!");
            }
        }

        private async void turnoff()
        {
            // Registry anahtarını aç
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);

            if (key != null)
            {
                // Değerin varlığını kontrol et
                if (key.GetValue("USERINIT") != null)
                {
                    // Değeri değiştir
                    key.SetValue("USERINIT", "C:\\Windows\\system32\\userinit.exe,");

                    Console.WriteLine("Değer başarıyla değiştirildi.");
                }
                else
                {
                    Console.WriteLine("Belirtilen değer bulunamadı.");
                    MessageBox.Show("Belirtilen değer bulunamadı. Windows konfigrasyon hatası!");
                }
            }
            else
            {
                Console.WriteLine("Belirtilen anahtar bulunamadı. Windows konfigrasyon hatası!");
                MessageBox.Show("Belirtilen anahtar bulunamadı. Windows konfigrasyon hatası!");
            }

            // Registry anahtarını kapat
            key?.Close();
        }




        private void Kaldir_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("Yükleme esnasına yükleyici kapatılamaz.");
        }










        static void DeleteTaskFromScheduler(string taskName)
        {
            // Görev Zamanlayıcısı oluşturucuyu başlat
            using (TaskService taskService = new TaskService())
            {
                // Belirtilen adla görevi ara
                Microsoft.Win32.TaskScheduler.Task task = taskService.FindTask(taskName);

                // Eğer görev bulunduysa sil
                if (task != null)
                {
                    taskService.RootFolder.DeleteTask(taskName);
                    Console.WriteLine($"'{taskName}' adlı görev başarıyla silindi.");
                }
                else
                {
                    Console.WriteLine($"'{taskName}' adlı görev bulunamadı.");
                }
            }
        }


        static void DeleteFilesInFolder(string folderPath)
        {
            try
            {
                // Klasör içindeki tüm dosya yollarını al
                string[] files = Directory.GetFiles(folderPath);

                // Her bir dosyayı sil
                foreach (string file in files)
                {
                    File.Delete(file);
                    Console.WriteLine($"Dosya silindi: {file}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }


        static void DeleteFolder(string folderPath)
        {
            try
            {
                // Klasör içindeki tüm alt dosya ve klasörleri al
                string[] files = Directory.GetFiles(folderPath);
                string[] dirs = Directory.GetDirectories(folderPath);

                // Dosyaları sil ve çalışan uygulamaları kontrol et
                foreach (string file in files)
                {
                    // Dosya silmeden önce çalışıp çalışmadığını kontrol et
                    if (IsProcessRunning(file))
                    {
                        // Eğer dosya çalışıyorsa, uygulamayı sonlandır
                        StopProcess(file);
                    }

                    // Dosyayı sil
                    File.Delete(file);
                    Console.WriteLine($"Dosya silindi: {file}");
                }

                // Tüm alt klasörleri sil (recursive olarak)
                foreach (string dir in dirs)
                {
                    DeleteFolder(dir);
                }

                // Klasörü sil
                Directory.Delete(folderPath);
                Console.WriteLine($"Klasör silindi: {folderPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }

        static bool IsProcessRunning(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            Process[] processes = Process.GetProcessesByName(fileName);
            return processes.Length > 0;
        }

        static void StopProcess(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            Process[] processes = Process.GetProcessesByName(fileName);
            foreach (Process process in processes)
            {
                process.Kill(); // Uygulamayı sonlandır
                process.WaitForExit(); // Uygulamanın kapanmasını bekle
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

        static async void DeleteService(string serviceName)
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                try
                {
                    // Servis çalışıyorsa durdur
                    if (sc.Status != ServiceControllerStatus.Stopped)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10)); // Servisin durmasını bekler, 10 saniye içinde durmazsa TimeoutException fırlatır
                    }



                    // Servisi sil
                    //string delCmd = $"delete \"{serviceName}\"";
                    //ExecuteCommand("sc", delCmd);
                    System.Diagnostics.Process.Start("sc", $"delete {serviceName}");

                    Console.WriteLine($"'{serviceName}' adlı servis başarıyla silindi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Servis silme hatası: {ex.Message}");
                    MessageBox.Show(ex.Message);
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
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



    }
}
