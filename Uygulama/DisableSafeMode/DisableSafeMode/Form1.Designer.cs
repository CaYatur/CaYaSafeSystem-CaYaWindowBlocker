namespace DisableSafeMode
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
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            pictureBox3 = new PictureBox();
            label4 = new Label();
            textBox1 = new TextBox();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(85, 159);
            label1.Name = "label1";
            label1.Size = new Size(612, 30);
            label1.TabIndex = 0;
            label1.Text = "GÜVENLİ MOD GÜVENLİK SEBEBİYLE DEVRE DIŞI BIRAKILDI!";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.PCCC2;
            pictureBox1.Location = new Point(327, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.pc_ERROR;
            pictureBox2.Location = new Point(12, 388);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(50, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(68, 423);
            label2.Name = "label2";
            label2.Size = new Size(482, 15);
            label2.TabIndex = 3;
            label2.Text = "Bilgisayar devre dışı bıraklıdı vede normal kullanım için bilgisayar kitlenmiş bulunmaktadır.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.Highlight;
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(163, 15);
            label3.TabIndex = 5;
            label3.Text = "CaYaSafeSystemForSafeMode";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.WWblock;
            pictureBox3.Location = new Point(191, -58);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(400, 250);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            pictureBox3.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(12, 325);
            label4.Name = "label4";
            label4.Size = new Size(266, 60);
            label4.TabIndex = 7;
            label4.Text = "Güvenlik nedeniyle:\r\nBilgisayarı kapatmak veya yeniden başlatmak için\r\nCTRL+ALT+DELETE\r\nkombinasyonunu kullanınız.";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(627, 415);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(161, 23);
            textBox1.TabIndex = 8;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(627, 397);
            label5.Name = "label5";
            label5.Size = new Size(121, 15);
            label5.TabIndex = 9;
            label5.Text = "12 Haneli kodu giriniz";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label5);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Opacity = 0D;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DisableSafeMode";
            TopMost = true;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label2;
        private Label label3;
        private PictureBox pictureBox3;
        private Label label4;
        private TextBox textBox1;
        private Label label5;
    }
}