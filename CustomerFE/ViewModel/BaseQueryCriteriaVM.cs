using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerFE.ViewModel
{
    public class BaseQueryCriteriaVM
    {
        public string Search { get; set; }
        public SortingEnum SortOrder { get; set; }
        public string SortColumn { get; set; }
        public int Limit { get; set; } = 9;
        public int Page { get; set; } = 1;
    }
}
