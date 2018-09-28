using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public interface ILootboxBinder
    {
        bool Bind(ILootboxBindableObject data);
    }
}
