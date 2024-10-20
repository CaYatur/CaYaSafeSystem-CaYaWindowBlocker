namespace CaYaLabOffline
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Location = new Point(-1, -11);
            panel1.Name = "panel1";
            panel1.Size = new Size(10, 586);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveBorder;
            panel2.Location = new Point(975, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(13, 586);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveBorder;
            panel3.Location = new Point(5, 553);
            panel3.Name = "panel3";
            panel3.Size = new Size(1053, 12);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveBorder;
            panel4.Location = new Point(8, -7);
            panel4.Name = "panel4";
            panel4.Size = new Size(1053, 16);
            panel4.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.wifi__Özel_;
            pictureBox1.Location = new Point(168, 38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(70, 59);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(15, 535);
            label1.Name = "label1";
            label1.Size = new Size(191, 15);
            label1.TabIndex = 5;
            label1.Text = "İnternet Bağlantısını Kontrol Ediniz.";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonShadow;
            button1.Location = new Point(238, 185);
            button1.Name = "button1";
            button1.Size = new Size(190, 41);
            button1.TabIndex = 6;
            button1.Text = "Tekrar Dene";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.InfoText;
            label2.Location = new Point(238, 32);
            label2.Name = "label2";
            label2.Size = new Size(510, 65);
            label2.TabIndex = 8;
            label2.Text = "Ağa Bağlanamıyoruz.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.InfoText;
            label3.Location = new Point(312, 97);
            label3.Name = "label3";
            label3.Size = new Size(353, 40);
            label3.TabIndex = 9;
            label3.Text = "İnternet Bağlantı Hatası!";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.InfoText;
            label4.Location = new Point(209, 150);
            label4.Name = "label4";
            label4.Size = new Size(586, 17);
            label4.TabIndex = 10;
            label4.Text = "Lütfen Bilgisayarınızın Wi-Fi Bağlantısını Kontrol Ediniz. Eğer Bağlı Değilse Lütfen Bağlayınız.";
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.Location = new Point(583, 185);
            button2.Name = "button2";
            button2.Size = new Size(190, 41);
            button2.TabIndex = 11;
            button2.Text = "Kapat";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.MediumAquamarine;
            button3.Location = new Point(853, 506);
            button3.Name = "button3";
            button3.Size = new Size(116, 41);
            button3.TabIndex = 12;
            button3.Text = "Bilgisayarı Kapat";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click_1;
            // 
            // button4
            // 
            button4.BackColor = Color.SkyBlue;
            button4.Location = new Point(690, 506);
            button4.Name = "button4";
            button4.Size = new Size(157, 41);
            button4.TabIndex = 13;
            button4.Text = "Bilgisayarı Yeniden Başlat";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(985, 563);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YouAreOffline";
            TopMost = true;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private PictureBox pictureBox1;
        private Label label1;
        private Button button1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}