using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public interface ILootboxSerializeStrategy<Ts>
        where Ts : Slot<Ts>, new()
    {
        string Serialize<Tl>(Tl lootbox)
            where Tl : ILootbox<Ts>;

        Tl Deserialize<Tl>(string value)
            where Tl : new();
    }
}
