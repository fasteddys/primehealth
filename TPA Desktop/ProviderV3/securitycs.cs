using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProviderV2
{
    public partial class securitycs : Form
    {
        public securitycs()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1 .Text == "admin" && textBox2 .Text =="ABC123!@#")
            {
              
                    //this .Height=749;
                //label18.Hide();

            }
            else
            {
               MessageBox.Show("Please insert right user and password", "TPA Manager",
                MessageBoxButtons.RetryCancel , MessageBoxIcon.Asterisk);

            }
        }
    }
}
