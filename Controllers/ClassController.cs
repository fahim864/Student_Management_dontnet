using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Controllers.Data;
using StudentManagementAPI.Controllers.Models;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly SchoolContext _context;

        public ClassController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes.Include(c => c.Sections).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var classItem = await _context.Classes.Include(c => c.Sections).FirstOrDefaultAsync(c => c.Id == id);

            if (classItem == null)
            {
                return NotFound();
            }

            return classItem;
        }

        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class classItem)
        {
            classItem.CreatedAt = DateTime.UtcNow;
            classItem.UpdatedAt = DateTime.UtcNow;

            _context.Classes.Add(classItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClass), new { id = classItem.Id }, classItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class classItem)
        {
            if (id != classItem.Id)
            {
                return BadRequest();
            }

            classItem.UpdatedAt = DateTime.UtcNow;
            _context.Entry(classItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classItem = await _context.Classes.FindAsync(id);
            if (classItem == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
