using Lootbox.Abstractions;
using System;

namespace Lootbox.Core
{
    public class LootboxBinder
        : ILootboxBinder
        , IInjectableSettings<LootboxBinderSettings>
    {
        private readonly ILootboxMapper _mapper;
        private readonly LootboxBinderSettings _settings;
        private readonly ScLootbox _lootbox;

        public LootboxBinder(ISettingsInjector<LootboxBinder, LootboxBinderSettings> settingsInjector
            , ILootboxSerializer lootboxSerializer
            , ILootboxMapper mapper)
        {
            _mapper = mapper;
            _settings = settingsInjector?.Settings
                ?? throw new ArgumentNullException(nameof(settingsInjector));

           // _lootbox = ScLootbox
           // _settings.FileLocation
        }

        public bool Bind(ILootboxBindableObject data)
        {
            throw new NotImplementedException();
        }
    }
}
