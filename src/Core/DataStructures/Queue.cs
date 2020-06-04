namespace Core.DataStructures
{
    public class Queue<T>
    {
        protected Item<T> _first;
        protected Item<T> _last;

        public Queue()
        {
            _first = new Item<T>(default);
            _last = _first;
        }

        public void Enqueue(T data)
        {
            var newQueueItem = new Item<T>(data);

            _last.Next = newQueueItem;
            _last = newQueueItem;
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                return default;
            }

            var aux = _first.Next;
            _first.Next = aux.Next;

            if (aux == _last)
            {
                _last = _first;
            }

            return aux.Data;
        }

        public T Peek()
        {
            if (IsEmpty())
            {
                return default;
            }

            return _first.Next.Data;
        }

        public int Count()
        {
            int count = 0;

            var aux = _first.Next;
            while (aux != null)
            {
                aux = aux.Next;
                count++;
            }

            return count;
        }

        public bool IsEmpty()
        {
            return _first == _last;
        }
    }
}
