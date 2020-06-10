using System;
using System.Linq;
using System.Text;
using Core.DataStructures;
using Core.Extensions;
using Domain.MedicalAppointments;

namespace Domain.People
{
    //Classe genérica para pessoas(cliente e médico) possuindo apenas nome, identificador e uma lista de consultas
    public abstract class Person : IComparable<Person>
    {
        protected Person(Code code, string name)
        {
            Code = code;
            Name = name;
            _medicalAppointments = new List<MedicalAppointment>();
        }

        public Code Code
        {
            get { return GetCode(); }
            protected set { SetCode(value); }
        }

        #region Code

        private Code _code;

        public Code GetCode()
        {
            return _code;
        }

        public void SetCode(Code value)
        {
            _code = value ?? throw new ArgumentException("Código não pode ser nulo.");
        }

        #endregion Code

        public string Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        #region Name

        private string _name;

        public string GetName()
        {
            return _name;
        }

        public void SetName(string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ArgumentException("Nome não pode ser nulo nem vazio nem em branco.");
            }

            _name = value;
        }

        #endregion Name

        public List<MedicalAppointment> MedicalAppointments
        {
            get { return GetMedicalAppointments(); }
        }

        #region MedicalAppointments

        private readonly List<MedicalAppointment> _medicalAppointments;

        public List<MedicalAppointment> GetMedicalAppointments()
        {
            return _medicalAppointments;
        }

        #endregion MedicalAppointments

        public decimal TotalValueMedicalAppointments
        {
            get { return GetTotalValueMedicalAppointments(); }
            set { SetTotalValueMedicalAppointments(value); }
        }

        #region TotalValueMedicalAppointments

        private decimal _totalValueMedicalAppointments = 0m;

        public decimal GetTotalValueMedicalAppointments()
        {
            return _totalValueMedicalAppointments;
        }

        private void SetTotalValueMedicalAppointments(decimal value)
        {
            _totalValueMedicalAppointments = value;
        }

        #endregion TotalValueMedicalAppointments

        public void AddMedicalAppointment(MedicalAppointment medicalAppointment)
        {
            if (medicalAppointment is null)
            {
                throw new ArgumentNullException("Consulta médica não pode ser nula.");
            }
            TotalValueMedicalAppointments += GetMedicalAppointmentValue(medicalAppointment);
            _medicalAppointments.Add(medicalAppointment);
        }

        protected abstract decimal GetMedicalAppointmentValue(MedicalAppointment medicalAppointment);

        public override string ToString()
        {
            var builder = new StringBuilder($"Código: {Code} - Nome: {Name}");

            builder.Append($" - Valor Total: {TotalValueMedicalAppointments:0.##}");

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Person other && Equals(other);
        }

        private bool Equals(Person other)
        {
            return Code.Equals(other.Code) && Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public int CompareTo(Person other)
        {
            if (other == null)
            {
                return 1;
            }

            return TotalValueMedicalAppointments.CompareTo(other.TotalValueMedicalAppointments);
        }
    }
}
