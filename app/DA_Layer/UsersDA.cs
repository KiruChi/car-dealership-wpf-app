using Kursova.Classes;
using MySql.Data.MySqlClient;
using Kursova.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursova.DA_Layer
{
    public static class UsersDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static Users RetrieveUser(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = @parameter1 LIMIT 1";
            cmd = DBHelper.RunQueryWithParameters(query, username);
            Users aUser = null;
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string uName = dr["Username"].ToString();
                    string password = dr["Password"].ToString();
                    string role = dr["Role"].ToString();
                    aUser = new Users(uName, password, role);
                }
            }
            return aUser;
        }
    }
}
