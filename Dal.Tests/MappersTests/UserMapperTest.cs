using Dal.Mappers.DbToDto;
using DB.Models;
using NUnit.Framework;

namespace Dal.Tests.MappersTests
{
    [TestFixture]
    internal class UserMapperTest
    {
        [Test]
        public void MapDbUserTest()
        {
            var dbUser = new User {UserId = 1, Name = "Name", Email = "email", ImageUrl = "image"};
            var mapper = new UserMapper();
            var result = mapper.Map(dbUser);

            Assert.AreEqual(result.UserId, 1);
            Assert.AreEqual(result.Name, "Name");
            Assert.AreEqual(result.Email, "email");
            Assert.AreEqual(result.ImageUrl, "image");
        }

        [Test]
        public void MapExternalDbUserTest()
        {
            var externalDbUser = new Org.Feeder.FeederDb.User(1, "Name", "email", "image");
            var mapper = new Mappers.UserMapper();
            var result = mapper.Map(externalDbUser);

            Assert.AreEqual(result.UserId, 1);
            Assert.AreEqual(result.Name, "Name");
            Assert.AreEqual(result.Email, "email");
            Assert.AreEqual(result.ImageUrl, "image");
        }
    }
}