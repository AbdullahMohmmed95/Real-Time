using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiItem>>> Get() =>
            await _context.ApiItems.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiItem>> Get(int id)
        {
            var item = await _context.ApiItems.FindAsync(id);
            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<ApiItem>> Post(ApiItem item)
        {
            _context.ApiItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ApiItem item)
        {
            if (id != item.Id) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ApiItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.ApiItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

