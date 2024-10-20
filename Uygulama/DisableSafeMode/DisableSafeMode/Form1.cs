using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;

namespace DisableSafeMode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }


        private void InitializeTimer()
        {
            Thread bgThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(650);
                    try
                    {
                        EndProcess("taskmgr");
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

        private bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }



        private async void Form1_Load(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            {
                //RunUac();
                //return; // Ýþlemi sonlandýr
                Process.Start("userinit.exe");
                await Task.Delay(5000);
                Environment.Exit(0);
            }
            else
            {
                // Registry anahtarýný aç
                RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\CaYaSafeSystem");
                if (key != null)
                {
                    // Deðeri oku
                    string readValue = key.GetValue("SFMD") as string;

                    if (readValue != null)
                    {
                        Opacity = 100;

                        TopMost = false;
                        TopMost = true;
                        TopMost = false;
                        TopMost = true;
                        TopMost = false;
                        TopMost = true;
                        TopMost = false;
                        TopMost = true;

                        // Dosyanýn oluþturulacaðý dizin
                        string dizin = @"C:\ProgramData"; // Kullanýcý adýnýza göre deðiþtirin

                        // Dosyanýn adý
                        string dosyaAdi = "UNAUTH";

                        // Dosyanýn tam yolu
                        string dosyaYolu = Path.Combine(dizin, dosyaAdi);

                        // FileStream kullanarak dosyayý oluþtur
                        using (FileStream fs = File.Create(dosyaYolu))
                        {
                            Console.WriteLine("Dosya oluþturuldu: " + dosyaYolu);
                        }
                    }
                    else
                    {
                        Process.Start("userinit.exe");
                        await Task.Delay(5000);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Process.Start("userinit.exe");
                    await Task.Delay(5000);
                    Environment.Exit(0);
                }
            }
        }


        private void RunUac()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    Verb = "runas", // Uygulamayý yönetici olarak baþlat
                    UseShellExecute = true
                };

                Process.Start(startInfo);
                Application.Exit(); // Þu anki uygulamayý kapat
            }
            catch (Exception ex)
            {
                MessageBox.Show("UYARI! Ýþinize Devam Etmek Ýçin Onaylamanýz Gerekmektedir.");
            }
        }

        private bool IsRunAsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }









        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
            {
                //DialogResult result = MessageBox.Show("Bu Bilgisayar KÝTLENMÝÞTÝR!", "CaYa©", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Alt+F4 tuþ kombinasyonunu engelle
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {

            // Registry anahtarýný aç
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\CaYaSafeSystem");

            // Deðeri oku
            string readValue = key.GetValue("SFMD") as string;

            // Okunan deðeri kontrol et
            if (readValue != null)
            {
                //Console.WriteLine("Okunan Deðer: " + readValue);
                //MessageBox.Show("Deðer: " + readValue);

                // Deðer kontrolü ve iþlem yapma
                if (readValue == textBox1.Text)
                {
                    Console.WriteLine("Deðer doðru.");
                    textBox1.Enabled = false;
                    textBox1.Text = "Giriþ yapýlýyor...";
                    Process.Start("userinit.exe");
                    await Task.Delay(5000);
                    Environment.Exit(0);
                    //MessageBox.Show("Deðer DOÐRU!");
                    // Burada ek iþlemleri gerçekleþtir
                }
                else
                {
                    Console.WriteLine("Deðer doðru deðil.");
                    //MessageBox.Show("Deðer YANLIÞ!");
                    // Burada baþka bir iþlemi gerçekleþtir
                }
            }
            else
            {
                Console.WriteLine("Belirtilen deðer bulunamadý.");
            }

            // Registry anahtarýný kapat
            key.Close();
        }
    }
}