namespace Lootbox.Abstractions
{
    public class LootboxDefaultSerializer<Tl, Tf, Ts> : ILootboxSerializer<Tl>
        where Tl : Lootbox<Tf, Ts>, new()
        where Tf : Fraction<Ts>, new()
        where Ts : Slot<Ts>, new()
    {       
        public readonly ILootboxSerializeStrategy<Tf, Ts> _strategy;

        public LootboxDefaultSerializer(ILootboxSerializeStrategy<Tf, Ts> strategy)
        {
            _strategy = strategy;
        }

        public Tl Deserialize(string value)
        {
            return _strategy.Deserialize<Tl>(value);
        }

        public string Serialize(Tl lootbox)
        {
            return _strategy.Serialize<Tl>(lootbox);
        }
    }
}
