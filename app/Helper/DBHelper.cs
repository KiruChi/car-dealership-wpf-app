using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Kursova.Helper
{
    public static class DBHelper
    {
        private static MySqlConnection connection;
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static void EstablishConnection()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "127.0.0.1";
                builder.UserID = "root";
                builder.Password = "TYREN458srcetr"; // This should be your password to your MySQL connection
                builder.Database = "company_DB";
                builder.SslMode = MySqlSslMode.None;
                connection = new MySqlConnection(builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("connection Failed");
            }
        }
        public static MySqlCommand RunQueryNoParameters(string query)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQueryWithParameters(string query, params string[] mas)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    string a = "@parameter";
                    int i = 1;
                    foreach (string s in mas)
                    {
                        a = a + i.ToString();
                        cmd.Parameters.AddWithValue(a, s);
                        a = "@parameter";
                        i = i + 1;
                    }
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQuerySearch(string query, string searchParameter)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@searchParameter", $"%{searchParameter}%");
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQueryCars(string query, string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6, string parameter7, 
            string parameter8, string parameter9 , string parameter10, string parameter11, string parameter12, string parameter13, string parameter14, string parameter15, string parameter16, string parameter17, 
            string parameter18, string parameter19, string parameter20, string parameter21, string parameter22, string parameter23, string parameter24)
        {
            try
            {
                if (parameter1 != null && parameter1 != "" && parameter1 != "Body")
                {
                    query = query + " and body = \"" + parameter1 + "\"";
                }
                if (parameter2 != null && parameter2 != "" && parameter2 != "Color")
                {
                    query = query + " and color = \"" + parameter2 + "\"";
                }
                if (parameter3 != null && parameter3 != "" && parameter3 != "Brand")
                {
                    query = query + " and brand = \"" + parameter3 + "\"";
                }
                if (parameter4 != null && parameter4 != "" && parameter4 != "Model")
                {
                    query = query + " and model = \"" + parameter4 + "\"";
                }
                if (parameter5 != null && parameter5 != "" && parameter5 != "Interior material")
                {
                    query = query + " and material = \"" + parameter5 + "\"";
                }
                if (parameter6 != null && parameter6 != "" && parameter6 != "Transmission")
                {
                    query = query + " and kpp = \"" + parameter6 + "\"";
                }
                if (parameter9 != null && parameter9 != "" && parameter9 != "from")
                {
                    query = query + " and car_year between \"" + parameter9 + "\"";
                }
                else
                {
                    query = query + " and car_year between car_year";
                }
                if (parameter10 != null && parameter10 != "" && parameter10 != "to")
                {
                    query = query + " and \"" + parameter10 + "\"";
                }
                else
                {
                    query = query + " and car_year";
                }
                if (parameter11 != null && parameter11 != "" && parameter11 != "from")
                {
                    query = query + " and car_mileage between \"" + parameter11 + "\"";
                }
                else
                {
                    query = query + " and car_mileage between car_mileage";
                }
                if (parameter12 != null && parameter12 != "" && parameter12 != "to")
                {
                    query = query + " and \"" + parameter12 + "\"";
                }
                else
                {
                    query = query + " and car_mileage";
                }
                if (parameter13 != null && parameter13 != "" && parameter13 != "Fuel")
                {
                    query = query + " and fuel = \"" + parameter13 + "\"";
                }
                if (parameter14 != null && parameter14 != "" && parameter14 != "Engine")
                {
                    query = query + " and car_engine = \"" + parameter14 + "\"";
                }
                if (parameter15 != null && parameter15 != "" && parameter15 != "Car drive")
                {
                    query = query + " and car_drive = \"" + parameter15 + "\"";
                }
                if (parameter16 != null && parameter16 != "" && parameter16 != "Seats")
                {
                    query = query + " and car_seats = \"" + parameter16 + "\"";
                }
                if (parameter17 != null && parameter17 != "" && parameter17 != "Сar heat")
                {
                    query = query + " and car_heat = \"" + parameter17 + "\"";
                }
                if (parameter18 != null && parameter18 != "" && parameter18 != "Air conditioner")
                {
                    query = query + " and car_air_cond = \"" + parameter18 + "\"";
                }
                if (parameter19 != null && parameter19 != "" && parameter19 != "Discs")
                {
                    query = query + " and car_discs = \"" + parameter19 + "\"";
                }
                if (parameter20 != null && parameter20 != "" && parameter20 != "Rubber")
                {
                    query = query + " and car_rubber = \"" + parameter20 + "\"";
                }
                if (parameter21 != null && parameter21 != "" && parameter21 != "Owner")
                {
                    query = query + " and CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) = \"" + parameter21 + "\"";
                }
                if (parameter22 != null && parameter22 != "" && parameter22 != "Motor show")
                {
                    query = query + " and b_name = \"" + parameter22 + "\"";
                }
                if (parameter23 != null && parameter23 != "" && parameter23 != "Status")
                {
                    query = query + " and mc_status = \"" + parameter23 + "\"";
                }
                if (parameter24 != null && parameter24 != "" && parameter24 != "Class")
                {
                    query = query + " and car_class = \"" + parameter24 + "\"";
                }
                query = query + " group by 1,10"; 
                if (parameter7 != null && parameter7 != "" && parameter7 != "from" && parameter7 != " ")
                {
                    query = query + " having cp_price between \"" + parameter7 + "\"";
                }
                else
                {
                    query = query + " having cp_price between cp_price";
                }
                if (parameter8 != null && parameter8 != "" && parameter8 != "to" && parameter8 != " ")
                {
                    query = query + " and \"" + parameter8 + "\"";
                }
                else
                {
                    query = query + " and cp_price ";
                }
                query = query + " order by mc_status";
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQueryClients(string query, string parameter1, string parameter2, string parameter3, string parameter4)
        {
            try
            {
                if (parameter2 != null && parameter2 != "" && parameter2 != "Sex")
                {
                    query = query + " and cl_sex = \"" + parameter2 + "\"";
                }
                if (parameter3 != null && parameter3 != "" && parameter3 != "from")
                {
                    query = query + " and cl_birthday between \"" + parameter3 + "\"";
                }
                else
                {
                    query = query + " and cl_birthday between cl_birthday";
                }
                if (parameter4 != null && parameter4 != "" && parameter4 != "to")
                {
                    query = query + " and \"" + parameter4 + "\"";
                }
                else
                {
                    query = query + " and cl_birthday";
                }
                query = query + " group by 1";
                if (parameter1 == "Buyer")
                {
                    query = query + " having car = \"\"";
                }
                else
                {
                    query = query + " having car = car";
                }
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQueryEmployees(string query, string parameter1, string parameter2, string parameter3, string parameter4, string parameter5)
        {
            try
            {
                if (parameter1 != null && parameter1 != "" && parameter1 != "Motor show")
                {
                    query = query + " and b_name = \"" + parameter1 + "\"";
                }
                if (parameter2 != null && parameter2 != "" && parameter2 != "Post")
                {
                    query = query + " and em_post = \"" + parameter2 + "\"";
                }
                if (parameter3 != null && parameter3 != "" && parameter3 != "Sex")
                {
                    query = query + " and em_sex = \"" + parameter3 + "\"";
                }
                if (parameter4 != null && parameter4 != "" && parameter4 != "from")
                {
                    query = query + " and em_birthday between \"" + parameter4 + "\"";
                }
                else
                {
                    query = query + " and em_birthday between em_birthday";
                }
                if (parameter5 != null && parameter5 != "" && parameter5 != "to")
                {
                    query = query + " and \"" + parameter5 + "\"";
                }
                else
                {
                    query = query + " and em_birthday";
                }
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQuerySales(string query, string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6)
        {
            try
            {
                if (parameter1 != null && parameter1 != "" && parameter1 != "Client name")
                {
                    query = query + " and CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) = \"" + parameter1 + "\"";
                }
                if (parameter2 != null && parameter2 != "" && parameter2 != "Employee name")
                {
                    query = query + " and CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) = \"" + parameter2 + "\"";
                }
                if (parameter3 != null && parameter3 != "" && parameter3 != "from")
                {
                    query = query + " and sa_date between \"" + parameter3 + "\"";
                }
                else
                {
                    query = query + " and sa_date between sa_date";
                }
                if (parameter4 != null && parameter4 != "" && parameter4 != "to")
                {
                    query = query + " and \"" + parameter4 + "\"";
                }
                else
                {
                    query = query + " and sa_date";
                }
                if (parameter5 != null && parameter5 != "" && parameter5 != "from")
                {
                    query = query + " and sa_price between \"" + parameter5 + "\"";
                }
                else
                {
                    query = query + " and sa_price between sa_price";
                }
                if (parameter6 != null && parameter6 != "" && parameter6 != "to")
                {
                    query = query + " and \"" + parameter6 + "\"";
                }
                else
                {
                    query = query + " and sa_price";
                }
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQueryStatistic(string query, string parameter1, string parameter2, string parameter3, string parameter4, string parameter5)
        {
            try
            {
                if (parameter1 != null && parameter1 != "" && parameter1 != "All")
                {
                    query = query + " and b_name = \"" + parameter1 + "\"";
                }
                if (parameter2 != null && parameter2 != "" && parameter2 != "from")
                {
                    query = query + " and sa_date between \"" + parameter2 + "\"";
                }
                else
                {
                    query = query + " and sa_date between sa_date";
                }
                if (parameter3 != null && parameter3 != "" && parameter3 != "to")
                {
                    query = query + " and \"" + parameter3 + "\"";
                }
                else
                {
                    query = query + " and sa_date";
                }
                if (parameter4 != null && parameter4 != "" && parameter4 != "from")
                {
                    query = query + " and sa_price between \"" + parameter4 + "\"";
                }
                else
                {
                    query = query + " and sa_price between sa_price";
                }
                if (parameter5 != null && parameter5 != "" && parameter5 != "to")
                {
                    query = query + " and \"" + parameter5 + "\"";
                }
                else
                {
                    query = query + " and sa_price ";
                }
                query = query + " group by 1";
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
    }
}
