using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class PagedResponeDto<Tmodel> : BaseQueryCriteriaDto
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Tmodel> Items { get; set; }
    }
}
