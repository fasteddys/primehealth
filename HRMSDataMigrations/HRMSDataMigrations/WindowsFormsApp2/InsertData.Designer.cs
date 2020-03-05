namespace WindowsFormsApp2
{
    partial class InsertData
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
            this.ExcelData = new System.Windows.Forms.DataGridView();
            this.Browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Save = new System.Windows.Forms.Button();
            this.ExcelDataNotLdap = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ExcelDataOutDomain = new System.Windows.Forms.DataGridView();
            this.BrowseOutDomain = new System.Windows.Forms.Button();
            this.SaveOutDomain = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ExcelData)).BeginInit();
            this.ExcelDataNotLdap.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExcelDataOutDomain)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExcelData
            // 
            this.ExcelData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExcelData.Location = new System.Drawing.Point(7, 7);
            this.ExcelData.Margin = new System.Windows.Forms.Padding(4);
            this.ExcelData.Name = "ExcelData";
            this.ExcelData.Size = new System.Drawing.Size(1100, 399);
            this.ExcelData.TabIndex = 0;
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(597, 438);
            this.Browse.Margin = new System.Windows.Forms.Padding(4);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(100, 28);
            this.Browse.TabIndex = 1;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(294, 438);
            this.Save.Margin = new System.Windows.Forms.Padding(4);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(100, 28);
            this.Save.TabIndex = 2;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // ExcelDataNotLdap
            // 
            this.ExcelDataNotLdap.Controls.Add(this.tabPage2);
            this.ExcelDataNotLdap.Controls.Add(this.tabPage1);
            this.ExcelDataNotLdap.Location = new System.Drawing.Point(12, 12);
            this.ExcelDataNotLdap.Name = "ExcelDataNotLdap";
            this.ExcelDataNotLdap.SelectedIndex = 0;
            this.ExcelDataNotLdap.Size = new System.Drawing.Size(1122, 502);
            this.ExcelDataNotLdap.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ExcelDataOutDomain);
            this.tabPage2.Controls.Add(this.BrowseOutDomain);
            this.tabPage2.Controls.Add(this.SaveOutDomain);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1114, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Not Ldap User ";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // ExcelDataOutDomain
            // 
            this.ExcelDataOutDomain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExcelDataOutDomain.Location = new System.Drawing.Point(7, 7);
            this.ExcelDataOutDomain.Margin = new System.Windows.Forms.Padding(4);
            this.ExcelDataOutDomain.Name = "ExcelDataOutDomain";
            this.ExcelDataOutDomain.Size = new System.Drawing.Size(1100, 399);
            this.ExcelDataOutDomain.TabIndex = 3;
            // 
            // BrowseOutDomain
            // 
            this.BrowseOutDomain.Location = new System.Drawing.Point(597, 438);
            this.BrowseOutDomain.Margin = new System.Windows.Forms.Padding(4);
            this.BrowseOutDomain.Name = "BrowseOutDomain";
            this.BrowseOutDomain.Size = new System.Drawing.Size(100, 28);
            this.BrowseOutDomain.TabIndex = 4;
            this.BrowseOutDomain.Text = "Browse";
            this.BrowseOutDomain.UseVisualStyleBackColor = true;
            this.BrowseOutDomain.Click += new System.EventHandler(this.BrowseOutDomain_Click);
            // 
            // SaveOutDomain
            // 
            this.SaveOutDomain.Location = new System.Drawing.Point(294, 438);
            this.SaveOutDomain.Margin = new System.Windows.Forms.Padding(4);
            this.SaveOutDomain.Name = "SaveOutDomain";
            this.SaveOutDomain.Size = new System.Drawing.Size(100, 28);
            this.SaveOutDomain.TabIndex = 5;
            this.SaveOutDomain.Text = "Save";
            this.SaveOutDomain.UseVisualStyleBackColor = true;
            this.SaveOutDomain.Click += new System.EventHandler(this.SaveNotLdap_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ExcelData);
            this.tabPage1.Controls.Add(this.Browse);
            this.tabPage1.Controls.Add(this.Save);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1114, 473);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ldap Usrs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // InsertData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 535);
            this.Controls.Add(this.ExcelDataNotLdap);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InsertData";
            this.Text = "InsertData";
            ((System.ComponentModel.ISupportInitialize)(this.ExcelData)).EndInit();
            this.ExcelDataNotLdap.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExcelDataOutDomain)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ExcelData;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TabControl ExcelDataNotLdap;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView ExcelDataOutDomain;
        private System.Windows.Forms.Button BrowseOutDomain;
        private System.Windows.Forms.Button SaveOutDomain;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}