using Moodle.Application.UseCases.Courses;
using Moodle.Application.UseCases.Messages;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Enums;

namespace Moodle.Presentation.Menus
{
    public class MainMenu
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly ICourseService _courseService;
        private readonly User _currentUser;

        public MainMenu(IUserService userService, IMessageService messageService, ICourseService courseService, User currentUser)
        {
            _userService = userService;
            _messageService = messageService;
            _courseService = courseService;
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

                Console.WriteLine("0. Odjava");
                Console.Write("Odabir: ");
                var choice = Console.ReadLine();

                if (choice == "0") return;
                if (!int.TryParse(choice, out int choiceInt))
                {
                    Console.WriteLine("Nepoznata opcija.");
                    Console.ReadKey();
                    continue;
                }

                int currentOption = 1;

                // Privatni chat
                if (choiceInt == currentOption++)
                {
                    var chatMenu = new ChatMenu(_messageService, _userService, _currentUser.Id);
                    await chatMenu.StartAsync();
                    continue;
                }

                // Student: Moji kolegiji
                if (_currentUser.Role == UserRole.Student && choiceInt == currentOption++)
                {
                    var courseMenu = new CourseMenu(_courseService, _userService, _currentUser);
                    await courseMenu.StartAsync();
                    continue;
                }

                // Professor: Moji kolegiji
                if (_currentUser.Role == UserRole.Professor)
                {
                    if (choiceInt == currentOption++)
                    {
                        var courseMenu = new CourseMenu(_courseService, _userService, _currentUser);
                        await courseMenu.StartAsync();
                        continue;
                    }
                    // Upravljanje kolegijima
                    if (choiceInt == currentOption++)
                    {
                        var courseMenu = new CourseMenu(_courseService, _userService, _currentUser, isManagementMode: true);
                        await courseMenu.StartAsync();
                        continue;
                    }
                }

                // Admin: User Management
                if (_currentUser.Role == UserRole.Admin && choiceInt == currentOption++)
                {
                    var userManagementMenu = new UserManagementMenu(_userService);
                    await userManagementMenu.StartAsync();
                    continue;
                }

                Console.WriteLine("Nepoznata opcija.");
                Console.ReadKey();
            }
        }
    }
}

