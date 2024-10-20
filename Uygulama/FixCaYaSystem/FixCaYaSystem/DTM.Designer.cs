namespace CpgX
{
    partial class DTM
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
            components = new System.ComponentModel.Container();
            timerCheck = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // DTM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(10, 10);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DTM";
            Opacity = 0D;
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "DTM";
            Load += DTM_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timerCheck;
    }
}