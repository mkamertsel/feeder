using Caliburn.Micro;

namespace Feeder.Presentation.ViewModels
{
    public class IntroScreenViewModel : Screen
    {
        private readonly INavigationService navigationService;

        public IntroScreenViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            GoButtonText = "Ok";
        }

        public string GoButtonText { get; set; }

        public void ReadyToStart()
        {
            navigationService.For<MainScreenViewModel>().Navigate();
        }
    }
}