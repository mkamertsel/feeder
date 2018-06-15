using DB.Models;

namespace Dal.Mappers.DbToDto
{
    public class CommentMapper : IMapper<Comment, Core.Entities.Comment>
    {
        public Core.Entities.Comment Map(Comment source)
        {
            if (source == null)
            {
                return null;
            }
            return new Core.Entities.Comment
            {
                PostId = source.PostId,
                CommenterName = source.CommenterName,
                CommenterEmail = source.CommenterEmail,
                Text = source.Text
            };
        }
    }
}