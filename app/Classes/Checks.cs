using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Classes
{
    public class Checks
    {
        private int id;
        private int carid; 
        private int service_centerid;
        private string date;
        private int check_price;
        private long car_price;

        public Checks(int id, int carid, int service_centerid, string date, int check_price, long car_price)
        {
            Id = id;
            Carid = carid;
            Service_centerid = service_centerid;
            Date = date;
            Check_price = check_price;
            Car_price = car_price;
        }

        public int Id { get => id; set => id = value; }
        public int Carid { get => carid; set => carid = value; }
        public int Service_centerid { get => service_centerid; set => service_centerid = value; }
        public string Date { get => date; set => date = value; }
        public int Check_price { get => check_price; set => check_price = value; }
        public long Car_price { get => car_price; set => car_price = value; }
    }
}
