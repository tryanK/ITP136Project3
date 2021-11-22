using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    class Orders
    {


        public Orders(string n, int b, int f, int s, int p, double t)
        {
            customerName = n;
            burger = b;
            fries = f;
            shakes = s;
            package = p;
            total = t;

        }

        public string customerName { get; set; }
        public int burger { get; set; }
        public int fries { get; set; }
        public int shakes { get; set; }
        public int package { get; set; }
        public double total { get; set; }
    }
}