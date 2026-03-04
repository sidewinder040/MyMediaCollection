using Microsoft.UI.Xaml.Controls;
using MyMediaCollection.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace MyMediaCollection.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService(Frame rootFrame) 
        {
            AppFrame = rootFrame;
        }
        private readonly IDictionary<string, Type> _pages = new 
            ConcurrentDictionary<string, Type>();
        public const string RootPage = "(Root)";

        public const string UnknownPage = "(Unknown)";

        private static Frame AppFrame;
        public string CurrentPage => throw new NotImplementedException();

        public void Configure(string page, Type type)
        {
            if (_pages.Values.Any(v => v == type))
            {
                throw new ArgumentException($"The {type.Name} view has already been registered under another name.");
            }

            _pages[page] = type;
        }
        public void NavigateTo(string page)
        {
            NavigateTo(page, null);
        }

        public void NavigateTo(string page, object parameter)
        {
            if (!_pages.ContainsKey(page))
            {
                throw new ArgumentException($"Unable to find a page registered with the name {page}.");
            }

            AppFrame.Navigate(_pages[page], parameter);
        }

        public void GoBack()
        {
            if (AppFrame?.CanGoBack == true)
            {
                AppFrame.GoBack();
            }
        }

    }
}
