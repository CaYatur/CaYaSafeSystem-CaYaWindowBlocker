namespace CaYaSafeSystemSetup
{
    public partial class Form1 : Form
    {
        string dosyaYolu = "C:\\Program Files (x86)\\CaYa\\CaYaSafeSystem\\ver.txt";

        string hedefVersiyon = "1.0.0"; // Hedef versiyonu burada belirtin


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            checkver();
            label4.Text = "Sürüm: " + hedefVersiyon;


        }

        private void checkver()
        {

            if (File.Exists(dosyaYolu))
            {
                string dosyaIcerigi = File.ReadAllText(dosyaYolu);

                if (VersiyonKarsilastir(dosyaIcerigi, hedefVersiyon) == 0)
                {
                    Console.WriteLine("Dosya belirtilen versiyona sahip.");
                    button3.Enabled = true;
                    button1.Visible = true;
                    button2.Visible = false;
                    // Burada yapýlacak iþlemi gerçekleþtirin
                }
                else if (VersiyonKarsilastir(dosyaIcerigi, hedefVersiyon) > 0)
                {
                    Console.WriteLine("Dosya belirtilen versiyondan büyük.");
                    button2.Enabled = false;
                    label3.Visible = true;
                    // Burada yapýlacak iþlemi gerçekleþtirin
                }
                else
                {
                    Console.WriteLine("Dosya belirtilen versiyondan küçük.");
                    button4.Visible = true;
                    // Burada yapýlacak iþlemi gerçekleþtirin
                }
            }
            else
            {
                Console.WriteLine("Belirtilen dosya bulunamadý.");
            }


        }

        static int VersiyonKarsilastir(string versiyon1, string versiyon2)
        {
            string[] versiyon1Parcalar = versiyon1.Split('.');
            string[] versiyon2Parcalar = versiyon2.Split('.');

            for (int i = 0; i < Math.Max(versiyon1Parcalar.Length, versiyon2Parcalar.Length); i++)
            {
                int v1 = i < versiyon1Parcalar.Length ? int.Parse(versiyon1Parcalar[i]) : 0;
                int v2 = i < versiyon2Parcalar.Length ? int.Parse(versiyon2Parcalar[i]) : 0;

                if (v1 < v2)
                    return -1;
                else if (v1 > v2)
                    return 1;
            }

            return 0;
        }

        //ONAR
        private void button1_Click(object sender, EventArgs e)
        {
            //onar or = new onar();
            //or.Show();
            //Hide();


            string klasorYolu = Path.GetDirectoryName(dosyaYolu);
            if (!Directory.Exists(klasorYolu))
            {
                Directory.CreateDirectory(klasorYolu);
            }
            using (StreamWriter writer = File.CreateText(dosyaYolu))
            {
                writer.WriteLine(hedefVersiyon);
            }

            Yukle yk = new Yukle();
            yk.Show();
            Hide();
        }
        //YÜKLE
        private void button2_Click(object sender, EventArgs e)
        {
            string klasorYolu = Path.GetDirectoryName(dosyaYolu);
            if (!Directory.Exists(klasorYolu))
            {
                Directory.CreateDirectory(klasorYolu);
            }
            using (StreamWriter writer = File.CreateText(dosyaYolu))
            {
                writer.WriteLine(hedefVersiyon);
            }

            Yukle yk = new Yukle();
            yk.Show();
            Hide();
        }
        //KALDIR
        private void button3_Click(object sender, EventArgs e)
        {
            //// Dosyayý silme iþlemi
            //if (File.Exists(dosyaYolu))
            //{
            //    File.Delete(dosyaYolu);
            //    Console.WriteLine("Dosya silindi.");
            //}
            //else
            //{
            //    Console.WriteLine("Belirtilen dosya bulunamadý.");
            //}
            Kaldir kl = new Kaldir();
            kl.Show();
            Hide();
        }
        //GÜNCELLE
        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = File.CreateText(dosyaYolu))
            {
                //writer.WriteLine("Bu dosya belirli bir versiyona sahip deðil.");
                writer.WriteLine(hedefVersiyon);
            }
            guncelle guncelle = new guncelle();
            guncelle.Show();
            Hide();
        }
    }
}