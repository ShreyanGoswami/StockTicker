using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Stock_ticker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Real time stock ticker";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        //public IActionResult TestAzureConnect()
        //{
        //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //    builder.DataSource = "userdetails58.database.windows.net";
        //    builder.UserID = "Shreyan";
        //    builder.Password = "pass@123";
        //    builder.InitialCatalog = "Users";

        //    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        //    {
        //        connection.Open();
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("SELECT * from User_Info ");

        //        String sql = sb.ToString();

        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    ViewData["Name"] = reader.GetString(1);

        //                    //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
        //                }
        //            }
        //        }
        //    }


        //    return View();
        //}

        public async Task<IActionResult> GetStockData(string [] companies)
        {
            var client= new HttpClient();

            string query = "";
            foreach (string company in companies)
            {
                query += company + ",";
            }

            query = query.TrimEnd(',');

            var uri = new Uri("http://finance.google.com/finance/info?client=ig&q=NASDAQ%3A"+query);

            var response = await client.GetAsync(uri);

            string textResult = await response.Content.ReadAsStringAsync();
            string test = "Test";
            ViewData["test"] = test;
            ViewData["res"] = textResult;
            return View();
        }

        public async Task<ActionResult> GetData()
        {
            var client = new HttpClient();
            var uri = new Uri("http://finance.google.com/finance/info?client=ig&q=NASDAQ%3AAAPL,GOOG");

            var response = await client.GetAsync(uri);

            string textResult = await response.Content.ReadAsStringAsync();
            return new ContentResult { Content = textResult, ContentType = "application/json" };

        }

        public async Task<ActionResult> GetDataV2(string [] market,string[] companies)
        {
            var client = new HttpClient();

            string query = "";
            foreach (string company in companies)
            {
                query += company + ",";
            }

            query = query.TrimEnd(',');

            var uri = new Uri("http://finance.google.com/finance/info?client=ig&q=" + query);

            var response = await client.GetAsync(uri);
            string textResult = await response.Content.ReadAsStringAsync();
            char [] remove = { '\n', '/'};
            textResult = textResult.TrimStart(remove);
            return new ContentResult { Content = textResult, ContentType = "application/json" };

        }

        public ActionResult pushNotification(string pref)
        {
            string resp = "";
            try
            {
                string url = "https://fcm.googleapis.com/fcm/send";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Headers["Authorization"] =
                    "Key=AAAArCUdAGc:APA91bFTCsgYTK7WFXQjVZJLAkeM0Ge4RWorM8tiaL-ACUTunyIJzgsF6FA0Dp1uT3G2jGHGjra_WeiLoKUpRgofG8XPUhhygJ4FI3SiDzNxPWJ5lF4xJKvBkBNSjwD-yqRy5ChsKq-i";
                req.Headers["Sender"] = "Id=739357032551";
                req.Headers["Content-type"] = "application/json";
                req.Method = "POST";
                var data =
                    "{\"to\": \"c--91-AqBVU:APA91bFZwCtwVMSXA24IDfzmal1WL-VnUbjptsxCjxC0kbw0I4uOviHq4V0tSs9eF_g-qvoXC55_4TVBsPq8plRQ9TTGixWy8Y5bHod45LPAB3SzxsXg7lgyIStJDRbLtRLH6wtWnlZi\",\"notification\": {\"body\": \"user has added "+pref+"\",\"title\": \"Alert from WebApp \"}}";

                byte[] byte_data = Encoding.ASCII.GetBytes(data);

                Stream newStream = req.GetRequestStreamAsync().Result;
                newStream.Write(byte_data, 0, data.Length);
                var response = (HttpWebResponse)req.GetResponseAsync().Result;
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                resp = "{\"status\";\"success\"}";
                return new ContentResult { Content = resp, ContentType = "application/json" };


            }
            catch (Exception ex)
            {
                string str = ex.Message;
                resp = "{\"status\";\"failed\"}";
                return new ContentResult { Content = resp, ContentType = "application/json" };

            }

        }
    }
}
