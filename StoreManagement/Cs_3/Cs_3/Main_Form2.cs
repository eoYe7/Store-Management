using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cs_3
{
    public partial class Main_Form2 : Form
    {
        public static string username1;
        public Main_Form2(string username2)
        {
            InitializeComponent();
            username.Text = username1;
            username.Enabled = false;
            username1 = username2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Users_UI u = new Users_UI(); 
            panel2.Controls.Clear();
            panel2.Controls.Add(u);
            u.Dock = DockStyle.Fill;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Items_UI u = new Items_UI();
            panel2.Controls.Clear();
            panel2.Controls.Add(u);
            u.Dock = DockStyle.Fill;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Stocks_UI s = new Stocks_UI();
            panel2.Controls.Clear();
            panel2.Controls.Add(s);
            s.Dock = DockStyle.Fill;
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void storeicon_Click(object sender, EventArgs e)
        {
            Stores_UI u = new Stores_UI();
            panel2.Controls.Clear();
            panel2.Controls.Add(u);
            u.Dock = DockStyle.Fill;
        }

        private void logfile_Click(object sender, EventArgs e)
        {
            logfile u = new logfile();
            panel2.Controls.Clear();
            panel2.Controls.Add(u);
            u.Dock = DockStyle.Fill;
        }

        private void Main_Form2_Load(object sender, EventArgs e)
        {
            username.Text = "welcome " + username1;
        }
    }
}
