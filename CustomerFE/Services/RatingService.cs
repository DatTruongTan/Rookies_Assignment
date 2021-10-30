using CustomerFE.ViewModel.Rating;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServerBE.Data;
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

            //var request = await client
            //    .PostAsync($"https://localhost:44373/api/Ratings", ratingDto);

            //HttpClient client = new HttpClient();

            var jsonString = JsonConvert.SerializeObject(ratingDto);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(new Uri("https://localhost:44373/api/Ratings"), content);

            string resultContent = result.Content.ReadAsStringAsync().Result;
            return resultContent;
            //var pagedProducts = await response.Content.ReadAsAsync<PagedResponseDto<ProductDto>>();
            //return result;
        }

        public async Task<List<RatingViewModel>> GetAllAsync()
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);
            
            var response = await client.GetAsync("https://localhost:44373/api/Ratings");

            response.EnsureSuccessStatusCode();
            var ratingProducts = await response.Content.ReadAsAsync<List<RatingViewModel>>();
            return ratingProducts;
        }

        public Task<List<RatingViewModel>> GetAllByProductIdAsync(RatingGetRequest request)
        {
            throw new NotImplementedException();
        }   



        public Task<RatingViewModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
