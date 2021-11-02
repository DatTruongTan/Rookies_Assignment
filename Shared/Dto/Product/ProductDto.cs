using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto.Product
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Brand { get; set; }
        public int Gender { get; set; }
        public int Size { get; set; }
        public int Rating { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
