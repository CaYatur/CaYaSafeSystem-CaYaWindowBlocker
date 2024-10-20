using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaYaLabOffline
{
    public partial class Offline : Form
    {

        private System.Windows.Forms.Timer timerCheck;
        public Offline()
        {
            InitializeComponent();
            InitializeTimer();
        }


        public static bool IsWifiAvailable()
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                     networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
            }

            return false;
        }



        private bool IsNetworkAvailable()
        {
            bool isWifiConnected = false;
            bool isLANConnected = false;

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                // Wi-Fi bağlantısı kontrolü
                if ((networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    isWifiConnected = true;
                }

                // LAN bağlantısı kontrolü
                else if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    isLANConnected = true;
                }
            }

            // Hem Wi-Fi hem de LAN bağlantısı yoksa işlem gerçekleştir
            if (!isLANConnected)
            {
                // İşlemi gerçekleştir
                Console.WriteLine("assda");
                return false;
            }

            return true; // En az bir tür bağlı ise işlemi gerçekleştirme
        }








        private void Offline_Load(object sender, EventArgs e)
        {
            wifiCheckTimer.Interval = 1000; // 10 saniyede bir kontrol et
            wifiCheckTimer.Tick += new EventHandler(CheckWifiStatus);
            wifiCheckTimer.Start();
        }



        private bool IsNetworkConnected()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    GatewayIPAddressInformationCollection gatewayAddresses = networkInterface.GetIPProperties().GatewayAddresses;

                    if (gatewayAddresses.Count > 0)
                    {
                        // Cihaz ağa bağlı çünkü en az bir varsayılan ağ geçidi var.
                        return true;
                    }
                }
            }

            // Cihaz ağa bağlı değil.
            return false;
        }

        private void CheckWifiStatus(object sender, EventArgs e)
        {
            if (IsNetworkConnected())
            {
                // Wifi bağlantısı mevcut, belirli işlemleri gerçekleştirin.

                timerCheck.Stop();
            }
            else
            {
                // Timer'ı ayarla (10000 milisaniyede bir, yani 10 saniyede bir çalışacak)


                timerCheck.Start();
            }
        }




        private void TimerCheck_Tick(object sender, EventArgs e)
        {

            AntiRemove();

            // Burada belirli bir programın (örneğin Notepad.exe) çalışıp çalışmadığını kontrol ediyoruz
            string targetProcessName = "chrome";
            if (IsProcessRunning(targetProcessName))
            {

                /////
                ///
                Google("chrome");
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName2 = "cmd";
            if (IsProcessRunning(targetProcessName2))
            {

                /////
                ///
                Google("cmd");
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName3 = "WindowsTerminal";
            if (IsProcessRunning(targetProcessName3))
            {

                /////
                ///
                Google("WindowsTerminal");
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName4 = "powershell";
            if (IsProcessRunning(targetProcessName4))
            {

                /////
                ///
                Google(targetProcessName4);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName5 = "powershell_ise";
            if (IsProcessRunning(targetProcessName5))
            {

                /////
                ///
                Google(targetProcessName5);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName6 = "taskschd.msc";
            if (IsProcessRunning(targetProcessName6))
            {

                /////
                ///
                Google(targetProcessName6);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName7 = "mmc";
            if (IsProcessRunning(targetProcessName7))
            {

                /////
                ///
                Google(targetProcessName7);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName8 = "regedit";
            if (IsProcessRunning(targetProcessName8))
            {

                /////
                ///
                Google(targetProcessName8);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName9 = "procexp";
            if (IsProcessRunning(targetProcessName9))
            {

                /////
                ///
                Google(targetProcessName9);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName10 = "procexp64";
            if (IsProcessRunning(targetProcessName10))
            {

                /////
                ///
                Google(targetProcessName10);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName11 = "procexp64a";
            if (IsProcessRunning(targetProcessName11))
            {

                /////
                ///
                Google(targetProcessName11);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }
            string targetProcessName12 = "ProcessHacker";
            if (IsProcessRunning(targetProcessName12))
            {

                /////
                ///
                Google(targetProcessName12);
                Form1 Offline = new Form1();
                Offline.ShowDialog();

            }

        }

        private bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }




        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_CLOSE = 0x0010;




        private void AntiRemove()
        {
            // Kapatmak istediğiniz pencerenin adını belirtin
            string hedefPencereAdi = "Program Dosyaları (x86)";
            string hedefPencereAdi2 = "Görev Yöneticisi";

            // Pencere adına göre pencereyi zorla kapatma fonksiyonunu çağırın
            ZorlaKapatPencere(hedefPencereAdi);
            ZorlaKapatPencere(hedefPencereAdi2);
            ZorlaKapatPencere("Komut İstemi");
            ZorlaKapatPencere("Sysinternals Process Explorer");
            ZorlaKapatPencere("Process Explorer");
            ZorlaKapatPencere("Komut İstemi");
        }



        static void ZorlaKapatPencere(string pencereAdi)
        {
            // Pencere tanıtıcısını (handle) bul
            IntPtr hWindow = FindWindow(null, pencereAdi);

            // Pencere bulunduysa
            if (hWindow != IntPtr.Zero)
            {
                // WM_CLOSE mesajını göndererek pencereyi kapat
                SendMessage(hWindow, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Console.WriteLine("Pencere zorla kapatıldı.");
                Form1 Offline = new Form1();
                Offline.ShowDialog();
            }
            else
            {
                Console.WriteLine("Belirtilen pencere adına sahip bir pencere bulunamadı.");
            }
        }







        private void InitializeTimer()
        {
            // Timer oluşturulması ve ayarlanması
            timerCheck = new System.Windows.Forms.Timer();
            timerCheck.Interval = 850; // 5000 milisaniye (5 saniye)
            timerCheck.Tick += new EventHandler(TimerCheck_Tick);

        }

        private void Google(string processNameToClose)
        {
            //string processNameToClose = "chrome"; // Kapatılacak hedef uygulamanın adını buraya girin.

            // Process.GetProcessesByName yöntemiyle hedef uygulamayı bulun
            Process[] processes = Process.GetProcessesByName(processNameToClose);

            if (processes.Length > 0)
            {
                foreach (Process process in processes)
                {
                    // Hedef uygulamayı kapat
                    process.Kill();
                    Console.WriteLine($"{processNameToClose} başarıyla kapatıldı.");
                }
            }
            else
            {
                Console.WriteLine($"{processNameToClose} adında bir uygulama bulunamadı.");
            }

        }

        private void Offline_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
