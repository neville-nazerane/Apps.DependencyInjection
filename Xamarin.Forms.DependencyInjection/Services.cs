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

        static IServiceProvider Provider { get; set; }

        public static Func<Page, Task<bool>> OnPageLoading { private get; set; }

        public static void ConfigureServices(Action<IServiceCollection> config)
        {
            var services = new ServiceCollection();
            config(services);
            Provider = services.BuildServiceProvider();
        }

        static IServiceProvider currentProvider;

        public static TService Get<TService>()
        {
            if (currentProvider == null) return Provider.GetService<TService>();
            else return currentProvider.GetService<TService>();
        }

        static INavigation navigation;

        public static void SetNavigation(INavigation navigation)
            => Services.navigation = navigation;

        public static async Task<TPage> NavigateAsync<TPage>()
            where TPage : Page
        {
            var scope = Provider.CreateScope();
            var scopeProvider = scope.ServiceProvider;
            var oldProvider = currentProvider;
            currentProvider = scopeProvider;
            var page = scopeProvider.GetService<TPage>();
            if (OnPageLoading != null && !(await OnPageLoading.Invoke(page)))
                currentProvider = oldProvider;
            else
            {
                scopeProvider.GetService<IInitializer>().Init();
                if (navigation != null) await navigation.PushAsync(page);
            }

            return page; 
        }


    }
}
