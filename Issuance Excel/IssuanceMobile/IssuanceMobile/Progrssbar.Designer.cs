namespace ProgressLingkar
{
    partial class Progrssbar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Progrssbar));
            this.circularProgressBar2 = new CircularProgressBar.CircularProgressBar();
            this.SuspendLayout();
            // 
            // circularProgressBar2
            // 
            this.circularProgressBar2.AnimationFunction = ((WinFormAnimation.AnimationFunctions.Function)(resources.GetObject("circularProgressBar2.AnimationFunction")));
            this.circularProgressBar2.AnimationSpeed = 2000;
            this.circularProgressBar2.BackColor = System.Drawing.Color.White;
            this.circularProgressBar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.circularProgressBar2.ForeColor = System.Drawing.Color.Gray;
            this.circularProgressBar2.InnerColor = System.Drawing.Color.White;
            this.circularProgressBar2.InnerMargin = 2;
            this.circularProgressBar2.InnerWidth = -1;
            this.circularProgressBar2.Location = new System.Drawing.Point(12, 6);
            this.circularProgressBar2.MarqueeAnimationSpeed = 200;
            this.circularProgressBar2.Name = "circularProgressBar2";
            this.circularProgressBar2.OuterColor = System.Drawing.Color.Linen;
            this.circularProgressBar2.OuterMargin = -25;
            this.circularProgressBar2.OuterWidth = 26;
            this.circularProgressBar2.ProgressColor = System.Drawing.Color.Red;
            this.circularProgressBar2.ProgressWidth = 25;
            this.circularProgressBar2.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularProgressBar2.Size = new System.Drawing.Size(125, 133);
            this.circularProgressBar2.StartAngle = 270;
            this.circularProgressBar2.Step = 29;
            this.circularProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.circularProgressBar2.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar2.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.circularProgressBar2.SubscriptText = "";
            this.circularProgressBar2.SuperscriptColor = System.Drawing.Color.Transparent;
            this.circularProgressBar2.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.circularProgressBar2.SuperscriptText = "%";
            this.circularProgressBar2.TabIndex = 2;
            this.circularProgressBar2.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.circularProgressBar2.Value = 70;
            this.circularProgressBar2.Click += new System.EventHandler(this.circularProgressBar2_Click);
            // 
            // Progrssbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(149, 140);
            this.Controls.Add(this.circularProgressBar2);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "Progrssbar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private CircularProgressBar.CircularProgressBar circularProgressBar2;
    }
}

