using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Cs_3
{
    public partial class logfile : UserControl
    {
        public logfile()
        {
            InitializeComponent();
            StyleDatagridview(dataGridView1);

        }
        string logurl = "http://localhost:8080/webservices/logapi.php";


        public class stockstransaction
        {
            public stockstransaction(string stock_id, string store_code, string item_id, string stock_type, string quantity, string transactionType, string username1, string date)
            {

                this.stock_id = stock_id;
                this.store_code = store_code;
                this.item = item_id;
                this.stock_type = stock_type;
                this.quantity = quantity;
                this.transactionType = transactionType;
                this.user_name1 = username1;
                this.date = date;
            }
            public string stock_id { get; set; }
            public string store_code { get; set; }
            public string item { get; set; }
            public string stock_type { get; set; }
            public string quantity { get; set; }
            public string transactionType { get; set; }
            public string user_name1 { get; set; }
            public string date { get; set; }


        }
        void StyleDatagridview(DataGridView DaGridView)
        {
            //DaGridView.BorderStyle = BorderStyle.None;
            DaGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            DaGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DaGridView.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            DaGridView.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            DaGridView.ForeColor = Color.MidnightBlue;
            DaGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;//optional
            DaGridView.EnableHeadersVisualStyles = false;
            DaGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DaGridView.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10, FontStyle.Bold);
            DaGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 38);
            DaGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void logfile_Load(object sender, EventArgs e)
        {

        }


        private void webservices(string data)
        {
            // Create a request to the API endpoint
            WebRequest request = WebRequest.Create(logurl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Convert the data string to bytes
            byte[] requestData = Encoding.UTF8.GetBytes(data);

            // Write the data to the request stream
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestData, 0, requestData.Length);
            }

            // Get the response from the API
            using (WebResponse response = request.GetResponse())
            {
                // You can handle the response if needed
                // For example, you can read the response data or check the status code
                using (Stream responseStream = response.GetResponseStream())
                {
                    //     // Read the response data
                    StreamReader reader = new StreamReader(responseStream);
                    List<stockstransaction> storelog = new List<stockstransaction>();
                    foreach (string row in reader.ReadToEnd().Split('#'))
                    {
                        try
                        {
                            storelog.Add(new stockstransaction(row.Split(',')[0], row.Split(',')[1], row.Split(',')[2], row.Split(',')[3], row.Split(',')[4], row.Split(',')[5], row.Split(',')[6], row.Split(',')[7]));
                        }
                        catch { }
                    }
                    dataGridView1.DataSource = storelog;

                }
            }

            // Optionally, perform any additional actions after sending the data to the API

        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void search_Click(object sender, EventArgs e)
        {
            webservices("showalllogfile");
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //dateTimePickerStart.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //dateTimePickerEnd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}/*
  * By
  1- Abdulrazzaq Al-Feel /7753467387
  2-Ibrahim Al-Hakimi
  3-Zakaria Nejad
*/
