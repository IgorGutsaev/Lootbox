using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public interface IInjectableSettings<TSettings>
    where TSettings : struct
    {

    }

    public interface ISettingsInjector<TComponent, TSettings>
        where TComponent : IInjectableSettings<TSettings>
        where TSettings : struct
    {
        TSettings Settings { get; }
    }
}
