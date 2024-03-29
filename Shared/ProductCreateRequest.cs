﻿using Microsoft.AspNetCore.Http;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared
{
    public class ProductCreateRequest
    {
        [Required]
        public string Name { get; set; }
        public int Price { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public GenderEnum Gender { get; set; }
        public SizeEnum Size { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
