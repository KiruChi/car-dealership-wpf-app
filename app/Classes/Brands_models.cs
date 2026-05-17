using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Classes
{
    public class Brands_models
    {
        private int id;
        private string brand;
        private string model;

        public Brands_models(int id, string brand, string model)
        {
            Id = id;
            Brand = brand;
            Model = model;
        }

        public int Id { get => id; set => id = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
    }
}
