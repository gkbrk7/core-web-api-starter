using Microsoft.AspNetCore.Mvc;
using my_books_api.Data.Services;
using my_books_api.Data.ViewModels;
using System.Threading.Tasks;

namespace my_books_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] BookVM book)
        {
            await _booksService.AddBookAsync(book);
            return Ok();
        }

        [HttpGet("get-all-books")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _booksService.GetAllBooksAsync());
        }

        [HttpGet("get-book-by-id/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            return Ok(await _booksService.GetBookByIdAsync(id));
        }

        [HttpPut("update-book-by-id/{id}")]
        public async Task<IActionResult> UpdateBookById(int id, [FromBody] BookVM book)
        {
            return Ok(await _booksService.UpdateBookById(id, book));
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public async Task<IActionResult> DeleteBookById(int id)
        {
            await _booksService.DeleteBookById(id);
            return Ok();
        }
    }
}