using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public interface ILootboxSerializeStrategy<Tf, Ts>
        where Tf : Fraction<Ts>, new()
        where Ts : Slot<Ts>, new()
    {
        string Serialize<Tl>(Tl lootbox)
            where Tl : ILootbox<Tf, Ts>;

        Tl Deserialize<Tl>(string value)
            where Tl : new();
    }
}
