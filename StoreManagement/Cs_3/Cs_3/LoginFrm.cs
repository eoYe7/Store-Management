using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Cs_3
{
    public partial class LoginFrm : Form
    {
      

        public LoginFrm()
        {
            InitializeComponent();
        }

       public static string usern;
        private void button1_Click(object sender, EventArgs e)
        {
            string data = $"login=&name={textBox1.Text}&pass={textBox2.Text}";
            ProxyClass proxy = new ProxyClass(this);
            proxy.WebServices(data);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public class ProxyClass
        {
           
            private LoginFrm parentForm;
            public static string username = "s";
            public ProxyClass(LoginFrm parentForm)
            {

                this.parentForm = parentForm;            
            }
             string url = "http://localhost:8080/webservices/users.php";
            public void WebServices(string data)
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] byteData = Encoding.UTF8.GetBytes(data);
                request.ContentLength = byteData.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(byteData, 0, byteData.Length);
                }
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string result = reader.ReadToEnd();
                            if (result == "logi")
                            {
                                 username = parentForm.textBox1.Text;
                                   Main_Form2 mf = new Main_Form2(username);
                                mf.Show();
                                parentForm.Hide();
                            }
                            else
                            {
                                MessageBox.Show("invaled username or password"+ result);
                                return;
                            }
                           
                        }
                    }
                }
            }
        }
    }
}