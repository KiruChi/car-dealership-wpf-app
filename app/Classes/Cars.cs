using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Classes
{
    public class Cars
    {
        private int id;
        private string name;
        private string brand;
        private string model;
        private string clas;
        private string color;
        private string body;
        private string material;
        private string kpp;
        private long price;
        private string year;
        private long mileage;
        private string fuel;
        private string engine;
        private string drive;
        private int seats;
        private string heat;
        private string air_cond;
        private string discs;
        private string rubber;
        private string owner;
        private string vin_code;
        private string type_sale;
        private string motor_show;
        private string status;

        public Cars(int id, string name, string brand, string model, string clas, string color, string body, string material, string kpp, long price, string year, long mileage, string fuel, string engine, string drive, int seats, string heat, string air_cond, string discs, string rubber, string owner, string vin_code, string type_sale, string motor_show, string status)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Model = model;
            Clas = clas;
            Color = color;
            Body = body;
            Material = material;
            Kpp = kpp;
            Price = price;
            Year = year;
            Mileage = mileage;
            Fuel = fuel;
            Engine = engine;
            Drive = drive;
            Seats = seats;
            Heat = heat;
            Air_cond = air_cond;
            Discs = discs;
            Rubber = rubber;
            Owner = owner;
            Vin_code = vin_code;
            Type_sale = type_sale;
            Motor_show = motor_show;
            Status = status;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
        public string Clas { get => clas; set => clas = value; }
        public string Color { get => color; set => color = value; }
        public string Body { get => body; set => body = value; }
        public string Material { get => material; set => material = value; }
        public string Kpp { get => kpp; set => kpp = value; }
        public long Price { get => price; set => price = value; }
        public string Year { get => year; set => year = value; }
        public long Mileage { get => mileage; set => mileage = value; }
        public string Fuel { get => fuel; set => fuel = value; }
        public string Engine { get => engine; set => engine = value; }
        public string Drive { get => drive; set => drive = value; }
        public int Seats { get => seats; set => seats = value; }
        public string Heat { get => heat; set => heat = value; }
        public string Air_cond { get => air_cond; set => air_cond = value; }
        public string Discs { get => discs; set => discs = value; }
        public string Rubber { get => rubber; set => rubber = value; }
        public string Owner { get => owner; set => owner = value; }
        public string Vin_code { get => vin_code; set => vin_code = value; }
        public string Type_sale { get => type_sale; set => type_sale = value; }
        public string Motor_show { get => motor_show; set => motor_show = value; }
        public string Status { get => status; set => status = value; }
    }
}
