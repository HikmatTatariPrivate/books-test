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
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public BooksController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var username = User.Identity?.Name; 
            return await _context.Books.Where(b => b.UserUsername == username).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var username = User.Identity?.Name; 

            var book = await _context.Books
                .Where(b => b.Id == id && b.UserUsername == username)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return Unauthorized();
            }

            book.UserUsername = username;

            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }


            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized(); // no logged-in user

            if (id != book.Id)
                return BadRequest();

            // Check if the book exists and belongs to the current user
            var existingBook = await _context.Books
                .Where(b => b.Id == id && b.UserUsername == username)
                .FirstOrDefaultAsync();

            if (existingBook == null)
                return NotFound(); // either book doesn't exist or doesn't belong to this user

            // Update only allowed fields
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Price = book.Price;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id)) // make sure this also checks username
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            // Only delete if the book belongs to the logged-in user
            var book = await _context.Books
                .Where(b => b.Id == id && b.UserUsername == username)
                .FirstOrDefaultAsync();

            if (book == null)
                return NotFound(); // book doesn't exist OR doesn't belong to user

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            var username = User.Identity?.Name; // get logged-in username
            if (string.IsNullOrEmpty(username))
                return false; // no user logged in, treat as not found

            return _context.Books.Any(b => b.Id == id && b.UserUsername == username);
        }
    }

}