using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Abstractions
{
    public abstract class LootboxBuilder<Tf, T>
         where Tf : Fraction<T>, new()
         where T : Slot<T>, new()
    {
        protected Lootbox<Tf, T> box;

        public LootboxBuilder<Tf, T> AddFractions(Action<FractionCollection<Tf, T>> fractionsConfig)
        {
            fractionsConfig(box.Fractions);
            return this;
        }

        public LootboxBuilder<Tf, T> SetVersion(string version)
        {
            box.Version = version.Trim();
            return this;
        }

        public LootboxBuilder<Tf, T> SetStartDate(DateTime createdtime)
        {
            box.Created = createdtime;
            return this;
        }

        public Lootbox<Tf, T> Build() => box;
    }
}
