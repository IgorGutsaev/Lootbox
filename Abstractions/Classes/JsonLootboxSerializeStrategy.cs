using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Lootbox.Abstractions
{
    public class JsonLootboxSerializeStrategy<Ts> : ILootboxSerializeStrategy
        where Ts : Slot<Ts>, new()
    {
        private readonly IEnumerable<JsonConverter> _converters;

        public JsonLootboxSerializeStrategy(IJsonConvertersFabric fabric)
        {
            _converters = fabric.BuildConverters();
        }

        public string Serialize(object lootbox)
        {
            return JsonConvert.SerializeObject(lootbox, Formatting.Indented, _converters.ToArray());
        }

        public Tl Deserialize<Tl>(string value) where Tl : new()
        {
            return JsonConvert.DeserializeObject<Tl>(value, _converters.ToArray());
        }
    }
}