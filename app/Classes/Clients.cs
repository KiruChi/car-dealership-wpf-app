using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Classes
{
    public class Clients
    {
        private int id;
        private string full_name;
        private string phone;
        private string sex;
        private string birthday;
        private string address;
        private long RNOCPP;

        public Clients(int id, string full_name, string phone, string sex, string birthday, string address, long rNOCPP1)
        {
            Id = id;
            Full_name = full_name;
            Phone = phone;
            Sex = sex;
            Birthday = birthday;
            Address = address;
            RNOCPP1 = rNOCPP1;
        }

        public int Id { get => id; set => id = value; }
        public string Full_name { get => full_name; set => full_name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Sex { get => sex; set => sex = value; }
        public string Birthday { get => birthday; set => birthday = value; }
        public string Address { get => address; set => address = value; }
        public long RNOCPP1 { get => RNOCPP; set => RNOCPP = value; }
    }
}
