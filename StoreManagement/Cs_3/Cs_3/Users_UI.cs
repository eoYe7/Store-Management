using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace Cs_3
{
    public partial class Users_UI : UserControl
    {
        public class Users{
            public Users(string name, string pass)
            {
                this.name = name;
                this.pass = pass;
            }
            public string name { set; get; }
            public string pass { set; get; }
            }
        public Users_UI()
        {
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = $"adduser=''&name={textBox1.Text }&pass={textBox2.Text }";
            webservices(data);
            Users_UI_Load(this, null);
        }
        string url = "http://localhost:8080/webservices/users.php";
        void webservices(string data)
        {
           
            WebRequest RE = WebRequest.Create(url);
            RE.Method = "post";
            RE.ContentType = "application/x-www-form-urlencoded";
            Stream st = RE.GetRequestStream();
            Byte[] bt = Encoding.UTF8.GetBytes(data);
            st.Write(bt, 0, bt.Length);

            WebResponse res = RE.GetResponse();
            st = res.GetResponseStream();
            StreamReader str = new StreamReader(st);
           
            List<Users> user_info = new List<Users>();
            foreach (string row in str.ReadToEnd().Split('#'))
            { 
                try {
                user_info.Add(new Users(row.Split(',')[1], row.Split(',')[2]));
                } catch { }
            }
            dataGridView1.DataSource = user_info;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string data = $"updateuser=''&name={textBox1.Text }&pass={textBox2.Text }";
            webservices(data);
            Users_UI_Load(this, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string data = $"deleteuser=''&name={textBox1.Text }&pass={textBox2.Text }";
            webservices(data);
            Users_UI_Load(this, null);
        }
        
        private void Users_UI_Load(object sender, EventArgs e)
        {
            webservices("showallusers=''");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
