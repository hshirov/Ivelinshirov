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
    public class BiographyServiceTests
    {
        [Test]
        public async Task Get_ReturnsBiography()
        {
            // Arrange
            List<Biography> biographies = new() { Biographies.biography };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, biographies);

            BiographyService service = new BiographyService(context);

            // Act
            Biography result = await service.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, biographies.FirstOrDefault());
        }

        [Test]
        public async Task Update_UpdatesBiography()
        {
            // Arrange
            List<Biography> biographies = new() { Biographies.biography };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, biographies);

            BiographyService service = new BiographyService(context);

            var toUpdate = Biographies.biography;
            string newText = "Test text";
            toUpdate.Text = newText;

            // Act
            await service.Update(toUpdate);

            // Assert
            Assert.AreEqual(service.Get().Result.Text, newText);
        }
    }
}
