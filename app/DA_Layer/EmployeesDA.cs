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
using System.Windows.Media;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Kursova.DA_Layer
{
    class EmployeesDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Employees> RetrieveEmployees()
        {
            string query = "SELECT ID_employee, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS full_name, em_phone, em_email, em_sex, CONCAT(year(em_birthday), '-',month(em_birthday),'-',day(em_birthday)) as em_birthday, em_post, em_address, em_passport, em_RNOCPP, b_name FROM company_db.employee left join branch on ID_motor_show = ID_branch";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Employees> cr = new List<Employees>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_employee"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["em_phone"].ToString();
                    string email = dr["em_email"].ToString();
                    string sex = dr["em_sex"].ToString();
                    string birthday = dr["em_birthday"].ToString();
                    string post = dr["em_post"].ToString();
                    string address = dr["em_address"].ToString();
                    long passport = Convert.ToInt64(dr["em_passport"]);
                    long RNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    string motor_show = dr["b_name"].ToString();
                    Employees carr = new Employees(id, full_name, phone, email, sex, birthday, post, address, passport, RNOCPP, motor_show);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static Employees RetrieveEmployeesWithName(string name)
        {
            string query = "SELECT ID_employee, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS full_name, em_phone, em_email, em_sex, CONCAT(year(em_birthday), '-',month(em_birthday),'-',day(em_birthday)) as em_birthday, em_post, em_address, em_passport, em_RNOCPP, b_name FROM company_db.employee left join branch on ID_motor_show = ID_branch where CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) = @parameter1 limit 1";
            cmd = DBHelper.RunQueryWithParameters(query, name);
            Employees a = new Employees(0, "", "", "", "", "", "","",0,0,"");
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_employee"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["em_phone"].ToString();
                    string email = dr["em_email"].ToString();
                    string sex = dr["em_sex"].ToString();
                    string birthday = dr["em_birthday"].ToString();
                    string post = dr["em_post"].ToString();
                    string address = dr["em_address"].ToString();
                    long passport = Convert.ToInt64(dr["em_passport"]);
                    long RNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    string motor_show = dr["b_name"].ToString();
                    a = new Employees(id, full_name, phone, email, sex, birthday, post, address, passport, RNOCPP, motor_show);
                }
            }
            return a;
        }
        public static List<Employees> RetrieveEmployeesWithMotorShow(string MotorShow)
        {
            string query = "SELECT ID_employee, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS full_name, em_phone, em_email, em_sex, CONCAT(year(em_birthday), '-',month(em_birthday),'-',day(em_birthday)) as em_birthday, em_post, em_address, em_passport, em_RNOCPP, b_name FROM company_db.employee left join branch on ID_motor_show = ID_branch where b_name = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, MotorShow);
            List<Employees> cr = new List<Employees>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_employee"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["em_phone"].ToString();
                    string email = dr["em_email"].ToString();
                    string sex = dr["em_sex"].ToString();
                    string birthday = dr["em_birthday"].ToString();
                    string post = dr["em_post"].ToString();
                    string address = dr["em_address"].ToString();
                    long passport = Convert.ToInt64(dr["em_passport"]);
                    long RNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    string motor_show = dr["b_name"].ToString();
                    Employees carr = new Employees(id, full_name, phone, email, sex, birthday, post, address, passport, RNOCPP, motor_show);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static void AddEmployee(string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8, string p9, string p10, string p11)
        {
            string[] mas = p2.Split(' ');
            string query = "";
            if (p1 == "false")
            {
                query = "INSERT INTO Employee (em_first_name, em_middle_name, em_last_name, em_phone, em_email, em_sex, em_birthday, em_post, em_address, em_passport, em_RNOCPP, ID_motor_show) VALUES (@parameter2, @parameter3, @parameter4, @parameter5, @parameter6, @parameter7, @parameter8, @parameter9, @parameter10, @parameter11, @parameter12, @parameter13)";
            }
            else
            {
                query = "INSERT INTO Employee (ID_employee, em_first_name, em_middle_name, em_last_name, em_phone, em_email, em_sex, em_birthday, em_post, em_address, em_passport, em_RNOCPP, ID_motor_show) VALUES (@parameter1, @parameter2, @parameter3, @parameter4, @parameter5, @parameter6, @parameter7, @parameter8, @parameter9, @parameter10, @parameter11, @parameter12, @parameter13)";
            }
            List<Branches> a8 = BranchesDA.RetrieveMotor_shows();
            foreach (Branches c in a8)
            {
                if (p11 == c.Name)
                {
                    p11 = c.Id.ToString();
                }
            }
            cmd = DBHelper.RunQueryWithParameters(query, p1, mas[0], mas[1], mas[2], p3, p4, p5, p6, p7, p8, p9, p10, p11);
        }
        public static List<Employees> RetrieveEmployeesWithParameters(string p1, string p2, string p3, string p4, string p5)
        {
            string query = "SELECT ID_employee, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS full_name, em_phone, em_email, em_sex, CONCAT(year(em_birthday), '-',month(em_birthday),'-',day(em_birthday)) as em_birthday, em_post, em_address, em_passport, em_RNOCPP, b_name FROM company_db.employee left join branch on ID_motor_show = ID_branch where ID_employee = ID_employee";
            
            cmd = DBHelper.RunQueryEmployees(query, p1, p2, p3, p4, p5);
            List<Employees> cr = new List<Employees>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_employee"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["em_phone"].ToString();
                    string email = dr["em_email"].ToString();
                    string sex = dr["em_sex"].ToString();
                    string birthday = dr["em_birthday"].ToString();
                    string post = dr["em_post"].ToString();
                    string address = dr["em_address"].ToString();
                    long passport = Convert.ToInt64(dr["em_passport"]);
                    long RNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    string motor_show = dr["b_name"].ToString();
                    Employees carr = new Employees(id, full_name, phone, email, sex, birthday, post, address, passport, RNOCPP, motor_show);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Employees> SearchEmployees(string s)
        {
            string query = "SELECT ID_employee, CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) AS full_name, em_phone, em_email, em_sex, CONCAT(year(em_birthday), '-',month(em_birthday),'-',day(em_birthday)) as em_birthday, em_post, em_address, em_passport, em_RNOCPP, b_name FROM company_db.employee left join branch on ID_motor_show = ID_branch where ID_employee like @searchParameter or CONCAT(em_first_name, ' ',em_middle_name,' ',em_last_name) like @searchParameter or em_phone like @searchParameter or em_email like @searchParameter or em_birthday like @searchParameter or em_post like @searchParameter or em_address like @searchParameter or em_passport like @searchParameter or em_RNOCPP like @searchParameter or b_name like @searchParameter";
            cmd = DBHelper.RunQuerySearch(query, s);
            List<Employees> cr = new List<Employees>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_employee"]);
                    string full_name = dr["full_name"].ToString();
                    string phone = dr["em_phone"].ToString();
                    string email = dr["em_email"].ToString();
                    string sex = dr["em_sex"].ToString();
                    string birthday = dr["em_birthday"].ToString();
                    string post = dr["em_post"].ToString();
                    string address = dr["em_address"].ToString();
                    long passport = Convert.ToInt64(dr["em_passport"]);
                    long RNOCPP = Convert.ToInt64(dr["em_RNOCPP"]);
                    string motor_show = dr["b_name"].ToString();
                    Employees carr = new Employees(id, full_name, phone, email, sex, birthday, post, address, passport, RNOCPP, motor_show);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Employees> DeleteEmployee(string s)
        {
            string query;
            query = "DELETE FROM company_db.sale where ID_employee = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            query = "DELETE FROM company_db.employee where ID_employee = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            List<Employees> cr = new List<Employees>();
            cr = EmployeesDA.RetrieveEmployees();
            return cr;
        }
        public static void UpdateEmployee(string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8, string p9, string p10, string p11)
        {
            string[] mas = p2.Split(' ');
            string query = "UPDATE company_db.employee SET em_first_name = @parameter2, em_middle_name = @parameter3, em_last_name = @parameter4, em_phone = @parameter5, em_email = @parameter6, em_sex = @parameter7, em_birthday = @parameter8, em_post = @parameter9, em_address = @parameter10, em_passport = @parameter11, em_RNOCPP = @parameter12, ID_motor_show = @parameter13 WHERE ID_employee = @parameter1";
            List<Branches> a8 = BranchesDA.RetrieveMotor_shows();
            foreach (Branches c in a8)
            {
                if (p11 == c.Name)
                {
                    p11 = c.Id.ToString();
                }
            }
            cmd = DBHelper.RunQueryWithParameters(query, p1, mas[0], mas[1], mas[2], p3, p4, p5, p6, p7, p8, p9, p10, p11);
        }
    }
}
