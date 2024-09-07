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
    public partial class Items_UI : UserControl
    {
        public class Items_model
        {
            public Items_model(string id,string name,string price)
            {
                this.id = id;
                this.name = name;
                this.price = price;
            }
            public string id { set; get; }
            public string name { set; get; }
            public string price { set; get; }
        }
        public Items_UI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = $"additem=''&name={textBox1.Text}&price={price.Text}";
            webservices(data);
            Items_UI_Load(this, null);
        }
        string url = "http://localhost:8080/webservices/Items.php";
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

            List<Items_model> user_info = new List<Items_model>();
            foreach (string row in str.ReadToEnd().Split('#'))
            {
                try
                {
                    user_info.Add(new Items_model(row.Split(',')[0], row.Split(',')[1], row.Split(',')[2]));
                }
                catch{ 

                }
            }
            dataGridView1.DataSource = user_info;
        }
        string id = "";
        private void button2_Click(object sender, EventArgs e)
        {
            string data = $"updateitem='1'&name={textBox1.Text}&Price={price.Text}&id={textBoxID.Text}";
            webservices(data);
            Items_UI_Load(this, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string data = $"deleteitem=''&id={textBoxID.Text}";
            webservices(data);
            Items_UI_Load(this, null);
        }

        private void Items_UI_Load(object sender, EventArgs e)
        {
            webservices("showallitems=''");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            price.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }

