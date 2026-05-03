namespace AdminPanel
{
    partial class BlackWhiteListSystem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlackWhiteListSystem));
            listBox1 = new ListBox();
            checkBox1 = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            listBox2 = new ListBox();
            listBox3 = new ListBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 72);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(136, 394);
            listBox1.TabIndex = 0;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(12, 47);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(139, 19);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Tüm bilgisayarları seç";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(37, 9);
            label1.Name = "label1";
            label1.Size = new Size(72, 21);
            label1.TabIndex = 2;
            label1.Text = "Cihazlar";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(802, 9);
            label2.Name = "label2";
            label2.Size = new Size(79, 21);
            label2.TabIndex = 3;
            label2.Text = "KaraListe";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(429, 9);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 4;
            label3.Text = "BeyazListe";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(154, 332);
            label4.Name = "label4";
            label4.Size = new Size(143, 60);
            label4.TabIndex = 5;
            label4.Text = "BeyazListe\r\nMevcut serviste \r\nengellenen pencere\r\nengelini kaldırmak içindir.";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(154, 406);
            label5.Name = "label5";
            label5.Size = new Size(148, 60);
            label5.TabIndex = 6;
            label5.Text = "KaraListe\r\nMevcut serviste \r\nengellenen pencereler\r\ndışında engellemek içindir.";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(329, 103);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(293, 23);
            textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(699, 103);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(294, 23);
            textBox2.TabIndex = 8;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(329, 132);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(293, 334);
            listBox2.TabIndex = 9;
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.ItemHeight = 15;
            listBox3.Location = new Point(700, 131);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(294, 334);
            listBox3.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new Point(329, 74);
            button1.Name = "button1";
            button1.Size = new Size(293, 23);
            button1.TabIndex = 11;
            button1.Text = "Beyaz listeye ekle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(699, 76);
            button2.Name = "button2";
            button2.Size = new Size(293, 23);
            button2.TabIndex = 12;
            button2.Text = "Kara listeye ekle";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.IndianRed;
            button3.ForeColor = SystemColors.Control;
            button3.Location = new Point(329, 47);
            button3.Name = "button3";
            button3.Size = new Size(293, 23);
            button3.TabIndex = 13;
            button3.Text = "Beyaz listeden kaldır";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.IndianRed;
            button4.ForeColor = SystemColors.Control;
            button4.Location = new Point(700, 47);
            button4.Name = "button4";
            button4.Size = new Size(293, 23);
            button4.TabIndex = 14;
            button4.Text = "Kara listeden kaldır";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // BlackWhiteListSystem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1016, 480);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox2);
            Controls.Add(listBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(checkBox1);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BlackWhiteListSystem";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BlackWhiteListSystem";
            Load += BlackWhiteListSystem_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private CheckBox checkBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox1;
        private TextBox textBox2;
        private ListBox listBox2;
        private ListBox listBox3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}