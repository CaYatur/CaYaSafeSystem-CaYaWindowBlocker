using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ServerStartt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        const int SW_HIDE = 0;
        private void Form1_Load(object sender, EventArgs e)
        {


            //// Çalýþtýrýlacak uygulamanýn yolunu ve adýný belirtin
            //string uygulamaYolu = @"C:\Program Files (x86)\CaYaSafe\server.exe";

            //// ProcessStartInfo nesnesini oluþturun ve konsolu gizleyin
            //ProcessStartInfo startInfo = new ProcessStartInfo
            //{
            //    FileName = uygulamaYolu,
            //    CreateNoWindow = true,
            //    UseShellExecute = false
            //};

            //// Process nesnesini baþlatýn
            //using (Process exeProcess = Process.Start(startInfo))
            //{
            //    exeProcess.WaitForExit();
            //}



            // Çalýþtýrýlacak uygulamanýn yolu ve adýný belirtin
            string uygulamaYolu = @"C:\Program Files (x86)\CaYaSafe\server.exe";

            // ProcessStartInfo nesnesini oluþturun ve konsolu gizleyin
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c cd /d \"{System.IO.Path.GetDirectoryName(uygulamaYolu)}\" && \"{uygulamaYolu}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = System.IO.Path.GetDirectoryName(uygulamaYolu)
            };

            // Process nesnesini baþlatýn
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}