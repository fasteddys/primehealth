namespace InHouseSendApproval
{
    partial class ManageSendApproval
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageSendApproval));
            this.ExcelFileUpload = new System.Windows.Forms.OpenFileDialog();
            this.UploadExcelbtn = new System.Windows.Forms.Button();
            this.ErrorMessage = new System.Windows.Forms.Label();
            this.UsersExcel = new System.Windows.Forms.DataGridView();
            this.Browse = new System.Windows.Forms.Button();
            this.PDFFilesLocation = new System.Windows.Forms.TextBox();
            this.OpenPDFLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LocationLab = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Send = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UsersExcel)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExcelFileUpload
            // 
            this.ExcelFileUpload.FileName = "ExcelFileUpload";
            this.ExcelFileUpload.FileOk += new System.ComponentModel.CancelEventHandler(this.ExcelFileUploadAction);
            // 
            // UploadExcelbtn
            // 
            this.UploadExcelbtn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.UploadExcelbtn.CausesValidation = false;
            this.UploadExcelbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UploadExcelbtn.Location = new System.Drawing.Point(118, 64);
            this.UploadExcelbtn.Name = "UploadExcelbtn";
            this.UploadExcelbtn.Size = new System.Drawing.Size(156, 43);
            this.UploadExcelbtn.TabIndex = 0;
            this.UploadExcelbtn.Text = "Bowse Excel";
            this.UploadExcelbtn.UseVisualStyleBackColor = false;
            this.UploadExcelbtn.Click += new System.EventHandler(this.UploadExcelbtn_Click);
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.AutoSize = true;
            this.ErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.ErrorMessage.Location = new System.Drawing.Point(341, 82);
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Size = new System.Drawing.Size(0, 17);
            this.ErrorMessage.TabIndex = 1;
            // 
            // UsersExcel
            // 
            this.UsersExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UsersExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersExcel.Location = new System.Drawing.Point(0, 226);
            this.UsersExcel.Name = "UsersExcel";
            this.UsersExcel.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.UsersExcel.RowTemplate.Height = 30;
            this.UsersExcel.Size = new System.Drawing.Size(1530, 414);
            this.UsersExcel.TabIndex = 2;
            // 
            // Browse
            // 
            this.Browse.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Browse.Location = new System.Drawing.Point(118, 15);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(156, 47);
            this.Browse.TabIndex = 4;
            this.Browse.Text = "Browse PDF Location";
            this.Browse.UseVisualStyleBackColor = false;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // PDFFilesLocation
            // 
            this.PDFFilesLocation.Location = new System.Drawing.Point(294, 19);
            this.PDFFilesLocation.Name = "PDFFilesLocation";
            this.PDFFilesLocation.Size = new System.Drawing.Size(369, 22);
            this.PDFFilesLocation.TabIndex = 5;
            // 
            // OpenPDFLocation
            // 
            this.OpenPDFLocation.RootFolder = System.Environment.SpecialFolder.DesktopDirectory;
            this.OpenPDFLocation.HelpRequest += new System.EventHandler(this.OpenPDFLocation_HelpRequest);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ErrorMessage);
            this.panel1.Controls.Add(this.LocationLab);
            this.panel1.Controls.Add(this.Browse);
            this.panel1.Controls.Add(this.UploadExcelbtn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PDFFilesLocation);
            this.panel1.Location = new System.Drawing.Point(34, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(683, 121);
            this.panel1.TabIndex = 7;
            // 
            // LocationLab
            // 
            this.LocationLab.AutoSize = true;
            this.LocationLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationLab.Location = new System.Drawing.Point(12, 23);
            this.LocationLab.Name = "LocationLab";
            this.LocationLab.Size = new System.Drawing.Size(99, 18);
            this.LocationLab.TabIndex = 6;
            this.LocationLab.Text = "PDF Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Uolad Excel";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Send);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1530, 226);
            this.panel3.TabIndex = 10;
            // 
            // Send
            // 
            this.Send.BackColor = System.Drawing.SystemColors.GrayText;
            this.Send.Image = global::InHouseSendApproval.Properties.Resources.SendEmail;
            this.Send.Location = new System.Drawing.Point(769, 53);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(147, 84);
            this.Send.TabIndex = 10;
            this.Send.UseVisualStyleBackColor = false;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // ManageSendApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1530, 640);
            this.Controls.Add(this.UsersExcel);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageSendApproval";
            this.Text = "Inhouse Send Approvals";
            ((System.ComponentModel.ISupportInitialize)(this.UsersExcel)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ExcelFileUpload;
        private System.Windows.Forms.Button UploadExcelbtn;
        private System.Windows.Forms.Label ErrorMessage;
        private System.Windows.Forms.DataGridView UsersExcel;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.TextBox PDFFilesLocation;
        private System.Windows.Forms.FolderBrowserDialog OpenPDFLocation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LocationLab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button Send;
    }
}

