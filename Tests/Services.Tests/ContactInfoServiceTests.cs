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
    public class ContactInfoServiceTests
    {
        [Test]
        public async Task Get_ReturnsContactInfo()
        {
            // Arrange
            List<ContactInfo> contactInfos = new() { ContactInfos.info };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, contactInfos);

            ContactInfoService service = new ContactInfoService(context);

            // Act
            var result = await service.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, contactInfos.FirstOrDefault());
        }

        [Test]
        public async Task Update_UpdatesContactInfo()
        {
            // Arrange
            List<ContactInfo> contactInfos = new() { ContactInfos.info };

            ApplicationDbContext context = InMemoryFactory.InitializeContext();
            await DataSeeder.SeedDataInDbContext(context, contactInfos);

            ContactInfoService service = new ContactInfoService(context);

            var toUpdate = ContactInfos.info;
            string newEmail = "newtestmail@testmail.com";
            toUpdate.ReceiverEmail = newEmail;

            // Act
            await service.Update(toUpdate);

            // Assert
            Assert.AreEqual(service.Get().Result.ReceiverEmail, newEmail);
        }
    }
}
