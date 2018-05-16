using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftShop.Models
{
    public class Brand
    {
        private ShopStoreContext context;
        public int Id { get; set; }
        public string BrandName { get; set; }
    }
}
