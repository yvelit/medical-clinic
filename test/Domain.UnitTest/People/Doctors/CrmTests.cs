using System;
using Domain.People.Doctors;
using Xunit;

namespace Domain.UnitTest.People.Doctors
{
    public static class CrmTests
    {
        [Fact(DisplayName = "Deve criar um crm válido")]
        public static void Should_create_a_valid_code()
        {
            // arrange
            var value = "12345";

            // act & assert
            new Crm(value);
        }

        [Theory(DisplayName = "Deve levantar exceção ao criar um crm inválido")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("12")]
        [InlineData("abc")]
        public static void Should_throw_exception_when_create_a_invalid_code(string value)
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new Crm(value));
        }

        [Fact(DisplayName = "Deve retornar verdadeiro ao comparar dois crms iguais")]
        public static void Should_return_true_when_compare_two_equals_codes()
        {
            // arrange
            var code1 = (Crm)"12345";
            var code2 = (Crm)"12345";

            // act
            var result = code1.Equals(code2);

            // assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Deve retornar falso ao comparar dois crms diferentes")]
        public static void Should_return_false_when_compare_two_different_codes()
        {
            // arrange
            var code1 = (Crm)"12345";
            var code2 = (Crm)"54321";

            // act
            var result = code1.Equals(code2);

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Deve transformar uma crm em uma string")]
        public static void Should_parse_a_code_into_a_string()
        {
            // arrange
            var value = "12345";
            var code = (Crm)value;

            // act
            var result = code.ToString();

            // assert
            Assert.Equal(value, result);
        }

        [Fact(DisplayName = "HashCode deve ser o seu valor")]
        public static void HashCode_should_be_value()
        {
            // arrange
            var value = "12345";
            var code = (Crm)value;

            // act
            var result = code.GetHashCode();

            // assert
            Assert.Equal(value.GetHashCode(), result);
        }
    }
}
