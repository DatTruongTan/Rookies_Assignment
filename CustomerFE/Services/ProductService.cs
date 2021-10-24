using Newtonsoft.Json;
using Shared;
using Shared.Constants;
using Shared.Dto;
using Shared.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFE.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<PagedResponseDto<ProductDto>> GetProductAsync(ProductCriteriaDto productCriteriaDto)
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);
            //var client = _clientFactory.CreateClient("ServerBE");

            var response = await client.GetAsync(ConstEndPoint.GET_PRODUCTS);

            //var queryString = $"searchString={productCriteriaDto.Search}";

            //var response = await client.GetAsync($"{ConstEndPoint.GET_PRODUCTS}?{queryString}");
            //var response = await client.GetAsync("api/products");

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
