using System;
using System.Linq;
using Core.Extensions;

namespace Application
{
    internal class Program
    {
        private static MenuItem[] _menuItems;

        private static void Main(string[] args)
        {
            Console.WriteLine("MEDICAL CLINIC");
            _menuItems = GetMenuItems();

            while (true)
            {
                ImprimirMenuItems(_menuItems);
                var option = ConsoleExtensions.ReadLine<int>("", (i) =>
                 {
                     if (i < 0 || i > _menuItems.Count())
                     {
                         throw new InvalidOperationException();
                     }
                 });

                if (option == 0)
                {
                    break;
                }

                Executar(option);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void Executar(int valorOpcao)
        {
            MenuItem menuItem = _menuItems[--valorOpcao];

            Console.WriteLine();
            Console.WriteLine($"Executing: {menuItem.Titulo}");

            menuItem.Action.Invoke();
            Console.WriteLine();
        }

        private static void ImprimirMenuItems(MenuItem[] menuItems)
        {
            Console.WriteLine("SELECT A OPTION");
            Console.WriteLine("===================");
            Console.WriteLine("0 - Sair");

            var i = 1;
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine($"{i} - {menuItem.Titulo}");
                i++;
            }
        }

        private static MenuItem[] GetMenuItems()
        {
            return new MenuItem[]
            {
                new MenuItem("Load customers from file", LoadCustomersFromFile),
                new MenuItem("Load doctors from file", LoadDoctorsFromFile),
                new MenuItem("Get customer", GetCustomer),
                new MenuItem("Get doctors", GetDoctors),
                new MenuItem("Get medical appointments", GetMedicalAppointments),
            };
        }

        private static void GetMedicalAppointments()
        {
            throw new NotImplementedException();
        }

        private static void GetDoctors()
        {
            throw new NotImplementedException();
        }

        private static void GetCustomer()
        {
            throw new NotImplementedException();
        }

        private static void LoadDoctorsFromFile()
        {
            throw new NotImplementedException();
        }

        private static void LoadCustomersFromFile()
        {
            throw new NotImplementedException();
        }
    }
}
