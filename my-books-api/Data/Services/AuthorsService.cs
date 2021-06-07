using System;
using System.Threading.Tasks;
using my_books_api.Data.Models;
using my_books_api.Data.ViewModels;

namespace my_books_api.Data.Services
{
    public class AuthorsService
    {
        private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;

        }

        public async Task AddAuthorAsync(AuthorVM author)
        {
            await _context.Authors.AddAsync(new Author
            {
                FullName = author.FullName
            });

            await _context.SaveChangesAsync();
        }
    }
}
