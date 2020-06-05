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
        private readonly Hashtable<Crm,Doctor> _doctors;
        private readonly Hashtable<Cpf,Customer> _customers;
        private readonly Hashtable<DateTime,Hashtable<MedicalAppointment, MedicalAppointment>> _medicalAppointmentHashs;
        private readonly Queue<Crm>[] _lastDoctorAppointments;

        public MedicalClinic()
        {
            _doctors = new Hashtable<Crm,Doctor>();
            _customers = new Hashtable<Cpf,Customer>();
            _medicalAppointmentHashs = new Hashtable<DateTime, Hashtable<MedicalAppointment, MedicalAppointment>>();
            _lastDoctorAppointments = new Queue<Crm>[10];
            for (int i = 0; i < _lastDoctorAppointments.Length; i++)
            {
                _lastDoctorAppointments[i] = new Queue<Crm>();
            }
        }

        public void AddCustomer(Cpf cpf, string name, CustomerType customerType = CustomerType.Normal)
        {
            if (_customers.Exist(cpf))
            {
                throw new InvalidOperationException("Cliente já existe.");
            }

            var customer = new Customer(cpf, name, customerType);

            _customers.Add(cpf,customer);
        }

        public void AddDoctor(Crm crm, string name, MedicalSpecialty medicalSpecialty)
        {
            if (_doctors.Exist(crm))
            {
                throw new InvalidOperationException("Médico já existe.");
            }

            var doctor = new Doctor(crm, name, medicalSpecialty);


            _lastDoctorAppointments[(int)medicalSpecialty].Enqueue(crm);
            _doctors.Add(crm,doctor);
        }

        public void AddMedicalAppointment(Cpf cpf, MedicalAppointmentType medicalAppointmentType, MedicalSpecialty medicalSpecialty, DateTime date)
        {
            var customer = GetCustomer(cpf);
            Crm crm = _lastDoctorAppointments[(int)medicalSpecialty].Dequeue();
            _lastDoctorAppointments[(int)medicalSpecialty].Enqueue(crm);
            var doctor = GetDoctor(crm);

            var medicalAppointment = new MedicalAppointment(date, medicalAppointmentType, customer, doctor);

            if (!_medicalAppointmentHashs.Exist(date))
            {
                _medicalAppointmentHashs.Add(date, new Hashtable<MedicalAppointment, MedicalAppointment>());
            }

            var medicalAppointments = _medicalAppointmentHashs.Find(date);

            if (medicalAppointments.Exist(medicalAppointment))
            {
                throw new InvalidOperationException("Consulta médica já existe.");
            }

            customer.AddMedicalAppointment(medicalAppointment);
            doctor.AddMedicalAppointment(medicalAppointment);

            medicalAppointments.Add(medicalAppointment,medicalAppointment);
        }

        public Customer GetCustomer(Cpf cpf)
        {
            if (!_customers.Exist(cpf))
            {
                throw new InvalidOperationException("Cliente não existe.");
            }

            return _customers.Find(cpf);
        }

        public Doctor GetDoctor(Crm crm)
        {
            if (!_doctors.Exist(crm))
            {
                throw new InvalidOperationException("Médico não existe.");
            }

            return _doctors.Find(crm);
        }

        public MedicalAppointment[] GetMedicalAppointments(DateTime date, MedicalSpecialty type)
        {
            if (!_medicalAppointmentHashs.Exist(date))
            {
                return new List<MedicalAppointment>().ToArray();
            }

            var medicalAppointments = _medicalAppointmentHashs.Find(date);

            return medicalAppointments
                .ToArray()
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
            var count = 0;

            foreach (var medicalAppointments in _medicalAppointmentHashs.ToArray())
            {
                count += medicalAppointments.Count();
            }

            return count;
        }
    }
}
