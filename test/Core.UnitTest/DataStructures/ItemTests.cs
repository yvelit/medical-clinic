using System;
using System.Collections.Generic;
using System.Text;
using Core.DataStructures;
using Xunit;

namespace Core.UnitTest.DataStructures
{
    public static class ItemTests
    {
        [Fact(DisplayName = "Deve criar um item válido")]
        public static void Should_create_a_valid_item()
        {
            // arrange & act
            var item1 = new Item<int>(1);

            // assert
            Assert.Equal(1, item1.Data);
            Assert.Null(item1.Next);
            Assert.Null(item1.Previous);

            // arrange & act
            var item3 = new Item<int>(2, item1, item1);

            // assert
            Assert.Equal(2, item3.Data);
            Assert.NotNull(item3.Next);
            Assert.NotNull(item3.Previous);
        }
    }
}
