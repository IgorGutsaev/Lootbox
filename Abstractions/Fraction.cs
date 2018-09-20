using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lootbox.Abstractions
{
    public abstract class Fraction<T> //: ICollection<T>
        where T : Slot<T>, new()
    {
        [JsonProperty(PropertyName = "Slots", Order = 1)]
        private ICollection<T> _items = new List<T>();

        [JsonProperty(Order = 2)]
        public string Identifier { get; set; }

        public Fraction()
        {
            _items.Clear();
        }

        [JsonIgnore]
        public int Count => _items.Count;

        [JsonIgnore]
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            item.SetParent(this);
            _items.Add(item);
        }

        public Fraction<T> AppendSlot(T item)
        {
            if (item != null)
            {
                item.SetParent(this);
                Add(item);
            }
            return this;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public override string ToString()
        {
            return Identifier;
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return _items.GetEnumerator();
        //}
    }
}
