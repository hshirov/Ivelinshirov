using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
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
    public class ArtworkServiceTests
    {
        [Test]
        public async Task Add_AddsArtwork()
        {
            // Arrange
            List<Artwork> artworks = new () { Artworks.Painting1 };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, artworks);

            ArtworkService service = new ArtworkService(context);

            // Act
            await service.Add(Artworks.Painting2);

            // Assert
            Assert.AreEqual(artworks.Count() + 1, service.GetAll().Count());
        }

        [Test]
        public async Task Remove_RemovesArtwork()
        {
            // Arrange
            List<Artwork> artworks = new() { Artworks.Painting1, Artworks.Painting2 };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, artworks);

            ArtworkService service = new ArtworkService(context);

            // Act
            await service.Remove(Artworks.Painting1.Id);

            // Assert
            Assert.AreEqual(artworks.Count - 1, service.GetAll().ToList().Count);
        }

        [Test]
        public async Task Update_UpdatesArtwork()
        {
            // Arrange
            List<Artwork> artworks = new() { Artworks.Painting1 };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, artworks);

            ArtworkService service = new ArtworkService(context);

            Artwork toUpdate = Artworks.Painting1;
            toUpdate.IsFeaturedOnHomePage = Artworks.Painting2.IsFeaturedOnHomePage;
            toUpdate.Title = Artworks.Painting2.Title;

            // Act
            await service.Update(toUpdate);

            // Assert
            Assert.AreEqual(context.Artworks.FirstOrDefault().IsFeaturedOnHomePage, Artworks.Painting2.IsFeaturedOnHomePage);
            Assert.AreEqual(context.Artworks.FirstOrDefault().Title, Artworks.Painting2.Title);
        }

        [Test]
        public async Task UpdatePosition_UpdatesArtworkPosition()
        {
            // Arrange
            List<Artwork> artworks = new() { Artworks.Painting1, Artworks.Painting2 };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, artworks);

            ArtworkService service = new ArtworkService(context);

            // Act
            await service.UpdatePosition(Artworks.Painting1.Id, 3);

            // Assert
            Assert.AreEqual(context.Artworks.FirstOrDefault().PositionPreference, 3);
        }

        [Test]
        public async Task Get_ReturnsArtworkById()
        {
            // Arrange
            List<Artwork> artworks = new() { Artworks.Painting1 };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, artworks);

            ArtworkService service = new ArtworkService(context);

            // Act
            Artwork result = await service.Get(Artworks.Painting1.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, artworks.FirstOrDefault());
        }

        [Test]
        public async Task GetAll_ReturnsAllRecords()
        {
            // Arrange
            List<Artwork> artworks = new() { Artworks.Painting1, Artworks.Painting2 };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, artworks);

            ArtworkService service = new ArtworkService(context);

            // Act
            var result = await service.GetAll().ToListAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count(), 0);
        }

        [Test]
        public async Task GetAllFeaturedOnHomePage_ReturnsAllFeaturedRecords()
        {
            // Arrange
            List<Artwork> artworks = new() { Artworks.Painting1, Artworks.Painting2 };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, artworks);

            ArtworkService service = new ArtworkService(context);

            // Act
            var result = await service.GetAllFeaturedOnHomePage();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count(), 0);
            Assert.AreEqual(result.FirstOrDefault().IsFeaturedOnHomePage, true);
        }

        [Test]
        public async Task GetAllFromCategory_ReturnsAllRecordsFromCategory()
        {
            // Arrange
            List<Artwork> artworks = new() { Artworks.Painting1, Artworks.Painting2, Artworks.Drawing1 };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, artworks);

            ArtworkService service = new ArtworkService(context);

            // Act
            var result = await service.GetAllFromCategory(Categories.Drawings.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count(), 0);
            Assert.AreEqual(result.FirstOrDefault().Category, Categories.Drawings);
        }
    }
}
