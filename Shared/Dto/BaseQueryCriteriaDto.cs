using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class BaseQueryCriteriaDto
    {
        public string Search { get; set; }
        public SortingEnum SortOrder { get; set; }
        public string SortColumn { get; set; }
        public int Limit { get; set; } = 9;
        public int Page { get; set; } = 1;
    }
}
