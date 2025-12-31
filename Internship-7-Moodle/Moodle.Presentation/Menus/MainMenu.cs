using Moodle.Application.UseCases.Users;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Entities;
using Moodle.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Moodle.Presentation.Menus
{
    public class MainMenu
    {
        private readonly IUserService _userService;
        private readonly User _currentUser;

        public MainMenu(IUserService userService, User currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Main Menu ({_currentUser.Role}) ===");

                Console.WriteLine("1. Privatni chat");
                int optionNumber = 2;

                if (_currentUser.Role == UserRole.Student)
                {
                    Console.WriteLine($"{optionNumber++}. Moji kolegiji");
                }
                else if (_currentUser.Role == UserRole.Professor)
                {
                    Console.WriteLine($"{optionNumber++}. Moji kolegiji");
                    Console.WriteLine($"{optionNumber++}. Upravljanje kolegijima");
                }
                else if (_currentUser.Role == UserRole.Admin)
                {
                    Console.WriteLine($"{optionNumber++}. Upravljanje korisnicima");
                }

                Console.WriteLine($"{optionNumber}. Odjava");
                Console.Write("Odabir: ");
                var choice = Console.ReadLine();

                int choiceInt;
                if (!int.TryParse(choice, out choiceInt))
                {
                    Console.WriteLine("Nepoznata opcija. Pritisnite tipku za nastavak...");
                    Console.ReadKey();
                    continue;
                }

                int currentOption = 1;

                // Privatni chat - svi
                if (choiceInt == currentOption++)
                {
                    // TODO: pozovi privatni chat menu
                    Console.WriteLine("Privatni chat (nije implementirano)");
                    Console.ReadKey();
                    continue;
                }

                if (_currentUser.Role == UserRole.Student)
                {
                    if (choiceInt == currentOption++)
                    {
                        // TODO: Moji kolegiji student
                        Console.WriteLine("Moji kolegiji (student) (nije implementirano)");
                        Console.ReadKey();
                        continue;
                    }
                }
                else if (_currentUser.Role == UserRole.Professor)
                {
                    if (choiceInt == currentOption++)
                    {
                        // TODO: Moji kolegiji profesor
                        Console.WriteLine("Moji kolegiji (profesor) (nije implementirano)");
                        Console.ReadKey();
                        continue;
                    }
                    if (choiceInt == currentOption++)
                    {
                        // TODO: Upravljanje kolegijima
                        Console.WriteLine("Upravljanje kolegijima (nije implementirano)");
                        Console.ReadKey();
                        continue;
                    }
                }
                else if (_currentUser.Role == UserRole.Admin)
                {
                    if (choiceInt == currentOption++)
                    {
                        var userManagementMenu = new UserManagementMenu(_userService);
                        await userManagementMenu.StartAsync();
                        continue;
                    }
                }

                // Odjava
                if (choiceInt == currentOption)
                {
                    Console.WriteLine("Odjava...");
                    break;
                }

                Console.WriteLine("Nepoznata opcija. Pritisnite tipku za nastavak...");
                Console.ReadKey();
            }
        }
    }
}

