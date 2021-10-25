using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared;
using Shared.Constants;
using Shared.Dto;
using Shared.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CustomerFE.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ProductService(
            IHttpClientFactory clientFactory, 
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResponseDto<ProductDto>> GetProductAsync(ProductCriteriaDto productCriteriaDto)
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);

            var Search = productCriteriaDto.Search;
            var SortOrder = productCriteriaDto.SortOrder;
            var SortColumn = productCriteriaDto.SortColumn;
            var Page = productCriteriaDto.Page;
            var Limit = productCriteriaDto.Limit;
            var Types = productCriteriaDto.Types;
            string typeString = "Types=0";
            foreach(var Type in Types)
            {
                typeString += Type;
            }
            var queryString = $"Search={Search}&SortOrder={SortOrder}&SortColumn={SortColumn}&Limit={Limit}&Page={Page}";
            var response = await client
                .GetAsync($"https://localhost:44373/api/Products?{queryString}");
            //.GetAsync($"{ConstConfiguration.BACK_END_ENDPOINT}/{ConstEndPoint.GET_PRODUCTS}?{queryString}");

            response.EnsureSuccessStatusCode();
            var pagedProducts = await response.Content.ReadAsAsync<PagedResponseDto<ProductDto>>();
            return pagedProducts;
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);
            var response = await client.GetAsync($"{ConstEndPoint.GET_PRODUCTS}\\{id}");
            response.EnsureSuccessStatusCode();
            var Product = await response.Content.ReadAsAsync<ProductDto>();
            return Product;
        }

        public async Task<bool> UpdateProduct(ProductDto product)
        {
            var productCreateRequest = new ProductCreateRequest
            {
                Name = product.Name
            };

            var json = JsonConvert.SerializeObject(productCreateRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);
            var res = await client.PutAsync(
                            $"{ConstEndPoint.GET_PRODUCTS}\\{product.Id}",
                            data);

            res.EnsureSuccessStatusCode();

            return await Task.FromResult(true);

        }
    }
}
