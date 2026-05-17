using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursova.Classes;
using MySql.Data.MySqlClient;
using Kursova.Helper;
using System.Data;
using Org.BouncyCastle.Asn1.Pkcs;
using System.ComponentModel;
using System.Buffers;

namespace Kursova.DA_Layer
{
    class ClientsDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Clients> RetrieveClients()
        {
            string query = "SELECT ID_client, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS full_name, cl_sex, CONCAT(year(cl_birthday), '-',month(cl_birthday),'-',day(cl_birthday)) as cl_birthday, cl_phone, cl_address, cl_RNOCPP FROM company_db.client";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Clients> cr = new List<Clients>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_client"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["cl_phone"].ToString();
                    string sex = dr["cl_sex"].ToString();
                    string birthday = dr["cl_birthday"].ToString();
                    string address = dr["cl_address"].ToString();
                    long RNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    Clients carr = new Clients(id, full_name, phone, sex, birthday, address, RNOCPP);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static Clients RetrieveClientsWithName(string name)
        {
            string query = "SELECT ID_client, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS full_name, cl_sex, CONCAT(year(cl_birthday), '-',month(cl_birthday),'-',day(cl_birthday)) as cl_birthday, cl_phone, cl_address, cl_RNOCPP FROM company_db.client where CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) = @parameter1 limit 1";
            cmd = DBHelper.RunQueryWithParameters(query, name);
            Clients a = new Clients(0, "", " ", "", "","", 0);
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_client"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["cl_phone"].ToString();
                    string sex = dr["cl_sex"].ToString();
                    string birthday = dr["cl_birthday"].ToString();
                    string address = dr["cl_address"].ToString();
                    long RNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    a = new Clients(id, full_name, phone, sex, birthday, address, RNOCPP);
                }
            }
            return a;
        }
        public static void AddClient(string p1, string p2, string p3, string p4, string p5, string p6, string p7)
        {
            string[] mas = p2.Split(' ');
            string query = "";
            if (p1 == "false")
            {
                query = "INSERT INTO Client (cl_first_name, cl_middle_name, cl_last_name, cl_phone, cl_sex, cl_birthday, cl_address, cl_RNOCPP) VALUES (@parameter2, @parameter3, @parameter4, @parameter5, @parameter6, @parameter7, @parameter8, @parameter9)";
            }
            else
            {
                query = "INSERT INTO Client (ID_client, cl_first_name, cl_middle_name, cl_last_name, cl_phone, cl_sex, cl_birthday, cl_address, cl_RNOCPP) VALUES (@parameter1, @parameter2, @parameter3, @parameter4, @parameter5, @parameter6, @parameter7, @parameter8, @parameter9)";
            }
            cmd = DBHelper.RunQueryWithParameters(query, p1, mas[0], mas[1], mas[2], p3, p4, p5, p6, p7);
        }
        public static List<Clients> RetrieveClientsWithParameters(string p1, string p2, string p3, string p4)
        {
            string query;
            if (p1 == "Owner")
            {
                query = "SELECT ID_client, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS full_name, cl_sex, CONCAT(year(cl_birthday), '-',month(cl_birthday),'-',day(cl_birthday)) as cl_birthday, cl_phone, cl_address, cl_RNOCPP, group_concat(ID_car) as car FROM company_db.client join car using (ID_client) where ID_client = ID_client";
            }
            else
            {
                query = "SELECT ID_client, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS full_name, cl_sex, CONCAT(year(cl_birthday), '-',month(cl_birthday),'-',day(cl_birthday)) as cl_birthday, cl_phone, cl_address, cl_RNOCPP, group_concat(ifnull(ID_car,\"\")) as car FROM company_db.client left join car using (ID_client) where ID_client = ID_client";
            }
            cmd = DBHelper.RunQueryClients(query, p1, p2, p3, p4);
            List<Clients> cr = new List<Clients>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_client"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["cl_phone"].ToString();
                    string sex = dr["cl_sex"].ToString();
                    string birthday = dr["cl_birthday"].ToString();
                    string address = dr["cl_address"].ToString();
                    long RNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    Clients carr = new Clients(id, full_name, phone, sex, birthday, address, RNOCPP);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Clients> SearchClients(string s)
        {
            string query = "SELECT ID_client, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS full_name, cl_sex, CONCAT(year(cl_birthday), '-',month(cl_birthday),'-',day(cl_birthday)) as cl_birthday, cl_phone, cl_address, cl_RNOCPP FROM company_db.client where ID_client like @searchParameter or CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) like @searchParameter or cl_birthday like @searchParameter or cl_phone like @searchParameter or cl_address like @searchParameter or cl_RNOCPP like @searchParameter";
            cmd = DBHelper.RunQuerySearch(query, s);
            List<Clients> cr = new List<Clients>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_client"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["cl_phone"].ToString();
                    string sex = dr["cl_sex"].ToString();
                    string birthday = dr["cl_birthday"].ToString();
                    string address = dr["cl_address"].ToString();
                    long RNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    Clients carr = new Clients(id, full_name, phone, sex, birthday, address, RNOCPP);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Clients> DeleteClients(string s)
        {
            string query;
            query = "DELETE FROM company_db.sale where ID_client = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            query = "DELETE FROM company_db.client where ID_client = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            List<Clients> cr = new List<Clients>();
            cr = ClientsDA.RetrieveClients();
            return cr;
        }
        public static void UpdateClient(string p1, string p2, string p3, string p4, string p5, string p6, string p7)
        {
            string[] mas = p2.Split(' ');
            string query = "UPDATE company_db.client SET cl_first_name = @parameter2, cl_middle_name = @parameter3, cl_last_name = @parameter4, cl_phone = @parameter5, cl_sex = @parameter6, cl_birthday = @parameter7, cl_address = @parameter8, cl_RNOCPP = @parameter9 WHERE ID_client = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, p1, mas[0], mas[1], mas[2], p3, p4, p5, p6, p7);
        }
    }
}
