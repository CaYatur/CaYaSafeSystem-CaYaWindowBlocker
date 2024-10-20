using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckUnAuth
{
    public partial class DTM : Form
    {
        public DTM()
        {
            InitializeComponent();
            Form1.RequestEvent += HandleForm2Request;
            InitializeTimer();
        }


        private void InitializeTimer()
        {
            // Timer oluşturulması ve ayarlanması
            timerCheck = new System.Windows.Forms.Timer();
            timerCheck.Interval = 500; // 5000 milisaniye (5 saniye)
            timerCheck.Tick += new EventHandler(TimerCheck_Tick);
            timerCheck.Start();

        }



        private async void TimerCheck_Tick(object sender, EventArgs e)
        {
            // Burada belirli bir programın (örneğin Notepad.exe) çalışıp çalışmadığını kontrol ediyoruz
            //string targetProcessName = "taskmgr";
            //if (IsProcessRunning(targetProcessName))
            //{

            //    EndProcess("taskmgr");

            //}
            
            Thread bgThread2 = new Thread(() =>
            {
                EndProcess("taskmgr");
                EndProcess("regedit");
                EndProcess("cmd");
                EndProcess("powershell");
                EndProcess("explorer");
                EndProcess("chrome");
                EndProcess("LogonUI");

            });
            bgThread2.IsBackground = true;
            bgThread2.Start();


            //
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


        private void DTM_Load(object sender, EventArgs e)
        {



        }

        private async void HandleForm2Request(object sender, EventArgs e)
        {
            // Form2'den gelen isteği işleyin.
            // Bu metot, Form2'de bir buton tıklandığında çağrılacak.
            //MessageBox.Show("Form2'den istek alındı ve işlem gerçekleştirildi.");
            
            timerCheck.Stop();
            timerCheck.Enabled = false;
            Hide();
            //this.Close();
        }

        private void DTM_FormClosing(object sender, FormClosingEventArgs e)
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
