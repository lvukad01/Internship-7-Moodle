using Moodle.Application.UseCases.Users;
using Moodle.Application.UseCases.Messages;
using Moodle.Application.UseCases.Courses;
using Moodle.Domain.Enums;
using Moodle.Application.UseCases.Statistics;

namespace Moodle.Presentation.Menus
{
    public class MainMenu
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly ICourseService _courseService;
        private readonly IStatisticsService _statisticsService;
        private readonly User _currentUser;

        public MainMenu(IUserService userService, IMessageService messageService, ICourseService courseService, IStatisticsService statisticsService, User currentUser)
        {
            _userService = userService;
            _messageService = messageService;
            _courseService = courseService;
            _statisticsService = statisticsService;
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
                {
                    Console.WriteLine($"{optionNumber++}. Upravljanje korisnicima");
                    Console.WriteLine($"{optionNumber++}. Statistike");
                }

                Console.WriteLine("0. Odjava");
                Console.Write("Odabir: ");
                var choice = Console.ReadLine();



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
                        var courseMenu = new CourseMenu(_courseService, _userService, _currentUser);
                        await courseMenu.StartAsync();
                        continue;
                    }
                }

                if (_currentUser.Role == UserRole.Professor)
                {
                    if (choiceInt == currentOption++)
                    {
                        var courseMenu = new CourseMenu(_courseService, _userService, _currentUser);
                        await courseMenu.StartAsync();
                        continue;
                    }
                    if (choiceInt == currentOption++)
                    {
                        var courseManagementMenu = new CourseManagementMenu(_courseService, _userService, _currentUser);
                        await courseManagementMenu.StartAsync();
                        continue;
                    }
                }

                if (_currentUser.Role == UserRole.Admin)
                {
                    if (choiceInt == currentOption++)
                    {
                        var userManagementMenu = new UserManagementMenu(_userService);
                        await userManagementMenu.StartAsync();
                        continue;
                    }
                    if (choiceInt == currentOption++)
                    {
                        var statisticsMenu = new StatisticsMenu(_statisticsService);
                        await statisticsMenu.StartAsync();
                        continue;
                    }
                }

                if (choiceInt == 0)
                {
                    Console.WriteLine("Odjava...");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("Nepoznata opcija. Pritisnite tipku za nastavak...");
                Console.ReadKey();
            }
        }
    }
}

