namespace EklentiNOTREMOVED
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
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Chartreuse;
            button1.Location = new Point(12, 103);
            button1.Name = "button1";
            button1.Size = new Size(215, 97);
            button1.TabIndex = 0;
            button1.Text = "Eklentiyi ekle";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.Location = new Point(457, 103);
            button2.Name = "button2";
            button2.Size = new Size(215, 97);
            button2.TabIndex = 1;
            button2.Text = "Eklentiyi kaldır";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(252, 144);
            label1.Name = "label1";
            label1.Size = new Size(172, 15);
            label1.TabIndex = 2;
            label1.Text = "Eklenti ekleme kaldırma sistemi";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(55, 9);
            label2.Name = "label2";
            label2.Size = new Size(554, 25);
            label2.TabIndex = 3;
            label2.Text = "Bazı güvenlik protokollerinin düzgün çalışması için gereklidir";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 203);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 4;
            label3.Text = "10 Aşama içerir";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(457, 203);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 5;
            label4.Text = "Çoklu seçenek";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 245);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Eklenti Sistemi";
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
    }
}