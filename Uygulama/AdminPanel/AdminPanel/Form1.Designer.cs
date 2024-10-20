namespace AdminPanel
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            txtNewItem = new TextBox();
            listBox1 = new ListBox();
            label1 = new Label();
            button4 = new Button();
            label3 = new Label();
            button5 = new Button();
            label4 = new Label();
            label5 = new Label();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            label2 = new Label();
            button9 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(565, 330);
            button1.Name = "button1";
            button1.Size = new Size(160, 56);
            button1.TabIndex = 0;
            button1.Text = "Cihaz Ekle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(160, 56);
            button2.TabIndex = 1;
            button2.Text = "Loglar (Tutulan kayıtlar)";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(178, 12);
            button3.Name = "button3";
            button3.Size = new Size(160, 56);
            button3.TabIndex = 2;
            button3.Text = "Bildirim gönder";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // txtNewItem
            // 
            txtNewItem.Location = new Point(384, 363);
            txtNewItem.Name = "txtNewItem";
            txtNewItem.Size = new Size(175, 23);
            txtNewItem.TabIndex = 3;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(565, 20);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(160, 244);
            listBox1.TabIndex = 4;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(384, 315);
            label1.Name = "label1";
            label1.Size = new Size(155, 45);
            label1.TabIndex = 5;
            label1.Text = "IPV4 (SizeBağlıBilgisayarAdı)\r\nÖrnek:\r\n192.168.1.100 (Bilgisayar1)";
            // 
            // button4
            // 
            button4.Location = new Point(344, 12);
            button4.Name = "button4";
            button4.Size = new Size(160, 56);
            button4.TabIndex = 7;
            button4.Text = "Kara-Beyaz Liste";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.IndianRed;
            label3.Location = new Point(12, 140);
            label3.Name = "label3";
            label3.Size = new Size(436, 30);
            label3.TabIndex = 8;
            label3.Text = "Bilgilendirme: Burdaki tüm işlemler için karşıdaki bilgisayarların açık olması gerek.\r\naçık olmayan üzerinde işlem yapamazsınız.";
            // 
            // button5
            // 
            button5.Location = new Point(14, 222);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 9;
            button5.Text = "Yenile";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 189);
            label4.Name = "label4";
            label4.Size = new Size(158, 15);
            label4.TabIndex = 10;
            label4.Text = "Mevcut açık bilgisayar sayısı:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.MenuHighlight;
            label5.Location = new Point(14, 204);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 11;
            label5.Text = "Bekleniyor...";
            // 
            // button6
            // 
            button6.BackColor = Color.IndianRed;
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.Location = new Point(12, 337);
            button6.Name = "button6";
            button6.Size = new Size(193, 49);
            button6.TabIndex = 12;
            button6.Text = "Tüm bilgisayarları kapat";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(565, 268);
            button7.Name = "button7";
            button7.Size = new Size(160, 56);
            button7.TabIndex = 14;
            button7.Text = "Cihaz Kaldır \r\n(Yukarıdan cihaz seçiniz)";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(12, 74);
            button8.Name = "button8";
            button8.Size = new Size(160, 56);
            button8.TabIndex = 15;
            button8.Text = "Ekran izle";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 304);
            label2.Name = "label2";
            label2.Size = new Size(151, 30);
            label2.TabIndex = 16;
            label2.Text = "Açık olan tüm bilgisayarlar\r\n1 dakika içerisinde kapatılır.";
            // 
            // button9
            // 
            button9.Location = new Point(178, 74);
            button9.Name = "button9";
            button9.Size = new Size(160, 56);
            button9.TabIndex = 17;
            button9.Text = "Dosya transfer/komut çalıştırma";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 398);
            Controls.Add(button9);
            Controls.Add(label2);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(listBox1);
            Controls.Add(txtNewItem);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AdminPanel CaYaSafeSystem";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox txtNewItem;
        private ListBox listBox1;
        private Label label1;
        private Button button4;
        private Label label3;
        private Button button5;
        private Label label4;
        private Label label5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Label label2;
        private Button button9;
    }
}