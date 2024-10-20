namespace CaYaSafeSystemSetup
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
            button2 = new Button();
            button3 = new Button();
            label2 = new Label();
            button1 = new Button();
            button4 = new Button();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(129, 143);
            label1.Name = "label1";
            label1.Size = new Size(225, 25);
            label1.TabIndex = 0;
            label1.Text = "CaYaSafeSystem Kurucu";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.PCCC2;
            pictureBox1.Location = new Point(177, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(129, 240);
            button2.Name = "button2";
            button2.Size = new Size(225, 75);
            button2.TabIndex = 3;
            button2.Text = "Kur";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(129, 321);
            button3.Name = "button3";
            button3.Size = new Size(225, 75);
            button3.TabIndex = 4;
            button3.Text = "Kaldır";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.MenuHighlight;
            label2.Location = new Point(12, 537);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 5;
            label2.Text = "CaYaSafeSystemSetup";
            // 
            // button1
            // 
            button1.Location = new Point(129, 240);
            button1.Name = "button1";
            button1.Size = new Size(225, 75);
            button1.TabIndex = 6;
            button1.Text = "Onar";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // button4
            // 
            button4.Location = new Point(129, 240);
            button4.Name = "button4";
            button4.Size = new Size(225, 75);
            button4.TabIndex = 7;
            button4.Text = "Güncelle";
            button4.UseVisualStyleBackColor = true;
            button4.Visible = false;
            button4.Click += button4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(133, 222);
            label3.Name = "label3";
            label3.Size = new Size(217, 15);
            label3.TabIndex = 8;
            label3.Text = "Zaten güncel sürümü kullanıyorsunuz!";
            label3.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 12);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 9;
            label4.Text = "Sürüm:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 561);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button4);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CaYaSafeSystem Kurucu";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Button button2;
        private Button button3;
        private Label label2;
        private Button button1;
        private Button button4;
        private Label label3;
        private Label label4;
    }
}