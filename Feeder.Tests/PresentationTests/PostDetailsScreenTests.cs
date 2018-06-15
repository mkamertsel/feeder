using System.Collections.Generic;
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
    internal class PostDetailsScreenTests
    {
        private Mock<IPostService> postService;
        private Mock<ICommentService> commentService;
        private Mock<IUserService> userService;
        private Mock<IWindowManager> windowManager;
        private Mock<ILoader> loader;
        private List<CommentExtended> comments;
        private List<User> users;
        private PostDetails post;

        [SetUp]
        public void Setup()
        {
            post = new PostDetails
            {
                Title = "title",
                Body = "post body",
                Id = 1,
                UserId = 1
            };
            users = new List<User>
            {
                new User {UserId = 1, Name = "Name", Email = "email", ImageUrl = "image"},
                new User {UserId = 1, Name = "Name", Email = "l", ImageUrl = "image"}
            };
            comments = new List<CommentExtended>
            {
                new Comment
                {
                    Text = "comment",
                    CommenterName = "name",
                    PostId = 1,
                    CommenterEmail = "email"
                } as CommentExtended,
                new Comment
                {
                    Text = "comment",
                    CommenterName = "name",
                    PostId = 1,
                    CommenterEmail = "l"
                } as CommentExtended,
                new Comment
                {
                    Text = "comment",
                    CommenterName = "name",
                    PostId = 1,
                    CommenterEmail = "l"
                } as CommentExtended,
                new Comment
                {
                    Text = "comment",
                    CommenterName = "name",
                    PostId = 1,
                    CommenterEmail = "email"
                } as CommentExtended
            };
            postService = new Mock<IPostService>();
            windowManager = new Mock<IWindowManager>();
            userService = new Mock<IUserService>();
            commentService = new Mock<ICommentService>();
            loader = new Mock<ILoader>();
        }

        [Test]
        public void LoadDataTest()
        {
            postService.Setup(service => service.GetPostDetails(It.IsAny<int>())).Returns(() => post);
            commentService.Setup(service => service.GetComments(It.IsAny<int>())).Returns(() => comments);
            userService.Setup(service => service.GetUsers()).Returns(() => users);

            var viewModel = new PostDetailsScreenViewModel(null,
                windowManager.Object,
                userService.Object,
                postService.Object,
                commentService.Object,
                loader.Object);

            viewModel.LoadData();


            Assert.AreNotEqual(null, viewModel.Post);
            Assert.AreEqual(post.Id, viewModel.Post.Id);

            Assert.AreNotEqual(null, viewModel.DefaultCommentsList);
            Assert.AreEqual(comments.Count, viewModel.DefaultCommentsList.Count);

            Assert.AreNotEqual(null, viewModel.DefaultUsersList);
            Assert.AreEqual(users.Count, viewModel.DefaultUsersList.Count);
        }

    }
   
}