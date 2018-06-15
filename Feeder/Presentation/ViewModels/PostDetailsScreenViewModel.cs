using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Entities;
using Feeder.BackgroundWorkers;
using Services;

namespace Feeder.Presentation.ViewModels
{
    public class PostDetailsScreenViewModel : Screen
    {
        private readonly ICommentService commentService;
        private readonly ILoader loader;
        private readonly INavigationService navigationService;
        private readonly Observer<IEnumerable<Comment>> observer;
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IWindowManager windowManager;

        private readonly BackgroundWorker worker;
        private BindableCollection<Comment> comments = new BindableCollection<Comment>();
        private bool isLoading;
        private PostDetails post;
        private string postAuthorIcon;
        private bool screenContentIsVisible;

        public PostDetailsScreenViewModel(INavigationService navigationService,
            IWindowManager windowManager, IUserService userService, IPostService postService,
            ICommentService commentService, ILoader loader)
        {
            this.navigationService = navigationService;
            this.windowManager = windowManager;
            this.userService = userService;
            this.postService = postService;
            this.commentService = commentService;
            this.loader = loader;

            worker = new BackgroundWorker();

            InitFielsd();

            InitWorker(worker);

            observer = InitObserver();

            loader.RegisterObserver(observer);
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                NotifyOfPropertyChange(() => IsLoading);
            }
        }

        public int DetailsId { get; set; }

        public PostDetails Post
        {
            get { return post; }
            set
            {
                post = value;
                NotifyOfPropertyChange(() => Post);
            }
        }

        public string PostAuthorIcon
        {
            get { return postAuthorIcon; }
            set
            {
                postAuthorIcon = value;
                NotifyOfPropertyChange(() => PostAuthorIcon);
            }
        }

        public IReadOnlyCollection<CommentExtended> DefaultCommentsList { get; set; }

        public IReadOnlyCollection<User> DefaultUsersList { get; set; }

        public BindableCollection<Comment> Comments
        {
            get { return comments ?? (comments = new BindableCollection<Comment>()); }
            set
            {
                comments = value;
                NotifyOfPropertyChange(() => Comments);
            }
        }

        public bool ScreenContentIsVisible
        {
            get { return screenContentIsVisible; }
            set
            {
                screenContentIsVisible = value;
                NotifyOfPropertyChange(() => ScreenContentIsVisible);
            }
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            loader.Dispose();
        }

        private Observer<IEnumerable<Comment>> InitObserver()
        {
            return new Observer<IEnumerable<Comment>>(async () => await commentService.GetCommentsAsync(DetailsId),
                data =>
                {
                    if (data != null && data.Count() != Comments.Count)
                    {
                        DefaultCommentsList = data as List<CommentExtended>;

                        DefaultUsersList = userService.GetUsers();

                        ApplyUserInfo();
                    }
                });
        }

        private void InitFielsd()
        {
            IsLoading = true;
            ScreenContentIsVisible = true;
        }

        private void InitWorker(BackgroundWorker worker)
        {
            worker.DoWork += (sender, e) => { LoadData(); };
            worker.RunWorkerCompleted += RunWorkerCompletedHandler;
        }

        private void RunWorkerCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                IsLoading = false;

                PostAuthorIcon = DefaultUsersList.First(x => x.UserId == Post.UserId)?.ImageUrl;
                ApplyUserInfo();
            }
            else
            {
                ShowError(e.Error.InnerException != null ? e.Error.InnerException.Message : e.Error.Message);
                worker.RunWorkerAsync();
            }
        }

        protected override void OnInitialize()
        {
            worker.RunWorkerAsync();

            base.OnInitialize();
        }

        public void LoadData()
        {
            Post = postService.GetPostDetails(DetailsId);

            DefaultCommentsList = commentService.GetComments(DetailsId) as List<CommentExtended>;

            DefaultUsersList = userService.GetUsers();
        }

        public void ShowError(string message)
        {
            ScreenContentIsVisible = false;

            var userViewModel = new ErrorPopupViewModel(navigationService, windowManager, message);
            windowManager.ShowDialog(userViewModel);

            ScreenContentIsVisible = true;
        }

        private void ApplyUserInfo()
        {
            foreach (var commentExtended in DefaultCommentsList)
            {
                commentExtended.UserIcon =
                    DefaultUsersList.FirstOrDefault(x => x.Email == commentExtended.CommenterEmail)?.ImageUrl;
            }
            Comments = new BindableCollection<Comment>(DefaultCommentsList);
        }

        public void GoBack()
        {
            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }
    }
}