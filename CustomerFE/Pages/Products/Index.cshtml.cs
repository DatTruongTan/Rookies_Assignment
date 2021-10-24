using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using CustomerFE.Services;
using CustomerFE.ViewModel;
using CustomerFE.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Constants;
using Shared.Dto.Product;
using Shared.Enum;

namespace CustomerFE.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public IndexModel(
            IProductService productService,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _productService = productService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public PagedResponseVM<ProductViewModel> Products { get; set; }
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            var productCriteriaDto = new ProductCriteriaDto()
            {
                Search = searchString,
                SortOrder = SortingEnum.Accsending,
                Page = pageIndex ?? 1,
                //Limit = int.Parse(_configuration[ConstConfiguration.PAGING_LIMIT])
                Limit = 9
            };
            var pagedProducts = await _productService.GetProductAsync(productCriteriaDto);
            Products = _mapper.Map<PagedResponseVM<ProductViewModel>>(pagedProducts);
        }
    }
}
