using Moq;
using StoreLab.ApplicationCore.Interfaces;
using StoreLab.ApplicationCore.Models;
using StoreLab.ApplicationCore.Services;

namespace StoreLab.ApplicationCore.Tests
{
    public class CatalogServiceTests
    {
        [Test]
        public async Task Search_ReturnsResultsFromRepository()
        {
            var mockRepo = new Mock<ICatalogRepository>();
            var expected = new List<CatalogItem> { new CatalogItem { Id = 1, Name = "Test", Description = "Desc 3" } };
            mockRepo.Setup(r => r.Search("test")).ReturnsAsync(expected);
            var service = new CatalogService(mockRepo.Object);
            var result = await service.Search("test");
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetCatalogItemById_ReturnsItemFromRepository()
        {
            var mockRepo = new Mock<ICatalogRepository>();
            var expected = new CatalogItem { Id = 2, Name = "Item2", Description = "Desc 2" };
            mockRepo.Setup(r => r.GetCatalogItemById(2)).ReturnsAsync(expected);
            var service = new CatalogService(mockRepo.Object);
            var result = await service.GetCatalogItemById(2);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
