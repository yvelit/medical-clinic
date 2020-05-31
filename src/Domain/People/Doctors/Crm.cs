using System;
using System.Text.RegularExpressions;

namespace Domain.People.Doctors
{
    public class Crm : Code
    {
        private const string CRM_REGEX = @"^\d{5}$";

        static Crm()
        {
            _crmRegex = new Regex(CRM_REGEX);
        }

        private static readonly Regex _crmRegex;

        public static explicit operator Crm(string value)
        {
            return new Crm(value);
        }

        public Crm(string value) : base(value)
        {
        }

        protected override void Validate(string value)
        {
            base.Validate(value);

            if (!_crmRegex.IsMatch(value))
            {
                throw new ArgumentException("CRM is not in format 'XXXXX'.");
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Crm other && base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
