using Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerFE.ViewModel.Product
{
    public class ProductViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Price { get; set; }

        public string CategoryId { get; set; }

        //public int Brand { get; set; }

        public GenderEnum Gender { get; set; }
        
        public int Size { get; set; }
        
        public int Rating { get; set; }
        
        public string ImagePath { get; set; }
    }
}
