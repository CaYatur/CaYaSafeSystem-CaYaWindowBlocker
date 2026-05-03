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
    public partial class onar : Form
    {
        public onar()
        {
            InitializeComponent();
        }

        private async void onar_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;

            // ProgressBar'ın ilerlemesini sıfırla
            progressBar1.Value = 0;

            // Bazı işlemleri simüle etmek için döngü kullan
            for (int i = 0; i <= 100; i++)
            {
                // İşlem sırasında bekleme süresi eklemek için Thread.Sleep kullanılabilir
                // System.Threading.Thread.Sleep(50);
                await Task.Delay(50);
                // ProgressBar'ı güncelle
                progressBar1.Value = i;

                // ProgressBar'ın güncellenmesini sağlamak için yenileme işlemi yap
                progressBar1.Refresh();

                // Yapılacak işlemi buraya yazabilirsiniz
                // Örneğin, dosya kopyalama, veritabanı işlemleri, vb.
            }

            if (progressBar1.Value == 100)
            {
                //İşlem gerçekleştir


                Hide();
                //MessageBox.Show("Onarma İşlemi Tamamlandı!");
                //Environment.Exit(0);
                repair rp = new repair();
                rp.Show();
            }
        }

        private void onar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("Yükleme esnasına yükleyici kapatılamaz.");
        }
    }
}
