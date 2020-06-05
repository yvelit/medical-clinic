using System;
using Core.Extensions;
using Domain.People;
using Domain.People.Customers;
using Domain.People.Doctors;

namespace Domain.MedicalAppointments
{
    public class MedicalAppointment : ICustomerRelated, IDoctorRelated,IComparable<MedicalAppointment>
    {
        public MedicalAppointment(DateTime date, MedicalAppointmentType medicalAppointmentType, Customer customer, Doctor doctor)
        {
            Date = date;
            MedicalAppointmentType = medicalAppointmentType;
            Customer = customer;
            Doctor = doctor;
        }

        public DateTime Date
        {
            get { return GetDate(); }
            set { SetDate(value); }
        }

        #region Date

        private DateTime _date;

        public DateTime GetDate()
        {
            return _date;
        }

        public void SetDate(DateTime date)
        {
            _date = date;
        }

        #endregion Date

        public MedicalAppointmentType MedicalAppointmentType
        {
            get { return GetMedicalAppointmentType(); }
            set { SetMedicalAppointmentType(value); }
        }

        #region MedicalAppointmentType

        private MedicalAppointmentType _medicalAppointmentType;

        private MedicalAppointmentType GetMedicalAppointmentType()
        {
            return _medicalAppointmentType;
        }

        private void SetMedicalAppointmentType(MedicalAppointmentType value)
        {
            _medicalAppointmentType = value;
        }

        #endregion MedicalAppointmentType

        #region ICustomerRelated

        public Cpf CustomerCode
        {
            get { return GetCustomerCode(); }
        }

        #region CustomerCode

        public Cpf GetCustomerCode()
        {
            return Customer.Code as Cpf;
        }

        #endregion CustomerCode

        public Customer Customer
        {
            get { return GetCustomer(); }
            set { SetCustomer(value); }
        }

        #region Customer

        private Customer _customer;

        public Customer GetCustomer()
        {
            return _customer;
        }

        private void SetCustomer(Customer value)
        {
            if (value is null)
            {
                throw new ArgumentException("Cliente não pode ser nulo.");
            }

            _customer = value;
        }

        #endregion Customer

        #endregion ICustomerRelated

        #region IDoctorRelated

        public Crm DoctorCode
        {
            get { return GetDoctorCode(); }
        }

        #region DoctorCode

        public Crm GetDoctorCode()
        {
            return Doctor.Code as Crm;
        }

        #endregion DoctorCode

        public Doctor Doctor
        {
            get { return GetDoctor(); }
            set { SetDoctor(value); }
        }

        #region Doctor

        private Doctor _doctor;

        public Doctor GetDoctor()
        {
            return _doctor;
        }

        private void SetDoctor(Doctor value)
        {
            if (value is null)
            {
                throw new ArgumentException("Médico não pode ser nulo.");
            }

            _doctor = value;
        }

        #endregion Doctor

        #endregion IDoctorRelated

        public decimal Value
        {
            get { return GetValue(); }
        }

        #region Value

        private decimal GetValue()
        {
            decimal multiplier;

            switch (MedicalAppointmentType)
            {
                case MedicalAppointmentType.OnDemand:
                    multiplier = 1;
                    break;

                case MedicalAppointmentType.Scheduled:
                    multiplier = 0.9m;
                    break;

                default:
                    throw new NotImplementedException("Tipo de consulta médica ainda não implementado.");
            }

            return Doctor.MedicalSpecialtyValue * multiplier;
        }

        #endregion Value

        public override bool Equals(object obj)
        {
            return obj is MedicalAppointment other && Equals(other);
        }

        private bool Equals(MedicalAppointment other)
        {
            return Date == other.Date &&
                   MedicalAppointmentType == other.MedicalAppointmentType &&
                   CustomerCode.Equals(other.CustomerCode) &&
                   DoctorCode.Equals(other.DoctorCode) &&
                   Customer.Equals(other.Customer) &&
                   Doctor.Equals(other.Doctor);
        }

        public override int GetHashCode()
        {
            var hashCode = 825685457;
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + MedicalAppointmentType.GetHashCode();
            hashCode = hashCode * -1521134295 + CustomerCode.GetHashCode();
            hashCode = hashCode * -1521134295 + DoctorCode.GetHashCode();
            return hashCode;
        }

        public int CompareTo(MedicalAppointment other)
        {
            if (other == null)
            {
                return 1;
            }

            return Date.CompareTo(other.Date);
        }

        public override string ToString()
        {
            return $"Data: {Date} - Tipo de consulta: {MedicalAppointmentType.GetDescription()} - Cliente: {CustomerCode} - Médico: {DoctorCode} - Especialidade Médica: {Doctor.MedicalSpecialty.GetDescription()} - Valor: {Value}";
        }
    }
}
