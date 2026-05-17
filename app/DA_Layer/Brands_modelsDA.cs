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
    class Brands_modelsDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Brands_models> RetrieveBrands_models()
        {
            string query = "SELECT * FROM company_db.brand_model";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Brands_models> cr = new List<Brands_models>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_brand_model"]);
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    Brands_models a = new Brands_models(id, brand, model);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static List<Brands_models> RetrieveModels(string b)
        {
            string query = "SELECT * FROM company_db.brand_model where brand = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, b);
            List<Brands_models> cr = new List<Brands_models>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_brand_model"]);
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    Brands_models a = new Brands_models(id, brand, model);
                    cr.Add(a);
                }
            }
            return cr;
        }
    }
}
