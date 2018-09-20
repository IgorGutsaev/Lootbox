using Newtonsoft.Json;

namespace Lootbox.Abstractions
{
    public class JsonLootboxSerializeStrategy<Tf, Ts> : ILootboxSerializeStrategy<Tf, Ts>
        where Tf : Fraction<Ts>, new()
        where Ts : Slot<Ts>, new()
    {
        public string Serialize<Tl>(Tl lootbox) where Tl : Lootbox<Tf, Ts>, new()
        {
            return JsonConvert.SerializeObject(lootbox, Formatting.Indented);
        }

        public Tl Deserialize<Tl>(string value) where Tl : Lootbox<Tf, Ts>, new()
        {
            return JsonConvert.DeserializeObject<Tl>(value);
        }
    }
}