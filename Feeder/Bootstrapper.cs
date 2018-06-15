using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Configurations;
using CustomClient;
using Feeder.BackgroundWorkers;
using Feeder.Presentation.ViewModels;
using Services;

namespace Feeder
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Instance(container);
            container.Singleton<IWindowManager, WindowManager>().Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<ShellViewModel>()
                .PerRequest<IntroScreenViewModel>()
                .PerRequest<MainScreenViewModel>()
                .PerRequest<PostDetailsScreenViewModel>()
                .PerRequest<ErrorPopupViewModel>();
            container.Singleton<ILoader, Loader>();
            container.Singleton<IConfiguration, Configuration>();
            container.Singleton<IClient, Client>();
            ServicesComponentRegistration.RegisterServices(container);
        }



        protected override void OnStartup(object sender, StartupEventArgs args)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}