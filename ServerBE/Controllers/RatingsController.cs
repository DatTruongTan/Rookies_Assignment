using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerBE.Data;
using ServerBE.Models;
using ServerBE.Services;
using Shared;
using Shared.Dto.Rating;
using Shared.ViewModels;

namespace ServerBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public RatingsController(
            ApplicationDbContext context,
            IFileStorageService fileStorageService,
            IMapper mapper
            )
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<List<RatingDto>> Getratings()
        {
            //var query = _context.products.AsQueryable();

            //var query = from rt in _context.ratings
            //            join p in _context.products on rt.ProductId equals p.Id into prt
            //            from p in prt.DefaultIfEmpty()
            //            select new { p, rt };
            //if (!string.IsNullOrEmpty(request.ProductId))
            //{
            //    query = query.Where(p => p.Id == request.ProductId);
            //}
            //var queryList = await query.ToListAsync();

            var query = from rt in _context.ratings
                        select new { rt };
            var comment = await query.Select(x => new RatingDto()
            {
                Id = x.rt.Id,
                TextComment = x.rt.TextComment,
                RatingIndex = x.rt.RatingIndex,
                ProductId = x.rt.ProductId,
                UserId = ""
            }).ToListAsync();

            return comment;

            //var rating = _mapper.Map<RatingDto>(query);

            //var data = new RatingDto
            //{
            //    Id = rating.Id,
            //    TextComment = rating.TextComment,
            //    RatingIndex = rating.RatingIndex,
            //    ProductId = rating.ProductId
            //};
            //return data;
            //return await _context.ratings.ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(string id)
        {
            var rating = await _context.ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(string id, Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> PostRating([FromBody] RatingCreateRequest request)
        {
            var rating = new Rating
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = request.ProductId,
                TextComment = request.TextComment,
                RatingIndex = request.RatingIndex,
            };
            _context.ratings.Add(rating);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(string id)
        {
            var rating = await _context.ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(string id)
        {
            return _context.ratings.Any(e => e.Id == id);
        }
    }
}
