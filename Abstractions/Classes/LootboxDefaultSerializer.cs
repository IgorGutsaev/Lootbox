namespace Lootbox.Abstractions
{
    public class LootboxDefaultSerializer : ILootboxSerializer
    {       
        public readonly ILootboxSerializeStrategy _strategy;

        public LootboxDefaultSerializer(ILootboxSerializeStrategy strategy)
        {
            _strategy = strategy;
        }

        public Tl Deserialize<Tl>(string value)
            where Tl : new()
        {
            return _strategy.Deserialize<Tl>(value);
        }

        public string Serialize(object lootbox)
        {
            return _strategy.Serialize(lootbox);
        }
    }
}
