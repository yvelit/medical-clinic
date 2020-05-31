using System;
using System.Text;
using Core.DataStructures;
using Xunit;

namespace Core.UnitTest.DataStructures
{
    public static class ListTests
    {
        [Fact(DisplayName = "Deve criar uma lista válida")]
        public static void Should_create_a_valid_list()
        {
            new List<int>();
        }

        [Fact(DisplayName = "Deve adicionar um item na lista")]
        public static void Should_add_an_item_in_list()
        {
            // arrange
            var list = new List<int>();

            // act
            list.Add(1);

            // assert
            Assert.True(list.Exist(1));
            Assert.Equal(1, list.Count());
        }

        [Fact(DisplayName = "Deve retirar um item na lista")]
        public static void Should_remove_an_item_on_list()
        {
            // arrange
            var list = new List<int>();
            list.Add(1);

            // act
            list.Remove(1);

            // assert
            Assert.False(list.Exist(1));
            Assert.Equal(0, list.Count());
        }

        [Fact(DisplayName = "Deve lançar exceção ao tentar retirar um item que não na lista")]
        public static void Should_throw_exception_when_try_remove_an_inexistent_item_on_list()
        {
            // arrange
            var list = new List<int>();

            // act & assert
            Assert.Throws<InvalidOperationException>(()=> list.Remove(1));
        }

        [Fact(DisplayName = "Deve retirar um item no meio da lista")]
        public static void Should_remove_an_item_in_middle_of_list()
        {
            // arrange
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // act
            list.Remove(2);

            // assert
            Assert.False(list.Exist(2));
            Assert.Equal(2, list.Count());
        }

        [Fact(DisplayName = "Deve retirar um item no final da lista")]
        public static void Should_remove_an_item_at_end_of_list()
        {
            // arrange
            var list = new List<int>();
            list.Add(1);
            list.Add(2);

            // act
            list.Remove(2);

            // assert
            Assert.False(list.Exist(2));
            Assert.Equal(1, list.Count());
        }

        [Fact(DisplayName = "Deve retornar se a lista está vazia")]
        public static void Should_return_if_list_is_empty()
        {
            // arrange
            var list = new List<int>();

            // act & assert
            Assert.True(list.IsEmpty());
        }
    }
}
