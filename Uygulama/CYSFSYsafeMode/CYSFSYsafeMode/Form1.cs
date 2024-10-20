using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;

namespace CYSFSYsafeMode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            {
                RunUac();
                return;
            }
            

            // Registry anahtarýný aç
            RegistryKey key = Registry.LocalMachine.CreateSubKey("Software\\CaYaSafeSystem");
            // Deðeri oku
            string readValue = key.GetValue("SFMD") as string;

            if (readValue == null)
            {
                // Þifre uzunluðu
                int length = 12;

                // Rasgele þifre oluþtur
                string password = GenerateRandomPassword(length);

                // Deðer oluþtur ve deðeri ayarla
                key.SetValue("SFMD", password);

                Console.WriteLine("Deðer baþarýyla oluþturuldu ve ayarlandý.");

                label3.Text = "12 Basamaklý Kodunuz: " + password;

                turnon();

                label5.Text = "Durum: Korunuyor";
                label5.ForeColor = Color.Green;

                label7.Visible = true;
                textBox1.Visible = true;

                label7.Visible = true;
                textBox1.Visible = true;
                MessageBox.Show("Ýþlem baþarýlý! Kodu lütfen kaydediniz: " + password);
                key.Close();

            }
            else
            {
                if (textBox1.Text == readValue)
                {
                    // Þifre uzunluðu
                    int length = 12;

                    // Rasgele þifre oluþtur
                    string password = GenerateRandomPassword(length);

                    // Deðer oluþtur ve deðeri ayarla
                    key.SetValue("SFMD", password);

                    Console.WriteLine("Deðer baþarýyla oluþturuldu ve ayarlandý.");

                    label3.Text = "12 Basamaklý Kodunuz: " + password;

                    turnon();

                    label5.Text = "Durum: Korunuyor";
                    label5.ForeColor = Color.Green;

                    label7.Visible = true;
                    textBox1.Visible = true;

                    label7.Visible = true;
                    textBox1.Visible = true;
                    MessageBox.Show("Ýþlem baþarýlý! Kodu lütfen kaydediniz: " + password);
                    key.Close();
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Lütfen mevcut kodu giriniz.");
                    }
                    else
                    {
                        MessageBox.Show("Hatalý kod!");
                    }
                }
            }
            


        }

        static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random rnd = new Random();
            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[rnd.Next(validChars.Length)];
            }

            return new string(password);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            {
                RunUac();
                return;
            }
            

            // Registry anahtarýný aç
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\CaYaSafeSystem", true);

            // Deðeri oku
            string readValue = key.GetValue("SFMD") as string;

            // Deðerin varlýðýný kontrol et
            if (readValue != null)
            {
                if (textBox1.Text == readValue)
                {
                    // Deðeri sil
                    key.DeleteValue("SFMD");
                    Console.WriteLine("Deðer baþarýyla silindi.");
                    turnoff();


                    label5.Text = "Durum: Korunmuyor";
                    label5.ForeColor = Color.Red;

                    label7.Visible = false;
                    textBox1.Visible = false;


                    // Registry anahtarýný kapat
                    key?.Close();
                    MessageBox.Show("Baþarýlý bir þekilde tekrar Güvenli Mod aktif edildi!");
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Lütfen mevcut kodu giriniz.");
                    }
                    else
                    {
                        MessageBox.Show("Hatalý kod!");
                    }
                }
            }
            else
            {
                label5.Text = "Durum: Korunmuyor";
                label5.ForeColor = Color.Red;

                label7.Visible = false;
                textBox1.Visible = false;

                // Registry anahtarýný kapat
                key?.Close();

                Console.WriteLine("Belirtilen anahtar bulunamadý.");
                MessageBox.Show("Deðer bulunamadý!");
            }







        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Registry anahtarýný aç
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\CaYaSafeSystem");
            if (key != null)
            {
                // Deðeri oku
                string readValue = key.GetValue("SFMD") as string;

                if (readValue != null)
                {
                    label5.Text = "Durum: Korunuyor (Güvenlik nedeniyle kod tekrar gösterilemez.)";
                    label5.ForeColor = Color.Green;

                    label7.Visible = true;
                    textBox1.Visible = true;
                }
                else
                {
                    label5.Text = "Durum: Korunmuyor";
                    label5.ForeColor = Color.Red;

                    label7.Visible = false;
                    textBox1.Visible = false;
                }
            }
        }








        private async void turnon()
        {
            // Registry anahtarýný aç
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);

            if (key != null)
            {
                // Deðerin varlýðýný kontrol et
                if (key.GetValue("USERINIT") != null)
                {
                    // Deðeri deðiþtir
                    key.SetValue("USERINIT", "C:\\Users\\cagan\\source\\repos\\DisableSafeMode\\DisableSafeMode\\bin\\Debug\\net6.0-windows\\DisableSafeMode.exe,");

                    Console.WriteLine("Deðer baþarýyla deðiþtirildi.");
                }
                else
                {
                    Console.WriteLine("Belirtilen deðer bulunamadý.");
                    MessageBox.Show("Belirtilen deðer bulunamadý. Windows konfigrasyon hatasý!");
                }
            }
            else
            {
                Console.WriteLine("Belirtilen anahtar bulunamadý. Windows konfigrasyon hatasý!");
                MessageBox.Show("Belirtilen anahtar bulunamadý. Windows konfigrasyon hatasý!");
            }

            // Registry anahtarýný kapat
            key?.Close();
        }

        private async void turnoff()
        {
            // Registry anahtarýný aç
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);

            if (key != null)
            {
                // Deðerin varlýðýný kontrol et
                if (key.GetValue("USERINIT") != null)
                {
                    // Deðeri deðiþtir
                    key.SetValue("USERINIT", "C:\\Windows\\system32\\userinit.exe,");

                    Console.WriteLine("Deðer baþarýyla deðiþtirildi.");
                }
                else
                {
                    Console.WriteLine("Belirtilen deðer bulunamadý.");
                    MessageBox.Show("Belirtilen deðer bulunamadý. Windows konfigrasyon hatasý!");
                }
            }
            else
            {
                Console.WriteLine("Belirtilen anahtar bulunamadý. Windows konfigrasyon hatasý!");
                MessageBox.Show("Belirtilen anahtar bulunamadý. Windows konfigrasyon hatasý!");
            }

            // Registry anahtarýný kapat
            key?.Close();
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
                MessageBox.Show("UYARI! Bu iþlem için yönetici haklarý gerekmektedir.");
            }
        }

        private bool IsRunAsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}