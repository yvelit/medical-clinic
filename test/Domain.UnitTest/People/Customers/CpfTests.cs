using System;
using Domain.People.Customers;
using Xunit;

namespace Domain.UnitTest.People.Customers
{
    public static class CpfTests
    {
        [Fact(DisplayName = "Deve criar um cpf válido")]
        public static void Should_create_a_valid_code()
        {
            // arrange
            var value = "123456789-10";

            // act & assert
            new Cpf(value);
        }

        [Theory(DisplayName = "Deve levantar exceção ao criar um cpf inválido")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("123")]
        [InlineData("abc")]
        public static void Should_throw_exception_when_create_a_invalid_code(string value)
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new Cpf(value));
        }

        [Fact(DisplayName = "Deve retornar verdadeiro ao comparar dois cpfs iguais")]
        public static void Should_return_true_when_compare_two_equals_codes()
        {
            // arrange
            var code1 = (Cpf)"123456789-10";
            var code2 = (Cpf)"123456789-10";

            // act
            var result = code1.Equals(code2);

            // assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Deve retornar falso ao comparar dois cpfs diferentes")]
        public static void Should_return_false_when_compare_two_different_codes()
        {
            // arrange
            var code1 = (Cpf)"123456789-10";
            var code2 = (Cpf)"109876543-21";

            // act
            var result = code1.Equals(code2);

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Deve transformar uma cpf em uma string")]
        public static void Should_parse_a_code_into_a_string()
        {
            // arrange
            var value = "123456789-10";
            var code = (Cpf)value;

            // act
            var result = code.ToString();

            // assert
            Assert.Equal(value, result);
        }

        [Fact(DisplayName = "HashCode deve ser o seu valor")]
        public static void HashCode_should_be_value()
        {
            // arrange
            var value = "123456789-10";
            var code = (Cpf)value;

            // act
            var result = code.GetHashCode();

            // assert
            Assert.Equal(value.GetHashCode(), result);
        }
    }
}
