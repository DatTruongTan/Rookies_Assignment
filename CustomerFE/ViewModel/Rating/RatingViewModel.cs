using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerFE.ViewModel.Rating
{
    public class RatingViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string ProductId { get; set; }
        
        [Required]
        public string TextComment { get; set; }

        [Required]
        public int RatingIndex { get; set; }

        public string UserId { get; set; }
    }
}
