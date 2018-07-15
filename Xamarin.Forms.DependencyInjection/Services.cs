using Apps.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Forms.DependencyInjection
{
    public static class Services
    {

        static IServiceProvider provider;
        static IServiceProvider currentProvider;
        static INavigation navigation;

        static ServiceConfiguration _configuration;
        static Page _navigationPage = new NavigationPage();

        public static Page NavigationPage
        {
            get => _navigationPage;
            set
            {
                _navigationPage = value;
                if (value != null) navigation = value.Navigation;
            }
        }

        public static ServiceConfiguration Configuration
        {
            private get => _configuration;
            set
            {
                _configuration = value;
                IServiceCollection services = new ServiceCollection();
                Configuration.AdditionalServices(services);
                Configuration.ConfigureServices(services);
                provider = services.BuildServiceProvider();
            }
        }

        public static TService Get<TService>()
        {
            if (currentProvider == null) return provider.GetService<TService>();
            else return currentProvider.GetService<TService>();
        }

        public static void SetNavigation(INavigation navigation)
        {
            Services.navigation = navigation;
            NavigationPage = null;
        }

        public static async Task<TPage> NavigateAsync<TPage>(bool selfNavigate = true)
            where TPage : Page
        {
            var scope = provider.CreateScope();
            var scopeProvider = scope.ServiceProvider;
            var oldProvider = currentProvider;
            currentProvider = scopeProvider;
            var page = scopeProvider.GetService<TPage>();
            if (await Configuration.OnPageLoading(page))
            {
                scopeProvider.GetService<IInitializer>().Init();
                if (selfNavigate && navigation != null) await navigation.PushAsync(page);
            }
            else currentProvider = oldProvider;
            return page;
        }


    }
}
