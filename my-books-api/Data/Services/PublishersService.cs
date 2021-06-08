using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_books_api.Data.Models;
using my_books_api.Data.Paging;
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

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var publisherData = _context.Publishers.Where(x => x.Id == publisherId).Select(x => new PublisherWithBooksAndAuthorsVM
            {
                Name = x.Name,
                BookAuthors = x.Books.Select(x => new BookAuthorVM
                {
                    BookName = x.Title,
                    BookAuthors = x.BookAuthors.Select(x => x.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();
            return publisherData;
        }

        public async Task DeletePublisherById(int id)
        {
            var publisher = _context.Publishers.Find(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }
        }

        public PublisherVM GetPublisherById(int id)
        {
            return _context.Publishers.Where(x => x.Id == id).Select(x => new PublisherVM
            {
                Name = x.Name
            }).FirstOrDefault();
        }

        public IEnumerable<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var publishers = _context.Publishers.ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        publishers = publishers.OrderByDescending(x => x.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                publishers = publishers.Where(x => x.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // Paging
            int pageSize = 5;
            publishers = PaginatedList<Publisher>.Create(publishers.AsQueryable(), pageNumber ?? 1, pageSize);


            return publishers;
        }
    }
}
