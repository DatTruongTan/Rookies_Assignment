using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerFE.Services;
using CustomerFE.ViewModel;
using CustomerFE.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace CustomerFE.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DetailsModel(IProductService productService,
            IConfiguration configuration,
            IMapper mapper)
        {
            _productService = productService;
            _configuration = configuration;
            _mapper = mapper;
        }
        public ProductViewModel Products { get; set; }
        public async Task OnGetAsync(string id)
        {
            var productDetail = await _productService.GetProductByIdAsync(id);
            Products = _mapper.Map<ProductViewModel>(productDetail);
        }
    }
}
