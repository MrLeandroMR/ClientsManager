using System;
using ClientsManager.Core.Services;
using ClientsManager.Core.Services.Contracts;
using ClientsManager.Services;
using ClientsManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace ClientsManager
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            Container = RegisterServices();
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage));
        }

        public IServiceProvider Container { get; private set; }

        private IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<MainViewModel>();
            serviceCollection.AddTransient<ClienteDataService>();
            serviceCollection.AddSingleton<IClienteData, ClienteDataService>();
            serviceCollection.AddSingleton<IHttpData, HttpDataService>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
