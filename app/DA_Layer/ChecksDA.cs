using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursova.Classes;
using MySql.Data.MySqlClient;
using Kursova.Helper;
using System.Data;
using Mysqlx.Crud;
using System.Windows.Media;
using System.Windows;
using Org.BouncyCastle.Crypto.Engines;
using System.Collections;


namespace Kursova.DA_Layer
{
    class ChecksDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Checks> RetrieveChecks()
        {
            string query = "SELECT ID_check, ID_car, ID_service_center, CONCAT(year(ch_date), '-',month(ch_date),'-',day(ch_date)) as ch_date, ch_price, cp_price FROM company_db.check";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Checks> cr = new List<Checks>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_check"]);
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    int service_centerid = Convert.ToInt32(dr["ID_service_center"]);
                    string date = dr["ch_date"].ToString();
                    int check_price = Convert.ToInt32(dr["ch_price"]);
                    long car_price = Convert.ToInt64(dr["cp_price"]);
                    Checks a = new Checks(id, carid, service_centerid, date, check_price, car_price);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static Checks RetrieveChecksWithCarId(string Id)
        {
            string query = "SELECT ID_check, ID_car, ID_service_center, CONCAT(year(ch_date), '-',month(ch_date),'-',day(ch_date)) as ch_date, ch_price, cp_price FROM company_db.check where ID_car = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, Id);
            Checks a = new Checks(0, 0, 0, "", 0, 0);
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_check"]);
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    int service_centerid = Convert.ToInt32(dr["ID_service_center"]);
                    string date = dr["ch_date"].ToString();
                    int check_price = Convert.ToInt32(dr["ch_price"]);
                    long car_price = Convert.ToInt64(dr["cp_price"]);
                    a = new Checks(id, carid, service_centerid, date, check_price, car_price);
                }
            }
            return a;
        }
        public static List<Checks> RetrieveChecksWithServiceCenterId(string Id)
        {
            string query = "SELECT ID_check, ID_car, ID_service_center, CONCAT(year(ch_date), '-',month(ch_date),'-',day(ch_date)) as ch_date, ch_price, cp_price FROM company_db.check where ID_service_center = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, Id);
            List<Checks> cr = new List<Checks>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_check"]);
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    int service_centerid = Convert.ToInt32(dr["ID_service_center"]);
                    string date = dr["ch_date"].ToString();
                    int check_price = Convert.ToInt32(dr["ch_price"]);
                    long car_price = Convert.ToInt64(dr["cp_price"]);
                    Checks a = new Checks(id, carid, service_centerid, date, check_price, car_price);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static void AddCheck(string p1, string p2, string p3, string p4, string p5, string p6)
        {
            string query = "";
            if (p1 == "false")
            {
                query = "INSERT INTO `Check` (ID_car, ID_service_center, ch_date, ch_price, cp_price) VALUES (@parameter2, @parameter3, @parameter4, @parameter5, @parameter6)";
            }
            else
            {
                query = "INSERT INTO `Check` (ID_check, ID_car, ID_service_center, ch_date, ch_price, cp_price) VALUES (@parameter1, @parameter2, @parameter3, @parameter4, @parameter5, @parameter6)";
            }
            List<Branches> a = BranchesDA.RetrieveService_centers();
            foreach (Branches c in a)
            {
                if (p3 == c.Name)
                {
                    p3 = c.Id.ToString();
                }
            }
            cmd = DBHelper.RunQueryWithParameters(query, p1, p2, p3, p4, p5, p6);
        }
        public static void UpdateCheck(string p1, string p2, string p3, string p4, string p5, string p6)
        {
            string query = "update company_db.check set ID_service_center = @parameter3, ch_date = @parameter4, ch_price = @parameter5, cp_price = @parameter6 where ID_car = @parameter2";
            List<Branches> a = BranchesDA.RetrieveService_centers();
            foreach (Branches c in a)
            {
                if (p3 == c.Name)
                {
                    p3 = c.Id.ToString();
                }
            }
            cmd = DBHelper.RunQueryWithParameters(query, p1, p2, p3, p4, p5, p6);

        }
    }
}
