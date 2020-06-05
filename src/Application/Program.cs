using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core.Extensions;
using Domain;
using Domain.MedicalAppointments;
using Domain.People.Customers;
using Domain.People.Doctors;

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
            var lines = LoadFromFile();
            Console.WriteLine("Carregando...");
            int failCounter = 0;
            foreach (string line in lines)
            {
                var temp = line.Split(';');
                try
                {
                    _medicalClinic.AddMedicalAppointment(
                        new Cpf(temp[0]),
                        (MedicalAppointmentType)int.Parse(temp[1]),
                        (MedicalSpecialty)int.Parse(temp[2]),
                        Convert.ToDateTime(temp[3]));
                }
                catch (System.Exception)
                {
                    failCounter++;
                }
            }

            Console.WriteLine($"Foram carregados {_medicalClinic.CountMedicalAppointment()} consultas, {failCounter} falharam");
        }

        private static void GetMedicalAppointments()
        {
            Console.WriteLine();

            var day = ConsoleExtensions.ReadLine<int>("Digite o dia da data que deseja pesquisar:", d =>
             {
                 if (d < 0 || d > 31)
                 {
                     throw new InvalidOperationException();
                 }
             });

            var month = ConsoleExtensions.ReadLine<int>("Digite o mês da data que deseja pesquisar:", d =>
            {
                if (d < 0 || d > 12)
                {
                    throw new InvalidOperationException();
                }
            });

            var year = ConsoleExtensions.ReadLine<int>("Digite o ano da data que deseja pesquisar:", d =>
            {
                if (d < 0 || d > 2300)
                {
                    throw new InvalidOperationException();
                }
            });

            var date = new DateTime(year, month, day);

            Console.WriteLine("Especialidades médicas:");
            Console.WriteLine($"0 - {MedicalSpecialty.Urologist.GetDescription()}");
            Console.WriteLine($"1 - {MedicalSpecialty.GeneralClinic.GetDescription()}");
            Console.WriteLine($"2 - {MedicalSpecialty.Otorhinolaryngology.GetDescription()}");
            Console.WriteLine($"3 - {MedicalSpecialty.Orthopedy.GetDescription()}");
            Console.WriteLine($"4 - {MedicalSpecialty.Anesthesiologist.GetDescription()}");
            Console.WriteLine($"5 - {MedicalSpecialty.Dermatologist.GetDescription()}");
            Console.WriteLine($"6 - {MedicalSpecialty.Gynecologist.GetDescription()}");
            Console.WriteLine($"7 - {MedicalSpecialty.Neurologist.GetDescription()}");
            Console.WriteLine($"8 - {MedicalSpecialty.Pedriatrician.GetDescription()}");
            Console.WriteLine($"9 - {MedicalSpecialty.Surgeon.GetDescription()}");
            var code = ConsoleExtensions.ReadLine<int>("Digite o código da especialidade médica: ", (c) =>
             {
                 if (c < 0 || c > 9)
                 {
                     throw new InvalidOperationException();
                 }
             });

            MedicalSpecialty medicalSpecialty = (MedicalSpecialty)code;

            var medicalAppointments = _medicalClinic.GetMedicalAppointments(date, medicalSpecialty);

            foreach (var ma in medicalAppointments)
            {
                Console.WriteLine(ma.ToString());
            }
        }

        private static void GetDoctors()
        {
            Console.WriteLine();
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
            var lines = LoadFromFile();
            Console.WriteLine("Carregando...");
            int failCounter = 0;
            foreach (string line in lines)
            {
                var temp = line.Split(';');
                try
                {
                    _medicalClinic.AddDoctor(
                        new Crm(temp[0]),
                        temp[1],
                        (MedicalSpecialty)int.Parse(temp[2]));
                }
                catch (System.Exception)
                {
                    failCounter++;
                }
            }
            Console.WriteLine($"Foram carregados {_medicalClinic.CountDoctor()} médicos, {failCounter} falharam");
        }

        private static void LoadCustomersFromFile()
        {
            var lines = LoadFromFile();
            Console.WriteLine("Carregando...");
            int failCounter = 0;
            foreach (string line in lines)
            {
                var temp = line.Split(';');
                try
                {
                    _medicalClinic.AddCustomer(new Cpf(temp[0]), temp[1]);
                }
                catch (System.Exception)
                {
                    failCounter++;
                }
            }
            Console.WriteLine($"Foram carregados {_medicalClinic.CountCustomer()} pacientes, {failCounter} falharam");
        }

        private static IEnumerable<string> LoadFromFile()
        {
            Console.WriteLine();
            Console.WriteLine();

            var path = ConsoleExtensions.ReadLine<string>("Digite o caminho do arquivo:", p =>
            {
                if (!File.Exists(p))
                {
                    Console.WriteLine("Arquivo não existe");
                    throw new InvalidOperationException();
                }
            });

            try
            {
                return File.ReadAllLines(path, Encoding.UTF8);
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("Você não tem permissão de acesso a esse arquivo.");
            }
        }
    }
}
