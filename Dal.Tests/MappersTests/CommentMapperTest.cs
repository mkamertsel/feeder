using Dal.Mappers.DbToDto;
using DB.Models;
using NUnit.Framework;

namespace Dal.Tests.MappersTests
{
    [TestFixture]
    internal class CommentMapperTest
    {
        [Test]
        public void MapDbCommentTest()
        {
            var dbComment = new Comment
            {
                Text = "comment",
                CommenterName = "name",
                PostId = 1,
                CommenterEmail = "email"
            };
            var mapper = new CommentMapper();
            var result = mapper.Map(dbComment);

            Assert.AreEqual(result.PostId, 1);
            Assert.AreEqual(result.CommenterName, "name");
            Assert.AreEqual(result.CommenterEmail, "email");
            Assert.AreEqual(result.Text, "comment");
        }

        [Test]
        public void MapExternalDbCommentTest()
        {
            var externalDbComment = new Org.Feeder.FeederDb.Comment(1, "comment", "name", "email");
            var mapper = new Mappers.CommentMapper();
            var result = mapper.Map(externalDbComment);

            Assert.AreEqual(result.PostId, 1);
            Assert.AreEqual(result.Text, "comment");
            Assert.AreEqual(result.CommenterName, "name");
            Assert.AreEqual(result.CommenterEmail, "email");
        }
    }
}