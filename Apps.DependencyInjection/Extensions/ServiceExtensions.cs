using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.DependencyInjection.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddInitializer(this IServiceCollection services)
            => services.AddSingleton<IInitializer, Initializer>();

        public static IServiceCollection AddListenersManager<TManager>(this IServiceCollection services)
            where TManager : ListenersManager
            => services.AddInitializer()
                         .AddScoped<TManager>();
    }
}
