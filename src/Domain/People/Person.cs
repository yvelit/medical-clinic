using System;
using System.Linq;
using System.Text;
using Core.DataStructures;
using Core.Extensions;
using Domain.MedicalAppointments;

namespace Domain.People
{
    public abstract class Person
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
            _code = value ?? throw new ArgumentException("Code cannot be null.");
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
                throw new ArgumentException("Name cannot be null or empty or blank.");
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

        public void AddMedicalAppointment(MedicalAppointment medicalAppointment)
        {
            if (medicalAppointment is null)
            {
                throw new ArgumentNullException("MedicalAppointment cannot be null.");
            }

            _medicalAppointments.Add(medicalAppointment);
        }

        protected abstract decimal GetMedicalAppointmentValue(MedicalAppointment medicalAppointment);

        public override string ToString()
        {
            var builder = new StringBuilder($"Código: {Code} - Nome: {Name} - Consultas: [");

            builder.Append(string.Join(",", MedicalAppointments.ToArray().Select(x => x.ToString()).ToArray()));

            builder.Append("]");

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
    }
}
