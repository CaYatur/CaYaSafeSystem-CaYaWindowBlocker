using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AdminPanel
{
    public partial class TwoFA : Form
    {


        public TwoFA()
        {
            InitializeComponent();
        }













        private async void button1_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri(AppSettings.TwoFactorAuthServerUrl);
            ClientWebSocket webSocket = new ClientWebSocket();
            CancellationToken cancellationToken = new CancellationToken();

            try
            {
                await webSocket.ConnectAsync(uri, cancellationToken);

                // Server'dan mesaj alınması
                byte[] receiveBuffer = new byte[1024];
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), cancellationToken);

                string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                //MessageBox.Show($"Sunucudan gelen mesaj: {receivedMessage}");

                // JSON mesajın içinden sadece password kısmını al
                dynamic json = JsonConvert.DeserializeObject(receivedMessage);
                string password = json.password;

                // Eşleşme kontrolü
                if (password == textBox1.Text)
                {
                    //MessageBox.Show("Mesajlar eşleşiyor. İşlem gerçekleştiriliyor...");
                    // İşlem gerçekleştirme kodunu buraya ekleyin
                    Hide();
                    Form1 f1 = new Form1();
                    f1.Show();
                }
                else
                {
                    MessageBox.Show("Hatalı parola!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}");
            }
            finally
            {
                if (webSocket.State == WebSocketState.Open)
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationToken);
                webSocket.Dispose();
            }
        }





        private async void TwoFA_Load(object sender, EventArgs e)
        {
            
        }






        private void TwoFA_FormClosing(object sender, FormClosingEventArgs e)
        {
            // WebSocket bağlantısı kapatılabilir
        }



        
    }
}
