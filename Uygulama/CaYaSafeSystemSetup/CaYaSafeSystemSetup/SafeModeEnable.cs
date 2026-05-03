using Microsoft.Win32;
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

namespace CaYaSafeSystemSetup
{
    public partial class SafeModeEnable : Form
    {
        public SafeModeEnable()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Registry anahtarını aç
            RegistryKey key = Registry.LocalMachine.CreateSubKey("Software\\CaYaSafeSystem");
            // Değeri oku
            string readValue = key.GetValue("SFMD") as string;

            if (readValue == null)
            {
                // Şifre uzunluğu
                int length = 12;

                // Rasgele şifre oluştur
                string password = GenerateRandomPassword(length);

                // Değer oluştur ve değeri ayarla
                key.SetValue("SFMD", password);

                turnon();

                Console.WriteLine("Değer başarıyla oluşturuldu ve ayarlandı.");

                label2.Text = password;



                //MessageBox.Show("İşlem başarılı! Kodu lütfen kaydediniz: " + password);
                key.Close();

                button3.Visible = true;
                button1.Visible = false;
                button2.Visible = false;

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

        private async void turnon()
        {
            // Registry anahtarını aç
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);

            if (key != null)
            {
                // Değerin varlığını kontrol et
                if (key.GetValue("USERINIT") != null)
                {
                    // Değeri değiştir
                    key.SetValue("USERINIT", "\"C:\\Users\\Default\\AppData\\Roaming\\DisableSafeMode\\DisableSafeMode.exe\"");

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

        private void SafeModeEnable_Load(object sender, EventArgs e)
        {
            // Registry anahtarını aç
            RegistryKey key = Registry.LocalMachine.CreateSubKey("Software\\CaYaSafeSystem");
            // Değeri oku
            string readValue = key.GetValue("SFMD") as string;
            if (readValue != null)
            {
                label2.Text = "Daha önceden zaten oluşturulmuş!";
                button2.Visible = false;
                button1.Text = "İleri";
            }
            key.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            EnableAdons ea = new EnableAdons();
            ea.Show();
        }

        private void SafeModeEnable_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("Yükleme esnasına yükleyici kapatılamaz.");
        }







        //private async void turnoff()
        //{
        //    // Registry anahtarını aç
        //    RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);

        //    if (key != null)
        //    {
        //        // Değerin varlığını kontrol et
        //        if (key.GetValue("USERINIT") != null)
        //        {
        //            // Değeri değiştir
        //            key.SetValue("USERINIT", "C:\\Windows\\system32\\userinit.exe,");

        //            Console.WriteLine("Değer başarıyla değiştirildi.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Belirtilen değer bulunamadı.");
        //            MessageBox.Show("Belirtilen değer bulunamadı. Windows konfigrasyon hatası!");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Belirtilen anahtar bulunamadı. Windows konfigrasyon hatası!");
        //        MessageBox.Show("Belirtilen anahtar bulunamadı. Windows konfigrasyon hatası!");
        //    }

        //    // Registry anahtarını kapat
        //    key?.Close();
        //}


        //private void KAPAT()
        //{
        //    // Registry anahtarını aç
        //    RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\CaYaSafeSystem", true);

        //    // Değeri oku
        //    string readValue = key.GetValue("SFMD") as string;

        //    // Değerin varlığını kontrol et
        //    if (readValue != null)
        //    {
        //        if (textBox1.Text == readValue)
        //        {
        //            // Değeri sil
        //            key.DeleteValue("SFMD");
        //            Console.WriteLine("Değer başarıyla silindi.");
        //            turnoff();


        //            label5.Text = "Durum: Korunmuyor";
        //            label5.ForeColor = Color.Red;

        //            label7.Visible = false;
        //            textBox1.Visible = false;


        //            // Registry anahtarını kapat
        //            key?.Close();
        //            MessageBox.Show("Başarılı bir şekilde tekrar Güvenli Mod aktif edildi!");
        //        }
        //        else
        //        {
        //            if (textBox1.Text == "")
        //            {
        //                MessageBox.Show("Lütfen mevcut kodu giriniz.");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Hatalı kod!");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        label5.Text = "Durum: Korunmuyor";
        //        label5.ForeColor = Color.Red;

        //        label7.Visible = false;
        //        textBox1.Visible = false;

        //        // Registry anahtarını kapat
        //        key?.Close();

        //        Console.WriteLine("Belirtilen anahtar bulunamadı.");
        //        MessageBox.Show("Değer bulunamadı!");
        //    }
        //}


    }
}
