using System;
using System.Threading.Tasks;
using my_books_api.Data.Models;
using my_books_api.Data.ViewModels;

namespace my_books_api.Data.Services
{
    public class PublishersService
    {
        private readonly AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;

        }

        public async Task AddPublisherAsync(PublisherVM publisher)
        {
            await _context.Publishers.AddAsync(new Publisher
            {
                Name = publisher.Name
            });

            await _context.SaveChangesAsync();
        }
    }
}
