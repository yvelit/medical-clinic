using System;
using System.Linq;
using Core.DataStructures;
using Core.Extensions;
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
        private readonly Queue<Crm>[] _lastDoctorAppointments;

        public MedicalClinic()
        {
            _doctors = new Hashtable<Doctor>();
            _customers = new Hashtable<Customer>();
            _medicalAppointments = new Hashtable<MedicalAppointment>();
            _lastDoctorAppointments = new Queue<Crm>[10];
            for (int i = 0; i < _lastDoctorAppointments.Length; i++)
            {
                _lastDoctorAppointments[i] = new Queue<Crm>();
            }
        }

        public void AddCustomer(Cpf cpf, string name, CustomerType customerType = CustomerType.Normal)
        {
            var customer = new Customer(cpf, name, customerType);

            if (_customers.Exist(customer))
            {
                throw new InvalidOperationException("Cliente já existe.");
            }

            _customers.Add(customer);
        }

        public void AddDoctor(Crm crm, string name, MedicalSpecialty medicalSpecialty)
        {
            var doctor = new Doctor(crm, name, medicalSpecialty);

            if (_doctors.Exist(doctor))
            {
                throw new InvalidOperationException("Médico já existe.");
            }

            _lastDoctorAppointments[(int)medicalSpecialty].Add(crm);
            _doctors.Add(doctor);
        }

        public void AddMedicalAppointment(Cpf cpf, MedicalAppointmentType medicalAppointmentType, MedicalSpecialty medicalSpecialty, DateTime date)
        {
            var customer = GetCustomer(cpf);
            Crm crm = _lastDoctorAppointments[(int)medicalSpecialty].Remove();
            _lastDoctorAppointments[(int)medicalSpecialty].Add(crm);
            var doctor = GetDoctor(crm);

            var medicalAppointment = new MedicalAppointment(date, medicalAppointmentType, customer, doctor);

            if (_medicalAppointments.Exist(medicalAppointment))
            {
                throw new InvalidOperationException("Consulta médica já existe.");
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
                throw new InvalidOperationException("Cliente não existe.");
            }

            return _customers.Find(key);
        }

        public Doctor GetDoctor(Crm crm)
        {
            var key = crm.GetHashCode();

            if (!_doctors.Exist(key))
            {
                throw new InvalidOperationException("Médico não existe.");
            }

            return _doctors.Find(key);
        }

        public MedicalAppointment[] GetMedicalAppointments(DateTime date, MedicalSpecialty type)
        {
            var medicalAppointments = _medicalAppointments.ToArray();
            var sortedMedicalAppointments = medicalAppointments.MergeSort();

            return sortedMedicalAppointments
                .Where(x => x.Date == date && x.Doctor.MedicalSpecialty == type)
                .ToArray()
                ;
        }

        public Doctor[] GetDoctors()
        {
            var doctors = _doctors.ToArray();

            return doctors.MergeSortDescending().ToArray();
        }

        public int CountCustomer()
        {
            return _customers.Count();
        }
///home/victor/Área de Trabalho

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
