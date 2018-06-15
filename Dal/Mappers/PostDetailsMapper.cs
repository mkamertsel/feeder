using Org.Feeder.FeederDb;

namespace Dal.Mappers
{
    public class PostDetailsMapper : IMapper<Post, DB.Models.Post>
    {
        public DB.Models.Post Map(Post source)
        {
            if (source == null)
            {
                return null;
            }
            return new DB.Models.Post
            {
                Id = source.Id,
                Title = source.Title,
                UserId = source.UserId,
                Body = source.Body
            };
        }
    }
}