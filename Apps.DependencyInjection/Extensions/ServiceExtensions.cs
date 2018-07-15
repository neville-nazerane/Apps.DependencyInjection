using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.DependencyInjection.Extensions
{
    public static class ServiceExtensions
    {

        static IServiceCollection AddDataInit(this IServiceCollection services)
            => services.AddSingleton(typeof(IDataInitializer<>), typeof(DataInitializer<>));

        public static IServiceCollection AddListenersManager<TManager>(
                                                this IServiceCollection services,
                                                ServiceLifetime serviceLifetime = ServiceLifetime.Scoped
            )
            where TManager : ListenersManager
        {
            services
                .AddDataInit()
                .Add(new ServiceDescriptor(typeof(TManager), typeof(TManager), serviceLifetime));
            return services;
        }

        public static IServiceCollection AddListenersManager<TService, TImplementation>(
                                        this IServiceCollection services,
                                        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped
        )
            where TImplementation : ListenersManager, TService
        {
                services
                    .AddDataInit()
                    .Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), serviceLifetime));
                return services;
            }
    }
}
