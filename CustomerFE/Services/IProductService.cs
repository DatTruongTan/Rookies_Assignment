using Shared.Dto;
using Shared.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerFE.Services
{
    public interface IProductService
    {
        Task<PagedResponseDto<ProductDto>> GetProductAsync(ProductCriteriaDto productCriteriaDto);
        Task<ProductDto> GetProductByIdAsync(string id);
        Task<bool> UpdateProduct(ProductDto product);
    }
}
