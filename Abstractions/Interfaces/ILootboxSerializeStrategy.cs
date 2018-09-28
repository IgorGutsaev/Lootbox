using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public interface ILootboxSerializeStrategy
    {
        string Serialize(object lootbox);

        Tl Deserialize<Tl>(string value)
            where Tl : new();
    }
}
