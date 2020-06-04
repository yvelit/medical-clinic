using System;

namespace Core.DataStructures
{
    public class List<T>
    {
        protected Item<T> _first;
        protected Item<T> _last;

        public List()
        {
            _first = new Item<T>(default);
            _last = _first;
        }

        public void Add(T data)
        {
            var newItem = new Item<T>(data);

            var aux = _last;
            aux.Next = newItem;

            _last = newItem;
            _last.Previous = aux;
        }

        public T Remove(T data)
        {
            var aux = _first.Next;

            while (aux != null)
            {
                if (aux.Data.Equals(data))
                {
                    var previous = aux.Previous;
                    var next = aux.Next;

                    previous.Next = next;

                    if (next != null)
                    {
                        next.Previous = previous;
                    }

                    if (aux.Equals(_last))
                    {
                        _last = previous;
                    }

                    return aux.Data;
                }

                aux = aux.Next;
            }

            throw new InvalidOperationException("Elemento não existe.");
        }

        public T Find(T data)
        {
            var aux = _first.Next;

            while (aux != null)
            {
                if (aux.Data.Equals(data))
                {
                    return aux.Data;
                }

                aux = aux.Next;
            }

            return default;
        }

        public bool Exist(T data)
        {
            var result = Find(data);

            return !result.Equals(default(T));
        }

        public int Count()
        {
            var count = 0;

            ForEach(t => count++);

            return count;
        }

        public bool IsEmpty()
        {
            return _first == _last;
        }

        public void ForEach(Action<T> action)
        {
            var aux = _first.Next;

            while (aux != null)
            {
                action?.Invoke(aux.Data);

                aux = aux.Next;
            }
        }

        public T First()
        {
            return _first.Next.Data;
        }

        public T[] ToArray()
        {
            var array = new T[Count()];

            var count = 0;

            ForEach(x =>
            {
                array[count] = x;
                count++;
            });

            return array;
        }
    }
}
