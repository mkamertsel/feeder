using Caliburn.Micro;

namespace Services
{
    public static class ServicesComponentRegistration
    {
        public static void RegisterServices(SimpleContainer container)
        {
            container.Singleton<IPostService, PostService>();
            container.Singleton<ICommentService, CommentService>();
            container.Singleton<IUserService, UserService>();
        }
    }
}