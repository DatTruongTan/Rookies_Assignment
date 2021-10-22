using CustomerFE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CustomerFE.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using CustomerFE.Services;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using CustomerFE.ViewModel;
using CustomerFE.ViewModel.Product;
using Shared.Enum;
using Shared.Constants;
using Shared.Dto.Product;

namespace CustomerFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public HomeController(
            ILogger<HomeController> logger, 
            IProductService productService,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _logger = logger;
            _productService = productService;
            _configuration = configuration;
            _mapper = mapper;
        }

        //ProductAPI _api = new ProductAPI();
        //public PagedResponseVM<ProductViewModel> Products { get; set; }
        //public async Task<IActionResult> Index()
        public IActionResult Index()

        {
            //List<Product> products = new List<Product>();
            //HttpClient client = _api.Initial();
            //HttpResponseMessage res = await client.GetAsync("api/Products");
            //if (res.IsSuccessStatusCode)
            //{
            //    var result = res.Content.ReadAsStringAsync().Result;
            //    products = JsonConvert.DeserializeObject<List<Product>>(result);
            //}
            //return View(products);
            return View();
        }

        //public async Task<IActionResult> Index(string sortOrder,
        //    string currentFilter, string searchString, int? pageIndex)
        //{
        //    var productCriteriaDto = new ProductCriteriaDto()
        //    {
        //        Search = searchString,
        //        SortOrder = SortingEnum.Accsending,
        //        Page = pageIndex ?? 1,
        //        Limit = int.Parse(_configuration[ConstConfiguration.PAGING_LIMIT])
        //    };
        //    var pagedBrands = await _productService.GetProductAsync(productCriteriaDto);
        //    Products = _mapper.Map<PagedResponseVM<ProductViewModel>>(pagedBrands);

        //    return View(Products);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
