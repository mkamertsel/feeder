using Caliburn.Micro;

namespace Feeder.Presentation.ViewModels
{
    public class ErrorPopupViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private IWindowManager windowManager;

        public ErrorPopupViewModel(INavigationService navigationService, IWindowManager windowManager, string message)
        {
            this.windowManager = windowManager;
            this.navigationService = navigationService;
            ErrorMessage = message;
        }

        public string ErrorMessage { get; set; }

        public void Dismiss()
        {
            TryClose(false);
        }
    }
}
