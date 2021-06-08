using System;
using System.Collections.Generic;
using my_books_api.Data.Models;

namespace my_books_api.Data.Services
{
    public class LogsService
    {
        private readonly AppDbContext _context;

        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Log> GetAllLogsFromDb() => _context.Logs;
    }
}
