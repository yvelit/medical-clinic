using System;
using Core.DataStructures;
using Domain.MedicalAppointments;
using Domain.People;
using Domain.People.Customers;
using Domain.People.Doctors;

namespace Domain
{
    public class MedicalClinic
    {
        private readonly Hashtable<Doctor> _doctors;
        private readonly Hashtable<Customer> _customers;
        private readonly Hashtable<MedicalAppointment> _medicalAppointments;

        public MedicalClinic()
        {
            _doctors = new Hashtable<Doctor>();
            _customers = new Hashtable<Customer>();
            _medicalAppointments = new Hashtable<MedicalAppointment>();
        }

        public void AddCustomer(Cpf cpf, string name, CustomerType customerType)
        {
            var customer = new Customer(cpf, name, customerType);

            if (_customers.Exist(customer))
            {
                throw new InvalidOperationException("Customer already exists.");
            }

            _customers.Add(customer);
        }

        public void AddDoctor(Crm crm, string name, MedicalSpecialty medicalSpecialty)
        {
            var doctor = new Doctor(crm, name, medicalSpecialty);

            if (_doctors.Exist(doctor))
            {
                throw new InvalidOperationException("Doctor already exists.");
            }

            _doctors.Add(doctor);
        }

        public void AddMedicalAppointment(DateTime date, MedicalAppointmentType medicalAppointmentType, Cpf cpf, Crm crm)
        {
            var customer = GetCustomer(cpf);
            var doctor = GetDoctor(crm);

            var medicalAppointment = new MedicalAppointment(date, medicalAppointmentType, customer, doctor);

            if (_medicalAppointments.Exist(medicalAppointment))
            {
                throw new InvalidOperationException("Medical appointment already exists.");
            }

            customer.AddMedicalAppointment(medicalAppointment);
            doctor.AddMedicalAppointment(medicalAppointment);

            _medicalAppointments.Add(medicalAppointment);
        }

        public Customer GetCustomer(Cpf cpf)
        {
            var key = cpf.GetHashCode();

            if (!_customers.Exist(key))
            {
                throw new InvalidOperationException("Customer does not exist");
            }

            return _customers.Find(key);
        }

        public Doctor GetDoctor(Crm crm)
        {
            var key = crm.GetHashCode();

            if (!_doctors.Exist(key))
            {
                throw new InvalidOperationException("Doctor does not exist");
            }

            return _doctors.Find(key);
        }

        public int CountCustomer()
        {
            return _customers.Count();
        }

        public int CountDoctor()
        {
            return _doctors.Count();
        }

        public int CountMedicalAppointment()
        {
            return _medicalAppointments.Count();
        }
    }
}
