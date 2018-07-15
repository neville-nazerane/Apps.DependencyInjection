using System;
using System.Collections.Generic;
using System.Text;
using Apps.DependencyInjection;
using Apps.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;


namespace Xamarin.Forms.DependencyInjection
{
    public class ListenerConfiguration
    {
        private readonly IServiceCollection services;
        readonly List<Action<IServiceProvider>> listeners;

        internal ListenerConfiguration(IServiceCollection services)
        {
            this.services = services;
            listeners = new List<Action<IServiceProvider>>();
        }

        public ListenerConfiguration Add<TManager>() where TManager : ListenersManager
        {
            services.AddListenersManager<TManager>();
            listeners.Add(p =>
                        p.GetService<IDataInitializer<TManager>>().Init(p.GetService<TManager>()));
            return this;
        }

        internal void Initialize(IServiceProvider provider)
        {
            foreach (var listen in listeners) listen(provider);
        }

    }
}
