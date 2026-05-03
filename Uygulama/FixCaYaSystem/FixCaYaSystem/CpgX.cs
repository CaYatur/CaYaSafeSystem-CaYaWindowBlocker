using CpgX;
using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;

namespace FixCaYaSystem
{
    public partial class CpgX : Form
    {
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

        private async void PCTurnOn()
        {
            KontrolVeKopyalamaStartup();
            await Task.Delay(20000);
            KontrolVeKopyalamaSuresiz();
        }


        private async void KontrolVeKopyalamaSuresiz()
        {
            while (true) // Sürekli bir döngü
            {
                //KontrolVeKopyalama(); // Ýþlemi gerçekleþtir
                KontrolVeKopyalama();


                // Belirli bir süre bekleyerek iþlemi tekrarla
                await Task.Delay(3000); // 10000 milisaniye (10 saniye)
            }
        }




        private Dictionary<string, string> KopyalamaListesi = new Dictionary<string, string>
        {
            //{ @"C:\CaYaSafe", @"C:\Program Files (x86)\CaYaSafe" },
            //{ @"C:\CaYaLab", @"C:\Program Files (x86)\CaYaLab" },
            //{ Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FolNamC"), @"C:\Program Files (x86)\" },
            { Path.Combine(@"C:\Users\Default\AppData\Roaming\FolNamC"), @"C:\Program Files (x86)\" },
            // Buraya dilediðiniz kadar kaynak ve hedef klasör ekleyebilirsiniz
        };

        public CpgX()
        {
            InitializeComponent();
            PCTurnOn();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form yüklenirken otomatik olarak kontrol iþlemini baþlat
            //KontrolVeKopyalama();
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

        private async void KontrolVeKopyalama()
        {
            bool herhangiBirDosyaKopyalandi = false;

            foreach (var kopyalamaCifti in KopyalamaListesi)
            {
                string kaynakKlasor = kopyalamaCifti.Key;
                string hedefKlasor = kopyalamaCifti.Value;

                KontrolVeOlusturKlasor(hedefKlasor);

                if (File.Exists(kaynakKlasor))
                {
                    if (!DosyaBütünlüðüKontrolu(kaynakKlasor, hedefKlasor))
                    {
                        KlasorVeDosyayiKopyala(kaynakKlasor, hedefKlasor);
                        herhangiBirDosyaKopyalandi = true;
                    }
                    continue;
                }

                string[] hedefDosyalar = Directory.GetFiles(hedefKlasor, "*", SearchOption.AllDirectories);
                string[] kaynakDosyalar = Directory.GetFiles(kaynakKlasor, "*", SearchOption.AllDirectories);

                foreach (string kaynakDosya in kaynakDosyalar)
                {
                    string hedefDosya = kaynakDosya.Replace(kaynakKlasor, hedefKlasor);

                    if (!Array.Exists(hedefDosyalar, dosya => dosya.Equals(hedefDosya, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (!DosyaBütünlüðüKontrolu(kaynakDosya, hedefDosya))
                        {
                            KlasorVeDosyayiKopyala(kaynakDosya, hedefDosya);
                            herhangiBirDosyaKopyalandi = true;
                        }
                    }
                }
            }

            if (herhangiBirDosyaKopyalandi)
            {
                //MessageBox.Show("Kopyalama iþlemi baþarýyla tamamlandý.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //EndProcess("explorer");
                //await Task.Delay(10000); // 10000 milisaniye (10 saniye)

                EndProcess("svchost");
                //EndProcess("explorer");
                //Process.Start("explorer.exe");

            }
            else
            {
                //MessageBox.Show("Zaten tüm dosya ve klasörler mevcut.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void KontrolVeKopyalamaStartup()
        {
            bool herhangiBirDosyaKopyalandi = false;

            foreach (var kopyalamaCifti in KopyalamaListesi)
            {
                string kaynakKlasor = kopyalamaCifti.Key;
                string hedefKlasor = kopyalamaCifti.Value;

                KontrolVeOlusturKlasor(hedefKlasor);

                if (File.Exists(kaynakKlasor))
                {
                    if (!DosyaBütünlüðüKontrolu(kaynakKlasor, hedefKlasor))
                    {
                        KlasorVeDosyayiKopyala(kaynakKlasor, hedefKlasor);
                        herhangiBirDosyaKopyalandi = true;
                    }
                    continue;
                }

                string[] hedefDosyalar = Directory.GetFiles(hedefKlasor, "*", SearchOption.AllDirectories);
                string[] kaynakDosyalar = Directory.GetFiles(kaynakKlasor, "*", SearchOption.AllDirectories);

                foreach (string kaynakDosya in kaynakDosyalar)
                {
                    string hedefDosya = kaynakDosya.Replace(kaynakKlasor, hedefKlasor);

                    if (!Array.Exists(hedefDosyalar, dosya => dosya.Equals(hedefDosya, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (!DosyaBütünlüðüKontrolu(kaynakDosya, hedefDosya))
                        {
                            KlasorVeDosyayiKopyala(kaynakDosya, hedefDosya);
                            herhangiBirDosyaKopyalandi = true;
                        }
                    }
                }
            }

            if (herhangiBirDosyaKopyalandi)
            {
                //MessageBox.Show("Kopyalama iþlemi baþarýyla tamamlandý.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CaYaSystemERROR form2 = new CaYaSystemERROR();
                form2.Show();
            }
            else
            {
                //MessageBox.Show("Zaten tüm dosya ve klasörler mevcut.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void KontrolVeOlusturKlasor(string klasorYolu)
        {
            if (!Directory.Exists(klasorYolu))
            {
                Directory.CreateDirectory(klasorYolu);
            }
        }

        private void KlasorVeDosyayiKopyala(string kaynak, string hedef)
        {
            // Kaynak dosya var mý diye kontrol et
            if (File.Exists(kaynak))
            {
                // Hedef klasörde eksik olan dosyayý kopyala (tüm alt klasörleriyle birlikte)
                string hedefKlasor = Path.GetDirectoryName(hedef);

                // Hedef klasörü kontrol et ve oluþtur (eðer yoksa)
                KontrolVeOlusturKlasor(hedefKlasor);

                // Dosyayý kopyala
                File.Copy(kaynak, hedef, true);
            }
            else
            {
                MessageBox.Show($"Kaynak dosya bulunamadý: {kaynak}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private bool DosyaBütünlüðüKontrolu(string kaynakDosya, string hedefDosya)
        {
            string kaynakHash = DosyaHashAl(kaynakDosya);
            string hedefHash = DosyaHashAl(hedefDosya);

            return kaynakHash == hedefHash;
        }

        private string DosyaHashAl(string dosyaYolu)
        {
            if (File.Exists(dosyaYolu))
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(dosyaYolu))
                    {
                        byte[] hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            else
            {
                // Dosya bulunamadýðýnda KopyalamaListesi'nden kaynak konumu al
                if (KopyalamaListesi.TryGetValue(dosyaYolu, out var kaynakKonumu))
                {
                    //MessageBox.Show($"Dosya bulunamadý: {dosyaYolu}\nKopyalanacak konum: {kaynakKonumu}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return string.Empty; // Dosya bulunamadýðý durumda boþ bir string dönebiliriz ya da baþka bir hata iþleme mekanizmasý kullanabilirsiniz.
                }
                else
                {
                    //MessageBox.Show($"Dosya bulunamadý: {dosyaYolu}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return string.Empty;
                }
            }
        }







    }
}