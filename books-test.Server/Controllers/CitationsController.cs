using books_test.Server.Data;
using books_test.Server.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books_test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitationsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CitationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citation>>> GetCitations()
        {
            return await _context.Citations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Citation>> GetCitation(int id)
        {
            var citation = await _context.Citations.FindAsync(id);
            if (citation == null)
            {
                return NotFound();
            }
            return citation;
        }

        [HttpPost]
        public async Task<ActionResult<Citation>> AddCitation(Citation citation)
        {
            _context.Citations.Add(citation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCitation), new { id = citation.Id }, citation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCitation(int id, Citation citation)
        {
            if (id != citation.Id)
            {
                return BadRequest();
            }

            _context.Entry(citation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitationExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitation(int id)
        {
            var citation = await _context.Citations.FindAsync(id);
            if (citation == null)
                return NotFound();

            _context.Citations.Remove(citation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitationExists(int id) => _context.Citations.Any(e => e.Id != 0 && e.Id == id);
    }
}
