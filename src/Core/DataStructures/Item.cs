namespace Core.DataStructures
{
    public class Item<T>
    {
        public Item(T data, Item<T> next, Item<T> previous)
        {
            Data = data;
            Next = next;
            Previous = previous;
        }

        public Item(T data) : this(data, null, null)
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

        public Item<T> Next
        {
            get { return GetNext(); }
            set { SetNext(value); }
        }

        #region Next

        private Item<T> _next;

        public Item<T> GetNext()
        {
            return _next;
        }

        public void SetNext(Item<T> value)
        {
            _next = value;
        }

        #endregion Next

        public Item<T> Previous
        {
            get
            {
                return GetPrevious();
            }
            set
            {
                SetPrevious(value);
            }
        }

        #region Previous

        private Item<T> _previous;

        private Item<T> GetPrevious()
        {
            return _previous;
        }

        private void SetPrevious(Item<T> value)
        {
            _previous = value;
        }

        #endregion Previous
    }
}
