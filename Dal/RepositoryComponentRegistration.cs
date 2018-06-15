using Core.Entities;
using Dal.Mappers;
using Dal.Repositories;
using Org.Feeder.FeederDb;
using SimpleInjector;
using Comment = Org.Feeder.FeederDb.Comment;
using PostSummary = Org.Feeder.FeederDb.PostSummary;
using User = Org.Feeder.FeederDb.User;

namespace Dal
{
    public static class RepositoryComponentRegistration
    {
        public static void RegisterRepositories(Container container)
        {
            RegisterMappers(container);
            container.Register<IPostRepository, PostRepository>();
            container.Register<ICommentRepository, CommentRepository>();
            container.Register<IUserRepository, UserRepository>();
        }
        private static void RegisterMappers(Container container)
        {
            container.Register<IMapper<PostSummary, DB.Models.PostSummary>, PostSummaryMapper>();
            container.Register<IMapper<Post, DB.Models.Post>, PostDetailsMapper>();
            container.Register<IMapper<Comment, DB.Models.Comment>, CommentMapper>();
            container.Register<IMapper<User, DB.Models.User>, UserMapper>();

            container.Register<IMapper<DB.Models.PostSummary, Core.Entities.PostSummary>, Dal.Mappers.DbToDto.PostSummaryMapper>();
            container.Register<IMapper<DB.Models.Post, PostDetails>, Dal.Mappers.DbToDto.PostDetailsMapper>();
            container.Register<IMapper<DB.Models.Comment, Core.Entities.Comment>, Dal.Mappers.DbToDto.CommentMapper>();
            container.Register<IMapper<DB.Models.User, Core.Entities.User>, Dal.Mappers.DbToDto.UserMapper>();
        }
    }
}