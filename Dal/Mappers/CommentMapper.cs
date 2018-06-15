using Org.Feeder.FeederDb;

namespace Dal.Mappers
{
    public class CommentMapper : IMapper<Comment, DB.Models.Comment>
    {
        public DB.Models.Comment Map(Comment source)
        {
            if (source == null)
            {
                return null;
            }
            return new DB.Models.Comment
            {
                PostId = source.PostId,
                CommenterName = source.CommenterName,
                CommenterEmail = source.CommenterEmail,
                Text = source.Text
            };
        }
    }
}