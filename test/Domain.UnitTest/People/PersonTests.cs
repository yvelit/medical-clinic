using System;
using Domain.MedicalAppointments;
using Domain.People;
using Domain.People.Customers;
using Domain.People.Doctors;
using Domain.UnitTest.People.Fakes;
using Xunit;

namespace Domain.UnitTest.People
{
    public static class PersonTests
    {
        [Fact(DisplayName = "Deve criar uma pessoa válida")]
        public static void Should_create_a_valid_person()
        {
            // arrange
            var code = (FakeCode)"code";
            var name = "name";

            // act
            var person = new FakePerson(code, name);

            // assert
            Assert.Equal(code, person.Code);
            Assert.Equal(name, person.Name);
            Assert.True(person.MedicalAppointments.IsEmpty());
        }

        [Theory(DisplayName = "Deve lançar uma exceção ao tentar criar uma pessoa inválida")]
        [InlineData(null, "name")]
        [InlineData("code", null)]
        public static void Should_throw_a_exception_when_try_create_a_invalid_person(string code, string name)
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new FakePerson((FakeCode)code, name));
        }

        [Fact(DisplayName = "Deve adicionar uma consulta médica")]
        public static void Should_add_a_medical_appointment()
        {
            // arrange
            var person = new FakePerson((FakeCode)"code", "name");
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var customer = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);
            var medicalAppointment = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);

            // act
            person.AddMedicalAppointment(medicalAppointment);

            // assert
            Assert.True(!person.MedicalAppointments.IsEmpty());
            Assert.True(person.MedicalAppointments.Count() == 1);
        }

        [Fact(DisplayName = "Deve lançar uma exceção ao adicionar uma consulta médica nula")]
        public static void Should_throw_a_exception_when_add_a_null_medical_appointment()
        {
            // arrange
            var person = new FakePerson((FakeCode)"code", "name");

            // act & assert
            Assert.Throws<ArgumentNullException>(() => person.AddMedicalAppointment(null));
        }

        [Fact(DisplayName = "Deve transformar uma pessoa sem consultas médicas em uma string")]
        public static void Should_parse_a_person_without_medical_appointments_into_a_string()
        {
            // arrange
            var person = new FakePerson((FakeCode)"code", "name");

            // act
            var result = person.ToString();

            // assert
            Assert.Equal("Código: code - Nome: name - Consultas: []", result);
        }

        [Fact(DisplayName = "Deve transformar uma pessoa com consultas médicas em uma string")]
        public static void Should_parse_a_person_with_medical_appointments_into_a_string()
        {
            // arrange
            var person = new FakePerson((FakeCode)"code", "name");
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var customer = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);
            var medicalAppointment1 = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);
            var medicalAppointment2 = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);
            person.AddMedicalAppointment(medicalAppointment1);
            person.AddMedicalAppointment(medicalAppointment2);

            // act
            var result = person.ToString();

            // assert
            Assert.Equal($"Código: code - Nome: name - Consultas: [{medicalAppointment1},{medicalAppointment2}]", result);
        }

        [Fact(DisplayName = "Deve retornar verdadeiro ao comparar duas pessoas iguais")]
        public static void Should_return_true_when_compare_two_equal_people()
        {
            // arrange
            var person1 = new FakePerson((FakeCode)"code", "name");
            var person2 = new FakePerson((FakeCode)"code", "name");

            // act
            var result = person1.Equals(person2);

            // assert
            Assert.True(result);
        }

        [Theory(DisplayName = "Deve retornar falso ao comparar duas pessoas diferentes")]
        [InlineData("code", "n")]
        [InlineData("c", "name")]
        public static void Should_return_true_when_compare_two_different_people(string code, string name)
        {
            // arrange
            var person1 = new FakePerson((FakeCode)"code", "name");
            var person2 = new FakePerson((FakeCode)code, name);

            // act
            var result = person1.Equals(person2);

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Deve retornar falso ao comparar uma pessoa com uma não pessoa")]
        public static void Should_return_false_when_compare_a_person_with_a_non_person()
        {
            // arrange
            var person1 = new FakePerson((FakeCode)"code", "name");

            // act
            var result = person1.Equals(new { });

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "HashCode deve ser o código")]
        public static void HashCode_should_be_code()
        {
            // arrange
            var code = (FakeCode)"code";
            var person = new FakePerson(code, "name");

            // act
            var result = person.GetHashCode();

            // assert
            Assert.Equal(code.GetHashCode(), result);
        }
    }
}
