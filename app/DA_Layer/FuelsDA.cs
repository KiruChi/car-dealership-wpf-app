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
    class FuelsDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Fuels> RetrieveFuels()
        {
            string query = "SELECT * FROM company_db.fuel";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Fuels> cr = new List<Fuels>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_fuel"]);
                    string name = dr["fuel"].ToString();
                    Fuels a = new Fuels(id, name);
                    cr.Add(a);
                }
            }
            return cr;
        }
    }
}
