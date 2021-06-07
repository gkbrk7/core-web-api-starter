using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using my_books_api.Data.Models;
using my_books_api.Data.ViewModels;

namespace my_books_api.Data.Services
{
    public class BooksService
    {
        private readonly AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddBookWithAuthorsAsync(BookVM book)
        {
            var _book = new Book
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };

            await _context.Books.AddAsync(_book);
            await _context.SaveChangesAsync();

            foreach (var id in book.AuthorIds)
            {
                var _book_author = new BookAuthor
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                await _context.BookAuthors.AddAsync(_book_author);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync() => await _context.Books.ToListAsync();

        public async Task<Book> GetBookByIdAsync(int bookId) => await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
        public async Task<Book> UpdateBookById(int bookId, BookVM book)
        {
            var _book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
            if (book != null)
            {
                _book.CoverUrl = book.CoverUrl;
                _book.DateAdded = DateTime.Now;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Description = book.Description;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.Title = book.Title;
                _book.IsRead = book.IsRead;

                await _context.SaveChangesAsync();
            }

            return _book;
        }

        public async Task DeleteBookById(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}