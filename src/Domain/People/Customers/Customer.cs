﻿using System;
using Domain.MedicalAppointments;
using Domain.People.Customers;

namespace Domain.People
{
    public class Customer : Person
    {
        public Customer(Cpf cpf, string name, CustomerType customerType) : base(cpf, name)
        {
            CustomerType = customerType;
        }

        public CustomerType CustomerType
        {
            get { return GetCustomerType(); }
            set { SetCustomerType(value); }
        }

        #region CustomerType

        private CustomerType _customerType;

        public CustomerType GetCustomerType()
        {
            return _customerType;
        }

        public void SetCustomerType(CustomerType value)
        {
            _customerType = value;
        }

        #endregion CustomerType

        protected override decimal GetMedicalAppointmentValue(MedicalAppointment medicalAppointment)
        {
            switch (medicalAppointment.Customer.CustomerType)
            {
                case CustomerType.Normal:
                    return medicalAppointment.Value;

                case CustomerType.Copartiction:
                    return medicalAppointment.Value * 0.5m;

                case CustomerType.HealthInsurance:
                    return 0;

                default:
                    throw new InvalidOperationException("Customer type not implemented yet.");
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Customer other && Equals(other);
        }

        private bool Equals(Customer other)
        {
            return CustomerType == other.CustomerType && base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString().Replace("Código", "CPF");
        }
    }
}