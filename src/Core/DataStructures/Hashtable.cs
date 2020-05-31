using System;
using System.Collections.Generic;

namespace Core.DataStructures
{
    public class Hashtable<T>
    {
        private readonly IDictionary<int, T> _table;

        public Hashtable()
        {
            _table = new Dictionary<int, T>();
        }

        public void Add(T data)
        {
            if (data.Equals(default(T)))
            {
                throw new ArgumentException("Data cannot be null.");
            }

            var key = data.GetHashCode();

            if (Exist(key))
            {
                throw new InvalidOperationException("An element with the same key already exists.");
            }

            _table.Add(key, data);
        }

        public T Remove(T data)
        {
            var key = data.GetHashCode();

            return Remove(key);
        }

        public T Remove(int key)
        {
            if (!Exist(key))
            {
                throw new InvalidOperationException("Elemend does not exist.");
            }

            var value = _table[key];

            _table.Remove(key);

            return value;
        }

        public bool Exist(T data)
        {
            var key = data.GetHashCode();

            return Exist(key);
        }

        public bool Exist(int key)
        {
            return _table.ContainsKey(key);
        }

        public T Find(int key)
        {
            try
            {
                return _table[key];
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
    }
}
