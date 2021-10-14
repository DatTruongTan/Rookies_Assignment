using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerFE.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Gender { get; set; }
        public int Size { get; set; }
    }
}
