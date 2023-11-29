using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_mysql
{
    internal class menu
    {
        public string Name { get; set; }
        public string Price { get; set; }


        public menu(string name, string price)
        {
            Name = name;
            Price = price;
        }
    }
}

