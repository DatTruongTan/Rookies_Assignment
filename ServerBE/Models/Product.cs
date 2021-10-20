using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Models
{
    public class Product
    {
        //[Key]
        public string Id { get; set; }

        //[Required(ErrorMessage = "Please enter product's name")]
        //[Display(Name = "Product Name")]
        //[StringLength(100)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter product's price")]
        //[Display(Name = "Product Price")]
        public int Price { get; set; }

        //[Required(ErrorMessage = "Please enter product's brand")]
        //[Display(Name = "Product Brand")]
        //[StringLength(100)]
        public int Brand { get; set; }

        //[Required(ErrorMessage = "Please choose gender suitable with product")]
        //[Display(Name = "Gender")]
        public int Gender { get; set; }
        
        //[Required(ErrorMessage = "Please enter product's size")]
        //[Display(Name = "Size")]
        public int Size { get; set; }

        public int Rating { get; set; }

        //[Required(ErrorMessage = "Please enter product's name")]
        //[Display(Name = "Image")]
        public string ImagePath { get; set; }

        public bool IsDeleted { get; set; }
    }
}
