using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_ticker.Controllers
{

    public class AuthController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public ActionResult AuthUser(string name, string pwd)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "userdetails58.database.windows.net";
            builder.UserID = "Shreyan";
            builder.Password = "pass@123";
            builder.InitialCatalog = "Users";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);
                cmd.CommandText = "Select * from User_Info where name='" + name + "' and pass='" + pwd + "'";
                //cmd.CommandText = "Select * from User_Info";
                //SqlParameter idParam = new SqlParameter("@n", SqlDbType.Text, 30);
                //SqlParameter passParam = new SqlParameter("@p", SqlDbType.Text, 50);
                //cmd.Parameters.Add(idParam);
                //cmd.Parameters.Add(passParam);
                //cmd.Parameters[0].Value = name;
                //cmd.Parameters[1].Value = pwd;
                SqlDataReader reader = cmd.ExecuteReader();
                string response = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        response = "{\"isPresent\":\"True\",\"Id\":" + id + "}";
                    }

                }
                else
                {
                    response = "{\"isPresent\":\"False\"}";
                }
                //JObject res = JObject.Parse(response);
                return new ContentResult { Content = response, ContentType = "application/json" };
            }
        }

        [HttpGet]
        public ActionResult AddPref(int id, string ticker, string market, string company)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.DataSource = @"userdetails58.database.windows.net";
            builder.UserID = "Shreyan";
            builder.Password = "pass@123";
            builder.InitialCatalog = "Users";
            int rows_affected = 0;
            string response = "";
            string pref = "";
            string comp = "";
            string mark = "";

            
            //using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"))
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);
                cmd.CommandText = @"select preferences,market,c_name from User_pref where id = " + id;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pref = reader.GetString(0);
                    mark = reader.GetString(1);
                    comp = reader.GetString(2);
                    if (!pref.Contains(ticker))
                        pref += "," + ticker;
                    if (!mark.Contains(market))
                        mark += "," + market;
                    if (!comp.Contains(company))
                        comp += "," + company;
                }
                connection.Dispose();
            }

            rows_affected = UpdatePref(id, pref);
            rows_affected += UpdateMarket(id, mark);
            rows_affected += UpdateComp(id, comp);
            if (rows_affected == 3)
            {
                response = "{\"Status\":\"Success\"}";
            }
            else
            {
                response = "{\"Status\":\"Failed\"}";
            }
            return new ContentResult { Content = response, ContentType = "application/json" };

        }

        private int UpdateComp(int id, string comp)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.DataSource = @"userdetails58.database.windows.net";
            builder.UserID = "Shreyan";
            builder.Password = "pass@123";
            builder.InitialCatalog = "Users";
            int rows_affected = 0;
            //using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"))

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);
                cmd.CommandText = @"update User_pref set c_name ='" + comp + "' where id =" + id;
                rows_affected = cmd.ExecuteNonQuery();
            }
            return rows_affected;
        }

        private int UpdateMarket(int id, string mark)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.DataSource = @"userdetails58.database.windows.net";
            builder.UserID = "Shreyan";
            builder.Password = "pass@123";
            builder.InitialCatalog = "Users";
            int rows_affected = 0;
            //using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"))

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);
                cmd.CommandText = @"update User_pref set market ='" + mark + "' where id =" + id;
                rows_affected = cmd.ExecuteNonQuery();
            }
            return rows_affected;
        }

        private int UpdatePref(int id, string pref)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.DataSource = @"userdetails58.database.windows.net";
            builder.UserID = "Shreyan";
            builder.Password = "pass@123";
            builder.InitialCatalog = "Users";
            int rows_affected = 0;
            //using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"))

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);
                cmd.CommandText = @"update User_pref set preferences ='" + pref + "' where id =" + id;
                rows_affected = cmd.ExecuteNonQuery();
            }
            return rows_affected;
        }

        [HttpGet]
        public ActionResult GetPref(int id)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @"(localdb)\MSSQLLocalDB";

            builder.DataSource = "userdetails58.database.windows.net";
            builder.UserID = "Shreyan";
            builder.Password = "pass@123";
            builder.InitialCatalog = "Users";
            string response = "";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            //SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

            connection.Open();
            SqlCommand cmd = new SqlCommand("Select preferences from User_pref where id=" + id, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
                response = reader.GetString(0);
            string[] strarr = response.Split(',');
            response = "[";
            foreach (string str in strarr)
            {
                response += "{\"Ticker\":\"" + str + "\"},";
            }
            response = response.TrimEnd(',');
            response += "]";
            return new ContentResult { Content = response, ContentType = "application/json" };
        }

        public ActionResult RemovePref(int id, string pref)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @"(localdb)\MSSQLLocalDB";

            builder.DataSource = "userdetails58.database.windows.net";
            builder.UserID = "Shreyan";
            builder.Password = "pass@123";
            builder.InitialCatalog = "Users";
            string response = "";
            string res = "";
            //SqlConnection connection = new SqlConnection(
            //@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            //using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"))
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);

                cmd.CommandText = "select preferences from User_pref where id=" + id;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    res = reader.GetString(0);
                }

                var arrPref = res.Split(',');
                var listPref = arrPref.ToList();
                listPref.Remove(pref);
                arrPref = listPref.ToArray();
                response = string.Join(",", arrPref);
                connection.Dispose();
            }

            //using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"))
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);

                cmd.CommandText = "update user_pref set preferences='" + response + "' where id=" + id;
                cmd.ExecuteNonQuery();
            }

            response = "{\"Status\":\"Success\"}";
            return new ContentResult { Content = response, ContentType = "application/json" };
        }
    }
}
