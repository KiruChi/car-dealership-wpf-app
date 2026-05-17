using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursova.Classes;
using MySql.Data.MySqlClient;
using Kursova.Helper;
using System.Data;

namespace Kursova.DA_Layer
{
    class ColorsDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Colors> RetrieveColors()
        {
            string query = "SELECT * FROM company_db.color";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Colors> cr = new List<Colors>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_color"]);
                    string name = dr["color"].ToString();
                    Colors a = new Colors(id, name);
                    cr.Add(a);
                }
            }
            return cr;
        }
    }
}
