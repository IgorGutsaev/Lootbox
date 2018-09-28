using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSettingsInjector<TComponent, TSettings>
            (this IServiceCollection serviceCollection
            , SetupAction<TSettings> setupAction)
            where TSettings : struct
            where TComponent : IInjectableSettings<TSettings>
        {
            return serviceCollection
                .AddSingleton<ISettingsInjector<TComponent, TSettings>>(sp => new SettingsInjector<TComponent, TSettings>(setupAction));
        }
    }
}
