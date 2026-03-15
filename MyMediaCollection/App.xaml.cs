using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using MyMediaCollection.Interfaces;
using MyMediaCollection.Services;
using MyMediaCollection.ViewModels;
using MyMediaCollection.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyMediaCollection
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static IHost HostContainer { get; private set;  }
        // public static MainViewModel ViewModel { get; } = new MainViewModel();
        private Window? _window;

        private async Task RegisterComponents(Frame rootFrame)
        {
            var navigationService = new NavigationService(rootFrame);
            navigationService.Configure(nameof(MainPage), typeof(MainPage));
            navigationService.Configure(nameof(ItemDetailsPage), typeof(ItemDetailsPage));
            var dataService = new SqliteDataService();
            await dataService.InitializeDataAsync();

            HostContainer = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<INavigationService>(navigationService);
                    services.AddSingleton<IDataService> (dataService);
                    services.AddTransient<MainViewModel>();
                    services.AddTransient<ItemDetailsViewModel>();
                }).Build();
        }
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            var rootFrame = new Frame();
            await RegisterComponents(rootFrame);
            rootFrame.NavigationFailed += RootFrame_NavigationFailed;
            rootFrame.Navigate(typeof(MainPage), args);
            _window.Content = rootFrame;
            _window.Activate();
        }
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception($"Error loading page {e.SourcePageType.FullName}");
        }
    }
}
