using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Controllers.Data;
using StudentManagementAPI.Controllers.Models;


namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly SchoolContext _context;

        public SectionController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            return await _context.Sections.Include(s => s.Class).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(int id)
        {
            var section = await _context.Sections.Include(s => s.Class).FirstOrDefaultAsync(s => s.Id == id);

            if (section == null)
            {
                return NotFound();
            }

            return section;
        }

        [HttpPost]
        public async Task<ActionResult<Section>> PostSection(Section section)
        {
            section.CreatedAt = DateTime.UtcNow;
            section.UpdatedAt = DateTime.UtcNow;

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSection), new { id = section.Id }, section);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(int id, Section section)
        {
            if (id != section.Id)
            {
                return BadRequest();
            }

            section.UpdatedAt = DateTime.UtcNow;
            _context.Entry(section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
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

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
