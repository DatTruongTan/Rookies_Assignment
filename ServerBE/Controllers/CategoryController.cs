using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerBE.Data;
using ServerBE.Models;
using Shared;
using Shared.Dto.Category;
using Shared.ViewModels;

namespace ServerBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Getcategories()
        {
            return await _context.categories.ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(string id)
        {
            var category = await _context.categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] string id, [FromForm] CategoryCreateRequest categoryCreateRequest)
        {
            var category = await _context.categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryCreateRequest.Name;
            category.Description = categoryCreateRequest.Description;

            _context.categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> PostCategory([FromForm] CategoryCreateRequest categoryCreateRequest)
        {
            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = categoryCreateRequest.Name,
                Description = categoryCreateRequest.Description
            };
            _context.categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Getcategories), new { id = category.Id }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _context.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.categories.Remove(category);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(string id)
        {
            return _context.categories.Any(e => e.Id == id);
        }
    }
}
