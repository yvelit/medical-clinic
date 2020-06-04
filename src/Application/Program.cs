using System;
using System.Linq;
using Core.Extensions;
using Domain;
using Domain.People.Customers;

namespace Application
{
    internal class Program
    {
        private static MenuItem[] _menuItems;
        private static MedicalClinic _medicalClinic;
        private static void Main(string[] args)
        {
            Console.WriteLine("CLINÍCA MÉDICA");
            _medicalClinic = new MedicalClinic();
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

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void Executar(int valorOpcao)
        {
            try
            {
                MenuItem menuItem = _menuItems[--valorOpcao];

                Console.WriteLine();
                Console.WriteLine($"Executando: {menuItem.Titulo}");

                menuItem.Action.Invoke();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Aconteceu algum erro.");
                Console.WriteLine(ex.Message);
            }
            
        }

        private static void ImprimirMenuItems(MenuItem[] menuItems)
        {
            Console.WriteLine("SELECIONE UMA OPÇÃO");
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
                new MenuItem("Carregar clientes de um arquivo", LoadCustomersFromFile),
                new MenuItem("Carregar médicos de um arquivo", LoadDoctorsFromFile),
                new MenuItem("Carregar consultas médicas de um arquivo", LoadMedicalAppointmentsFromFile),
                new MenuItem("Recuperar um cliente", GetCustomer),
                new MenuItem("Recuperar os médicos", GetDoctors),
                new MenuItem("Recuperar as consultas médicas", GetMedicalAppointments),
            };
        }

        private static void LoadMedicalAppointmentsFromFile()
        {
            throw new NotImplementedException();
        }

        private static void GetMedicalAppointments()
        {
            throw new NotImplementedException();
        }

        private static void GetDoctors()
        {
            var doctors = _medicalClinic.GetDoctors();

            foreach (var doctor in doctors)
            {
                Console.WriteLine(doctor.ToString());
            }
        }

        private static void GetCustomer()
        {
            Console.WriteLine();
            Console.WriteLine("Digite o cpf do cliente no formato 'XXXXXXXXX-XX':");
            var cpf = Console.ReadLine();

            var customer = _medicalClinic.GetCustomer((Cpf)cpf);
            Console.WriteLine(customer.ToString());
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
