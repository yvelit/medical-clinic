namespace Core.DataStructures
{
    public class QueueItem<T>
    {
        public QueueItem(T data, QueueItem<T> next)
        {
            Data = data;
            Next = next;
        }

        public QueueItem(T data) : this(data, null)
        {
        }

        public T Data
        {
            get { return GetData(); }
            private set { SetData(value); }
        }

        #region Data

        private T _data;

        public T GetData()
        {
            return _data;
        }

        private void SetData(T value)
        {
            _data = value;
        }

        #endregion Data

        public QueueItem<T> Next
        {
            get { return GetNext(); }
            set { SetNext(value); }
        }

        #region Next

        private QueueItem<T> _next;

        public QueueItem<T> GetNext()
        {
            return _next;
        }

        public void SetNext(QueueItem<T> value)
        {
            _next = value;
        }

        #endregion Next
    }
}
