using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public interface ILootboxSerializer<T>
    {
        string Serialize(T lootbox);

        T Deserialize(string value);
    }
}
