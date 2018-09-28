using Lootbox.Abstractions;
using Lootbox.Core.JsonConverters;
using Newtonsoft.Json;

namespace Lootbox.Core
{
    public class ScLootbox : Lootbox<ScFraction, ScSlot>
    {
        public override string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new ScSlotConverter());
        }
    }
}