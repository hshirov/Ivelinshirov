using Data;
using Data.Models;
using NUnit.Framework;
using Services.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.Common;
using Tests.Data;

namespace Tests.Services.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        [Test]
        public async Task Add_AddsCategory()
        {
            // Arrange
            List<Category> categories = new ()
            {
                Categories.Paintings,
                Categories.Drawings
            };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, categories);

            CategoryService service = new CategoryService(context);

            // Act
            var newCategory = new Category()
            {
                Name = "Sculptures",
                Artworks = new List<Artwork>()
            };

            await service.Add(newCategory);

            // Assert
            Assert.AreEqual(categories.Count() + 1, service.GetAll().Result.Count());
        }

        [Test]
        public async Task Remove_RemovesCategory()
        {
            // Arrange
            int testCategoryId = 3;
            List<Category> categories = new ()
            {
                Categories.Paintings,
                Categories.Drawings,
                new Category { Id = testCategoryId, Name = "Test Category"}
            };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, categories);

            CategoryService service = new CategoryService(context);

            // Act
            await service.Remove(testCategoryId);

            // Assert
            Assert.AreEqual(categories.Count - 1, service.GetAll().Result.Count());
        }

        [Test]
        public async Task GetById_ReturnsCategory()
        {
            // Arrange
            List<Category> categories = new()
            {
                Categories.Paintings,
                Categories.Drawings
            };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, categories);

            CategoryService service = new CategoryService(context);

            // Act
            var category = await service.GetById(1);

            // Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(category, categories.FirstOrDefault());
        }

        [Test]
        public async Task GetByName_ReturnsCategory()
        {
            // Arrange
            List<Category> categories = new()
            {
                Categories.Paintings,
                Categories.Drawings
            };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, categories);

            CategoryService service = new CategoryService(context);

            // Act
            var category = await service.GetByName("Paintings");

            // Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(category, categories.FirstOrDefault());
        }

        [Test]
        public async Task GetAll_ReturnsAllRecords()
        {
            // Arrange
            List<Category> categories = new()
            {
                Categories.Paintings,
                Categories.Drawings
            };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, categories);

            CategoryService service = new CategoryService(context);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), categories.Count);
        }
    }
}
