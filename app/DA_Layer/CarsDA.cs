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
using Google.Protobuf.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using System.Runtime.ConstrainedExecution;

namespace Kursova.DA_Layer
{
    class CarsDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Cars> RetrieveCars()
        {
            string query = "Select ID_car, car_name, brand, model, car_class, color, body, material, kpp, ifnull(cp_price,0) as cp_price, CONCAT(year(car_year), '-', month(car_year), '-', day(car_year)) as car_year, car_mileage, fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS client_name , car_vin_code, car_type_sale, b_name, mc_status from car left join brand_model using(ID_brand_model) left join color using(ID_color) left join body using(ID_body) left join interior_material using(ID_material) left join transmission using(ID_kpp) left join fuel using(ID_fuel) left join client using(ID_client) left join branch on ID_motor_show = ID_branch left join `check` using(ID_car) group by 1, 10 order by mc_status";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Cars> cr = new List<Cars>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_car"]);
                    string name = dr["car_name"].ToString();
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    string clas = dr["car_class"].ToString();
                    string color = dr["color"].ToString();
                    string body = dr["body"].ToString();
                    string material = dr["material"].ToString();
                    string kpp = dr["kpp"].ToString();
                    long price = Convert.ToInt64(dr["cp_price"]);
                    string year = dr["car_year"].ToString();
                    long mileage = Convert.ToInt64(dr["car_mileage"]);
                    string fuel = dr["fuel"].ToString();
                    string engine = dr["car_engine"].ToString();
                    string drive = dr["car_drive"].ToString();
                    int seats = Convert.ToInt32(dr["car_seats"]);
                    string heat = dr["car_heat"].ToString();
                    string air_cond = dr["car_air_cond"].ToString();
                    string discs = dr["car_discs"].ToString();
                    string rubber = dr["car_rubber"].ToString();
                    string owner = dr["client_name"].ToString();
                    string vin_code = dr["car_vin_code"].ToString();
                    string type_sale = dr["car_type_sale"].ToString();
                    string motor_show = dr["b_name"].ToString();
                    string status = dr["mc_status"].ToString();
                    Cars carr = new Cars(id, name, brand, model,clas, color, body, material, kpp, price, year, mileage, fuel, engine, drive, seats, heat, air_cond, discs, rubber, owner, vin_code, type_sale, motor_show, status);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Cars> RetrieveCarsAll()
        {
            string query = "Select ID_car, car_name, brand, model, car_class, color, body, material, kpp, ifnull(cp_price,0) as cp_price, CONCAT(year(car_year), '-', month(car_year), '-', day(car_year)) as car_year, car_mileage, fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS client_name , car_vin_code, car_type_sale, b_name, mc_status from car left join brand_model using(ID_brand_model) left join color using(ID_color) left join body using(ID_body) left join interior_material using(ID_material) left join transmission using(ID_kpp) left join fuel using(ID_fuel) left join client using(ID_client) left join branch on ID_motor_show = ID_branch left join `check` using(ID_car) group by 1, 10";
            cmd = DBHelper.RunQueryNoParameters(query);
            List<Cars> cr = new List<Cars>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_car"]);
                    string name = dr["car_name"].ToString();
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    string clas = dr["car_class"].ToString();
                    string color = dr["color"].ToString();
                    string body = dr["body"].ToString();
                    string material = dr["material"].ToString();
                    string kpp = dr["kpp"].ToString();
                    long price = Convert.ToInt64(dr["cp_price"]);
                    string year = dr["car_year"].ToString();
                    long mileage = Convert.ToInt64(dr["car_mileage"]);
                    string fuel = dr["fuel"].ToString();
                    string engine = dr["car_engine"].ToString();
                    string drive = dr["car_drive"].ToString();
                    int seats = Convert.ToInt32(dr["car_seats"]);
                    string heat = dr["car_heat"].ToString();
                    string air_cond = dr["car_air_cond"].ToString();
                    string discs = dr["car_discs"].ToString();
                    string rubber = dr["car_rubber"].ToString();
                    string owner = dr["client_name"].ToString();
                    string vin_code = dr["car_vin_code"].ToString();
                    string type_sale = dr["car_type_sale"].ToString();
                    string motor_show = dr["b_name"].ToString();
                    string status = dr["mc_status"].ToString();
                    Cars carr = new Cars(id, name, brand, model, clas, color, body, material, kpp, price, year, mileage, fuel, engine, drive, seats, heat, air_cond, discs, rubber, owner, vin_code, type_sale, motor_show, status);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Cars> RetrieveCarsWithParameters(string par1, string par2, string par3, string par4, string par5, string par6, string par7, string par8, string par9, string par10, string par11,
            string par12, string par13, string par14, string par15, string par16, string par17, string par18, string par19, string par20, string par21, string par22, string par23, string par24)
        {
            string query = "Select ID_car, car_name, brand, model, car_class, color, body, material, kpp, ifnull(cp_price,0) as cp_price, CONCAT(year(car_year), '-', month(car_year), '-', day(car_year)) as car_year, car_mileage, fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS client_name, car_vin_code, car_type_sale, b_name, mc_status from car left join brand_model using(ID_brand_model) left join color using(ID_color) left join body using(ID_body) left join interior_material using(ID_material) left join transmission using(ID_kpp) left join fuel using(ID_fuel) left join company_db.client using(ID_client) left join branch on ID_motor_show = ID_branch left join `check` using(ID_car) where ID_car = ID_car ";
            cmd = DBHelper.RunQueryCars(query, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12, par13, par14, par15, par16, par17, par18, par19, par20, par21, par22, par23, par24);
            List<Cars> cr = new List<Cars>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_car"]);
                    string name = dr["car_name"].ToString();
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    string clas = dr["car_class"].ToString();
                    string color = dr["color"].ToString();
                    string body = dr["body"].ToString();
                    string material = dr["material"].ToString();
                    string kpp = dr["kpp"].ToString();
                    long price = Convert.ToInt64(dr["cp_price"]);
                    string year = dr["car_year"].ToString();
                    long mileage = Convert.ToInt64(dr["car_mileage"]);
                    string fuel = dr["fuel"].ToString();
                    string engine = dr["car_engine"].ToString();
                    string drive = dr["car_drive"].ToString();
                    int seats = Convert.ToInt32(dr["car_seats"]);
                    string heat = dr["car_heat"].ToString();
                    string air_cond = dr["car_air_cond"].ToString();
                    string discs = dr["car_discs"].ToString();
                    string rubber = dr["car_rubber"].ToString();
                    string owner = dr["client_name"].ToString();
                    string vin_code = dr["car_vin_code"].ToString();
                    string type_sale = dr["car_type_sale"].ToString();
                    string motor_show = dr["b_name"].ToString();
                    string status = dr["mc_status"].ToString();
                    Cars carr = new Cars(id, name, brand, model, clas, color, body, material, kpp, price, year, mileage, fuel, engine, drive, seats, heat, air_cond, discs, rubber, owner, vin_code, type_sale, motor_show, status);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Cars> SearchCars(string s)
        {
            string query = "Select ID_car, car_name, brand, model, car_class, color, body, material, kpp, ifnull(cp_price,0) as cp_price, CONCAT(year(car_year), '-', month(car_year), '-', day(car_year)) as car_year, car_mileage, fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS client_name, car_vin_code, car_type_sale, b_name, mc_status from car left join brand_model using(ID_brand_model) left join color using(ID_color) left join body using(ID_body) left join interior_material using(ID_material) left join transmission using(ID_kpp) left join fuel using(ID_fuel) left join client using(ID_client) left join branch on ID_motor_show = ID_branch left join `check` using(ID_car) where ID_car like @searchParameter or car_name like @searchParameter or brand like @searchParameter or model like @searchParameter or car_class like @searchParameter or color like @searchParameter or body like @searchParameter or material like @searchParameter or kpp like @searchParameter or cp_price like @searchParameter or year(car_year) like @searchParameter or fuel like @searchParameter or car_engine like @searchParameter or car_drive like @searchParameter or car_discs like @searchParameter or car_rubber like @searchParameter or CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) like @searchParameter or car_vin_code like @searchParameter or car_type_sale like @searchParameter or b_name like @searchParameter or mc_status like @searchParameter group by 1, 10 order by mc_status";
            cmd = DBHelper.RunQuerySearch(query, s);
            List<Cars> cr = new List<Cars>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_car"]);
                    string name = dr["car_name"].ToString();
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    string clas = dr["car_class"].ToString();
                    string color = dr["color"].ToString();
                    string body = dr["body"].ToString();
                    string material = dr["material"].ToString();
                    string kpp = dr["kpp"].ToString();
                    long price = Convert.ToInt64(dr["cp_price"]);
                    string year = dr["car_year"].ToString();
                    long mileage = Convert.ToInt64(dr["car_mileage"]);
                    string fuel = dr["fuel"].ToString();
                    string engine = dr["car_engine"].ToString();
                    string drive = dr["car_drive"].ToString();
                    int seats = Convert.ToInt32(dr["car_seats"]);
                    string heat = dr["car_heat"].ToString();
                    string air_cond = dr["car_air_cond"].ToString();
                    string discs = dr["car_discs"].ToString();
                    string rubber = dr["car_rubber"].ToString();
                    string owner = dr["client_name"].ToString();
                    string vin_code = dr["car_vin_code"].ToString();
                    string type_sale = dr["car_type_sale"].ToString();
                    string motor_show = dr["b_name"].ToString();
                    string status = dr["mc_status"].ToString();
                    Cars carr = new Cars(id, name, brand, model, clas, color, body, material, kpp, price, year, mileage, fuel, engine, drive, seats, heat, air_cond, discs, rubber, owner, vin_code, type_sale, motor_show, status);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static List<Cars> DeleteCar(string s)
        {
            string query = "DELETE FROM company_db.check where ID_car = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            query = "DELETE FROM sale where ID_car = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            query = "DELETE FROM car where ID_car = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, s);
            List<Cars> cr = new List<Cars>();
            cr = CarsDA.RetrieveCars();
            return cr;
        }

        public static void AddCar(string par1, string par2, string par3, string par4, string par5, string par6, string par7, string par8, string par9, string par10, string par11,
            string par12, string par13, string par14, string par15, string par16, string par17, string par18, string par19, string par20, string par21, string par22, string par23, string par24)
        {
            string query;
            if (par1 == "false")
            {
                query = "INSERT INTO Car (car_name, ID_brand_model, car_class, ID_color, ID_body, ID_material, ID_kpp, car_year, car_mileage, ID_fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, ID_client, car_vin_code, car_type_sale, ID_motor_show, mc_status) VALUES ( @parameter2, @parameter3, @parameter4, @parameter5, @parameter6, @parameter7, @parameter8, @parameter9, @parameter10, @parameter11, @parameter12, @parameter13, @parameter14, @parameter15, @parameter16, @parameter17, @parameter18, @parameter19, @parameter20, @parameter21, @parameter22, @parameter23)";
            }
            else
            {
                query = "INSERT INTO Car (ID_car, car_name, ID_brand_model, car_class, ID_color, ID_body, ID_material, ID_kpp, car_year, car_mileage, ID_fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, ID_client, car_vin_code, car_type_sale, ID_motor_show, mc_status) VALUES (@parameter1, @parameter2, @parameter3, @parameter4, @parameter5, @parameter6, @parameter7, @parameter8, @parameter9, @parameter10, @parameter11, @parameter12, @parameter13, @parameter14, @parameter15, @parameter16, @parameter17, @parameter18, @parameter19, @parameter20, @parameter21, @parameter22, @parameter23)";
            }
            
            List<Brands_models> a1 = Brands_modelsDA.RetrieveBrands_models();
            foreach (Brands_models c in a1)
            {
                if (par3 == c.Brand && par4 == c.Model)
                {
                    par3 = c.Id.ToString();
                }
            }
            List<Classes.Colors> a2 = ColorsDA.RetrieveColors();
            foreach (Classes.Colors c in a2)
            {
                if (par6 == c.Name)
                {
                    par6= c.Id.ToString();
                }
            }
            List<Bodys> a3 = BodysDA.RetrieveBodys();
            foreach (Bodys c in a3)
            {
                if (par7 == c.Name)
                {
                    par7 = c.Id.ToString();
                }
            }
            List<Materials> a4 = MaterialsDA.RetrieveMaterials();
            foreach (Materials c in a4)
            {
                if (par8 == c.Name)
                {
                    par8 = c.Id.ToString();
                }
            }
            List<Transmissions> a5 = TransmissionsDA.RetrieveTransmissions();
            foreach (Transmissions c in a5)
            {
                if (par9 == c.Name)
                {
                    par9 = c.Id.ToString();
                }
            }
            List<Fuels> a6 = FuelsDA.RetrieveFuels();
            foreach (Fuels c in a6)
            {
                if (par12 == c.Name)
                {
                    par12 = c.Id.ToString();
                }
            }
            List<Clients> a7 = ClientsDA.RetrieveClients();
            foreach (Clients c in a7)
            {
                if (par20 == c.Full_name)
                {
                    par20 = c.Id.ToString();
                }
            }
            List<Branches> a8 = BranchesDA.RetrieveMotor_shows();
            foreach (Branches c in a8)
            {
                if (par23 == c.Name)
                {
                    par23 = c.Id.ToString();
                }
            }

            cmd = DBHelper.RunQueryWithParameters(query, par1, par2, par3, par5, par6, par7, par8, par9, par10, par11, par12, par13, par14, par15, par16, par17, par18, par19, par20, par21, par22, par23, par24);
        }
        public static void UpdateCar(string par1, string par2, string par3, string par4, string par5, string par6, string par7, string par8, string par9, string par10, string par11,
            string par12, string par13, string par14, string par15, string par16, string par17, string par18, string par19, string par20, string par21, string par22, string par23, string par24)
        {
            string query = "UPDATE company_db.car SET car_name = @parameter2, ID_brand_model = @parameter3, car_class = @parameter4, ID_color = @parameter5, ID_body = @parameter6, ID_material = @parameter7, ID_kpp = @parameter8, car_year = @parameter9, car_mileage = @parameter10, ID_fuel = @parameter11, car_engine = @parameter12, car_drive = @parameter13, car_seats = @parameter14, car_heat = @parameter15, car_air_cond = @parameter16, car_discs = @parameter17, car_rubber = @parameter18, ID_client = @parameter19, car_vin_code = @parameter20, car_type_sale = @parameter21, ID_motor_show = @parameter22, mc_status = @parameter23 WHERE ID_car = @parameter1";
            

            List<Brands_models> a1 = Brands_modelsDA.RetrieveBrands_models();
            foreach (Brands_models c in a1)
            {
                if (par3 == c.Brand && par4 == c.Model)
                {
                    par3 = c.Id.ToString();
                }
            }
            List<Classes.Colors> a2 = ColorsDA.RetrieveColors();
            foreach (Classes.Colors c in a2)
            {
                if (par6 == c.Name)
                {
                    par6 = c.Id.ToString();
                }
            }
            List<Bodys> a3 = BodysDA.RetrieveBodys();
            foreach (Bodys c in a3)
            {
                if (par7 == c.Name)
                {
                    par7 = c.Id.ToString();
                }
            }
            List<Materials> a4 = MaterialsDA.RetrieveMaterials();
            foreach (Materials c in a4)
            {
                if (par8 == c.Name)
                {
                    par8 = c.Id.ToString();
                }
            }
            List<Transmissions> a5 = TransmissionsDA.RetrieveTransmissions();
            foreach (Transmissions c in a5)
            {
                if (par9 == c.Name)
                {
                    par9 = c.Id.ToString();
                }
            }
            List<Fuels> a6 = FuelsDA.RetrieveFuels();
            foreach (Fuels c in a6)
            {
                if (par12 == c.Name)
                {
                    par12 = c.Id.ToString();
                }
            }
            List<Clients> a7 = ClientsDA.RetrieveClients();
            foreach (Clients c in a7)
            {
                if (par20 == c.Full_name)
                {
                    par20 = c.Id.ToString();
                }
            }
            List<Branches> a8 = BranchesDA.RetrieveMotor_shows();
            foreach (Branches c in a8)
            {
                if (par23 == c.Name)
                {
                    par23 = c.Id.ToString();
                }
            }

            cmd = DBHelper.RunQueryWithParameters(query, par1, par2, par3, par5, par6, par7, par8, par9, par10, par11, par12, par13, par14, par15, par16, par17, par18, par19, par20, par21, par22, par23, par24);
        }
        public static void UpdateCarStatus(string par1, string par2)
        {
            string query = "UPDATE company_db.car SET mc_status = @parameter2 WHERE ID_car = @parameter1";
            cmd = DBHelper.RunQueryWithParameters(query, par1, par2);
        }
        public static List<Cars> RetrieveCarsWithClientId(string Id)
        {
            string query = "Select ID_car, car_name, brand, model, car_class, color, body, material, kpp, ifnull(cp_price,0) as cp_price, CONCAT(year(car_year), '-', month(car_year), '-', day(car_year)) as car_year, car_mileage, fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS client_name , car_vin_code, car_type_sale, b_name, mc_status from car left join brand_model using(ID_brand_model) left join color using(ID_color) left join body using(ID_body) left join interior_material using(ID_material) left join transmission using(ID_kpp) left join fuel using(ID_fuel) left join client using(ID_client) left join branch on ID_motor_show = ID_branch left join `check` using(ID_car) where ID_client = @parameter1 group by 1, 10 order by mc_status";
            cmd = DBHelper.RunQueryWithParameters(query, Id);
            List<Cars> cr = new List<Cars>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_car"]);
                    string name = dr["car_name"].ToString();
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    string clas = dr["car_class"].ToString();
                    string color = dr["color"].ToString();
                    string body = dr["body"].ToString();
                    string material = dr["material"].ToString();
                    string kpp = dr["kpp"].ToString();
                    long price = Convert.ToInt64(dr["cp_price"]);
                    string year = dr["car_year"].ToString();
                    long mileage = Convert.ToInt64(dr["car_mileage"]);
                    string fuel = dr["fuel"].ToString();
                    string engine = dr["car_engine"].ToString();
                    string drive = dr["car_drive"].ToString();
                    int seats = Convert.ToInt32(dr["car_seats"]);
                    string heat = dr["car_heat"].ToString();
                    string air_cond = dr["car_air_cond"].ToString();
                    string discs = dr["car_discs"].ToString();
                    string rubber = dr["car_rubber"].ToString();
                    string owner = dr["client_name"].ToString();
                    string vin_code = dr["car_vin_code"].ToString();
                    string type_sale = dr["car_type_sale"].ToString();
                    string motor_show = dr["b_name"].ToString();
                    string status = dr["mc_status"].ToString();
                    Cars carr = new Cars(id, name, brand, model, clas, color, body, material, kpp, price, year, mileage, fuel, engine, drive, seats, heat, air_cond, discs, rubber, owner, vin_code, type_sale, motor_show, status);
                    cr.Add(carr);
                }
            }
            return cr;
        }
        public static Cars RetrieveCarsWithCarId(string carId)
        {
            string query = "Select ID_car, car_name, brand, model, car_class, color, body, material, kpp, ifnull(cp_price,0) as cp_price, CONCAT(year(car_year), '-', month(car_year), '-', day(car_year)) as car_year, car_mileage, fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS client_name , car_vin_code, car_type_sale, b_name, mc_status from car left join brand_model using(ID_brand_model) left join color using(ID_color) left join body using(ID_body) left join interior_material using(ID_material) left join transmission using(ID_kpp) left join fuel using(ID_fuel) left join client using(ID_client) left join branch on ID_motor_show = ID_branch left join `check` using(ID_car) where ID_car = @parameter1 group by 1, 10";
            cmd = DBHelper.RunQueryWithParameters(query, carId);
            Cars cr = new Cars(0, "", "", "", "", "", "", "", "", 0, "", 0, "", "", "", 0, "", "", "", "", "", "", "", "", "");
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_car"]);
                    string name = dr["car_name"].ToString();
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    string clas = dr["car_class"].ToString();
                    string color = dr["color"].ToString();
                    string body = dr["body"].ToString();
                    string material = dr["material"].ToString();
                    string kpp = dr["kpp"].ToString();
                    long price = Convert.ToInt64(dr["cp_price"]);
                    string year = dr["car_year"].ToString();
                    long mileage = Convert.ToInt64(dr["car_mileage"]);
                    string fuel = dr["fuel"].ToString();
                    string engine = dr["car_engine"].ToString();
                    string drive = dr["car_drive"].ToString();
                    int seats = Convert.ToInt32(dr["car_seats"]);
                    string heat = dr["car_heat"].ToString();
                    string air_cond = dr["car_air_cond"].ToString();
                    string discs = dr["car_discs"].ToString();
                    string rubber = dr["car_rubber"].ToString();
                    string owner = dr["client_name"].ToString();
                    string vin_code = dr["car_vin_code"].ToString();
                    string type_sale = dr["car_type_sale"].ToString();
                    string motor_show = dr["b_name"].ToString();
                    string status = dr["mc_status"].ToString();
                    MessageBox.Show(id.ToString());
                    MessageBox.Show(name);
                    cr = new Cars(id, name, brand, model, clas, color, body, material, kpp, price, year, mileage, fuel, engine, drive, seats, heat, air_cond, discs, rubber, owner, vin_code, type_sale, motor_show, status);
                }
            }
            return cr;
        }
        public static Cars RetrieveCarsWithCarVinCode(string carId)
        {
            string query = "Select ID_car, car_name, brand, model, car_class, color, body, material, kpp, ifnull(cp_price,0) as cp_price, CONCAT(year(car_year), '-', month(car_year), '-', day(car_year)) as car_year, car_mileage, fuel, car_engine, car_drive, car_seats, car_heat, car_air_cond, car_discs, car_rubber, CONCAT(cl_first_name, ' ',cl_middle_name,' ',cl_last_name) AS client_name , car_vin_code, car_type_sale, b_name, mc_status from car left join brand_model using(ID_brand_model) left join color using(ID_color) left join body using(ID_body) left join interior_material using(ID_material) left join transmission using(ID_kpp) left join fuel using(ID_fuel) left join client using(ID_client) left join branch on ID_motor_show = ID_branch left join `check` using(ID_car) where car_vin_code = @parameter1 group by 1, 10";
            cmd = DBHelper.RunQueryWithParameters(query, carId);
            Cars cr = new Cars(0, "", "", "", "", "", "", "", "", 0, "", 0, "", "", "", 0, "", "", "", "", "", "", "", "", "");
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID_car"]);
                    string name = dr["car_name"].ToString();
                    string brand = dr["brand"].ToString();
                    string model = dr["model"].ToString();
                    string clas = dr["car_class"].ToString();
                    string color = dr["color"].ToString();
                    string body = dr["body"].ToString();
                    string material = dr["material"].ToString();
                    string kpp = dr["kpp"].ToString();
                    long price = Convert.ToInt64(dr["cp_price"]);
                    string year = dr["car_year"].ToString();
                    long mileage = Convert.ToInt64(dr["car_mileage"]);
                    string fuel = dr["fuel"].ToString();
                    string engine = dr["car_engine"].ToString();
                    string drive = dr["car_drive"].ToString();
                    int seats = Convert.ToInt32(dr["car_seats"]);
                    string heat = dr["car_heat"].ToString();
                    string air_cond = dr["car_air_cond"].ToString();
                    string discs = dr["car_discs"].ToString();
                    string rubber = dr["car_rubber"].ToString();
                    string owner = dr["client_name"].ToString();
                    string vin_code = dr["car_vin_code"].ToString();
                    string type_sale = dr["car_type_sale"].ToString();
                    string motor_show = dr["b_name"].ToString();
                    string status = dr["mc_status"].ToString();
                    MessageBox.Show(id.ToString());
                    MessageBox.Show(name);
                    cr = new Cars(id, name, brand, model, clas, color, body, material, kpp, price, year, mileage, fuel, engine, drive, seats, heat, air_cond, discs, rubber, owner, vin_code, type_sale, motor_show, status);
                }
            }
            return cr;
        }
    }
}
