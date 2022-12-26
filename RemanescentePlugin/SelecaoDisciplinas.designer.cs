
namespace NWDTierExporter
{
    partial class SelecaoDisciplinas
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
            this.chkList_Hier = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // chkList_Hier
            // 
            this.chkList_Hier.FormattingEnabled = true;
            this.chkList_Hier.Location = new System.Drawing.Point(12, 12);
            this.chkList_Hier.Name = "chkList_Hier";
            this.chkList_Hier.Size = new System.Drawing.Size(120, 199);
            this.chkList_Hier.TabIndex = 0;
            this.chkList_Hier.SelectedIndexChanged += new System.EventHandler(this.chkList_Hier_SelectedIndexChanged);
            // 
            // SelecaoDisciplinas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(143, 217);
            this.Controls.Add(this.chkList_Hier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelecaoDisciplinas";
            this.Text = "SelecaoDisciplinas";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.CheckedListBox chkList_Hier;
    }
}