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
    class BodysDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Bodys> RetrieveBodys()
        {
            string query = "SELECT * FROM company_db.body";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Bodys> cr = new List<Bodys>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_body"]);
                    string name = dr["body"].ToString();
                    Bodys a = new Bodys(id, name);
                    cr.Add(a);
                }
            }
            return cr;
        }
    }
}
