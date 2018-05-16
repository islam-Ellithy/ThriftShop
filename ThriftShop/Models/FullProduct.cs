using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftShop.Models
{
    public class FullProduct
    {
        public Product product { get; set; }
        public Brand brand { get; set; }
    }
}
