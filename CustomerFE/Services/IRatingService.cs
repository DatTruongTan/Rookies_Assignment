using CustomerFE.ViewModel.Rating;
using Shared;
using Shared.Dto.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerFE.Services
{
    public interface IRatingService
    {
        Task<List<RatingDto>> GetAllAsync();
        Task<List<RatingDto>> GetAllByProductIdAsync(RatingGetRequest request);
        Task<string> CreateAsync(RatingDto ratingDto);
        Task<RatingDto> GetByIdAsync(string id);
    }
}
