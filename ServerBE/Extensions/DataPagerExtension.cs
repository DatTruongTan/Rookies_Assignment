using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dto;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace ServerBE.Extensions
{
    public static class DataPagerExtension
    {
        public static async Task<PagedModelDto<Tmodel>> PaginateAsync<Tmodel>(
            this IQueryable<Tmodel> query,
            BaseQueryCriteriaDto criteriaDto,
            CancellationToken cancellationToken
            )
            where Tmodel : class
        {
            var paged = new PagedModelDto<Tmodel>();

            paged.CurrentPage = (criteriaDto.Page < 0) ? 1 : criteriaDto.Page;
            paged.PageSize = criteriaDto.Limit;

            if (!string.IsNullOrEmpty(criteriaDto.SortOrder.ToString()) &&
                !string.IsNullOrEmpty(criteriaDto.SortColumn))
            {
                var sortOrder = criteriaDto.SortOrder == SortingEnum.Accsending ?
                                                            ConstSortingPage.ASC :
                                                            ConstSortingPage.DESC;
                var orderString = $"{criteriaDto.SortColumn} {sortOrder}";
                query = query.OrderBy(orderString);
            }

            var startRow = (paged.CurrentPage - 1) * paged.PageSize;

            paged.Items = await query
                                    .Skip(startRow)
                                    .Take(paged.PageSize)
                                    .ToListAsync(cancellationToken);

            paged.TotalItems = await query.CountAsync(cancellationToken);
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)paged.PageSize);

            return paged;

        }
    }
}
