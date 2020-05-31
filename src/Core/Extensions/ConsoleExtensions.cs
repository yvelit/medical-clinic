using System;
using System.Threading;

namespace Core.Extensions
{
    public class ConsoleExtensions
    {
        public static T ReadLine<T>(string text, Action<T> validation = null)
        {
            Console.WriteLine(text);
            var response = Console.ReadLine();
            try
            {
                var result = (T)Convert.ChangeType(response, typeof(T));
                validation?.Invoke(result);

                return result;
            }
            catch (Exception)
            {
                InvalidInput();
                return ReadLine<T>(text, validation);
            }
        }

        public static void Sleep(int x = 1)
        {
            Thread.Sleep(x * 1000);
        }

        public static void InvalidInput()
        {
            Console.WriteLine("Invalid input.");
            Sleep();
        }
    }
}
