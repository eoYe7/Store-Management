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
    public partial class Stocks_UI : UserControl
    {
        string url = "http://localhost:8080/webservices/items.php";
        string stockurl = "http://localhost:8080/webservices/stocks.php";
        string storURL = "http://localhost:8080/webservices/stores.php";

        public class stocks
        {
            public stocks(string stock_id, string store_code, string item_id, string stock_type , string quantity,string date)
            {
                
                this.stock_id = stock_id;
                this.store_code = store_code;
                this.item = item_id;
                this.stock_type = stock_type;
                this.quantity = quantity;
                this.date = date;
            }
            public string stock_id { get; set; }
            public string store_code { get; set; }
            public string item { get; set; }
            public string stock_type { get; set; }
            public string quantity { get; set; }
            public string date { get; set; }

           

        }

        public class stores
        {
            public stores(string store_code, string store_name, string location)
            {
                this.store_code = store_code;
                this.store_name = store_name;
                this.location = location;

            }
            public string store_name { set; get; }
            public string store_code { set; get; }
            public string location { set; get; }
        }

        public class Items_model
        {
            public Items_model(string id , string name,string price)
            {
                this.id = id;
                this.name = name;
                this.price = price;
            }
            public string name { set; get; }
            public string id { set; get; }
            public string price { get; set; }
        }

     
        List<stores> store_info;
        List<Items_model> items_info;
        public Stocks_UI()
        {
            InitializeComponent();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

     

        void getItems(string data)
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

            items_info = new List<Items_model>();
            foreach (string row in str.ReadToEnd().Split('#'))
            {
                try
                {
                    items_info.Add(new Items_model(row.Split(',')[0], row.Split(',')[1], row.Split(',')[2]));
                }
                catch { }
            }
            itemslist.DataSource = items_info;
            itemslist.DisplayMember = "name";
            itemslist.ValueMember = "id";

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
                    store_info.Add(new stores(row.Split(',')[0], row.Split(',')[1],row.Split(',')[2]));
                }
                catch { }
            }
            storeslist.DataSource = store_info;
            storeslist.DisplayMember = "store_name";
            storeslist.ValueMember = "store_code";

        }
   
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
  
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Stocks_UI_Load(object sender, EventArgs e)
        {
            getItems("showallitems");
            getstores("showallstores");
            AddStock("showallStocks");
            
        }


        private void AddStock(string data)
        {
            // Create a request to the API endpoint
            WebRequest request = WebRequest.Create(stockurl);
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
                    List<stocks> stocks = new List<stocks>();
                    foreach (string row in reader.ReadToEnd().Split('#'))
                    {
                        try
                        {
                            stocks.Add(new stocks(row.Split(',')[0], row.Split(',')[1], row.Split(',')[2], row.Split(',')[3], row.Split(',')[4], row.Split(',')[5]));
                        }
                        catch { }
                    }
                    dataGridView2.DataSource = stocks;

                }
            }

            // Optionally, perform any additional actions after sending the data to the API
        }


      
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string storeId = storeslist.SelectedValue.ToString();
                string itemId = itemslist.SelectedValue.ToString();
                string username = Main_Form2.username1;
                
                int quantity;

                string stockType = stockTypeList.SelectedItem.ToString();

                if (stockType == "withdraw from stoke")
                {
                    quantity = ((int)(quant.Value)) * -1;
                }
                else if (stockType == "add to stoke")
                {
                    quantity = (int)quant.Value;
                }
                else
                {
                    MessageBox.Show("Invalid stock type");
                    return;
                }

                // Construct the data string to be sent to the API
                string data = $"addtostocks=1&store_id={storeId}&item_id={itemId}&stock_type={stockType}&quantity={quantity}&user_name={username}";

                // Send the data to the API
                AddStock(data);
                MessageBox.Show("Data inserted Successfully");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void stockTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;

            // Format the selected dates to match the SQL datetime format (e.g., "yyyy-MM-dd HH:mm:ss")
            string formattedStartDate = startDate.ToString("yyyy-MM-dd HH:mm:ss");
            string formattedEndDate = endDate.ToString("yyyy-MM-dd HH:mm:ss");

            // Construct the condition for the query
            string condition = $"where transaction_date BETWEEN '{formattedStartDate}' AND '{formattedEndDate}'";
            string encodedCondition = Uri.EscapeDataString(condition);
            string data = $"showallStocks=1&condition={encodedCondition}";
            AddStock(data);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
