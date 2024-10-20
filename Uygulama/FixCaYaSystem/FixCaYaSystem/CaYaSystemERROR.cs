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

namespace CpgX
{
    public partial class CaYaSystemERROR : Form
    {
        public CaYaSystemERROR()
        {
            InitializeComponent();
        }

        private void CaYaSystemERROR_Load(object sender, EventArgs e)
        {
            TopMost = true;
            EndProcess("explorer");
            EndProcess("chrome");
            GeriSayim();
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

        private async void GeriSayim()
        {
            DTM form2 = new DTM();
            form2.Show();
            label9.Text = "Hizmetler Devre Dışı Bırakılıyor...";
            Label9Ortala();
            await Task.Delay(2500);
            label9.Text = "30";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "29";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "28";
            await Task.Delay(1000);
            label9.Text = "27";
            await Task.Delay(1000);
            label9.Text = "26";
            await Task.Delay(1000);
            label9.Text = "25";
            await Task.Delay(1000);
            label9.Text = "24";
            await Task.Delay(1000);
            label9.Text = "23";
            await Task.Delay(1000);
            label9.Text = "22";
            await Task.Delay(1000);
            label9.Text = "21";
            await Task.Delay(1000);
            label9.Text = "20";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "19";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "18";
            await Task.Delay(1000);
            label9.Text = "17";
            await Task.Delay(1000);
            label9.Text = "16";
            await Task.Delay(1000);
            label9.Text = "15";
            await Task.Delay(1000);
            label9.Text = "14";
            await Task.Delay(1000);
            label9.Text = "13";
            await Task.Delay(1000);
            label9.Text = "12";
            await Task.Delay(1000);
            label9.Text = "11";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "10";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "9";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "8";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "7";
            await Task.Delay(1000);
            label9.Text = "6";
            await Task.Delay(1000);
            label9.Text = "5";
            await Task.Delay(1000);
            label9.Text = "4";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "3";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "2";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "1";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "0";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "İstek İşleniyor...";
            Label9Ortala();
            await Task.Delay(1000);
            label9.Text = "Hizmetler Yükleniyor...";
            KontrolVeBaslat();
            Label9Ortala();
            await Task.Delay(8000);
            label9.Text = "Windows Başlatılıyor...";
            Label9Ortala();
            await Task.Delay(3000);
            label9.Text = "İstek Son Kez Elden Geçiriliyor...";
            Label9Ortala();
            await Task.Delay(800);
            label9.Text = "Sistem Yükleniyor...";
            Label9Ortala();
            await Task.Delay(1500);
            label9.Text = "Masaüstü Başlatılıyor...";
            Process.Start("explorer.exe");
            Label9Ortala();
            await Task.Delay(5000);
            label9.Text = "Düzeltme İşlemleri BAŞARILI!";
            Label9Ortala();
            await Task.Delay(1200);
            Close();
        }


        private void Label9Ortala()
        {
            // İlgili kontrolün genişliği ve yüksekliği
            int kontrolGenislik = label9.Width;
            int kontrolYukseklik = label9.Height;

            // Formun genişliği ve yüksekliği
            int formGenislik = this.Width;
            int formYukseklik = this.Height;

            // Formun orta noktasını hesapla
            int formOrtaX = formGenislik / 2;
            int formOrtaY = formYukseklik / 2;

            // İlgili kontrolü bir miktar aşağıya kaydır
            int ayracY = 40; // Kontrolün formun ortasının altında olmasını istediğiniz miktar
            int kontrolX = formOrtaX - (kontrolGenislik / 2);
            int kontrolY = formOrtaY + (ayracY - kontrolYukseklik / 2);

            label9.Location = new Point(kontrolX, kontrolY);
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





        private void KontrolVeBaslat()
        {
            string uygulamaYolu = @"C:\program files (x86)\CaYaSafe\cyRUN\cyRUN.exe";

            // Process sınıfı ile uygulamanın çalışıp çalışmadığını kontrol et
            if (!UygulamaCalisiyorMu(uygulamaYolu))
            {
                // Uygulama çalışmıyorsa başlat
                BaslatUygulama(uygulamaYolu);
            }
        }

        private bool UygulamaCalisiyorMu(string uygulamaYolu)
        {
            // Uygulamanın çalışıp çalışmadığını kontrol et
            Process[] processes = Process.GetProcessesByName("cyRUN");

            foreach (Process process in processes)
            {
                if (process.MainModule.FileName.Equals(uygulamaYolu, StringComparison.OrdinalIgnoreCase))
                {
                    // Uygulama çalışıyorsa true döndür
                    return true;
                }
            }

            // Uygulama çalışmıyorsa false döndür
            return false;
        }

        private void BaslatUygulama(string uygulamaYolu)
        {
            try
            {
                // Uygulamayı başlat
                Process.Start(uygulamaYolu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uygulama başlatılırken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
