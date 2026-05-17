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
    class TransmissionsDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Transmissions> RetrieveTransmissions()
        {
            string query = "SELECT * FROM company_db.transmission";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Transmissions> cr = new List<Transmissions>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_kpp"]);
                    string name = dr["kpp"].ToString();
                    Transmissions a = new Transmissions(id, name);
                    cr.Add(a);
                }
            }
            return cr;
        }
    }
}
