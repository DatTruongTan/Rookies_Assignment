using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerBE.Data;
using ServerBE.Extensions;
using ServerBE.Models;
using ServerBE.Services;
using Shared;
using Shared.Constants;
using Shared.Dto;
using Shared.Dto.Category;
using Shared.Dto.Product;
using Shared.Enum;
using Shared.ViewModels;

namespace ServerBE.Controllers
{
    //[Authorize("Bearer")]
    //[Authorize(Policy = ConstSecurity.ADMIN_ROLE_POLICY)]
    [EnableCors("AllowOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public ProductsController(
            ApplicationDbContext context,
            IFileStorageService fileStorageService,
            IMapper mapper
            )
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResponseDto<ProductDto>>> Getproduct(
            [FromQuery] ProductCriteriaDto productCriteriaDto,
            CancellationToken cancellationToken)
        {
            var productQuery = _context
                                    .products
                                    .AsQueryable();

            productQuery = ProductFilter(productQuery, productCriteriaDto);

            var pageProducts = await productQuery
                                        .AsNoTracking()
                                        .PaginateAsync(productCriteriaDto, cancellationToken);

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(pageProducts.Items);
            return new PagedResponseDto<ProductDto>
            {
                CurrentPage = pageProducts.CurrentPage,
                TotalPages = pageProducts.TotalPages,
                TotalItems = pageProducts.TotalItems,
                Search = productCriteriaDto.Search,
                SortColumn = productCriteriaDto.SortColumn,
                SortOrder = productCriteriaDto.SortOrder,
                Limit = productCriteriaDto.Limit,
                Items = productDto
            };
        }

        #region Private Method
        private IQueryable<Product> ProductFilter(
            IQueryable<Product> productQuery,
            ProductCriteriaDto productCriteriaDto)
        {
            if (!String.IsNullOrEmpty(productCriteriaDto.Search))
            {
                productQuery = productQuery.Where(b =>
                    b.Name.Contains(productCriteriaDto.Search));
            }

            if (productCriteriaDto.Types != null)
            {
                for (int i = 0; i < productCriteriaDto.Types.Length; i++)
                {
                    if (productCriteriaDto.Types[i] != "0")
                    {
                        productQuery = productQuery.Where(x =>
                        productCriteriaDto.Types.Any(t => t == x.CategoryId));
                    }
                }
            }

            return productQuery;
        }
        #endregion

        // GET: api/Products/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductViewModel>> GetProduct(string id)
        {
            var product = await _context
                                    .products
                                    .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Gender = product.Gender,
                Size = product.Size,
                Rating = product.Rating,
                //ImagePath = _fileStorageService.GetFileUrl(product.ImageName)
                ImagePath = $"//images//{product.ImageName}"

            };
            return productViewModel;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] string id,[FromForm] ProductCreateRequest productCreateRequest)
        {
            var product = await _context.products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productCreateRequest.Name;
            product.Price = productCreateRequest.Price;
            product.CategoryId = productCreateRequest.CategoryId;
            product.Gender = (int)productCreateRequest.Gender;
            product.Size = (int)productCreateRequest.Size;
            product.CreatedDate = DateTime.Now;

            if (productCreateRequest.ImageFile != null)
            {
                product.ImageName = await _fileStorageService.SaveFileAsync(productCreateRequest.ImageFile);
            }

            _context.products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> PostProduct([FromForm] ProductCreateRequest productCreateRequest)
        {
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = productCreateRequest.Name,
                Price = productCreateRequest.Price,
                CategoryId = productCreateRequest.CategoryId,
                Gender = (int)productCreateRequest.Gender,
                Size = (int)productCreateRequest.Size,
                ImageName = string.Empty,
                CreatedDate = DateTime.Now
            };

            if (productCreateRequest.ImageFile != null)
            {
                product.ImageName = await _fileStorageService.SaveFileAsync(productCreateRequest.ImageFile);
            }

            _context.products.Add(product);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), 
                                    new { id = product.Id }, 
                                    product
                                    );
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return _context.products.Any(e => e.Id == id);
        }
    }
}
