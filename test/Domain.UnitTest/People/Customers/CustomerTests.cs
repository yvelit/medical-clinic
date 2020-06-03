using System;
using Domain.MedicalAppointments;
using Domain.People;
using Domain.People.Customers;
using Domain.People.Doctors;
using Xunit;

namespace Domain.UnitTest.People.Customers
{
    public static class CustomerTests
    {
        [Fact(DisplayName = "Deve criar um cliente válido")]
        public static void Should_create_a_valid_customer()
        {
            // arrange
            var code = (Cpf)"012345678-90";
            var name = "name";

            // act
            var customer = new Customer(code, name, CustomerType.Normal);

            // assert
            Assert.Equal(code, customer.Code);
            Assert.Equal(name, customer.Name);
            Assert.True(customer.MedicalAppointments.IsEmpty());
        }

        [Theory(DisplayName = "Deve lançar uma exceção ao tentar criar um cliente inválido")]
        [InlineData(null, "name")]
        [InlineData("abc", "name")]
        [InlineData("123", "name")]
        [InlineData("012345678-90", null)]
        public static void Should_throw_a_exception_when_try_create_a_invalid_customer(string code, string name)
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new Customer((Cpf)code, name, CustomerType.Normal));
        }

        [Fact(DisplayName = "Deve adicionar uma consulta médica")]
        public static void Should_add_a_medical_appointment()
        {
            // arrange
            var customer = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var medicalAppointment = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);

            // act
            customer.AddMedicalAppointment(medicalAppointment);

            // assert
            Assert.True(!customer.MedicalAppointments.IsEmpty());
            Assert.True(customer.MedicalAppointments.Count() == 1);
        }

        [Fact(DisplayName = "Deve lançar uma exceção ao adicionar uma consulta médica nula")]
        public static void Should_throw_a_exception_when_add_a_null_medical_appointment()
        {
            // arrange
            var customer = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => customer.AddMedicalAppointment(null));
        }

        [Fact(DisplayName = "Deve transformar um cliente sem consultas médicas em uma string")]
        public static void Should_parse_a_customer_without_medical_appointments_into_a_string()
        {
            // arrange
            var customer = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);

            // act
            var result = customer.ToString();

            // assert
            Assert.Equal("CPF: 012345678-90 - Nome: name - Consultas: [] - Valor Total: 0 - Tipo de cliente: Normal", result);
        }

        [Fact(DisplayName = "Deve transformar um cliente com consultas médicas em uma string")]
        public static void Should_parse_a_customer_with_medical_appointments_into_a_string()
        {
            // arrange
            var customer = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);
            var doctor = new Doctor((Crm)"12345", "name", MedicalSpecialty.GeneralClinic);
            var medicalAppointment1 = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);
            var medicalAppointment2 = new MedicalAppointment(DateTime.Now, MedicalAppointmentType.OnDemand,customer,doctor);
            customer.AddMedicalAppointment(medicalAppointment1);
            customer.AddMedicalAppointment(medicalAppointment2);

            // act
            var result = customer.ToString();

            // assert
            Assert.Equal($"CPF: 012345678-90 - Nome: name - Consultas: [{medicalAppointment1},{medicalAppointment2}] - Valor Total: 160 - Tipo de cliente: Normal", result);
        }

        [Fact(DisplayName = "Deve retornar verdadeiro ao comparar dois clientes iguais")]
        public static void Should_return_true_when_compare_two_equal_customers()
        {
            // arrange
            var customer1 = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);
            var customer2 = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);

            // act
            var result = customer1.Equals(customer2);

            // assert
            Assert.True(result);
        }

        [Theory(DisplayName = "Deve retornar falso ao comparar dois clientes diferentes")]
        [InlineData("012345678-90", "n",CustomerType.Normal)]
        [InlineData("098765432-10", "name", CustomerType.Normal)]
        [InlineData("012345678-90", "name", CustomerType.HealthInsurance)]
        public static void Should_return_true_when_compare_two_different_customers(string code, string name,CustomerType customerType)
        {
            // arrange
            var customer1 = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);
            var customer2 = new Customer((Cpf)code, name, customerType);

            // act
            var result = customer1.Equals(customer2);

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Deve retornar falso ao comparar um cliente com um não cliente")]
        public static void Should_return_false_when_compare_a_customer_with_a_non_customer()
        {
            // arrange
            var customer1 = new Customer((Cpf)"012345678-90", "name", CustomerType.Normal);

            // act
            var result = customer1.Equals(new { });

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "HashCode deve ser o cpf")]
        public static void HashCode_should_be_cpf()
        {
            // arrange
            var code = (Cpf)"012345678-90";
            var customer = new Customer(code, "name", CustomerType.Normal);

            // act
            var result = customer.GetHashCode();

            // assert
            Assert.Equal(code.GetHashCode(), result);
        }
    }
}
