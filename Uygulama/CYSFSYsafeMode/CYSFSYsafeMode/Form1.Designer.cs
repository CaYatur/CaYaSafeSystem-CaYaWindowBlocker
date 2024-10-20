namespace CYSFSYsafeMode
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox1 = new TextBox();
            label7 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Green;
            button1.ForeColor = Color.White;
            button1.Location = new Point(12, 228);
            button1.Name = "button1";
            button1.Size = new Size(220, 88);
            button1.TabIndex = 0;
            button1.Text = "Güvenli modu devre dışı bırak";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.ForeColor = Color.White;
            button2.Location = new Point(392, 228);
            button2.Name = "button2";
            button2.Size = new Size(220, 88);
            button2.TabIndex = 1;
            button2.Text = "Güvenli modu aktif et";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 210);
            label1.Name = "label1";
            label1.Size = new Size(213, 15);
            label1.TabIndex = 2;
            label1.Text = "Güvenli modu kullanılamaz hale getirir.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(392, 210);
            label2.Name = "label2";
            label2.Size = new Size(145, 15);
            label2.TabIndex = 3;
            label2.Text = "Güvenli mod kullanılabilir.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(489, 30);
            label3.TabIndex = 4;
            label3.Text = "12 Basamaklı kod oluşturulmak için bekleniyor...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(12, 39);
            label4.Name = "label4";
            label4.Size = new Size(492, 30);
            label4.TabIndex = 5;
            label4.Text = "Kodu lütfen biryere yazın yada fotoğrafını çekin.";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(12, 90);
            label5.Name = "label5";
            label5.Size = new Size(67, 21);
            label5.TabIndex = 6;
            label5.Text = "Durum:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(12, 69);
            label6.Name = "label6";
            label6.Size = new Size(467, 21);
            label6.TabIndex = 7;
            label6.Text = "Bu kod Güvenli Modu açtığınızda giriş yapmak için gerekicektir.";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(204, 155);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(220, 23);
            textBox1.TabIndex = 8;
            textBox1.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(160, 137);
            label7.Name = "label7";
            label7.Size = new Size(308, 15);
            label7.TabIndex = 9;
            label7.Text = "Kaldırmak veya tekrar oluşturmak için mevcut kodu girin.";
            label7.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 328);
            Controls.Add(label7);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Güvenli mod ayarlayıcı";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private Label label7;
    }
}