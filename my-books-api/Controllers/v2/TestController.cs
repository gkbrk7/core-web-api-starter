using System;
using Microsoft.AspNetCore.Mvc;

namespace my_books_api.Controllers.v2
{
    [ApiVersion("2.0")] // To specify api version send https://....?api-version=2.0 as a querystring
    // [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")] // URL based versioning
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is a test controller V2");
        }
    }
}
