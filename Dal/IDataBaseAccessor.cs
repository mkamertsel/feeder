using System.Collections.Generic;
using Org.Feeder.FeederDb;

namespace Dal
{
    public interface IDataBaseAccessor
    {
        IEnumerable<PostSummary> GetPostSummaries();
        IList<Comment> GetComments(int postId);
        Post GetPost(int postId);
        IEnumerable<User> GetUsers();
    }
}