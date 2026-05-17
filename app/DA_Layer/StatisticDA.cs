using Kursova.Classes;
using Kursova.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Kursova.DA_Layer
{
    class StatisticDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Statistic> RetrieveStatisticCars(string p1, string p2, string p3, string p4, string p5)
        {
            string query = "SELECT CONCAT(brand, '-',model) as Name, count(ID_sale) as NumOfSale FROM company_db.sale join employee using(ID_employee) join branch on ID_motor_show = ID_branch join car using(ID_car) join brand_model using(ID_brand_model) where ID_sale = ID_sale";
            cmd = DBHelper.RunQueryStatistic(query, p1, p2, p3, p4, p5);
            List<Statistic> cr = new List<Statistic>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["name"].ToString();
                    int numofsale = Convert.ToInt32(dr["NumOfSale"]);
                    Statistic a = new Statistic(name, numofsale);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static List<Statistic> RetrieveStatisticEmployees(string p1, string p2, string p3, string p4, string p5)
        {
            string query = "SELECT CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) as Name, count(ID_sale) as NumOfSale FROM company_db.sale join employee using(ID_employee) join branch on ID_motor_show = ID_branch where ID_sale = ID_sale ";
            cmd = DBHelper.RunQueryStatistic(query, p1, p2, p3, p4, p5);
            List<Statistic> cr = new List<Statistic>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["name"].ToString();
                    int numofsale = Convert.ToInt32(dr["NumOfSale"]);
                    Statistic a = new Statistic(name, numofsale);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static List<Statistic> RetrieveStatisticClients(string p1, string p2, string p3, string p4, string p5)
        {
            string query = "SELECT CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) as Name, count(ID_sale) as NumOfSale FROM company_db.sale join employee using(ID_employee) join branch on ID_motor_show = ID_branch join client using(ID_client) where ID_sale = ID_sale ";
            cmd = DBHelper.RunQueryStatistic(query, p1, p2, p3, p4, p5);
            List<Statistic> cr = new List<Statistic>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["name"].ToString();
                    int numofsale = Convert.ToInt32(dr["NumOfSale"]);
                    Statistic a = new Statistic(name, numofsale);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static List<Statistic> RetrieveStatisticBranches(string p1, string p2, string p3, string p4, string p5)
        {
            string query = "SELECT b_name as Name, count(ID_sale) as NumOfSale FROM company_db.sale join employee using(ID_employee) join branch on ID_motor_show = ID_branch where b_type = 'Motor show' ";
            cmd = DBHelper.RunQueryStatistic(query, p1, p2, p3, p4, p5);
            List<Statistic> cr = new List<Statistic>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["name"].ToString();
                    int numofsale = Convert.ToInt32(dr["NumOfSale"]);
                    Statistic a = new Statistic(name, numofsale);
                    cr.Add(a);
                }
            }
            return cr;
        }
    }
}
