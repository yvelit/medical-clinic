using System;
using Domain.MedicalAppointments;
using Domain.People;
using Domain.People.Customers;
using Domain.People.Doctors;
using Xunit;

namespace Domain.UnitTest.People.Doctors
{
    public static class DoctorTests
    {
        [Fact(DisplayName = "Deve criar um médico válido")]
        public static void Should_create_a_valid_doctor()
        {
            // arrange
            var code = (Crm)"12345";
            var name = "name";
            var medicalSpecialty = MedicalSpecialty.GeneralClinic;

            // act
            var doctor = new Doctor(code, name, medicalSpecialty);

            // assert
            Assert.Equal(code, doctor.Code);
            Assert.Equal(name, doctor.Name);
            Assert.Equal(medicalSpecialty, doctor.MedicalSpecialty);
            Assert.True(doctor.MedicalAppointments.IsEmpty());
        }

        [Theory(DisplayName = "Deve lançar uma exceção ao tentar criar uma médico inválido")]
        [InlineData(null, "name")]
        [InlineData("abcde", "name")]
        [InlineData("123", "name")]
        [InlineData("12345", null)]
        public static void Should_throw_a_exception_when_try_create_a_invalid_doctor(string code, string name)
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new Doctor((Crm)code, name, MedicalSpecialty.GeneralClinic));
        }

        [Fact(DisplayName = "Deve adicionar uma consulta médica")]
        public static void Should_add_a_medical_appointment()
        {
            // arrange
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var customer = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);
            var medicalAppointment = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);

            // act
            doctor.AddMedicalAppointment(medicalAppointment);

            // assert
            Assert.True(!doctor.MedicalAppointments.IsEmpty());
            Assert.True(doctor.MedicalAppointments.Count() == 1);
            Assert.Equal(doctor.Code, doctor.MedicalAppointments.First().DoctorCode);
        }

        [Fact(DisplayName = "Deve lançar uma exceção ao adicionar uma consulta médica nula")]
        public static void Should_throw_a_exception_when_add_a_null_medical_appointment()
        {
            // arrange
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => doctor.AddMedicalAppointment(null));
        }

        [Fact(DisplayName = "Deve transformar uma médico sem consultas médicas em uma string")]
        public static void Should_parse_a_doctor_without_medical_appointments_into_a_string()
        {
            // arrange
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);

            // act
            var result = doctor.ToString();

            // assert
            Assert.Equal("CRM: 12345 - Nome: name - Consultas: [] - Valor Total: 0 - Especialidade médica: Clínica geral", result);
        }

        [Fact(DisplayName = "Deve transformar uma médico com consultas médicas em uma string")]
        public static void Should_parse_a_doctor_with_medical_appointments_into_a_string()
        {
            // arrange
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var customer = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);
            var medicalAppointment1 = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);
            var medicalAppointment2 = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);
            doctor.AddMedicalAppointment(medicalAppointment1);
            doctor.AddMedicalAppointment(medicalAppointment2);

            // act
            var result = doctor.ToString();

            // assert
            Assert.Equal($"CRM: 12345 - Nome: name - Consultas: [{medicalAppointment1},{medicalAppointment2}] - Valor Total: 128 - Especialidade médica: Clínica geral", result);
        }

        [Fact(DisplayName = "Deve retornar verdadeiro ao comparar dois médicos iguais")]
        public static void Should_return_true_when_compare_two_equals_doctors()
        {
            // arrange
            var doctor1 = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var doctor2 = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);

            // act
            var result = doctor1.Equals(doctor2);

            // assert
            Assert.True(result);
        }

        [Theory(DisplayName = "Deve retornar falso ao comparar dois médicos diferentes")]
        [InlineData("12345", "n", MedicalSpecialty.GeneralClinic)]
        [InlineData("54321", "name", MedicalSpecialty.GeneralClinic)]
        [InlineData("12345", "name", MedicalSpecialty.Orthopedy)]
        public static void Should_return_true_when_compare_two_different_doctors(string code, string name, MedicalSpecialty medicalSpecialty)
        {
            // arrange
            var doctor1 = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var doctor2 = new Doctor((Crm)code, name, medicalSpecialty);

            // act
            var result = doctor1.Equals(doctor2);

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Deve retornar falso ao comparar um médico com um não médico")]
        public static void Should_return_false_when_compare_a_doctor_with_a_non_doctor()
        {
            // arrange
            var doctor1 = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);

            // act
            var result = doctor1.Equals(new { });

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "HashCode deve ser o crm")]
        public static void HashCode_should_be_crm()
        {
            // arrange
            var code = (Crm)"12345";
            var doctor = new Doctor(code, "name", MedicalSpecialty.GeneralClinic);

            // act
            var result = doctor.GetHashCode();

            // assert
            Assert.Equal(code.GetHashCode(), result);
        }
    }
}
