using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Classes
{
    public class Branches
    {
        private int id;
        private string name;
        private string address;
        private string phone;
        private string email;
        private long EDRPOU;
        private string type;

        public Branches(int id, string name, string address, string phone, string email, long eDRPOU1, string type)
        {
            Id = id;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
            EDRPOU1 = eDRPOU1;
            Type = type;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public long EDRPOU1 { get => EDRPOU; set => EDRPOU = value; }
        public string Type { get => type; set => type = value; }
    }
}
