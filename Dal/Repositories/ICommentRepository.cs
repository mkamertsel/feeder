using System.Collections.Generic;
using Core.Entities;

namespace Dal.Repositories
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetComments(int postId);
    }
}