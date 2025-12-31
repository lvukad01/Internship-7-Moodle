using Moodle.Application.UseCases.Users;
using Moodle.Application.UseCases.Messages;
using Moodle.Domain.Enums;


namespace Moodle.Presentation.Menus
{
    public class MainMenu
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly User _currentUser;

        public MainMenu(IUserService userService, IMessageService messageService, User currentUser)
        {
            _userService = userService;
            _messageService = messageService;
            _currentUser = currentUser;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Main Menu ({_currentUser.Role}) ===");

                int optionNumber = 1;
                Console.WriteLine($"{optionNumber++}. Privatni chat");

                if (_currentUser.Role == UserRole.Student)
                    Console.WriteLine($"{optionNumber++}. Moji kolegiji");

                if (_currentUser.Role == UserRole.Professor)
                {
                    Console.WriteLine($"{optionNumber++}. Moji kolegiji");
                    Console.WriteLine($"{optionNumber++}. Upravljanje kolegijima");
                }

                if (_currentUser.Role == UserRole.Admin)
                    Console.WriteLine($"{optionNumber++}. Upravljanje korisnicima");

                Console.WriteLine($"0. Odjava");
                Console.Write("Odabir: ");
                var choice = Console.ReadLine();
                if (choice == "0")
                {
                    return;
                }
                if (!int.TryParse(choice, out int choiceInt))
                {
                    Console.WriteLine("Nepoznata opcija. Pritisnite tipku za nastavak...");
                    Console.ReadKey();
                    continue;
                }

                int currentOption = 1;

                if (choiceInt == currentOption++)
                {
                    var chatMenu = new ChatMenu(_messageService, _userService, _currentUser.Id);
                    await chatMenu.StartAsync();
                    continue;
                }

                if (_currentUser.Role == UserRole.Student)
                {
                    if (choiceInt == currentOption++)
                    {
                        Console.WriteLine("Moji kolegiji (student) (nije implementirano)");
                        Console.ReadKey();
                        continue;
                    }
                }

                if (_currentUser.Role == UserRole.Professor)
                {
                    if (choiceInt == currentOption++)
                    {
                        Console.WriteLine("Moji kolegiji (profesor) (nije implementirano)");
                        Console.ReadKey();
                        continue;
                    }
                    if (choiceInt == currentOption++)
                    {
                        Console.WriteLine("Upravljanje kolegijima (nije implementirano)");
                        Console.ReadKey();
                        continue;
                    }
                }

                // Admin opcija
                if (_currentUser.Role == UserRole.Admin)
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


