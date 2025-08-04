using Microsoft.AspNetCore.Mvc;
using backend.Models;       // ✅ This is where ApiItem lives
using backend.Data;         // ✅ Your DbContext
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiItem>>> GetTasks()
        {
            var items = await _context.ApiItems.ToListAsync();
            return Ok(items);
        }
    }
}

