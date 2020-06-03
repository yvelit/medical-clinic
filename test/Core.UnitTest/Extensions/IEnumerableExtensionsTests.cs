using System;
using System.Linq;
using Core.Extensions;
using Xunit;

namespace Core.UnitTest.Extensions
{
    public static class IEnumerableExtensionsTests
    {
        [Theory(DisplayName = "Deve retornar se um IEnumerable é vazio ou nulo")]
        [InlineData(true)]
        [InlineData(false)]
        public static void Should_return_if_ienumerable_is_null_or_empty(bool shouldCreate)
        {
            // arrange
            int[] enumerable = null;
            if (shouldCreate)
            {
                enumerable = new int[] { };
            }

            // act
            var result = enumerable.IsNullOrEmpty();

            // assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Deve ordenar um IEnumerable")]
        public static void Should_sort_an_ienumerable()
        {
            // arrange
            var unsorted = new int[10];
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                unsorted[i] = random.Next(0, 100);
            }
            var sorted = unsorted.OrderByDescending(x => x);

            // act
            var result = unsorted.MergeSortDescending();

            // assert
            Assert.Equal(sorted, result);
        }
    }
}
