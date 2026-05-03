namespace CaYaSafeSystemSetup
{
    partial class SafeModeEnable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SafeModeEnable));
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(355, 517);
            button1.Name = "button1";
            button1.Size = new Size(117, 32);
            button1.TabIndex = 0;
            button1.Text = "Atla (Önerilmez)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 517);
            button2.Name = "button2";
            button2.Size = new Size(196, 32);
            button2.TabIndex = 1;
            button2.Text = "Güvenli Modu Devre Dışı Bırak";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(240, 30);
            label1.TabIndex = 2;
            label1.Text = "12 Basamaklı kodunuz:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.SeaGreen;
            label2.Location = new Point(12, 39);
            label2.Name = "label2";
            label2.Size = new Size(133, 30);
            label2.TabIndex = 3;
            label2.Text = "Bekleniyor...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Orange;
            label3.Location = new Point(12, 69);
            label3.Name = "label3";
            label3.Size = new Size(378, 20);
            label3.TabIndex = 4;
            label3.Text = "Lütfen kodu bir yere kaydedin veya fotoğrafını çekin.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(12, 205);
            label4.Name = "label4";
            label4.Size = new Size(443, 220);
            label4.TabIndex = 5;
            label4.Text = resources.GetString("label4.Text");
            // 
            // button3
            // 
            button3.Location = new Point(139, 517);
            button3.Name = "button3";
            button3.Size = new Size(196, 32);
            button3.TabIndex = 6;
            button3.Text = "Devam etmek için hazırım";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button1_Click;
            // 
            // SafeModeEnable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 561);
            Controls.Add(button3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SafeModeEnable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Güvenli modu devre dışı bırak";
            TopMost = true;
            FormClosing += SafeModeEnable_FormClosing;
            Load += SafeModeEnable_Load;
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
        private Button button3;
    }
}