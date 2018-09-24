using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Lootbox.Abstractions
{
    public abstract class Lootbox<Tf, T> : ILootbox<Tf, T>
        where Tf: Fraction<T>, new()
        where T : Slot<T>, new()
    {
        public FractionCollection<Tf, T> Fractions { get; private set; } = new FractionCollection<Tf, T>();

        public string Version { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}