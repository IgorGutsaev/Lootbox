using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Lootbox.Abstractions
{
    public abstract class Lootbox<T> : ILootbox<T>
        where T : Slot<T>, new()
    {
        public FractionCollection<T> Fractions { get; private set; } = new FractionCollection<T>();

        public string Version { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}