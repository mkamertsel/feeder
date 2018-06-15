using System.Collections.Generic;
using Core.Entities;

namespace Dal.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<PostSummary> GetAllPosts();
        PostDetails GetPost(int postId);
    }
}