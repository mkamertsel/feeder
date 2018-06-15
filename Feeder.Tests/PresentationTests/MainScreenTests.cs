using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Entities;
using Feeder.BackgroundWorkers;
using Feeder.Presentation.ViewModels;
using Moq;
using NUnit.Framework;
using Services;

namespace Feeder.Tests.PresentationTests
{
    [TestFixture]
    internal class MainScreenTests
    {
        private Mock<IPostService> postService;
        private Mock<IWindowManager> windowManager;
        private Mock<ILoader> loader;
        private List<PostSummary> posts;

        [SetUp]
        public void Setup()
        {
            posts = new List<PostSummary>
            {
                new PostSummary {Id = 1, Title = "title 1"},
                new PostSummary {Id = 2, Title = "title 2"},
                new PostSummary {Id = 3, Title = "title 3"},
                new PostSummary {Id = 4, Title = "title 4"}
            };
            postService = new Mock<IPostService>();
            windowManager = new Mock<IWindowManager>();
            windowManager = new Mock<IWindowManager>();
            loader = new Mock<ILoader>();
        }

        [Test]
        public void LoadDataTest()
        {
            postService.Setup(service => service.GetAllPostsAsync()).Returns(() =>
            {
                var task = new Task<IEnumerable<PostSummary>>(() => posts);

                task.Start();
                task.Wait();
                return task;
            });

            var viewModel = new MainScreenViewModel(null,
                windowManager.Object,
                postService.Object,
                loader.Object);

            viewModel.ApplyDefaultData();


            Assert.AreNotEqual(null, viewModel.PostSummaries);
            Assert.AreEqual(posts.Count, viewModel.PostSummaries.Count);
        }
        [Test]
        public void FilterPostTest()
        {
            var viewModel = new MainScreenViewModel(null,
                windowManager.Object,
                postService.Object,
                loader.Object)
            {
                defaultData = posts,
                PostSummaries = new BindableCollection<PostSummary>(posts)
            };
            viewModel.PostFilter("3");

            Assert.IsTrue(viewModel.defaultData.Count == posts.Count);
            Assert.IsTrue(viewModel.PostSummaries.Count == 1);

            viewModel.PostFilter(string.Empty);
            Assert.IsTrue(viewModel.defaultData.Count == posts.Count);
            Assert.IsTrue(viewModel.PostSummaries.Count == posts.Count);
        }
    }
}