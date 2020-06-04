using System;

namespace Core.DataStructures
{
    public class Queue<T>
    {
        protected QueueItem<T> _first;
        protected QueueItem<T> _last;

        public Queue()
        {
            _first = new QueueItem<T>(default);
            _last = _first;
        }

        public void Add(T data)
        {
            var newQueueItem = new QueueItem<T>(data);

            _last.Next = newQueueItem;
            _last = newQueueItem;
        }

        public T Remove()
        {
            if(_first == _last) return default;
            QueueItem<T> result = _first.Next;
            _first.Next = result.Next;

            if (result == _last)
            {
                _last = _first;
            }
            
            return result.Data;
        }
    }
}
