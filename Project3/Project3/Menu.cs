using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    class Menu
    {
        public Menu(string line)
        {
            var split = line.Split(',');
            menuID = Convert.ToInt32(split[0]);
            menuName = split[1];
            menuPrice = Convert.ToDouble(split[2]);
        }

        public int menuID { get; set; }
        public double menuPrice { get; set; }
        public string menuName { get; set; }
    }
}