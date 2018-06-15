using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Services
{
    public interface IPostService
    {
        IEnumerable<PostSummary> GetAllPosts();
        PostDetails GetPostDetails(int postId);
        Task<IEnumerable<PostSummary>> GetAllPostsAsync();
        Task<PostDetails> GetPostDetailsAsync(int postId);
    }
}