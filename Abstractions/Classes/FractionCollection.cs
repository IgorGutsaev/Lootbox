using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lootbox.Abstractions
{
    public class FractionCollection<T> : ICollection<Fraction<T>>
        where T: Slot<T>, new()
    {
        private ICollection<Fraction<T>> _items = new List<Fraction<T>>();

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public Fraction<T> this[string identifier]
        {
            get { return _items.FirstOrDefault(f => string.Equals(identifier, f.Identifier, StringComparison.InvariantCultureIgnoreCase)); }
        }

        public void Add(Fraction<T> item)
        {
            _items.Add(item);
        }

        public FractionCollection< T> AppendFraction
            (Fraction<T> item)
        {
            if (!_items.Any(f => string.Equals(f.Identifier, item.Identifier, StringComparison.InvariantCultureIgnoreCase)))
                _items.Add(item);

            return this;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(Fraction<T> item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(Fraction<T>[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Fraction<T>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public bool Remove(Fraction<T> item)
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
