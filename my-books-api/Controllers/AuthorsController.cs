using Microsoft.AspNetCore.Mvc;
using my_books_api.Data.Services;
using my_books_api.Data.ViewModels;
using System.Threading.Tasks;

namespace my_books_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("add-author")]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorVM author)
        {
            await _authorsService.AddAuthorAsync(author);
            return Ok();
        }

        // [HttpGet("get-all-books")]
        // public async Task<IActionResult> GetAllAuthors()
        // {
        //     return Ok(await _booksService.GetAllAuthorsAsync());
        // }

        // [HttpGet("get-book-by-id/{id}")]
        // public async Task<IActionResult> GetAuthorById(int id)
        // {
        //     return Ok(await _booksService.GetAuthorByIdAsync(id));
        // }

        // [HttpPut("update-book-by-id/{id}")]
        // public async Task<IActionResult> UpdateAuthorById(int id, [FromBody] AuthorVM book)
        // {
        //     return Ok(await _booksService.UpdateAuthorById(id, book));
        // }

        // [HttpDelete("delete-book-by-id/{id}")]
        // public async Task<IActionResult> DeleteAuthorById(int id)
        // {
        //     await _booksService.DeleteAuthorById(id);
        //     return Ok();
        // }
    }
}