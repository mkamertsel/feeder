using Dal.Mappers.DbToDto;
using DB.Models;
using NUnit.Framework;

namespace Dal.Tests.MappersTests
{
    [TestFixture]
    internal class PostSummaryMapperTest
    {
        [Test]
        public void MapDbPostDetailsTest()
        {
            var dbPost = new PostSummary
            {
                Title = "title",
                Id = 1
            };
            var mapper = new PostSummaryMapper();
            var result = mapper.Map(dbPost);

            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Title, "title");
        }

        [Test]
        public void MapExternalDbPostDetailsTest()
        {
            var externalDbPostDetails = new Org.Feeder.FeederDb.PostSummary(1, "title");
            var mapper = new Mappers.PostSummaryMapper();
            var result = mapper.Map(externalDbPostDetails);

            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Title, "title");
        }
    }
}