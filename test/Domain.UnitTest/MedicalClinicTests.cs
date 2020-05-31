using System;
using Domain.MedicalAppointments;
using Domain.People.Customers;
using Domain.People.Doctors;
using Xunit;

namespace Domain.UnitTest
{
    public static class MedicalClinicTests
    {
        [Fact(DisplayName = "Deve criar uma clínica médica válida")]
        public static void Should_create_a_valid_medical_clinic()
        {
            // arrange & act & assert
            new MedicalClinic();
        }

        [Fact(DisplayName = "Deve adicionar um cliente na clínica médica")]
        public static void Should_add_a_customer_in_medical_clinic()
        {
            // arrange
            var medicalClinic = new MedicalClinic();

            // act
            medicalClinic.AddCustomer((Cpf)"123456789-10", "name", CustomerType.Normal);

            // assert
            Assert.Equal(1, medicalClinic.CountCustomer());
        }

        [Fact(DisplayName = "Deve lançar exceção ao adicionar um cliente já existente na clínica médica")]
        public static void Should_throw_exception_when_add_an_existent_customer_in_medical_clinic()
        {
            // arrange
            var medicalClinic = new MedicalClinic();
            var cpf = (Cpf)"123456789-10";
            var name = "name";
            var customerType = CustomerType.Normal;
            medicalClinic.AddCustomer(cpf, name, customerType);

            // act & assert
            Assert.Throws<InvalidOperationException>(() => medicalClinic.AddCustomer(cpf, name, customerType));
        }

        [Fact(DisplayName = "Deve adicionar um médico na clínica médica")]
        public static void Should_add_a_doctor_in_medical_clinic()
        {
            // arrange
            var medicalClinic = new MedicalClinic();

            // act
            medicalClinic.AddDoctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);

            // assert
            Assert.Equal(1, medicalClinic.CountDoctor());
        }

        [Fact(DisplayName = "Deve lançar exceção ao adicionar um médico já existente na clínica médica")]
        public static void Should_throw_exception_when_add_an_existent_doctor_in_medical_clinic()
        {
            // arrange
            var medicalClinic = new MedicalClinic();
            var crm = (Crm)"12345";
            var name = "name";
            var medicalSpecialty = MedicalSpecialty.GeneralClinic;
            medicalClinic.AddDoctor(crm, name, medicalSpecialty);

            // act & assert
            Assert.Throws<InvalidOperationException>(() => medicalClinic.AddDoctor(crm, name, medicalSpecialty));
        }

        [Fact(DisplayName = "Deve adicionar uma consulta médica na clínica")]
        public static void Should_add_a_medical_appointment_in_medical_clinic()
        {
            // arrange
            var medicalClinic = new MedicalClinic();
            var cpf = (Cpf)"123456789-10";
            medicalClinic.AddCustomer(cpf, "name", CustomerType.Normal);
            var crm = (Crm)"12345";
            medicalClinic.AddDoctor(crm, "name", MedicalSpecialty.Orthopedy);

            // act
            medicalClinic.AddMedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand, cpf, crm);

            // assert
            Assert.Equal(1, medicalClinic.CountMedicalAppointment());
        }

        [Fact(DisplayName = "Deve lançar exceção ao adicionar uma consulta médica já existente na clínica médica")]
        public static void Should_throw_exception_when_add_an_existent_medical_appointment_in_medical_clinic()
        {
            // arrange
            var medicalClinic = new MedicalClinic();
            var cpf = (Cpf)"123456789-10";
            medicalClinic.AddCustomer(cpf, "name", CustomerType.Normal);
            var crm = (Crm)"12345";
            medicalClinic.AddDoctor(crm, "name", MedicalSpecialty.Orthopedy);
            var date = DateTime.Now;
            medicalClinic.AddMedicalAppointment(date, MedicalAppointmentType.OnDemand, cpf, crm);

            // act & assert
            Assert.Throws<InvalidOperationException>(() => medicalClinic.AddMedicalAppointment(date, MedicalAppointmentType.OnDemand, cpf, crm));
        }

        [Fact(DisplayName = "Deve lançar exceção ao recuperar um cliente inexistente")]
        public static void Should_throw_exception_when_get_an_inexistent_customer()
        {
            // arrange
            var medicalClinic = new MedicalClinic();

            // act & assert
            Assert.Throws<InvalidOperationException>(() => medicalClinic.GetCustomer((Cpf)"123456789-10"));
        }

        [Fact(DisplayName = "Deve lançar exceção ao recuperar um médico inexistente")]
        public static void Should_throw_exception_when_get_an_inexistent_doctor()
        {
            // arrange
            var medicalClinic = new MedicalClinic();

            // act & assert
            Assert.Throws<InvalidOperationException>(() => medicalClinic.GetDoctor((Crm)"12345"));
        }
    }
}
