using Core.Extensions;
using Xunit;

namespace Core.UnitTest.Extensions
{
    public static class StringExtensionsTests
    {
        [Theory(DisplayName = "Deve retornar verdadeiro ao validar se uma string é nula ou vazia ou espaços em branco")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public static void Should_return_true_when_validate_if_a_string_is_null_or_empty_or_white_space(string value)
        {
            // arrange & act & assert
            Assert.True(value.IsNullOrEmptyOrWhiteSpace());
        }
    }
}
