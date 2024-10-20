namespace Notif
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Combine the command-line arguments into a single string
            string joinedArgs = string.Join(" ", args);

            // Check command-line arguments
            if (args.Length >= 3 && args.Any(arg => arg.StartsWith("-texthere")))
            {
                // Extract text from command-line arguments
                string[] parts = joinedArgs.Split(new[] { "-texthere" }, StringSplitOptions.RemoveEmptyEntries)
                                           .Select(s => s.Trim())
                                           .ToArray();

                if (parts.Length >= 3)
                {
                    // Extracted text
                    string text1 = parts[0];
                    string text2 = parts[1];
                    string text3 = parts[2];
                    string text4 = parts[3];

                    // Run the application with the main form and pass extracted text
                    Application.Run(new Form1(text1, text2, text3, text4));
                    return;
                }
            }

            MessageBox.Show("Invalid command-line arguments. Usage: myapp.exe -texthere1 -texthere2 -texthere3");
        }
    }
}