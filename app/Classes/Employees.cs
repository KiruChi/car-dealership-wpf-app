using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Classes
{
    public class Employees
    {
        private int id;
        private string full_name;
        private string phone;
        private string email;
        private string sex;
        private string birthday;
        private string post;
        private string address;
        private long passport;
        private long RNOCPP;
        private string motor_show;

        public Employees(int id, string full_name, string phone, string email, string sex, string birthday, string post, string address, long passport, long rNOCPP1, string motor_show)
        {
            Id = id;
            Full_name = full_name;
            Phone = phone;
            Email = email;
            Sex = sex;
            Birthday = birthday;
            Post = post;
            Address = address;
            Passport = passport;
            RNOCPP1 = rNOCPP1;
            Motor_show = motor_show;
        }

        public int Id { get => id; set => id = value; }
        public string Full_name { get => full_name; set => full_name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string Sex { get => sex; set => sex = value; }
        public string Birthday { get => birthday; set => birthday = value; }
        public string Post { get => post; set => post = value; }
        public string Address { get => address; set => address = value; }
        public long Passport { get => passport; set => passport = value; }
        public long RNOCPP1 { get => RNOCPP; set => RNOCPP = value; }
        public string Motor_show { get => motor_show; set => motor_show = value; }
    }
}
