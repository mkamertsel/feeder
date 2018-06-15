using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Services
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetComments(int postId);
        Task<IEnumerable<Comment>> GetCommentsAsync(int postId);
    }
}