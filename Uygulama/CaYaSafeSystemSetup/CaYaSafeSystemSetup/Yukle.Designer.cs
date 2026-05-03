namespace CaYaSafeSystemSetup
{
    partial class Yukle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Yukle));
            label2 = new Label();
            progressBar1 = new ProgressBar();
            label1 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(12, 11);
            label2.Name = "label2";
            label2.Size = new Size(271, 30);
            label2.TabIndex = 5;
            label2.Text = "CaYaSafeSystemDownload";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 405);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(776, 35);
            progressBar1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 352);
            label1.Name = "label1";
            label1.Size = new Size(410, 50);
            label1.TabIndex = 3;
            label1.Text = "Uygulama Kuruluyor...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(12, 322);
            label3.Name = "label3";
            label3.Size = new Size(238, 30);
            label3.TabIndex = 6;
            label3.Text = "Yükleme Hazırlanıyor...";
            // 
            // Yukle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(progressBar1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Yukle";
            Opacity = 0.8D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yükleme";
            TopMost = true;
            FormClosing += Yukle_FormClosing;
            Load += Yukle_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private ProgressBar progressBar1;
        private Label label1;
        private Label label3;
    }
}