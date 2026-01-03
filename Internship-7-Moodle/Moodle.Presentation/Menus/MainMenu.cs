using Moodle.Application.UseCases.Users;
using Moodle.Application.UseCases.Messages;
using Moodle.Application.UseCases.Courses;
using Moodle.Application.UseCases.Statistics;
using Moodle.Domain.Enums;
using Moodle.Presentation.Common;

namespace Moodle.Presentation.Menus
{
    public class MainMenu
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly ICourseService _courseService;
        private readonly IStatisticsService _statisticsService;
        private readonly User _currentUser;

        public MainMenu(
            IUserService userService,
            IMessageService messageService,
            ICourseService courseService,
            IStatisticsService statisticsService,
            User currentUser)
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
                var options = new List<string> { "Privatni chat" };

                if (_currentUser.Role == UserRole.Student)
                    options.Add("Moji kolegiji");

                if (_currentUser.Role == UserRole.Professor)
                {
                    options.Add("Moji kolegiji");
                    options.Add("Upravljanje kolegijima");
                }

                if (_currentUser.Role == UserRole.Admin)
                {
                    options.Add("Upravljanje korisnicima");
                    options.Add("Statistike");
                }

                options.Add("Odjava");

                int choice = MenuNavigator.Show($" MAIN MENU ({_currentUser.Role}) ", options);

                if (choice == -1)
                    return;

                int index = 0;

                if (choice == index++)
                {
                    var chatMenu = new ChatMenu(_messageService, _userService, _currentUser.Id);
                    await chatMenu.StartAsync();
                    continue;
                }

                if (_currentUser.Role == UserRole.Student)
                {
                    if (choice == index++)
                    {
                        var courseMenu = new CourseMenu(_courseService, _userService, _currentUser);
                        await courseMenu.StartAsync();
                        continue;
                    }
                }

                if (_currentUser.Role == UserRole.Professor)
                {
                    if (choice == index++)
                    {
                        var courseMenu = new CourseMenu(_courseService, _userService, _currentUser);
                        await courseMenu.StartAsync();
                        continue;
                    }
                    if (choice == index++)
                    {
                        var courseManagementMenu = new CourseManagementMenu(_courseService, _userService, _currentUser);
                        await courseManagementMenu.StartAsync();
                        continue;
                    }
                }

                if (_currentUser.Role == UserRole.Admin)
                {
                    if (choice == index++)
                    {
                        var userManagementMenu = new UserManagementMenu(_userService);
                        await userManagementMenu.StartAsync();
                        continue;
                    }
                    if (choice == index++)
                    {
                        var statisticsMenu = new StatisticsMenu(_statisticsService);
                        await statisticsMenu.StartAsync();
                        continue;
                    }
                }

                if (choice == index)
                {
                    Console.WriteLine("Odjava...");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}

