using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class PagedModelDto<Tmodel>
    {
        const int MaxPageSize = 50;
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Tmodel> Items { get; set; }

        public PagedModelDto()
        {
            Items = new List<Tmodel>();
        }

    }
}
