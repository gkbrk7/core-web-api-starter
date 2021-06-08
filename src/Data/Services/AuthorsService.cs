using System;
using System.Linq;
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

        public AuthorWithBooksVM GetAuthorWithBooksVM(int authorId)
        {
            var author = _context.Authors.Where(x => x.Id == authorId).Select(x => new AuthorWithBooksVM
            {
                FullName = x.FullName,
                BookTitles = x.BookAuthors.Select(x => x.Book.Title).ToList()
            }).FirstOrDefault();

            return author;
        }
    }
}
