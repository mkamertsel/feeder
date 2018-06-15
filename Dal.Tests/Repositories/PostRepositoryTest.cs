using System;
using System.Diagnostics;
using Configurations;
using Dal.Repositories;
using NUnit.Framework;
using SimpleInjector;

namespace Dal.Tests.Repositories
{
    [TestFixture]
    internal class PostRepositoryTest
    {
        private Container container;

        [OneTimeSetUp]
        public void ClassInit()
        {
            Debug.AutoFlush = true;
            container = new Container();

            container.RegisterSingleton<Configuration>();
            container.RegisterSingleton<IConfiguration>(container.GetInstance<Configuration>);

            container.RegisterSingleton<IDataBaseAccessor, DataBaseAccessor>();
            RepositoryComponentRegistration.RegisterRepositories(container);
        }

        [Test]
        public void GetAllPostsTest()
        {
            var repository = container.GetInstance<IPostRepository>();
            var result = repository.GetAllPosts();
        }

        [Test]
        public void GetPostByIdTest()
        {
            var postId = 5;
            var repository = container.GetInstance<IPostRepository>();
            try
            {
                var result = repository.GetPost(postId);
            }
            catch (Exception)
            {
                
            }
        }
    }
}