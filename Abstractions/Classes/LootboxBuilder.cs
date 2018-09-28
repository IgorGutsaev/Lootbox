using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public abstract class LootboxBuilder<T>
         where T : Slot<T>, new()
    {
        protected Lootbox<T> box;

        public LootboxBuilder<T> AddFractions(Action<FractionCollection<T>> fractionsConfig)
        {
            fractionsConfig(box.Fractions);
            return this;
        }

        public LootboxBuilder<T> SetVersion(string version)
        {
            box.Version = version.Trim();
            return this;
        }

        public LootboxBuilder<T> SetStartDate(DateTime createdtime)
        {
            box.Created = createdtime;
            return this;
        }

        public Lootbox<T> Build() => box;
    }
}
