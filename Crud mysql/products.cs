using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_mysql
{
    internal class products
    {
        public string Name { get; set; }
        public string Quantity { get; set; }


        public products(string name, string quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
