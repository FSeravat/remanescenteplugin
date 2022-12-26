
namespace NWDTierExporter
{
    partial class ProgressMonitor
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
            this.lbMon = new System.Windows.Forms.Label();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbMon
            // 
            this.lbMon.AutoSize = true;
            this.lbMon.Location = new System.Drawing.Point(55, 11);
            this.lbMon.Name = "lbMon";
            this.lbMon.Size = new System.Drawing.Size(130, 13);
            this.lbMon.TabIndex = 2;
            this.lbMon.Text = "Realizando Pré-Marcação";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(28, 27);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(184, 23);
            this.pgBar.TabIndex = 3;
            // 
            // ProgressMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 61);
            this.Controls.Add(this.pgBar);
            this.Controls.Add(this.lbMon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProgressMonitor";
            this.Text = "pgWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ProgressBar pgBar;
        public System.Windows.Forms.Label lbMon;
    }
}