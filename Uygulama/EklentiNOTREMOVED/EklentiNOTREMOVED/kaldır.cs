using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EklentiNOTREMOVED
{
    public partial class kaldır : Form
    {
        public kaldır()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SecureSystem");

            // Hedef klasör varsa içindekileri sil
            if (Directory.Exists(targetFolder))
            {
                DirectoryInfo directory = new DirectoryInfo(targetFolder);
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    subDirectory.Delete(true);
                }

                Console.WriteLine("Klasör ve içeriği başarıyla silindi.");
                MessageBox.Show("İşlem Başarılı! güvenle bu pencereyi kapatabilirsiniz.");
            }
            else
            {
                Console.WriteLine("Silinecek klasör bulunamadı.");
                MessageBox.Show("Silinecek klasör bulunamadı. HATA!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("KALICI OLARAK TÜM EKLENTİLERİ VE DOSYALARI KALDIRMAK İSTEDİĞİNİZDEN EMİNMİSİNİZ?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {




                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string chromePreferencesPath = Path.Combine(localAppDataPath, @"Google\Chrome\User Data\Default\Secure Preferences");

                // Dosya varsa sil
                if (File.Exists(chromePreferencesPath))
                {
                    File.Delete(chromePreferencesPath);
                    Console.WriteLine("Secure Preferences dosyası başarıyla silindi.");
                    MessageBox.Show("Aşama 1 Başarılı! Sıradaki işlem için Tamam'a tıklayınız.");
                }
                else
                {
                    Console.WriteLine("Silinecek dosya bulunamadı.");
                    MessageBox.Show("HATA AŞAMA 1");
                }



                string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SecureSystem");

                // Hedef klasör varsa içindekileri sil
                if (Directory.Exists(targetFolder))
                {
                    DirectoryInfo directory = new DirectoryInfo(targetFolder);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                    {
                        subDirectory.Delete(true);
                    }

                    Console.WriteLine("Klasör ve içeriği başarıyla silindi.");
                    //MessageBox.Show("İşlem Başarılı! güvenle bu pencereyi kapatabilirsiniz.");
                    MessageBox.Show("Aşama 2 Başarılı! Tüm işlemler bitti!");
                }
                else
                {
                    Console.WriteLine("Silinecek klasör bulunamadı.");
                    MessageBox.Show("Silinecek klasör bulunamadı. HATA!");
                    MessageBox.Show("HATA AŞAMA 2");
                }





            }
            else
            {
                MessageBox.Show("İşlem iptal edildi!");
            }

            

        }
    }
}
