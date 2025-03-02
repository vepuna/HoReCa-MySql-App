using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_mysql
{
    internal class Products
    {
        public string Name { get; set; }
        public string Quantity { get; set; }

        public Products(string name, string quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}