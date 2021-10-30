using AutoMapper;
using CustomerFE.ViewModel;
using CustomerFE.ViewModel.Product;
using CustomerFE.ViewModel.Rating;
using Shared.Dto;
using Shared.Dto.Product;
using Shared.Dto.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerFE.Mapping
{
    public class BrandAutoMapperProfile : Profile
    {
        public BrandAutoMapperProfile()
        {
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
            CreateMap<RatingDto, RatingViewModel>().ReverseMap();
            CreateMap<BaseQueryCriteriaDto, BaseQueryCriteriaVM>().ReverseMap();
            CreateMap<PagedResponseDto<ProductDto>, PagedResponseVM<ProductViewModel>>().ReverseMap();
        }
    }
}
