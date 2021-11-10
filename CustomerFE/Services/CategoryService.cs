using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerFE.ViewModel.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Constants;
using Shared.Dto.Category;

namespace CustomerFE.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public CategoryService(
            IHttpClientFactory clientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);

            var response = await client
                .GetAsync($"{ConstantUri.SERVER_SITE_URL}{ConstEndPoint.GET_CATEGORY}");

            response.EnsureSuccessStatusCode();
            var category = await response.Content.ReadAsAsync<List<CategoryDto>>();
            return category;
        }

        public async Task<CategoryDto> GetByIdAsync(string id)
        {
            var client = _clientFactory.CreateClient(ConstService.BACK_END_NAMED_CLIENT);

            var response = await client
                .GetAsync($"{ConstantUri.SERVER_SITE_URL}{ConstEndPoint.GET_CATEGORY}\\{id}");

            response.EnsureSuccessStatusCode();
            var category = await response.Content.ReadAsAsync<CategoryDto>();
            return category;
        }
    }
}
