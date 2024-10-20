using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckUnAuth
{
    public partial class APROVED : Form
    {
        public APROVED()
        {
            InitializeComponent();
            Form1.RequestEvent2 += HandleForm2Request;
        }

        private async void APROVED_Load(object sender, EventArgs e)
        {
            TopMost = true;
            TopMost = false;
            TopMost = true;
            TopMost = false;
            TopMost = true;
            TopMost = false;
            TopMost = true;
            TopMost = false;
            TopMost = true;
        }

        private async void HandleForm2Request(object sender, EventArgs e)
        {
            // Form2'den gelen isteği işleyin.
            // Bu metot, Form2'de bir buton tıklandığında çağrılacak.
            //MessageBox.Show("Form2'den istek alındı ve işlem gerçekleştirildi.");
            await Task.Delay(4500);

            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipTitle = "Yetkili Girişi";
            notifyIcon1.BalloonTipText = "Giriş İşlemi Başarıyla Gerçekleştirildi!";
            notifyIcon1.ShowBalloonTip(100);
            Hide();

        }
    }
}
