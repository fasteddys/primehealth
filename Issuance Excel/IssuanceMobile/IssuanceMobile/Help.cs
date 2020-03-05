using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IssuanceMobile
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            label2.Text = "Date : " + DateTime.Now.ToLongTimeString() + " -  Computer Name : " + Environment.MachineName;
        }
    }
}
