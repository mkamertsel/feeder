using System.Windows.Controls;
using Caliburn.Micro;

namespace Feeder.Presentation.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly SimpleContainer container;
        private INavigationService navigationService;

        public ShellViewModel(SimpleContainer container)
        {
            this.container = container;
            Title = "Feeder - Great content on your desktop.";
        }

        public string Title { get; set; }

        public void RegisterFrame(Frame frame)
        {
            navigationService = new FrameAdapter(frame);

            container.Instance(navigationService);

            navigationService.NavigateToViewModel(typeof(IntroScreenViewModel));
        }
    }
}