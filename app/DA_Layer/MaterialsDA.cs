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
    class MaterialsDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Materials> RetrieveMaterials()
        {
            string query = "SELECT * FROM company_db.interior_material";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Materials> cr = new List<Materials>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_material"]);
                    string name = dr["material"].ToString();
                    Materials a = new Materials(id, name);
                    cr.Add(a);
                }
            }
            return cr;
        }
    }
}
