using Lootbox.Abstractions;

namespace Lootbox.Core
{
    public class ScLootboxBuilder : LootboxBuilder<ScFraction, ScSlot>
    {
        public ScLootboxBuilder()
        {
            box = new ScLootbox();
        }
    }
}