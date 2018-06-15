using Core.Entities;
using DB.Models;

namespace Dal.Mappers.DbToDto
{
    public class PostDetailsMapper : IMapper<Post, PostDetails>
    {
        public PostDetails Map(Post source)
        {
            if (source == null)
            {
                return null;
            }
            return new PostDetails
            {
                Id = source.Id,
                Title = source.Title,
                UserId = source.UserId,
                Body = source.Body
            };
        }
    }
}