using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgressLingkar
{
    public partial class Progrssbar : Form
    {
        int number = 0;
        public Progrssbar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (circularProgressBar2.Value >= 100)

            //{    circularProgressBar2.Value += 10;
            //circularProgressBar2.Text = circularProgressBar2.Value.ToString();
            //  }
            //else{ this.Close(); }
            //if (number <= 100)

            //{
            //    number += 10;
            //    //circularProgressBar2.Text = circularProgressBar2.Value.ToString();
            //}
            //else { this.Close(); }

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //this.BringToFront();
        }

        private void circularProgressBar2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (number <= 100)

            {
                number += 10;
                //circularProgressBar2.Text = circularProgressBar2.Value.ToString();
            }
            else { this.Close(); }
        }
    }
}
