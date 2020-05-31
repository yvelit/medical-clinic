using System;
using Domain.MedicalAppointments;
using Domain.People;
using Domain.People.Customers;
using Domain.People.Doctors;
using Xunit;

namespace Domain.UnitTest.MedicalAppointments
{
    public static class MedicalAppointmentTests
    {
        [Fact(DisplayName = "Deve criar uma consulta médico válida")]
        public static void Should_create_a_valid_medical_appointment()
        {
            // arrange 
            var date = DateTime.Now;
            var medicalAppointmentType = MedicalAppointmentType.OnDemand;
            var customer = new Customer((Cpf)"123456789-10", "name", CustomerType.Normal);
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            // act
            var medicalAppointment = new MedicalAppointment(date, medicalAppointmentType,customer,doctor);

            // assert
            Assert.Equal(date, medicalAppointment.Date);
            Assert.Equal(medicalAppointmentType, medicalAppointment.MedicalAppointmentType);
            Assert.NotNull(medicalAppointment.Customer);
            Assert.NotNull(medicalAppointment.CustomerCode);
            Assert.NotNull(medicalAppointment.Doctor);
            Assert.NotNull(medicalAppointment.DoctorCode);
        }

        [Fact(DisplayName = "Deve lançar exceção ao criar uma relação entre consulta médica e um médico nulo")]
        public static void Should_throw_exception_when_create_a_relationship_between_medical_appointment_and_null_doctor()
        {
            // arrange 
            var customer = new Customer((Cpf)"123456789-10", "name", CustomerType.Normal);

            // act & assert
            Assert.Throws<ArgumentException>(() => new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand, customer, null));
        }

        [Fact(DisplayName = "Deve lançar exceção ao criar uma relação entre consulta médica e um cliente nulo")]
        public static void Should_throw_exception_when_create_a_relationship_between_medical_appointment_and_null_customer()
        {
            // arrange 
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);

            // act & assert
            Assert.Throws<ArgumentException>(() => new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand, null, doctor));
        }

        [Theory(DisplayName = "Deve recuperar o valor da consulta médica")]
        [InlineData(1,MedicalAppointmentType.OnDemand)]
        [InlineData(0.9,MedicalAppointmentType.Scheduled)]
        public static void Should_get_medical_appoitment_value(decimal multiplier,MedicalAppointmentType type)
        {
            // arrange 
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var customer = new Customer((Cpf)"123456789-10", "name", CustomerType.Normal);
            var medicalAppointment = new MedicalAppointment(DateTime.Now, type,customer,doctor);

            // act
            var result = medicalAppointment.Value;

            // assert
            Assert.Equal(doctor.MedicalSpecialtyValue * multiplier, result);
        }
    }
}
