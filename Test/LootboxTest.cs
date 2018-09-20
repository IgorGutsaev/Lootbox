using Lootbox.Abstractions;
using Lootbox.Core;
using Xunit;

namespace Lootbox.Test
{
    public class LootboxTest
    {
        [Theory]
        [MemberData(nameof(ScLootboxToolkit.ScLootboxSet), MemberType = typeof(ScLootboxToolkit))]
        public void Test_GenerateSet(ScLootbox lootbox)
        {
            var ser = new LootboxSerializer<ScLootbox, ScFraction, ScSlot>(
                new JsonLootboxSerializeStrategy<ScFraction, ScSlot>());

            string serialized = ser.Serialize(lootbox);
            var des = ser.Deserialize(serialized);
        }
    }
}
