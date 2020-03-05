using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medivisa_Desktop
{
    public partial class MainOptionscs : Form
    {
        public MainOptionscs()
        {
            InitializeComponent();
            label1.Text = "Hello :  " + Form1.NAME;
            if (Form1.TYPE == "puser" || Form1.TYPE == "User")
            {
                adminPanelToolStripMenuItem.Visible = false;
            }
        }

        private void claimRevisionToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            claimRevisionToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void claimRevisionToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            claimRevisionToolStripMenuItem.ForeColor = System.Drawing.Color.LightBlue;
        }

        private void doctorCommentToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            doctorCommentToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void doctorCommentToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            doctorCommentToolStripMenuItem.ForeColor = System.Drawing.Color.LightBlue;
        }

        private void profileToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            profileToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void profileToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            profileToolStripMenuItem.ForeColor = System.Drawing.Color.LightBlue;
        }

        private void adminPanelToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            adminPanelToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void adminPanelToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            adminPanelToolStripMenuItem.ForeColor = System.Drawing.Color.LightBlue;
        }

        private void claimRevisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClaimRevision cr = new ClaimRevision();
            cr.Show();
           
        }

        private void doctorCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoctorComment dc = new DoctorComment();
            dc.Show();
            
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profile pf = new Profile();
            pf.Show();
           
        }

        private void adminPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminPanel admin = new AdminPanel();
            admin.Show();
           
        }

        private void backLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Close();
        }

        private void backLoginToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            backLoginToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void backLoginToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            backLoginToolStripMenuItem.ForeColor = System.Drawing.Color.LightBlue;
        }
    }
}
