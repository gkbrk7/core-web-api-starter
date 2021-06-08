using System;
using Microsoft.AspNetCore.Mvc;

namespace my_books_api.Controllers.v1
{
    [ApiVersion("1.0")] // To specify api version send https://....?api-version=1.0 as a querystring
    [ApiVersion("1.1")]
    [Route("api/[controller]")]
    // [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult GetV1()
        {
            return Ok("This is a test controller V1.0");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.1")] // to use versioning mapping do header based configuration in startup first and when sending request, add header to execute specified version of api.
        public IActionResult GetV11()
        {
            return Ok("This is a test controller V1.1");
        }
    }
}
