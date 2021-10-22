using AutoMapper;
using CustomerFE.ViewModel;
using CustomerFE.ViewModel.Product;
using Shared.Dto;
using Shared.Dto.Product;
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
            CreateMap<BaseQueryCriteriaDto, BaseQueryCriteriaVM>().ReverseMap();
            CreateMap<PagedResponseDto<ProductDto>, PagedResponseVM<ProductViewModel>>().ReverseMap();
        }
    }
}
