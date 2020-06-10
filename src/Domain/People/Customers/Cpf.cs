using System;
using System.Text.RegularExpressions;

namespace Domain.People.Customers
{
    //Classe com identificador de cliente e verificação de validade com base em regex
    public class Cpf : Code
    {
        private const string CPF_REGEX = @"^\d{9}\-\d{2}$";

        private static readonly Regex _cpfRegex;

        static Cpf()
        {
            _cpfRegex = new Regex(CPF_REGEX);
        }

        public static explicit operator Cpf(string value)
        {
            return new Cpf(value);
        }

        public Cpf(string value) : base(value)
        {
        }

        protected override void Validate(string value)
        {
            base.Validate(value);
            if (!_cpfRegex.IsMatch(value))
            {
                throw new ArgumentException("CPF não está no formato: 'XXXXXXXXX-XX'.");
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Cpf other && base.Equals(other);
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
