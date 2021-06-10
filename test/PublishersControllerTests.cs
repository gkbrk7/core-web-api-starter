using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using my_books_api.Controllers;
using my_books_api.Data;
using my_books_api.Data.Models;
using my_books_api.Data.Services;
using my_books_api.Data.ViewModels;
using NUnit.Framework;

namespace my_books_tests
{
    public class PublishersControllerTests
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "BookDbControllerTest").Options;
        AppDbContext context;
        PublishersService publisherService;
        PublishersController publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            publisherService = new PublishersService(context);

            publishersController = new PublishersController(publisherService, new NullLogger<PublishersController>());
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

            context.SaveChanges();
        }

        [Test]
        public void HTTPGET_GetAllPublishers_WithSortBySearchStringPageNumber_ReturnOK_Test()
        {
            IActionResult actionResult = publishersController.GetAllPublishers("name_desc", "Publisher", 1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultData.First().Name, Is.EqualTo("Publisher 6"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(6));
            Assert.That(actionResultData.Count, Is.EqualTo(5));

            IActionResult actionResultSecondPage = publishersController.GetAllPublishers("name_desc", "Publisher", 2);
            Assert.That(actionResultSecondPage, Is.TypeOf<OkObjectResult>());

            var actionResultDataSecondPage = (actionResultSecondPage as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultDataSecondPage.First().Name, Is.EqualTo("Publisher 1"));
            Assert.That(actionResultDataSecondPage.First().Id, Is.EqualTo(1));
            Assert.That(actionResultDataSecondPage.Count, Is.EqualTo(1));
        }

        [Test]
        public void HTTPGET_GetPublisherById_ReturnOK_Test()
        {
            int publisherId = 1;

            IActionResult actionResult = publishersController.GetPublisherById(publisherId);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var publisherData = (actionResult as OkObjectResult).Value as PublisherVM;
            Assert.That(publisherData.Name, Is.EqualTo("Publisher 1"));
        }


        [Test]
        public void HTTPGET_GetPublisherById_ReturnNotFound_Test()
        {
            int publisherId = 99;

            IActionResult actionResult = publishersController.GetPublisherById(publisherId);
            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task HTTPPOST_AddPublisher_ReturnsCreated_Test()
        {
            var newPublisherVM = new PublisherVM
            {
                Name = "Gokberk"
            };

            IActionResult actionResult = await publishersController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public async Task HTTPDELETE_DeletePublisherById_Test()
        {
            int publisherId = 6;
            IActionResult actionResult = await publishersController.DeletePublisherById(publisherId);
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}
