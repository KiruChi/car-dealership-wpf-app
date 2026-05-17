using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Classes
{
    public class Statistic
    {
        private string name;
        private int numofsales;

        public Statistic(string name, int numofsales)
        {
            Name = name;
            Numofsales = numofsales;
        }

        public string Name { get => name; set => name = value; }
        public int Numofsales { get => numofsales; set => numofsales = value; }
    }
}
