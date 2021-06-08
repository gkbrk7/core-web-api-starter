using System;
using Microsoft.AspNetCore.Mvc;
using my_books_api.Data.Services;

namespace my_books_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogsService _logsService;
        public LogsController(LogsService logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("get-all-logs-from-db")]
        public IActionResult GetAllLogsFromDb()
        {
            try
            {
                var alllogs = _logsService.GetAllLogsFromDb();
                return Ok(alllogs);
            }
            catch (System.Exception)
            {
                return BadRequest("Could not load logs from the database");
            }
        }
    }
}
