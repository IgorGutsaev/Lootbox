using Lootbox.Abstractions;

namespace Lootbox.Core
{
    public class ScLootboxBuilder : LootboxBuilder<ScSlot>
    {
        public ScLootboxBuilder()
        {
            box = new ScLootbox();
        }
    }
}