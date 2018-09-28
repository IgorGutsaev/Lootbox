using Lootbox.Abstractions;
using Lootbox.Core;
using Lootbox.Core.JsonConverters;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit.Abstractions;

namespace Lootbox.Test
{
    public abstract class BaseTest
    {
        public readonly ITestOutputHelper _output;
        public readonly ILootboxSerializer _serializer;

        public BaseTest(ITestOutputHelper output)
        {
            IServiceProvider provider =
                new ServiceCollection()
                .AddSingleton<ILootboxSerializer, LootboxDefaultSerializer>()
                .AddSingleton<ILootboxSerializeStrategy>(sp => {
                    return new JsonLootboxSerializeStrategy<ScSlot>(sp.GetRequiredService<IJsonConvertersFabric>()); })
                .AddSingleton<IJsonConvertersFabric, ScJsonConvertersFabric>()
                .BuildServiceProvider();

            _serializer = provider.GetRequiredService<ILootboxSerializer>();
            _output = output;
        }
    }
}