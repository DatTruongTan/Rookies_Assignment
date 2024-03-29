﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public int Gender { get; set; }
        public int Size { get; set; }
        public int Rating { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
