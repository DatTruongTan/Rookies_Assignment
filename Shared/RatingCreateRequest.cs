using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class RatingCreateRequest
    {
        public string ProductId { get; set; }
        public string TextComment { get; set; }
        public int RatingIndex { get; set; }
        public string UserId { get; set; }
    }
}
