using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckUnAuth
{
    public partial class CheckUnAuth : Form
    {
        public CheckUnAuth()
        {
            InitializeComponent();
        }
        private bool Active = false;
        private void CheckUnAuth_Load(object sender, EventArgs e)
        {



            string dizn = @"C:\ProgramData"; // Kullanıcı adınıza göre değiştirin

            // Silinecek dosyanın adı
            string fileA = "verifyc.txt";

            // Dosyanın tam yolu
            string fileauth = Path.Combine(dizn, fileA);

            if (File.Exists(fileauth))
            {
                File.Delete(fileauth);
                Console.WriteLine("Dosya silindi: " + fileauth);
            }
            else
            {
                Console.WriteLine("Dosya mevcut değil: " + fileauth);
            }



            // Dosyanın bulunacağı dizin
            string dizin = @"C:\ProgramData"; // Kullanıcı adınıza göre değiştirin

            // Dosya adı
            string dosyaAdi = "UNAUTH";

            // Dosyanın tam yolu
            string dosyaYolu = Path.Combine(dizin, dosyaAdi);


            // Dosya var mı kontrol et
            if (File.Exists(dosyaYolu))
            {
                Console.WriteLine("Belirli dosya mevcut: " + dosyaYolu);
                Form1 f1 = new Form1();
                f1.Show();

                // Dosya varsa burada ek işlemleri gerçekleştirebilirsiniz
                // Örneğin, dosyayı açabilir, içeriğini okuyabilir veya değiştirebilirsiniz.
            }
            else
            {
                Console.WriteLine("Belirli dosya mevcut değil: " + dosyaYolu);



            }




            // Burada belirli bir programın (örneğin Notepad.exe) çalışıp çalışmadığını kontrol ediyoruz
            string targetProcessName = "cyRUN";


            Thread bgThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);



                    


                    if (IsProcessRunning(targetProcessName) && Active == false)
                    {

                        Active = true;

                    }

                    if (!IsProcessRunning(targetProcessName) && Active == true)
                    {

                        //Environment.Exit(0);
                        Process.Start("shutdown", "/s /f /t 0");

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

        private bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }


        private void CheckUnAuth_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
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
    }
}
