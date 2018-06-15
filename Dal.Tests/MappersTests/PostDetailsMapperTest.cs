using Dal.Mappers.DbToDto;
using DB.Models;
using NUnit.Framework;

namespace Dal.Tests.MappersTests
{
    [TestFixture]
    internal class PostDetailsMapperTest
    {
        [Test]
        public void MapDbPostDetailsTest()
        {
            var dbPost = new Post
            {
                Title = "title",
                Body = "post body",
                Id = 1,
                UserId = 1
            };
            var mapper = new PostDetailsMapper();
            var result = mapper.Map(dbPost);

            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.UserId, 1);
            Assert.AreEqual(result.Title, "title");
            Assert.AreEqual(result.Body, "post body");
        }

        [Test]
        public void MapExternalDbPostDetailsTest()
        {
            var externalDbPostDetails = new Org.Feeder.FeederDb.Post(1, 1, "title", "post body");
            var mapper = new Mappers.PostDetailsMapper();
            var result = mapper.Map(externalDbPostDetails);

            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.UserId, 1);
            Assert.AreEqual(result.Title, "title");
            Assert.AreEqual(result.Body, "post body");
        }
    }
}