using System;
using Domain.UnitTest.People.Fakes;
using Xunit;

namespace Domain.UnitTest.People
{
    public static class CodeTests
    {
        [Fact(DisplayName = "Deve criar um código válido")]
        public static void Should_create_a_valid_code()
        {
            // arrange
            var value = "value";

            // act & assert
            new FakeCode(value);
        }

        [Theory(DisplayName = "Deve levantar exceção ao criar um código inválido")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public static void Should_throw_exception_when_create_a_invalid_code(string value)
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new FakeCode(value));
        }

        [Theory(DisplayName = "Deve retornar verdadeiro ao comparar dois códigos iguais")]
        [InlineData("abc")]
        [InlineData("Abc")]
        [InlineData("ABC")]
        public static void Should_return_true_when_compare_two_equals_codes(string code)
        {
            // arrange
            var code1 = (FakeCode)"abc";
            var code2 = (FakeCode)code;

            // act
            var result = code1.Equals(code2);

            // assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Deve retornar falso ao comparar dois códigos diferentes")]
        public static void Should_return_false_when_compare_two_different_codes()
        {
            // arrange
            var code1 = (FakeCode)"abc";
            var code2 = (FakeCode)"abcd";

            // act
            var result = code1.Equals(code2);

            // assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Deve transformar uma código em uma string")]
        public static void Should_parse_a_code_into_a_string()
        {
            // arrange
            var value = "abc";
            var code = (FakeCode)value;

            // act
            var result = code.ToString();

            // assert
            Assert.Equal(value, result);
        }

        [Fact(DisplayName = "HashCode deve ser o seu valor")]
        public static void HashCode_should_be_value()
        {
            // arrange
            var value = "value";
            var code = (FakeCode)value;

            // act
            var result = code.GetHashCode();

            // assert
            Assert.Equal(value.GetHashCode(), result);
        }
    }
}
