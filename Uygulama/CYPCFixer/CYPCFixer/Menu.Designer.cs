namespace CYPCFixer
{
    partial class Menu
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
            listBoxProcesses = new ListBox();
            btnOptimize = new Button();
            btnCloseAll = new Button();
            btnCleanMemory = new Button();
            progressBar1 = new ProgressBar();
            btnForceCloseAll = new Button();
            btnCleanTemp = new Button();
            btnCleanAll = new Button();
            PCallOP = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // listBoxProcesses
            // 
            listBoxProcesses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxProcesses.FormattingEnabled = true;
            listBoxProcesses.ItemHeight = 15;
            listBoxProcesses.Location = new Point(328, 57);
            listBoxProcesses.Name = "listBoxProcesses";
            listBoxProcesses.Size = new Size(460, 259);
            listBoxProcesses.TabIndex = 0;
            listBoxProcesses.Click += listBoxProcesses_SelectedIndexChanged;
            // 
            // btnOptimize
            // 
            btnOptimize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnOptimize.Location = new Point(328, 10);
            btnOptimize.Name = "btnOptimize";
            btnOptimize.Size = new Size(460, 41);
            btnOptimize.TabIndex = 1;
            btnOptimize.Text = "Yüksek Sistem Kullanan Uygulamaları Tara";
            btnOptimize.UseVisualStyleBackColor = true;
            btnOptimize.Click += btnOptimize_Click;
            // 
            // btnCloseAll
            // 
            btnCloseAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCloseAll.Location = new Point(328, 321);
            btnCloseAll.Name = "btnCloseAll";
            btnCloseAll.Size = new Size(460, 41);
            btnCloseAll.TabIndex = 2;
            btnCloseAll.Text = "Yüksek Sistem Kullanan Uygulamaların Yükünü Azalt";
            btnCloseAll.UseVisualStyleBackColor = true;
            btnCloseAll.Click += btnCloseAll_Click;
            // 
            // btnCleanMemory
            // 
            btnCleanMemory.Location = new Point(12, 59);
            btnCleanMemory.Name = "btnCleanMemory";
            btnCleanMemory.Size = new Size(310, 41);
            btnCleanMemory.TabIndex = 3;
            btnCleanMemory.Text = "RAM Kullanımını temizle";
            btnCleanMemory.UseVisualStyleBackColor = true;
            btnCleanMemory.Click += btnCleanMemory_Click;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(12, 415);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(776, 23);
            progressBar1.TabIndex = 4;
            // 
            // btnForceCloseAll
            // 
            btnForceCloseAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnForceCloseAll.Location = new Point(328, 368);
            btnForceCloseAll.Name = "btnForceCloseAll";
            btnForceCloseAll.Size = new Size(460, 41);
            btnForceCloseAll.TabIndex = 5;
            btnForceCloseAll.Text = "Yüksek Sistem Kullanan Uygulamaları Kapat";
            btnForceCloseAll.UseVisualStyleBackColor = true;
            btnForceCloseAll.Click += btnForceCloseAll_Click;
            // 
            // btnCleanTemp
            // 
            btnCleanTemp.Location = new Point(624, 243);
            btnCleanTemp.Name = "btnCleanTemp";
            btnCleanTemp.Size = new Size(164, 72);
            btnCleanTemp.TabIndex = 6;
            btnCleanTemp.Text = "Temp Klasörünü Temizle";
            btnCleanTemp.UseVisualStyleBackColor = true;
            btnCleanTemp.Visible = false;
            btnCleanTemp.Click += btnCleanTemp_Click;
            // 
            // btnCleanAll
            // 
            btnCleanAll.Location = new Point(12, 106);
            btnCleanAll.Name = "btnCleanAll";
            btnCleanAll.Size = new Size(310, 41);
            btnCleanAll.TabIndex = 7;
            btnCleanAll.Text = "Tüm Artık Dosyaları Temizle";
            btnCleanAll.UseVisualStyleBackColor = true;
            btnCleanAll.Click += btnCleanAll_Click;
            // 
            // PCallOP
            // 
            PCallOP.Location = new Point(12, 12);
            PCallOP.Name = "PCallOP";
            PCallOP.Size = new Size(310, 41);
            PCallOP.TabIndex = 8;
            PCallOP.Text = "Tüm Bilgisayarı Optimize Et";
            PCallOP.UseVisualStyleBackColor = true;
            PCallOP.Click += PCallOP_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(12, 397);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 9;
            label1.Text = "Durum:";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(PCallOP);
            Controls.Add(btnCleanAll);
            Controls.Add(btnCleanTemp);
            Controls.Add(btnForceCloseAll);
            Controls.Add(progressBar1);
            Controls.Add(btnCleanMemory);
            Controls.Add(btnCloseAll);
            Controls.Add(btnOptimize);
            Controls.Add(listBoxProcesses);
            Name = "Menu";
            Text = "Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBoxProcesses;
        private Button btnOptimize;
        private Button btnCloseAll;
        private Button btnCleanMemory;
        private ProgressBar progressBar1;
        private Button btnForceCloseAll;
        private Button btnCleanTemp;
        private Button btnCleanAll;
        private Button PCallOP;
        private Label label1;
    }
}