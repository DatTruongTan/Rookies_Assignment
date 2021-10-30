using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Models
{
    public class Rating
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string TextComment { get; set; }
        public int RatingIndex { get; set; }
        public string UserId { get; set; }
    }
}
