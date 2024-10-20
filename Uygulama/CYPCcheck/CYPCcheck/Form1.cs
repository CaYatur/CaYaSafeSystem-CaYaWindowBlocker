using System.Diagnostics;

namespace CYPCcheck
{
    public partial class Form1 : Form
    {
        bool Closing = false;
        string programName = "PCDisableCY";
        private static bool isShutdownTriggered = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Thread bgThread = new Thread(() =>
            {

                while (Closing == false)
                {

                    // Belirli bir isme sahip çalýþan tüm iþlemleri al
                    Process[] processes = Process.GetProcessesByName(programName);



                    // Eðer belirtilen program çalýþmýyorsa
                    if (processes.Length == 0)
                    {
                        // Ýþlemi gerçekleþtir
                        //Process.Start("taskkill", "/F /IM " + programName); // Örnek olarak notepad.exe'yi kapatýr

                        ShutdownComputer();

                        Console.WriteLine(programName + " programý çalýþmýyor, iþlem gerçekleþtirildi.");
                    }
                    else
                    {
                        //Console.WriteLine(programName + " programý çalýþýyor.");
                    }


                    //Thread.Sleep(5);
                }


            });
            bgThread.IsBackground = true;
            bgThread.Start();

            ProgramCheck();
        }


        private void ProgramCheck()
        {
            // Mevcut programýn adýný al
            string programName = Process.GetCurrentProcess().ProcessName;

            // Çalýþan tüm iþlemleri kontrol edin
            Process[] runningProcesses = Process.GetProcessesByName(programName);

            if (runningProcesses.Length > 1)
            {
                // Birden fazla örnek çalýþýyor
                Console.WriteLine($"{programName} programýnýn birden fazla örneði çalýþýyor.");

                // Burada baþka bir iþlem gerçekleþtirebilirsiniz
                // Örneðin, tüm örnekleri kapatmak istiyorsanýz:
                //foreach (var process in runningProcesses)
                //{
                //    if (process.Id != Process.GetCurrentProcess().Id) // Kendi sürecinizi kapatmamak için kontrol
                //    {
                //        process.Kill();
                //        Console.WriteLine($"{process.ProcessName} kapatýldý. (ID: {process.Id})");
                //    }
                //}

                ShutdownComputer();

            }
            else if (runningProcesses.Length == 1)
            {
                // Sadece bir örnek çalýþýyor
                Console.WriteLine($"{programName} programýnýn bir örneði çalýþýyor.");
            }
            else
            {
                // Hiçbir örnek çalýþmýyor
                Console.WriteLine($"{programName} programýnýn çalýþmadýðý tespit edildi. Bu durumun olmasý beklenmiyor.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }

        static void ShutdownComputer()
        {
            if (!isShutdownTriggered) // Kapatma iþlemi daha önce tetiklenmemiþse
            {
                isShutdownTriggered = true; // Kapatma iþlemini tetikle
                ProcessStartInfo processInfo = new ProcessStartInfo("shutdown", "/s /f /t 0")
                {
                    CreateNoWindow = true, // Pencereyi oluþturma
                    UseShellExecute = false // Shell kullanma
                };

                Process.Start(processInfo);
            }
            else
            {
                Console.WriteLine("Bilgisayar zaten kapanma iþlemi için tetiklendi.");
            }
        }


    }
}