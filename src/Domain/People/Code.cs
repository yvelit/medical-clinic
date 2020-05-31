using System;
using Core.Extensions;

namespace Domain.People
{
    public abstract class Code
    {
        public Code(string value)
        {
            Value = value;
        }

        public string Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        #region Value

        private string _value;

        private string GetValue()
        {
            return _value;
        }

        private void SetValue(string value)
        {
            Validate(value);

            _value = value.ToLowerInvariant();
        }

        #endregion Value

        protected virtual void Validate(string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ArgumentException("Value cannot be null or empty or blank");
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Code other && Equals(other);
        }

        private bool Equals(Code other)
        {
            return Value.ToLowerInvariant() == other.Value.ToLowerInvariant();
        }

        public override int GetHashCode()
        {
            return Value.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
