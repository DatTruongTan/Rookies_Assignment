using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerFE.Services;
using CustomerFE.ViewModel;
using CustomerFE.ViewModel.Category;
using CustomerFE.ViewModel.Product;
using CustomerFE.ViewModel.Rating;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Dto.Rating;

namespace CustomerFE.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IRatingService _ratingService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DetailsModel(
            IProductService productService,
            IRatingService ratingService,
            IConfiguration configuration,
            IMapper mapper)
        {
            _productService = productService;
            _ratingService = ratingService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public ProductViewModel Products { get; set; }
        public List<RatingViewModel> Ratings { get; set; }
        public CategoryViewModel Category { get; set; }
        public async Task OnGetAsync(/*RatingGetRequest request,*/ string id)
        {
            var productDetail = await _productService.GetProductByIdAsync(id);
            Products = _mapper.Map<ProductViewModel>(productDetail);
            var ratings = await _ratingService.GetAllAsync();
            var ratingProducts = ratings.FindAll(x => x.ProductId == productDetail.Id);
            Ratings = _mapper.Map<List<RatingViewModel>>(ratingProducts);
        }

        public async Task OnPostAsync(/*RatingCreateRequest request,*/ int rating, string productId)
        {
            var productDetail = await _productService.GetProductByIdAsync(productId);
            Products = _mapper.Map<ProductViewModel>(productDetail);

            var ratingVM = new RatingDto()
            {
                TextComment = Request.Form["TextComment"],
                RatingIndex = int.Parse(Request.Form["rating"]),
                ProductId = Request.Form["ProductId"]
            };
            var ratings = await _ratingService.CreateAsync(ratingVM);
            var getRatings = await _ratingService.GetAllAsync();
            var ratingProducts = getRatings.FindAll(x => x.ProductId == productDetail.Id);
            Ratings = _mapper.Map<List<RatingViewModel>>(ratingProducts);
        }
    }
}
