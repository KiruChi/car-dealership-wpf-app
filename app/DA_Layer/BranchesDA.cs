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
    internal class BranchesDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Branches> RetrieveBranches()
        {
            string query = "SELECT * FROM company_db.branch";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Branches> cr = new List<Branches>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_branch"]);
                    string name = dr["b_name"].ToString();
                    string address = dr["b_address"].ToString();
                    string phone = dr["b_phone"].ToString();
                    string email = dr["b_email"].ToString();
                    long EDRPOU = Convert.ToInt64(dr["b_EDRPOU"]);
                    string type = dr["b_type"].ToString();
                    Branches carr = new Branches(id, name, address, phone, email, EDRPOU, type);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Branches> RetrieveMotor_shows()
        {
            string query = "SELECT * FROM company_db.branch where b_type = \"Motor show\"";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Branches> cr = new List<Branches>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_branch"]);
                    string name = dr["b_name"].ToString();
                    string address = dr["b_address"].ToString();
                    string phone = dr["b_phone"].ToString();
                    string email = dr["b_email"].ToString();
                    long EDRPOU = Convert.ToInt64(dr["b_EDRPOU"]);
                    string type = dr["b_type"].ToString();
                    Branches carr = new Branches(id, name, address, phone, email, EDRPOU, type);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Branches> RetrieveService_centers()
        {
            string query = "SELECT * FROM company_db.branch where b_type = \"Service center\"";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Branches> cr = new List<Branches>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_branch"]);
                    string name = dr["b_name"].ToString();
                    string address = dr["b_address"].ToString();
                    string phone = dr["b_phone"].ToString();
                    string email = dr["b_email"].ToString();
                    long EDRPOU = Convert.ToInt64(dr["b_EDRPOU"]);
                    string type = dr["b_type"].ToString();
                    Branches carr = new Branches(id, name, address, phone, email, EDRPOU, type);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static Branches RetrieveService_centersWithCarId(string id_)
        {
            string query = "SELECT * FROM company_db.branch join company_db.check on ID_branch = ID_service_center where ID_car = @parameter1 LIMIT 1";
            cmd = DBHelper.RunQueryWithParameters(query, id_);
            Branches cr = new Branches(0,"","","","",0,"");
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_branch"]);
                    string name = dr["b_name"].ToString();
                    string address = dr["b_address"].ToString();
                    string phone = dr["b_phone"].ToString();
                    string email = dr["b_email"].ToString();
                    long EDRPOU = Convert.ToInt64(dr["b_EDRPOU"]);
                    string type = dr["b_type"].ToString();
                    cr = new Branches(id, name, address, phone, email, EDRPOU, type);
                }
            }
            return cr;
        }
        public static void AddBranch(string p1, string p2, string p3, string p4, string p5, string p6, string p7)
        {
            string query = "";
            if (p1 == "false")
            {
                query = "INSERT INTO Branch (b_name, b_address,  b_phone, b_email, b_EDRPOU, b_type) VALUES (@parameter2, @parameter3, @parameter4, @parameter5, @parameter6, @parameter7)";
            }
            else
            {
                query = "INSERT INTO Branch (ID_branch, b_name, b_address,  b_phone, b_email, b_EDRPOU, b_type) VALUES (@parameter1, @parameter2, @parameter3, @parameter4, @parameter5, @parameter6, @parameter7)";
            }
            cmd = DBHelper.RunQueryWithParameters(query, p1, p2, p3, p4, p5, p6, p7);
        }
        public static List<Branches> SearchBranches(string s)
        {
            string query = "SELECT ID_branch, b_name, b_address, b_phone, b_email, b_EDRPOU, b_type FROM company_db.branch where ID_branch like @searchParameter or b_name like @searchParameter or b_address like @searchParameter or b_phone like @searchParameter or b_email like @searchParameter or b_EDRPOU like @searchParameter or b_type like @searchParameter";
            cmd = DBHelper.RunQuerySearch(query, s);
            List<Branches> cr = new List<Branches>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_branch"]);
                    string name = dr["b_name"].ToString();
                    string address = dr["b_address"].ToString();
                    string phone = dr["b_phone"].ToString();
                    string email = dr["b_email"].ToString();
                    long EDRPOU = Convert.ToInt64(dr["b_EDRPOU"]);
                    string type = dr["b_type"].ToString();
                    Branches carr = new Branches(id, name, address, phone, email, EDRPOU, type);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Branches> DeleteMotor_show(string s)
        {
            string query;
            query = "DELETE FROM company_db.branch where ID_branch = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            List<Branches> cr = new List<Branches>();
            cr = BranchesDA.RetrieveBranches();
            return cr;
        }
        public static List<Branches> DeleteService_center(string s)
        {
            string query;
            query = "DELETE FROM company_db.check where ID_service_center = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            query = "DELETE FROM company_db.branch where ID_branch = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            List<Branches> cr = new List<Branches>();
            cr = BranchesDA.RetrieveBranches();
            return cr;
        }
        public static void UpdateBranch(string p1, string p2, string p3, string p4, string p5, string p6, string p7)
        {
            string query = "UPDATE company_db.branch SET b_name = @parameter2, b_address = @parameter3, b_phone = @parameter4, b_email = @parameter5, b_EDRPOU = @parameter6, b_type = @parameter7 WHERE ID_branch = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, p1, p2, p3, p4, p5, p6, p7);
        }
    }
}