using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentManagementAPI.Controllers.Models;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ClassModelContext _context;

        public ClassController(ClassModelContext context) 
        { 
            _context = context;
        }

        [HttpGet]  
        public async Task<ActionResult<IEnumerable<ClassModel>>> GetClassLists()
        {
            if (_context.ClassModels == null)
            {
                return NotFound();
            }
            return await _context.ClassModels.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult< ClassModel>> GetClassList(int id)
        {
            if (_context.ClassModels == null)
            {
                return NotFound();
            }

            var classlist = await _context.ClassModels.FindAsync(id);
            if (classlist == null)
            {
                return NotFound();
            }
            return classlist;
        }

        [HttpPost]
        public async Task<ActionResult<ClassModel>> PostClassList(ClassModel model)
        {
            _context.ClassModels.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult<ClassModel>> PutClassList(int id, ClassModel model)
        {
            if(id!= model.Id)
            {
                return BadRequest();
            }
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!ClassAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClassList(int id)
        {
            if (_context.ClassModels == null)
            {
                return NotFound();
            }

            var classlist = await _context.ClassModels.FindAsync(id);
            if (classlist == null)
            {
                return NotFound();
            }

            _context.ClassModels.Remove(classlist);
            await _context.SaveChangesAsync();

            return Ok();
        }
        private bool ClassAvailable(int id)
        {
            return (_context.ClassModels?.Any(x => x.Id == id)).GetValueOrDefault();
        }
    }
}
