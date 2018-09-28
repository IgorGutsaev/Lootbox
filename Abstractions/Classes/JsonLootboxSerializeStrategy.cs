using Newtonsoft.Json;

namespace Lootbox.Abstractions
{
    public class JsonLootboxSerializeStrategy<Tf, Ts> : ILootboxSerializeStrategy<Tf, Ts>
        where Tf : Fraction<Ts>, new()
        where Ts : Slot<Ts>, new()
    {
        private readonly JsonConverter[] _converters;

        public JsonLootboxSerializeStrategy(params JsonConverter[] converters)
        {
            _converters = converters;
        }

        public string Serialize<Tl>(Tl lootbox) where Tl : ILootbox<Tf, Ts>
        {
            return JsonConvert.SerializeObject(lootbox, Formatting.Indented, _converters);
        }

        public Tl Deserialize<Tl>(string value) where Tl : new()
        {
            return JsonConvert.DeserializeObject<Tl>(value, _converters);
        }
    }
}