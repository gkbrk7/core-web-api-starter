using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using my_books_api.Data;
using my_books_api.Data.Models;
using my_books_api.Data.Services;
using NUnit.Framework;

namespace my_books_tests
{
    public class PublisherServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "BookDbTest").Options;
        AppDbContext context;
        PublishersService publisherService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            publisherService = new PublishersService(context);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            var publishers = new List<Publisher>{
                new Publisher{
                    Id = 1,
                    Name = "Publisher 1"
                },
                new Publisher{
                    Id = 2,
                    Name = "Publisher 2"
                },
                new Publisher{
                    Id = 3,
                    Name = "Publisher 3"
                },
                new Publisher{
                    Id = 4,
                    Name = "Publisher 4"
                },
                new Publisher{
                    Id = 5,
                    Name = "Publisher 5"
                },
                new Publisher{
                    Id = 6,
                    Name = "Publisher 6"
                },
            };
            context.Publishers.AddRange(publishers);

            var authors = new List<Author>{
                new Author{
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author{
                    Id = 2,
                    FullName = "Author 2"
                },
            };

            context.Authors.AddRange(authors);

            var books = new List<Book>{
                new Book{
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://....",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book{
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://....",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 2
                }
            };

            context.Books.AddRange(books);

            var books_authors = new List<BookAuthor>{
                new BookAuthor{
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new BookAuthor{
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new BookAuthor{
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };

            context.BookAuthors.AddRange(books_authors);

            context.SaveChanges();
        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = publisherService.GetAllPublishers("", "", null);
            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.AreEqual(result.Count(), 5);
        }

        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithPageNumber_Test()
        {
            var result = publisherService.GetAllPublishers("", "", 2);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.AreEqual(result.Count(), 1);
        }

        [Test, Order(2)]
        public void GetAllPublishers_WithNoSortBy_WithSearchString_WithNoPageNumber_Test()
        {
            var result = publisherService.GetAllPublishers("", "3", null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.AreEqual(result.Count(), 1);
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 3"));
        }

        [Test, Order(4)]
        public void GetAllPublishers_WithSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = publisherService.GetAllPublishers("name_desc", "", null);
            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.AreEqual(result.Count(), 5);
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));
        }

        [Test, Order(5)]
        public void GetPublisherById_WithResponse_Test()
        {
            var result = publisherService.GetPublisherById(1);
            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
        }

        [Test, Order(6)]
        public void GetPublisherById_WithoutResponse_Test()
        {
            var result = publisherService.GetPublisherById(99);
            Assert.That(result?.Name, Is.Null);
        }

    }
}