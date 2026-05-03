using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaYaSafeSystemSetup
{
    public partial class EnableAdons : Form
    {
        public EnableAdons()
        {
            InitializeComponent();
        }

        private void EnableAdons_Load(object sender, EventArgs e)
        {

        }

        private void EnableAdons_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("Yükleme esnasına yükleyici kapatılamaz.");
        }

        private void button2_Click(object sender, EventArgs e)
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
            button1.Text = "İleri";
            MessageBox.Show("İşlem Başarılı! Sıradaki aşamaya geçebilirsiniz.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Finish fh = new Finish();
            fh.Show();
        }
    }
}
