namespace Lootbox.Abstractions
{
    public class LootboxDefaultSerializer<Tf, Ts> : ILootboxSerializer<ILootbox<Tf, Ts>>
        where Tf : Fraction<Ts>, new()
        where Ts : Slot<Ts>, new()
    {       
        public readonly ILootboxSerializeStrategy<Tf, Ts> _strategy;

        public LootboxDefaultSerializer(ILootboxSerializeStrategy<Tf, Ts> strategy)
        {
            _strategy = strategy;
        }

        public Tl Deserialize<Tl>(string value)
            where Tl : new()
        {
            return _strategy.Deserialize<Tl>(value);
        }

        public string Serialize(ILootbox<Tf, Ts> lootbox)
        {
            return _strategy.Serialize(lootbox);
        }
    }
}
