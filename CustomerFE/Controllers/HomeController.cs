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

namespace CustomerFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        ProductAPI _api = new ProductAPI();
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Products");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(result);
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
