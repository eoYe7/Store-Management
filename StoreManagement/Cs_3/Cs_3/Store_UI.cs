using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Cs_3
{
    public partial class Stores_UI : UserControl
    {
        public Stores_UI()
        {
            InitializeComponent();
            getstores("showallstores");
        }
        string storURL = "http://localhost:8080/webservices/stores.php";
        List<stores> store_info;
        void webservices(string data)
        {

            WebRequest RE = WebRequest.Create(storURL);
            RE.Method = "post";
            RE.ContentType = "application/x-www-form-urlencoded";
            Stream st = RE.GetRequestStream();
            Byte[] bt = Encoding.UTF8.GetBytes(data);
            st.Write(bt, 0, bt.Length);

            WebResponse res = RE.GetResponse();
            st = res.GetResponseStream();
            StreamReader str = new StreamReader(st);

            List<stores> user_info = new List<stores>();
            foreach (string row in str.ReadToEnd().Split('#'))
            {
                try
                {
                    user_info.Add(new stores(row.Split(',')[0], row.Split(',')[1], row.Split(',')[2]));
                }
                catch { }
            }
            storelist.DataSource = user_info;
        }
        public class stores
        {
            public stores(string store_code ,string store_name ,string location)
            {
                this.store_code = store_code;
                this.store_name = store_name;
                this.location = location;

            }
            public string store_code { set; get; }
            public string store_name { set; get; }
            public string  location { set; get; }
        }
        void getstores(string data)
        {

            WebRequest RE = WebRequest.Create(storURL);
            RE.Method = "post";
            RE.ContentType = "application/x-www-form-urlencoded";
            Stream st = RE.GetRequestStream();
            Byte[] bt = Encoding.UTF8.GetBytes(data);
            st.Write(bt, 0, bt.Length);

            WebResponse res = RE.GetResponse();
            st = res.GetResponseStream();
            StreamReader str = new StreamReader(st);
             store_info = new List<stores>();
            foreach (string row in str.ReadToEnd().Split('#'))
            {
                try
                {
                    store_info.Add(new stores(row.Split(',')[0], row.Split(',')[1], row.Split(',')[2]));
                }
                catch { }
            }
            storelist.DataSource = store_info;
           

        }
        private void Stores_UI_Load(object sender, EventArgs e)
        {
            webservices("showallstores=''");
        }

        private void add_Click(object sender, EventArgs e)
        {
             string data = $"addstore='1'&store_code={store_code.Text}&store_name={store_name.Text}&location={location.Text}";
             getstores(data);
            Stores_UI_Load(this, null);
        }

        private void edit_Click(object sender, EventArgs e)
        {
            string data = $"updatestore='1'&store_name={store_name.Text}&location={location.Text}&store_code={store_code.Text}";
            getstores(data);

        }

        private void storelist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            store_code.Text = storelist.CurrentRow.Cells[0].Value.ToString();
            store_name.Text = storelist.CurrentRow.Cells[1].Value.ToString();
            location.Text = storelist.CurrentRow.Cells[2].Value.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
