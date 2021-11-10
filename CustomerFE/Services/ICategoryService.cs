using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dto.Category;

namespace CustomerFE.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(string id);
    }
}
