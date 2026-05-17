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

namespace Kursova.DA_Layer
{
    class SalesDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Sales> RetrieveSales()
        {
            string query = "SELECT ID_sale, car_vin_code, ID_car, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS cl_full_name, cl_RNOCPP, s.ID_client, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS em_full_name, em_RNOCPP, s.ID_employee, CONCAT(year(sa_date), '-',month(sa_date),'-',day(sa_date)) as sa_date, sa_price FROM company_db.sale s join company_db.client using(ID_client) join company_db.car using(ID_car)  join employee using(ID_employee)";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Sales> cr = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_sale"]);
                    string carname = dr["car_vin_code"].ToString();
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    string clientname = dr["cl_full_name"].ToString();
                    long clientRNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    int clientid = Convert.ToInt32(dr["ID_client"]);
                    string employeename = dr["em_full_name"].ToString();
                    long employeeRNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    int employeeid = Convert.ToInt32(dr["ID_employee"]);
                    string date = dr["sa_date"].ToString();
                    long price = Convert.ToInt64(dr["sa_price"]);
                    Sales a = new Sales(id, carname, carid, clientname, clientRNOCPP, clientid, employeename, employeeRNOCPP, employeeid, date, price);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static Sales RetrieveSalesWithCarId(string Id)
        {
            string query = "SELECT ID_sale, car_vin_code, ID_car, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS cl_full_name, cl_RNOCPP, s.ID_client, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS em_full_name, em_RNOCPP, s.ID_employee, CONCAT(year(sa_date), '-',month(sa_date),'-',day(sa_date)) as sa_date, sa_price FROM company_db.sale s join company_db.client using(ID_client) join company_db.car using(ID_car)  join employee using(ID_employee) where ID_car = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, Id);
            Sales a = new Sales(0, "", 0, "",0, 0, "", 0, 0,"",0);
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_sale"]);
                    string carname = dr["car_vin_code"].ToString();
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    string clientname = dr["cl_full_name"].ToString();
                    long clientRNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    int clientid = Convert.ToInt32(dr["ID_client"]);
                    string employeename = dr["em_full_name"].ToString();
                    long employeeRNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    int employeeid = Convert.ToInt32(dr["ID_employee"]);
                    string date = dr["sa_date"].ToString();
                    long price = Convert.ToInt64(dr["sa_price"]);
                    a = new Sales(id, carname, carid, clientname, clientRNOCPP, clientid, employeename, employeeRNOCPP, employeeid, date, price);
                }
            }
            return a;
        }
        public static void AddSale(string p1, string p2, string p3, string p4, string p5, string p6)
        {
            string query = "";
            if (p1 == "false")
            {
                query = "INSERT INTO Sale ( ID_car, ID_client, ID_employee, sa_date, sa_price) VALUES ( @parameter2, @parameter3, @parameter4, @parameter5, @parameter6)";
            }
            else
            {
                query = "INSERT INTO Sale (ID_sale, ID_car, ID_client, ID_employee, sa_date, sa_price) VALUES (@parameter1, @parameter2, @parameter3, @parameter4, @parameter5, @parameter6)";
            }
            List<Cars> a1 = CarsDA.RetrieveCars();
            foreach (Cars c in a1)
            {
                if (p2 == c.Vin_code)
                {
                    p2 = c.Id.ToString();
                }
            }
            List<Clients> a2 = ClientsDA.RetrieveClients();
            foreach (Clients c in a2)
            {
                if (p3 == c.Full_name)
                {
                    p3 = c.Id.ToString();
                }
            }
            List<Employees> a3 = EmployeesDA.RetrieveEmployees();
            foreach (Employees c in a3)
            {
                if (p4 == c.Full_name)
                {
                    p4 = c.Id.ToString();
                }
            }
            cmd = DBHelper.RunQueryWithParameters(query, p1, p2, p3, p4, p5, p6);
        }
        public static List<Sales> RetrieveSalesWithClientId(string Id)
        {
            string query = "SELECT ID_sale, car_vin_code, ID_car, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS cl_full_name, cl_RNOCPP, s.ID_client, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS em_full_name, em_RNOCPP, s.ID_employee, CONCAT(year(sa_date), '-',month(sa_date),'-',day(sa_date)) as sa_date, sa_price FROM company_db.sale s join company_db.client using(ID_client) join company_db.car using(ID_car)  join employee using(ID_employee) where s.ID_client = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, Id);
            List<Sales> cr = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_sale"]);
                    string carname = dr["car_vin_code"].ToString();
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    string clientname = dr["cl_full_name"].ToString();
                    long clientRNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    int clientid = Convert.ToInt32(dr["ID_client"]);
                    string employeename = dr["em_full_name"].ToString();
                    long employeeRNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    int employeeid = Convert.ToInt32(dr["ID_employee"]);
                    string date = dr["sa_date"].ToString();
                    long price = Convert.ToInt64(dr["sa_price"]);
                    Sales a = new Sales(id, carname, carid, clientname, clientRNOCPP, clientid, employeename, employeeRNOCPP, employeeid, date, price);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static List<Sales> RetrieveSalesWithEmployeeId(string Id)
        {
            string query = "SELECT ID_sale, car_vin_code, ID_car, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS cl_full_name, cl_RNOCPP, s.ID_client, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS em_full_name, em_RNOCPP, s.ID_employee, CONCAT(year(sa_date), '-',month(sa_date),'-',day(sa_date)) as sa_date, sa_price FROM company_db.sale s join company_db.client using(ID_client) join company_db.car using(ID_car)  join employee using(ID_employee) where ID_employee = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, Id);
            List<Sales> cr = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_sale"]);
                    string carname = dr["car_vin_code"].ToString();
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    string clientname = dr["cl_full_name"].ToString();
                    long clientRNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    int clientid = Convert.ToInt32(dr["ID_client"]);
                    string employeename = dr["em_full_name"].ToString();
                    long employeeRNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    int employeeid = Convert.ToInt32(dr["ID_employee"]);
                    string date = dr["sa_date"].ToString();
                    long price = Convert.ToInt64(dr["sa_price"]);
                    Sales a = new Sales(id, carname, carid, clientname, clientRNOCPP, clientid, employeename, employeeRNOCPP, employeeid, date, price);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static List<Sales> RetrieveSalesWithParameters(string p1, string p2, string p3, string p4, string p5, string p6)
        {
            string query = "SELECT ID_sale, car_vin_code, ID_car, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS cl_full_name, cl_RNOCPP, s.ID_client, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS em_full_name, em_RNOCPP, s.ID_employee, CONCAT(year(sa_date), '-',month(sa_date),'-',day(sa_date)) as sa_date, sa_price FROM company_db.sale s join company_db.client using(ID_client) join company_db.car using(ID_car)  join employee using(ID_employee) where ID_sale = ID_sale";

            cmd = DBHelper.RunQuerySales(query, p1, p2, p3, p4, p5, p6);
            List<Sales> cr = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_sale"]);
                    string carname = dr["car_vin_code"].ToString();
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    string clientname = dr["cl_full_name"].ToString();
                    long clientRNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    int clientid = Convert.ToInt32(dr["ID_client"]);
                    string employeename = dr["em_full_name"].ToString();
                    long employeeRNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    int employeeid = Convert.ToInt32(dr["ID_employee"]);
                    string date = dr["sa_date"].ToString();
                    long price = Convert.ToInt64(dr["sa_price"]);
                    Sales a = new Sales(id, carname, carid, clientname, clientRNOCPP, clientid, employeename, employeeRNOCPP, employeeid, date, price);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static List<Sales> SearchSales(string s)
        {
            string query = "SELECT ID_sale, car_vin_code, ID_car, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS cl_full_name, cl_RNOCPP, s.ID_client, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS em_full_name, em_RNOCPP, s.ID_employee, CONCAT(year(sa_date), '-',month(sa_date),'-',day(sa_date)) as sa_date, sa_price FROM company_db.sale s join company_db.client using(ID_client) join company_db.car using(ID_car)  join employee using(ID_employee) where ID_sale like @searchParameter or car_name like @searchParameter or car_vin_code like @searchParameter or ID_car like @searchParameter or CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) like @searchParameter or s.ID_client like @searchParameter or cl_RNOCPP like @searchParameter or CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) like @searchParameter or s.ID_employee like @searchParameter or em_RNOCPP like @searchParameter or CONCAT(year(sa_date), '-',month(sa_date),'-',day(sa_date)) like @searchParameter;";
            cmd = DBHelper.RunQuerySearch(query, s);
            List<Sales> cr = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_sale"]);
                    string carname = dr["car_vin_code"].ToString();
                    int carid = Convert.ToInt32(dr["ID_car"]);
                    string clientname = dr["cl_full_name"].ToString();
                    long clientRNOCPP = Convert.ToInt64(dr["cl_RNOCPP"]);
                    int clientid = Convert.ToInt32(dr["ID_client"]);
                    string employeename = dr["em_full_name"].ToString();
                    long employeeRNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    int employeeid = Convert.ToInt32(dr["ID_employee"]);
                    string date = dr["sa_date"].ToString();
                    long price = Convert.ToInt64(dr["sa_price"]);
                    Sales a = new Sales(id, carname, carid, clientname, clientRNOCPP, clientid, employeename, employeeRNOCPP, employeeid, date, price);
                    cr.Add(a);
                }
            }
            return cr;
        }
        public static List<Sales> DeleteSale(string s, string id)
        {
            string query;
            query = "DELETE FROM company_db.sale where ID_sale = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            CarsDA.UpdateCarStatus(id, "Available");
            List<Sales> cr = new List<Sales>();
            cr = SalesDA.RetrieveSales();
            return cr;
        }
        public static void UpdateSale(string p1, string p2, string p3, string p4, string p5, string p6)
        {
            string query = "UPDATE company_db.sale SET ID_car = @parameter2, ID_client = @parameter3, ID_employee = @parameter4, sa_date = @parameter5, sa_price = @parameter6 WHERE ID_sale = @parameter1";
            List<Cars> a1 = CarsDA.RetrieveCars();
            foreach (Cars c in a1) 
            {
                if (p2 == c.Vin_code)
                {
                    p2 = c.Id.ToString();
                }
            }
            List<Clients> a2 = ClientsDA.RetrieveClients();
            foreach (Clients c in a2)
            {
                if (p3 == c.Full_name)
                {
                    p3 = c.Id.ToString();
                }
            }
            List<Employees> a3 = EmployeesDA.RetrieveEmployees();
            foreach (Employees c in a3)
            {
                if (p4 == c.Full_name)
                {
                    p4 = c.Id.ToString();
                }
            }
            cmd = DBHelper.RunQueryWithParameters(query, p1, p2, p3, p4, p5, p6);
        }
    }
}
