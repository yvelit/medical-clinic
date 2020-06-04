using System;
using System.Collections.Generic;
using System.Linq;

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
                throw new ArgumentException("Data não pode ser nula.");
            }

            var key = data.GetHashCode();

            if (Exist(key))
            {
                throw new InvalidOperationException("Um elemento com o mesmo identificador já existe.");
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
                throw new InvalidOperationException("Elemento não existe.");
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

        public T[] ToArray()
        {
            return _table.Values.ToArray();
        }
    }
}
