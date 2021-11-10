using AutoMapper;
using ServerBE.Helpers;
using ServerBE.Models;
using Shared.Dto.Category;
using Shared.Dto.Product;
using Shared.Dto.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Data.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(src => src.ImagePath,
                           options => options.MapFrom(src => ImageHelper.GetFileUrl(src.ImageName)));
            CreateMap<Rating, RatingDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
