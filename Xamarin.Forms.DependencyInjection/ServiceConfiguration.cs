using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Xamarin.Forms.DependencyInjection
{
    public abstract class ServiceConfiguration
    {

        protected virtual bool EnableAutoPageAdd => true;

        internal ListenerConfiguration ListenerConfiguration { get; set; }

        internal void AdditionalServices(IServiceCollection services)
        {

            services.AddScoped(typeof(Passable<>));

            if (EnableAutoPageAdd)
            {
                var pages = GetType().Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Page)) && !t.IsAbstract);
                foreach (var page in pages) services.AddScoped(page);
            }
            ListenerConfiguration = new ListenerConfiguration(services);
            ConfigureListeners(ListenerConfiguration);
        }

        public virtual void ConfigureListeners(ListenerConfiguration configuration) { }
        
        public abstract void ConfigureServices(IServiceCollection services);
        
        public virtual Task<bool> OnPageLoading(Page page) => Task.FromResult(true);

        public virtual void OnCreated(IServiceProvider provider) { }

    }
}
