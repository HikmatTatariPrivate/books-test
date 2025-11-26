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
            var username = User.Identity?.Name;
            return await _context.Citations
                .Where(c => c.UserUsername == username)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Citation>> GetCitation(int id)
        {
            var username = User.Identity?.Name;

            var citation = await _context.Citations
                .Where(c => c.Id == id && c.UserUsername == username)
                .FirstOrDefaultAsync();

            if (citation == null)
            {
                return NotFound();
            }

            return citation;
        }

        [HttpPost]
        public async Task<ActionResult<Citation>> AddCitation(Citation citation)
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return Unauthorized();
            }

            citation.UserUsername = username;

            try
            {
                _context.Citations.Add(citation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetCitation), new { id = citation.Id }, citation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCitation(int id, Citation citation)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            if (id != citation.Id)
                return BadRequest();

            var existingCitation = await _context.Citations
                .Where(c => c.Id == id && c.UserUsername == username)
                .FirstOrDefaultAsync();

            if (existingCitation == null)
                return NotFound();

            existingCitation.Text = citation.Text;
     
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
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var citation = await _context.Citations
                .Where(c => c.Id == id && c.UserUsername == username)
                .FirstOrDefaultAsync();

            if (citation == null)
                return NotFound();

            _context.Citations.Remove(citation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitationExists(int id)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return false;

            return _context.Citations.Any(c => c.Id == id && c.UserUsername == username);
        }
    }
}
