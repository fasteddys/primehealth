namespace IssuanceMobile
{
    partial class Execl
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.UploadButton = new System.Windows.Forms.Button();
            this.DataGridExcelSheet = new System.Windows.Forms.DataGridView();
            this.DataGridInvalid = new System.Windows.Forms.DataGridView();
            this.loginerBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.AssignButton = new System.Windows.Forms.Button();
            this.CommandError = new System.Windows.Forms.Label();
            this.DataGridMedicalIdDupicated = new System.Windows.Forms.DataGridView();
            this.ExistsRows = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CountaAllData = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridExcelSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridInvalid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginerBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMedicalIdDupicated)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.label1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(158, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 38);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(75, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Addition Data By Excel Sheet";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UploadButton.Location = new System.Drawing.Point(12, 23);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(131, 42);
            this.UploadButton.TabIndex = 1;
            this.UploadButton.Text = "Upload Excel";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataGridExcelSheet
            // 
            this.DataGridExcelSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridExcelSheet.Location = new System.Drawing.Point(12, 141);
            this.DataGridExcelSheet.Name = "DataGridExcelSheet";
            this.DataGridExcelSheet.Size = new System.Drawing.Size(710, 376);
            this.DataGridExcelSheet.TabIndex = 3;
            this.DataGridExcelSheet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // DataGridInvalid
            // 
            this.DataGridInvalid.AllowUserToOrderColumns = true;
            this.DataGridInvalid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridInvalid.Location = new System.Drawing.Point(718, 567);
            this.DataGridInvalid.Name = "DataGridInvalid";
            this.DataGridInvalid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridInvalid.Size = new System.Drawing.Size(10, 118);
            this.DataGridInvalid.TabIndex = 5;
            // 
            // AssignButton
            // 
            this.AssignButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssignButton.Location = new System.Drawing.Point(559, 21);
            this.AssignButton.Name = "AssignButton";
            this.AssignButton.Size = new System.Drawing.Size(163, 44);
            this.AssignButton.TabIndex = 6;
            this.AssignButton.Text = "Assign Database";
            this.AssignButton.UseVisualStyleBackColor = true;
            this.AssignButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // CommandError
            // 
            this.CommandError.AutoSize = true;
            this.CommandError.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommandError.ForeColor = System.Drawing.Color.DarkRed;
            this.CommandError.Location = new System.Drawing.Point(318, 693);
            this.CommandError.Name = "CommandError";
            this.CommandError.Size = new System.Drawing.Size(129, 19);
            this.CommandError.TabIndex = 8;
            this.CommandError.Text = "Data Is Invalid";
            this.CommandError.Visible = false;
            // 
            // DataGridMedicalIdDupicated
            // 
            this.DataGridMedicalIdDupicated.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridMedicalIdDupicated.Location = new System.Drawing.Point(12, 568);
            this.DataGridMedicalIdDupicated.Name = "DataGridMedicalIdDupicated";
            this.DataGridMedicalIdDupicated.Size = new System.Drawing.Size(710, 117);
            this.DataGridMedicalIdDupicated.TabIndex = 10;
            // 
            // ExistsRows
            // 
            this.ExistsRows.AutoSize = true;
            this.ExistsRows.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExistsRows.ForeColor = System.Drawing.Color.Maroon;
            this.ExistsRows.Location = new System.Drawing.Point(596, 12);
            this.ExistsRows.Name = "ExistsRows";
            this.ExistsRows.Size = new System.Drawing.Size(88, 19);
            this.ExistsRows.TabIndex = 12;
            this.ExistsRows.Text = "Loading ..";
            this.ExistsRows.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(9, 722);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 19);
            this.label5.TabIndex = 16;
            this.label5.Text = "(Duplicated ID in Excel ) :";
            this.label5.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(612, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 42);
            this.button1.TabIndex = 17;
            this.button1.Text = "Duplicated Data In Server";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(267, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 19);
            this.label6.TabIndex = 18;
            this.label6.Text = "0";
            this.label6.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk_1);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(-4, 243);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(10, 23);
            this.progressBar1.TabIndex = 19;
            this.progressBar1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.CountaAllData);
            this.panel2.Location = new System.Drawing.Point(16, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(590, 64);
            this.panel2.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 19);
            this.label4.TabIndex = 18;
            this.label4.Text = "All Data In Excel :";
            this.label4.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(168, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 19);
            this.label2.TabIndex = 17;
            this.label2.Text = "No file is chosen";
            this.label2.Visible = false;
            // 
            // CountaAllData
            // 
            this.CountaAllData.AutoSize = true;
            this.CountaAllData.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountaAllData.ForeColor = System.Drawing.Color.Maroon;
            this.CountaAllData.Location = new System.Drawing.Point(3, 35);
            this.CountaAllData.Name = "CountaAllData";
            this.CountaAllData.Size = new System.Drawing.Size(180, 19);
            this.CountaAllData.TabIndex = 16;
            this.CountaAllData.Text = "No Data has inserted";
            this.CountaAllData.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.ExistsRows);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(12, 520);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(710, 42);
            this.panel3.TabIndex = 21;
            // 
            // Execl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(736, 721);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DataGridMedicalIdDupicated);
            this.Controls.Add(this.CommandError);
            this.Controls.Add(this.AssignButton);
            this.Controls.Add(this.DataGridInvalid);
            this.Controls.Add(this.DataGridExcelSheet);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.panel1);
            this.Name = "Execl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel";
            this.Load += new System.EventHandler(this.Execl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridExcelSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridInvalid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginerBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMedicalIdDupicated)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.DataGridView DataGridExcelSheet;
        private System.Windows.Forms.DataGridView DataGridInvalid;
        private System.Windows.Forms.Button AssignButton;
        private System.Windows.Forms.Label CommandError;
        private System.Windows.Forms.DataGridView DataGridMedicalIdDupicated;
        private System.Windows.Forms.Label ExistsRows;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource loginerBindingSource1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CountaAllData;
        private System.Windows.Forms.Panel panel3;
    }
}