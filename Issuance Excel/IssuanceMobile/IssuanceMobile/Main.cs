using IssuanceMobile.Properties;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void manuallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_manual man = new Add_manual();
            man.Show();
        }

        private void exelSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execl exe = new Execl();
            exe.Show();
        }

        private void hELPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help hp = new Help();
            hp.Show();
        }

        private void additionalToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            additionalToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void additionalToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            additionalToolStripMenuItem.ForeColor = System.Drawing.Color.LightSkyBlue;
        }

        private void cancellationToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            cancellationToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void cancellationToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            cancellationToolStripMenuItem.ForeColor = System.Drawing.Color.LightSkyBlue;
        }

        private void searchToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            searchToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void searchToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            searchToolStripMenuItem.ForeColor = System.Drawing.Color.LightSkyBlue;
        }

        private void hELPToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            hELPToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void hELPToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {

            hELPToolStripMenuItem.ForeColor = System.Drawing.Color.LightSkyBlue;
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search sh = new Search();
            sh.Show();
        }

        private void cancellationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deleting_Item delte = new Deleting_Item();
            delte.Show();
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DeleteItem delitem = new DeleteItem();
            //delitem.Show();
        }

        private void eXCELSHEETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DeleteExcel del = new DeleteExcel();
            //del.Show();
        }
    }
}
