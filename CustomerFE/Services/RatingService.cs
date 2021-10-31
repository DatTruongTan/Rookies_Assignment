using CustomerFE.ViewModel.Rating;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared;
using Shared.Constants;
using Shared.Dto.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFE.Services
{
    public class RatingService : IRatingService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public RatingService(
            IHttpClientFactory clientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<string> CreateAsync(RatingDto ratingDto)
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);

            var jsonString = JsonConvert.SerializeObject(ratingDto);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(new Uri($"https://localhost:5001/api/Ratings"), content);

            string resultContent = result.Content.ReadAsStringAsync().Result;
            return resultContent;
        }

        public async Task<List<RatingDto>> GetAllAsync()
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);
            
            var response = await client.GetAsync($"{ConstEndPoint.GET_RATINGS}");

            response.EnsureSuccessStatusCode();
            var ratingProducts = await response.Content.ReadAsAsync<List<RatingDto>>();
            return ratingProducts;
        }

        public async Task<List<RatingDto>> GetAllByProductIdAsync(RatingGetRequest request)
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);
            var response = await client.GetAsync($"{ConstEndPoint.GET_RATINGS}");
            response.EnsureSuccessStatusCode();
            var Product = await response.Content.ReadAsAsync<List<RatingDto>>();
            return Product;
        }   

        public async Task<RatingDto> GetByIdAsync(string id)
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);
            var response = await client.GetAsync($"{ConstEndPoint.GET_RATINGS}\\{id}");
            response.EnsureSuccessStatusCode();
            var Product = await response.Content.ReadAsAsync<RatingDto>();
            return Product;
        }
    }
}
