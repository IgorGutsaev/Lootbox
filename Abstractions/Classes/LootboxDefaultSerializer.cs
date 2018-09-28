namespace Lootbox.Abstractions
{
    public class LootboxDefaultSerializer<Ts> : ILootboxSerializer<ILootbox<Ts>>
        where Ts : Slot<Ts>, new()
    {       
        public readonly ILootboxSerializeStrategy<Ts> _strategy;

        public LootboxDefaultSerializer(ILootboxSerializeStrategy<Ts> strategy)
        {
            _strategy = strategy;
        }

        public Tl Deserialize<Tl>(string value)
            where Tl : new()
        {
            return _strategy.Deserialize<Tl>(value);
        }

        public string Serialize(ILootbox<Ts> lootbox)
        {
            return _strategy.Serialize(lootbox);
        }
    }
}
