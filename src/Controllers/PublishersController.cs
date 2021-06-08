using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books_api.ActionResults;
using my_books_api.Data.Models;
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
        private readonly ILogger<PublishersController> _logger;
        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        [HttpPost("add-publisher")]
        public async Task<IActionResult> AddPublisher([FromBody] PublisherVM publisher)
        {
            await _publishersService.AddPublisherAsync(publisher);
            return Ok();
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            return Ok(_publishersService.GetPublisherData(id));
        }

        [HttpDelete("delete-publisher-by-id")]
        public async Task<IActionResult> DeletePublisherById(int id)
        {
            await _publishersService.DeletePublisherById(id);
            return Ok();
        }

        // [HttpGet("get-publisher-by-id/{id}")]
        // // Specific Return Type : It does not give status code 
        // public PublisherVM GetPublisherById(int id)
        // {
        //     var response = _publishersService.GetPublisherById(id);
        //     if (response != null)
        //     {
        //         return response;
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // }

        // [HttpGet("get-publisher-by-id/{id}")]
        // // ActionResult<T> return type
        // public ActionResult<PublisherVM> GetPublisherById(int id)
        // {
        //     var response = _publishersService.GetPublisherById(id);
        //     if (response != null)
        //     {
        //         return response;
        //     }
        //     else
        //     {
        //         return NotFound();
        //     }
        // }

        // [HttpGet("get-publisher-by-id/{id}")]
        // // Custom Action Result
        // public CustomActionResult<PublisherVM> GetPublisherById(int id)
        // {
        //     var response = _publishersService.GetPublisherById(id);
        //     if (response != null)
        //     {
        //         var responseObj = new CustomActionResultVM<PublisherVM>
        //         {
        //             Data = response
        //         };
        //         return new CustomActionResult<PublisherVM>(responseObj);
        //     }
        //     else
        //     {
        //         var responseObj = new CustomActionResultVM<PublisherVM>
        //         {
        //             Exception = new System.Exception("This is coming from publishers controller")
        //         };
        //         return new CustomActionResult<PublisherVM>(responseObj);
        //     }
        // }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var response = _publishersService.GetPublisherById(id);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            //throw new System.Exception("This is an exception from get-all-publishers");
            try
            {
                _logger.LogInformation("This is just a log in GetAllPublishers()");
                var result = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
        }
    }
}