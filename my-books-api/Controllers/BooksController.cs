using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using my_books_api.Data.Services;
using my_books_api.Data.ViewModels;

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
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _booksService.AddBook(book);
            return Ok();
        }
    }
}