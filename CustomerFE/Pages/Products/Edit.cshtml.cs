using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerFE.Services;
using CustomerFE.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shared.Dto.Product;

namespace CustomerFE.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public EditModel(
            IProductService brandService,
            IConfiguration config,
            IMapper mapper)
        {
            _productService = brandService;
            _config = config;
            _mapper = mapper;
        }

        [BindProperty]
        public ProductViewModel Brand { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandDto = await _productService.GetProductByIdAsync(id.Value);

            if (brandDto == null)
            {
                return NotFound();
            }
            Brand = _mapper.Map<ProductViewModel>(brandDto);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid || id < 0)
            {
                return NotFound();
            }

            var brandDto = _mapper.Map<ProductDto>(Brand);
            if (await _productService.UpdateProduct(brandDto))
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
