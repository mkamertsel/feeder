using System.Collections.Generic;
using System.Linq;
using Dal.Mappers;
using Org.Feeder.FeederDb;

namespace Dal.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDataBaseAccessor dbAccessor;
        private readonly IMapper<Comment, DB.Models.Comment> dbCommentMapper;
        private readonly IMapper<DB.Models.Comment, Core.Entities.Comment> dtoCommentMapper;

        public CommentRepository(IDataBaseAccessor dbAccessor, IMapper<Comment, DB.Models.Comment> dbCommentMapper,
            IMapper<DB.Models.Comment, Core.Entities.Comment> dtoCommentMapper)
        {
            this.dbAccessor = dbAccessor;
            this.dbCommentMapper = dbCommentMapper;
            this.dtoCommentMapper = dtoCommentMapper;
        }

        public IEnumerable<Core.Entities.Comment> GetComments(int postId)
        {
            return
                dbAccessor.GetComments(postId).Select(x => dbCommentMapper.Map(x)).Select(x => dtoCommentMapper.Map(x));
        }
    }
}