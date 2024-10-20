namespace SiteEngelleyiciArayüz
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread backgroundThread = new Thread(() =>
            {
                Application.Run(new DisableTaskManager());
            });
            backgroundThread.Start();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            //Application.Run(new Lock());
            //Application.Run(new Kilit());
            //Application.Run(new cancel());
            //Application.Run(new CustomNotif());
        }
    }
}