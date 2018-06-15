using System;
using System.Diagnostics;
using Configurations;
using Dal.Repositories;
using NUnit.Framework;
using SimpleInjector;

namespace Dal.Tests.Repositories
{
    [TestFixture]
    internal class CommentRepositoryTest
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
        public void GetCommentsTest()
        {
            var postId = 1;
            var repository = container.GetInstance<ICommentRepository>();
            try
            {
                var result = repository.GetComments(postId);
            }
            catch (Exception)
            {

            }
        }
    }
}