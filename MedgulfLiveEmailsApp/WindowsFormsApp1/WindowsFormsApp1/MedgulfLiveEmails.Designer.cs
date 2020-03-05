namespace WindowsFormsApp1
{
    partial class MedgulfLiveEmails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MedgulfLiveEmails));
            this.CallCenterEMail = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // CallCenterEMail
            // 
            this.CallCenterEMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CallCenterEMail.Location = new System.Drawing.Point(0, 0);
            this.CallCenterEMail.MinimumSize = new System.Drawing.Size(20, 20);
            this.CallCenterEMail.Name = "CallCenterEMail";
            this.CallCenterEMail.Size = new System.Drawing.Size(935, 475);
            this.CallCenterEMail.TabIndex = 0;
            this.CallCenterEMail.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompletedAsync);
            // 
            // MedgulfLiveEmails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 475);
            this.Controls.Add(this.CallCenterEMail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MedgulfLiveEmails";
            this.Text = "Medgulf CallCenter Emails";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser CallCenterEMail;
    }
}

