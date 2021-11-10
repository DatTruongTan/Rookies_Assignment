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
using CustomerFE.ViewModel.Category;
using Shared.Dto.Category;

namespace CustomerFE.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public IndexModel(
            IProductService productService,
            ICategoryService categoryService,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _productService = productService;
            _categoryService = categoryService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public PagedResponseVM<ProductViewModel> Products { get; set; }
        public List<CategoryViewModel> Category { get; set; }
        public async Task OnGetAsync(SortingEnum sortOrder, string currentFilter, string searchString, int? pageIndex, int? limit, string[] types)
        {
            var productCriteriaDto = new ProductCriteriaDto()
            {
                Search = searchString,
                SortOrder = sortOrder,
                SortColumn = currentFilter,
                Page = pageIndex ?? 1,
                Limit = limit ?? 9,
                Types = types
            };
            var pagedProducts = await _productService.GetProductAsync(productCriteriaDto);
            Products = _mapper.Map<PagedResponseVM<ProductViewModel>>(pagedProducts);
            var category = await _categoryService.GetAllAsync();
            Category = _mapper.Map<List<CategoryViewModel>>(category);
        }
    }
}
