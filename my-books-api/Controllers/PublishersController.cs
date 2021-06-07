using Microsoft.AspNetCore.Mvc;
using my_books_api.Data.Services;
using my_books_api.Data.ViewModels;
using System.Threading.Tasks;

namespace my_books_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService _publishersService;
        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpPost("add-publisher")]
        public async Task<IActionResult> AddPublisher([FromBody] PublisherVM publisher)
        {
            await _publishersService.AddPublisherAsync(publisher);
            return Ok();
        }

        [HttpGet("get-all-publishers")]
        public async Task<IActionResult> GetAllPublishers()
        {
            return Ok(null);
        }

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