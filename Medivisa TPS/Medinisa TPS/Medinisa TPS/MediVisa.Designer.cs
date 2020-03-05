namespace MediVisa_TPS
{
    partial class MediVisa
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
            this.label1 = new System.Windows.Forms.Label();
            this.DVDTXT = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Process = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DVD_NO";
            // 
            // DVDTXT
            // 
            this.DVDTXT.Location = new System.Drawing.Point(76, 40);
            this.DVDTXT.Name = "DVDTXT";
            this.DVDTXT.Size = new System.Drawing.Size(148, 20);
            this.DVDTXT.TabIndex = 2;
            this.DVDTXT.TextChanged += new System.EventHandler(this.DVDTXT_TextChanged);
            this.DVDTXT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DVDTXT_KeyPress);
            // 
            // Process
            // 
            this.Process.BackColor = System.Drawing.Color.Lavender;
            this.Process.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Process.Location = new System.Drawing.Point(213, 95);
            this.Process.Name = "Process";
            this.Process.Size = new System.Drawing.Size(90, 45);
            this.Process.TabIndex = 6;
            this.Process.Text = "Process";
            this.Process.UseVisualStyleBackColor = false;
            this.Process.Click += new System.EventHandler(this.Process_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 85);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Backup";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // MediVisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(534, 176);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Process);
            this.Controls.Add(this.DVDTXT);
            this.Controls.Add(this.label1);
            this.Name = "MediVisa";
            this.Text = "MediVisa";
            this.Load += new System.EventHandler(this.MediVisa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DVDTXT;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button Process;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}