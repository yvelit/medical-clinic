using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataStructures
{
    public class Hashtable<TKey, TValue>
    {
        protected readonly IDictionary<int, TValue> _table;

        public Hashtable()
        {
            _table = new Dictionary<int, TValue>();
        }

        public void Add(TKey key, TValue value)
        {
            if (value.Equals(default(TValue)))
            {
                throw new ArgumentException("Valor não pode ser nula.");
            }

            if (Exist(key))
            {
                throw new InvalidOperationException("Um elemento com o mesmo identificador já existe.");
            }

            var k = key.GetHashCode();

            _table.Add(k, value);
        }

        public TValue Remove(TKey key)
        {
            if (!Exist(key))
            {
                throw new InvalidOperationException("Elemento não existe.");
            }

            var k = key.GetHashCode();

            var value = _table[k];

            _table.Remove(k);

            return value;
        }

        public bool Exist(TKey key)
        {
            var k = key.GetHashCode();
            return _table.ContainsKey(k);
        }

        public TValue Find(TKey key)
        {
            try
            {
                var k = key.GetHashCode();
                return _table[k];
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }

        public int Count()
        {
            return _table.Count;
        }

        public TValue[] ToArray()
        {
            return _table.Values.ToArray();
        }
    }
}
