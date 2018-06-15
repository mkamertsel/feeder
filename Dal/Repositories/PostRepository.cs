using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Dal.Mappers;
using Org.Feeder.FeederDb;
using PostSummary = Org.Feeder.FeederDb.PostSummary;

namespace Dal.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDataBaseAccessor dbAccessor;
        private readonly IMapper<Post, DB.Models.Post> dbPostMapper;
        private readonly IMapper<PostSummary, DB.Models.PostSummary> dbPostSummaryMapper;
        private readonly IMapper<DB.Models.Post, PostDetails> dtoPostMapper;
        private readonly IMapper<DB.Models.PostSummary, Core.Entities.PostSummary> dtoPostSummaryMapper;

        public PostRepository(IDataBaseAccessor accessor,
            IMapper<PostSummary, DB.Models.PostSummary> dbPostSummaryMapper,
            IMapper<Post, DB.Models.Post> dbPostMapper, IMapper<DB.Models.Post, PostDetails> dtoPostMapper,
            IMapper<DB.Models.PostSummary, Core.Entities.PostSummary> dtoPostSummaryMapper)
        {
            dbAccessor = accessor;
            this.dbPostSummaryMapper = dbPostSummaryMapper;
            this.dbPostMapper = dbPostMapper;
            this.dtoPostMapper = dtoPostMapper;
            this.dtoPostSummaryMapper = dtoPostSummaryMapper;
        }

        public IEnumerable<Core.Entities.PostSummary> GetAllPosts()
        {
            return GetDbPosts().Select(x => dtoPostSummaryMapper.Map(x));
        }

        public PostDetails GetPost(int postId)
        {
            return dtoPostMapper.Map(GetDbPost(postId));
        }

        private DB.Models.Post GetDbPost(int postId)
        {
            return dbPostMapper.Map(dbAccessor.GetPost(postId));
        }

        private IEnumerable<DB.Models.PostSummary> GetDbPosts()
        {
            IEnumerable<PostSummary> data;
            try
            {
                data = dbAccessor.GetPostSummaries();
            }
            catch (TimeoutException)
            {
                data = dbAccessor.GetPostSummaries();
            }
            return data.Select(source => dbPostSummaryMapper.Map(source));
        }
    }
}