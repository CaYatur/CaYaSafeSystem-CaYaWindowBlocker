using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace CYPCFixer
{
    public partial class Menu : Form
    {
        private int cpuThreshold = 20; // Varsayılan eşik değerleri // % lik cinsinden
        private int ramThresholdMB = 500; // MB cinsinden RAM eşik değeri
        private int gpuThreshold = 20; // % lik cinsinden

        public Menu()
        {
            InitializeComponent();
        }

        private async void btnOptimize_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            await Task.Run(() => ListHighResourceProcesses());
        }

        private void ListHighResourceProcesses()
        {
            Invoke((Action)(() => listBoxProcesses.Items.Clear())); // ListBox'ı temizle
            Process[] processes = Process.GetProcesses();
            int processCount = processes.Length;
            int processedCount = 0;

            Parallel.ForEach(processes, process =>
            {
                try
                {
                    processedCount++;
                    UpdateProgressBar(processedCount, processCount);

                    // Sistem süreçlerini hariç tut
                    if (IsSystemProcess(process)) return;

                    var cpuUsage = GetCpuUsageForProcess(process);
                    var ramUsageMB = GetRamUsageForProcess(process); // RAM kullanımını MB olarak al
                    var gpuUsage = GetGpuUsageForProcess(process); // GPU kullanımı ölçümü ekleyin

                    if (cpuUsage > cpuThreshold || ramUsageMB > ramThresholdMB || gpuUsage > gpuThreshold)
                    {
                        Invoke((Action)(() =>
                        {
                            listBoxProcesses.Items.Add($"{process.ProcessName} (PID: {process.Id}) - CPU: {cpuUsage}% - RAM: {ramUsageMB} MB - GPU: {gpuUsage}%");
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Süreç listelenemedi: {process.ProcessName}, Hata: {ex.Message}");
                }
            });

            if (listBoxProcesses.Items.Count == 0)
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show("Yüksek kaynak kullanan bir uygulama bulunamadı.", "CYPCFixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            }

            // İşlem tamamlandığında progress bar'ı sıfırla
            Invoke((Action)(() => progressBar1.Value = 0));
        }

        private bool IsSystemProcess(Process process)
        {
            try
            {
                string processPath = process.MainModule.FileName.ToLower();
                return processPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.System).ToLower()) ||
                       processPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.Windows).ToLower());
            }
            catch
            {
                return true;
            }
        }

        private void UpdateProgressBar(int processedCount, int processCount)
        {
            if (processCount <= 0)
            {
                // `processCount` sıfır veya negatif olduğunda, `progress` değerini 0 yap.
                progressBar1.Value = progressBar1.Minimum;
                return;
            }

            // `processedCount` ve `processCount`'a göre ilerleme yüzdesini hesapla
            int progress = (int)((double)processedCount / processCount * 100);

            // `progress` değerini `progressBar1`'ın Minimum ve Maximum değerleri arasında sınırla
            if (progress < progressBar1.Minimum)
                progress = progressBar1.Minimum;
            if (progress > progressBar1.Maximum)
                progress = progressBar1.Maximum;

            // `progressBar1`'in değerini ayarla
            Invoke((Action)(() =>
            {
                progressBar1.Value = progress;
            }));
        }


        private float GetCpuUsageForProcess(Process process)
        {
            try
            {
                var cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName, true);
                cpuCounter.NextValue();
                System.Threading.Thread.Sleep(500);
                return cpuCounter.NextValue() / Environment.ProcessorCount;
            }
            catch
            {
                return 0;
            }
        }

        private float GetRamUsageForProcess(Process process)
        {
            try
            {
                // RAM kullanımını MB olarak hesapla
                return process.WorkingSet64 / (1024 * 1024); // MB cinsinden
            }
            catch
            {
                return 0;
            }
        }

        public float GetGpuUsageForProcess(Process process)
        {
            try
            {
                float gpuUsage = 0;

                string query = "SELECT PercentProcessorTime FROM Win32_PerfFormattedData_GPUPerformanceCounters_GPUEngine WHERE Name LIKE 'pid_" + process.Id + "%_0'";

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                ManagementObjectCollection results = searcher.Get();

                foreach (ManagementObject result in results)
                {
                    gpuUsage += Convert.ToSingle(result["PercentProcessorTime"]);
                }

                return gpuUsage;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GPU kullanımı alınamadı: " + ex.Message);
                return 0;
            }
        }

        private float GetTotalPhysicalMemory()
        {
            var computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            return (float)(computerInfo.TotalPhysicalMemory / (1024 * 1024)); // Total Physical Memory in MB
        }

        private async void listBoxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProcesses.SelectedItem != null)
            {
                string selectedProcess = listBoxProcesses.SelectedItem.ToString();
                int pid = int.Parse(selectedProcess.Split(new string[] { "(PID: ", ")" }, StringSplitOptions.None)[1]);

                DialogResult dialogResult = MessageBox.Show(selectedProcess + " adlı uygulamanın yükünü azaltmak istiyor musunuz?", "Uygulama Yükünü Azalt", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    progressBar1.Value = 0;
                    await Task.Run(() => ReduceProcessLoad(pid, selectedProcess));
                }
            }
        }

        private void ReduceProcessLoad(int pid, string selectedProcess)
        {
            try
            {
                Process process = Process.GetProcessById(pid);

                // İşlem belleğini temizle
                SetProcessWorkingSetSize(process.Handle, -1, -1);

                Invoke((Action)(() =>
                {
                    MessageBox.Show(selectedProcess + " yükü azaltıldı.", "CYPCFixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show("Uygulama yükü azaltılamadı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        private async void btnCloseAll_Click(object sender, EventArgs e)
        {
            if (listBoxProcesses.Items.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Listelenen tüm işlemlerin yükünü azaltmak istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    progressBar1.Value = 0;
                    await Task.Run(() =>
                    {
                        int processCount = listBoxProcesses.Items.Count;
                        int processedCount = 0;

                        foreach (var item in listBoxProcesses.Items)
                        {
                            try
                            {
                                string selectedProcess = item.ToString();
                                int pid = int.Parse(selectedProcess.Split(new string[] { "(PID: ", ")" }, StringSplitOptions.None)[1]);

                                Process process = Process.GetProcessById(pid);
                                // İşlem belleğini temizle
                                SetProcessWorkingSetSize(process.Handle, -1, -1);

                                processedCount++;
                                UpdateProgressBar(processedCount, processCount);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Uygulama yükü azaltılamadı: {ex.Message}");
                            }
                        }

                        Invoke((Action)(() =>
                        {
                            MessageBox.Show("Tüm yüksek kaynak kullanan uygulamaların yükü azaltıldı.", "CYPCFixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Listeyi tekrar güncellemeyi kaldırdık, bu satır silindi
                            progressBar1.Value = 0;
                        }));
                    });
                }
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, uint dwFreeType);

        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint MEM_RELEASE = 0x00008000;
        const uint PAGE_READWRITE = 0x04;


        private async void btnCleanMemory_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            await Task.Run(() => CleanMemory());
        }

        private async void CleanMemory()
        {
            Process[] processes = Process.GetProcesses();
            int processCount = processes.Length;
            int processedCount = 0;

            foreach (Process process in processes)
            {
                try
                {
                    processedCount++;
                    UpdateProgressBar(processedCount, processCount);

                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);

                    // Bellek tahsisi
                    IntPtr ptr = VirtualAlloc(IntPtr.Zero, 1024, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);

                    if (ptr == IntPtr.Zero)
                    {
                        Console.WriteLine("Bellek tahsisi başarısız.");
                        return;
                    }

                    Console.WriteLine("Bellek tahsisi başarılı.");

                    // Bellek serbest bırakma
                    if (!VirtualFree(ptr, 0, MEM_RELEASE))
                    {
                        Console.WriteLine("Bellek serbest bırakma başarısız.");
                    }
                    else
                    {
                        Console.WriteLine("Bellek serbest bırakma başarılı.");
                    }

                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
                    SetProcessWorkingSetSize(process.Handle, -1, -1);


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"RAM temizlenemedi: {process.ProcessName}, Hata: {ex.Message}");
                }
            }

            Invoke((Action)(() =>
            {
                MessageBox.Show("RAM temizliği tamamlandı.", "CYPCFixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressBar1.Value = 0;
            }));
        }

        private async void btnForceCloseAll_Click(object sender, EventArgs e)
        {
            if (listBoxProcesses.Items.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Yüksek kaynak kullanan tüm uygulamaları zorla kapatmak istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    progressBar1.Value = 0;
                    await Task.Run(() =>
                    {
                        int processCount = listBoxProcesses.Items.Count;
                        int processedCount = 0;

                        foreach (var item in listBoxProcesses.Items)
                        {
                            try
                            {
                                string selectedProcess = item.ToString();
                                int pid = int.Parse(selectedProcess.Split(new string[] { "(PID: ", ")" }, StringSplitOptions.None)[1]);

                                // `taskkill` komutunu kullanarak işlemi kapat
                                ProcessStartInfo startInfo = new ProcessStartInfo
                                {
                                    FileName = "taskkill",
                                    Arguments = $"/PID {pid} /F",
                                    RedirectStandardOutput = true,
                                    RedirectStandardError = true,
                                    UseShellExecute = false,
                                    CreateNoWindow = true
                                };

                                using (Process process = Process.Start(startInfo))
                                {
                                    process.WaitForExit();
                                    string output = process.StandardOutput.ReadToEnd();
                                    string error = process.StandardError.ReadToEnd();

                                    if (!string.IsNullOrEmpty(error))
                                    {
                                        Console.WriteLine($"İşlem kapatılamadı: {error}");
                                    }
                                }

                                processedCount++;
                                UpdateProgressBar(processedCount, processCount);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"İşlem kapatılamadı: {ex.Message}");
                            }
                        }

                        Invoke((Action)(() =>
                        {
                            MessageBox.Show("Tüm yüksek kaynak kullanan uygulamalar zorla kapatıldı.", "CYPCFixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            progressBar1.Value = 0;
                        }));
                    });
                }
            }
        }


        private async void btnCleanTemp_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;

            await Task.Run(() => CleanTempFiles());
        }

        private void CleanTempFiles()
        {
            string tempPath = Path.GetTempPath(); // Temp dizininin yolu

            try
            {
                DirectoryInfo directory = new DirectoryInfo(tempPath);
                FileInfo[] files = directory.GetFiles();
                DirectoryInfo[] directories = directory.GetDirectories();

                // ProgressBar'ın maksimum değerini belirle
                int totalItems = files.Length + directories.Length;
                Invoke((Action)(() => progressBar1.Maximum = totalItems));

                int processedCount = 0;

                // Temp dosyalarını sil
                foreach (FileInfo file in files)
                {
                    try
                    {
                        file.Delete();
                        Console.WriteLine($"Silindi: {file.FullName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Dosya silinemedi: {file.FullName}, Hata: {ex.Message}");
                    }

                    processedCount++;
                    UpdateProgressBar(processedCount);
                }

                // Temp dizinlerini sil
                foreach (DirectoryInfo dir in directories)
                {
                    try
                    {
                        dir.Delete(true); // Dizin ve içindeki tüm dosyaları sil
                        Console.WriteLine($"Silindi: {dir.FullName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Dizin silinemedi: {dir.FullName}, Hata: {ex.Message}");
                    }

                    processedCount++;
                    UpdateProgressBar(processedCount);
                }

                Invoke((Action)(() =>
                {
                    MessageBox.Show("Temp dosyaları temizlendi.", "CYPCFixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                }));
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show("Temp dosyaları temizlenemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar1.Value = 0;
                }));
            }
        }

        private void UpdateProgressBar(int processedCount)
        {
            Invoke((Action)(() =>
            {
                progressBar1.Value = processedCount;
            }));
        }


        private async void btnCleanAll_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            await Task.Run(() => CleanAll());
        }

        private void CleanAll()
        {
            var tasks = new[]
            {
                Task.Run(() => ClearBrowserCache()),
                Task.Run(() => ClearWindowsTempFiles()),
                Task.Run(() => ClearApplicationCache(Path.GetTempPath())), // Örnek olarak temp dizinini kullandık
                Task.Run(() => EmptyRecycleBin()),
                Task.Run(() => CleanTempFiles())
            };

            int totalTasks = tasks.Length;
            int completedTasks = 0;

            Task.WhenAll(tasks).ContinueWith(_ =>
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show("Temizlik işlemleri tamamlandı.", "CYPCFixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                }));
            });

            foreach (var task in tasks)
            {
                task.ContinueWith(_ =>
                {
                    completedTasks++;
                    UpdateProgressBar(completedTasks, totalTasks);
                });
            }
        }

        private void ClearBrowserCache()
        {
            string[] browserCachePaths =
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Cookies))
            };

            foreach (string path in browserCachePaths)
            {
                if (Directory.Exists(path))
                {
                    try
                    {
                        DirectoryInfo directory = new DirectoryInfo(path);
                        foreach (FileInfo file in directory.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in directory.GetDirectories())
                        {
                            dir.Delete(true); // Dizin ve içindeki dosyaları sil
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Geçici internet dosyaları temizlenemedi: {path}, Hata: {ex.Message}");
                    }
                }
            }
        }

        private void ClearWindowsTempFiles()
        {
            string[] windowsTempPaths =
            {
                Path.GetTempPath(),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "SoftwareDistribution\\Download")
            };

            foreach (string path in windowsTempPaths)
            {
                if (Directory.Exists(path))
                {
                    try
                    {
                        DirectoryInfo directory = new DirectoryInfo(path);
                        foreach (FileInfo file in directory.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in directory.GetDirectories())
                        {
                            dir.Delete(true); // Dizin ve içindeki dosyaları sil
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Windows temp dosyaları temizlenemedi: {path}, Hata: {ex.Message}");
                    }
                }
            }
        }

        private void ClearApplicationCache(string appCachePath)
        {
            if (Directory.Exists(appCachePath))
            {
                try
                {
                    DirectoryInfo directory = new DirectoryInfo(appCachePath);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in directory.GetDirectories())
                    {
                        dir.Delete(true); // Dizin ve içindeki dosyaları sil
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Uygulama önbelleği temizlenemedi: {appCachePath}, Hata: {ex.Message}");
                }
            }
        }

        private void EmptyRecycleBin()
        {
            try
            {
                // Geri dönüşüm kutusunu boşaltmak için Windows API kullanılabilir.
                SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOSOUND);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Geri dönüşüm kutusu boşaltılamadı: {ex.Message}");
            }
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);

        private const uint SHERB_NOSOUND = 0x00000001;

        private async void PCallOP_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Tüm bilgisayarı tarayıp gereken işlemlerin hepsinin otomatik yapılmasını istiyormusunuz? Artık dosyaları temizleme, Gereken uygulamaları kapatma, Bellek temizleme", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                progressBar1.Value = 0;

                await Task.Run(() => ListHighResourceProcesses());
                await Task.Run(() => CleanAll());
                await Task.Run(() => CleanMemory());

                btnForceCloseAll_Click(this, EventArgs.Empty);

                MessageBox.Show("İşlemler Tamamlandı!");

            }
        }
    }
}