using System;
using Core.DataStructures;
using Xunit;

namespace Core.UnitTest.DataStructures
{
    public static class HashtableTests
    {
        [Fact(DisplayName = "Deve criar uma tabela hash válida")]
        public static void Should_create_a_valid_hashtable()
        {
            // arrange & act & assert
            new Hashtable<string>();
        }

        [Fact(DisplayName = "Deve adicionar um item na tabela hash")]
        public static void Should_add_an_item_in_hashtable()
        {
            // arrange
            var hashtable = new Hashtable<string>();
            var item = "oi";
            // act
            hashtable.Add(item);

            // assert
            Assert.Equal(1, hashtable.Count());
            Assert.True(hashtable.Exist(item));
        }

        [Fact(DisplayName = "Deve lançar exceção ao tentar adicionar um elemento default")]
        public static void Should_throw_exception_when_try_add_a_default_element()
        {
            var hashtable = new Hashtable<int>();

            // act & assert
            Assert.Throws<ArgumentException>(() => hashtable.Add(0));
        }

        [Fact(DisplayName = "Deve lançar exceção ao tentar adicionar um item já inserido na tabela hash")]
        public static void Should_throw_exception_when_try_add_an_existent_item_in_hashtable()
        {
            // arrange
            var hashtable = new Hashtable<string>();
            hashtable.Add("item");

            // act & assert
            Assert.Throws<InvalidOperationException>(() => hashtable.Add("item"));
        }

        [Fact(DisplayName = "Deve retirar um item da tabela hash")]
        public static void Should_remove_an_item_in_hashtable()
        {
            // arrange
            var hashtable = new Hashtable<string>();
            var item = "oi";
            hashtable.Add(item);

            // act
            hashtable.Remove(item);

            // assert
            Assert.Equal(0, hashtable.Count());
            Assert.False(hashtable.Exist(item));
        }

        [Fact(DisplayName = "Deve lançar exceção ao tentar retirar um item não existente na tabela hash")]
        public static void Should_throw_exception_when_try_remove_an_inexistent_item_in_hashtable()
        {
            // arrange
            var hashtable = new Hashtable<string>();

            // act & assert
            Assert.Throws<InvalidOperationException>(() => hashtable.Remove("item"));
        }

        [Fact(DisplayName = "Deve procurar um item na tabela hash")]
        public static void Should_find_an_item_in_hashtable()
        {
            // arrange
            var hashtable = new Hashtable<string>();
            var item = "oi";
            hashtable.Add(item);

            // act
            var result = hashtable.Find(item.GetHashCode());

            // assert
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Deve retornar nulo ao procurar um item não existente na tabela hash")]
        public static void Should_return_null_when_find_an_inexistent_item_in_hashtable()
        {
            // arrange
            var hashtable = new Hashtable<string>();

            // act
            var result = hashtable.Find(10);

            // assert
            Assert.Null(result);
        }
    }
}
