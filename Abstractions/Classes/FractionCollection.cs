using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lootbox.Abstractions
{
    public class FractionCollection<Tf, T> : ICollection<Tf>
        where Tf: Fraction<T>
        where T: Slot<T>, new()
    {
        private ICollection<Tf> _items = new List<Tf>();

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public Fraction<T> this[string identifier]
        {
            get { return _items.FirstOrDefault(f => string.Equals(identifier, f.Identifier, StringComparison.InvariantCultureIgnoreCase)); }
        }

        public void Add(Tf item)
        {
            _items.Add(item);
        }

        public FractionCollection<Tf, T> AppendFraction
            (Fraction<T> item)
        {
            if (!_items.Any(f => string.Equals(f.Identifier, item.Identifier, StringComparison.InvariantCultureIgnoreCase)))
                _items.Add((Tf)item);

            return this;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(Tf item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(Tf[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Tf> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public bool Remove(Tf item)
        {
            return _items.Remove(item);
        }

        public override string ToString()
        {
            return _items.Count.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
