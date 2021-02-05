using System;

using ClientsManager.ViewModels;

using Windows.UI.Xaml.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace ClientsManager.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = (App.Current as App).Container.GetService<MainViewModel>();

        public MainPage()
        {
            InitializeComponent();
            ViewModel.Initialize(flyoutBorder);
        }
    }
}
