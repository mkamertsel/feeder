using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Entities;
using Feeder.BackgroundWorkers;
using Feeder.Presentation.Views;
using Services;

namespace Feeder.Presentation.ViewModels
{
    public class MainScreenViewModel : Screen
    {
        private readonly ILoader loader;
        private readonly INavigationService navigationService;
        private readonly Observer<IEnumerable<PostSummary>> observer;
        private readonly IPostService postService;
        private readonly IWindowManager windowManager;
        private readonly BackgroundWorker worker;
        public List<PostSummary> defaultData;
        private bool isLoading;

        private BindableCollection<PostSummary> postSummaries = new BindableCollection<PostSummary>();
        private bool screenContentIsVisible;

        public MainScreenViewModel(INavigationService navigationService,
            IWindowManager windowManager,
            IPostService postService,
            ILoader loader)
        {
            this.navigationService = navigationService;
            this.postService = postService;
            this.windowManager = windowManager;
            this.loader = loader;

            worker = new BackgroundWorker();

            observer =
                new Observer<IEnumerable<PostSummary>>(async () => await postService.GetAllPostsAsync(),
                    data =>
                    {
                        if (data != null && data.Count() != defaultData.Count)
                        {
                            PostSummaries = new BindableCollection<PostSummary>(data);
                        }
                    });
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

        public BindableCollection<PostSummary> PostSummaries
        {
            get { return postSummaries ?? (postSummaries = new BindableCollection<PostSummary>()); }
            set
            {
                postSummaries = value;
                NotifyOfPropertyChange(() => PostSummaries);
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

        public string PostFilterValue { get; set; }

        public int DetailsId { get; set; }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            loader.Dispose();
        }

        protected override void OnActivate()
        {
            ScreenContentIsVisible = true;
            IsLoading = true;

            worker.DoWork += (sender, e) => { ApplyDefaultData(); };
            worker.RunWorkerCompleted += RunWorkerCompletedHandler;

            worker.RunWorkerAsync();

            loader.RegisterObserver(observer);
        }

        private void RunWorkerCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                IsLoading = false;
                if (!navigationService.CanGoForward)
                {
                    return;
                }
                var currentContent = navigationService.CurrentContent as MainScreenView;

                if (!string.IsNullOrEmpty(currentContent?.PostFilterValue.Text))
                {
                    PostFilter(currentContent.PostFilterValue.Text);
                }
            }
            else
            {
                ShowError(e.Error.InnerException != null ? e.Error.InnerException.Message : e.Error.Message);
                worker.RunWorkerAsync();
            }
        }

        public void ShowError(string message)
        {
            ScreenContentIsVisible = false;

            var userViewModel = new ErrorPopupViewModel(navigationService, windowManager, message);
            windowManager.ShowDialog(userViewModel);
            ScreenContentIsVisible = true;

            worker.RunWorkerAsync();
        }

        public void GoToPost(object obj)
        {
            var post = obj as PostSummary;
            if (post != null)
            {
                navigationService.For<PostDetailsScreenViewModel>().WithParam(x => x.DetailsId, post.Id).Navigate();
            }
        }

        public void PostFilter(string value)
        {
            PostFilterValue = value;
            if (defaultData == null)
            {
                defaultData = LoadData().Result.ToList();
            }
            var filteredData =
                defaultData.Where(x => x.Title.IndexOf(PostFilterValue, StringComparison.OrdinalIgnoreCase) >= 0);

            PostSummaries = new BindableCollection<PostSummary>(filteredData);
        }


        private async Task<IEnumerable<PostSummary>> LoadData()
        {
            return await postService.GetAllPostsAsync();
        }

        public void ApplyDefaultData()
        {
            if (defaultData == null)
            {
                try
                {
                    defaultData = LoadData().Result.ToList();
                }
                catch (Exception e)
                {
                    ShowError(e.Message);
                }
            }
            PostSummaries = new BindableCollection<PostSummary>(defaultData);
        }
    }
}