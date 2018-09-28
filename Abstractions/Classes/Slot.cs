using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public abstract class Slot<T> where T : Slot<T>, new()
    {
        [JsonIgnore]
        public Fraction<T> Parent { get; private set; } = null;

        public void SetParent(Fraction<T> parent)
        {
            Parent = parent;
        }
    }
}
