using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public delegate void SetupAction<T>(ref T setup) where T : struct;

    internal sealed class SettingsInjector<TComponent, TSettings>
            : ISettingsInjector<TComponent, TSettings>
            where TSettings : struct
            where TComponent : IInjectableSettings<TSettings>
    {
        public SettingsInjector(SetupAction<TSettings> setupAction)
        {
            _setupAction = setupAction;
        }

        public TSettings Settings
        {
            get
            {
                TSettings result = default(TSettings);

                _setupAction(ref result);

                return result;
            }
        }

        private SetupAction<TSettings> _setupAction;
    }
}
