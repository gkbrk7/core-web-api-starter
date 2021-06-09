using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using my_books_api.Data;
using my_books_api.Data.Models;
using NUnit.Framework;

namespace my_books_tests
{
    public class PublisherServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "BookDbTest").Options;
        AppDbContext context;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
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

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}