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
        private readonly User _loggedInUser;

        public MainMenu(IUserService userService, User loggedInUser)
        {
            _userService = userService;
            _loggedInUser = loggedInUser;
        }

        public async Task StartAsync()
        {
            switch (_loggedInUser.Role)
            {
                case UserRole.Admin:
                    var adminMenu = new AdminMenu(_userService, _loggedInUser);
                    await adminMenu.StartAsync();
                    break;

                //case UserRole.Professor:
                //    var professorMenu = new ProfessorMenu(_userService, _loggedInUser);
                //    await professorMenu.StartAsync();
                //    break;

                //case UserRole.Student:
                //    var studentMenu = new StudentMenu(_userService, _loggedInUser);
                //    await studentMenu.StartAsync();
                //    break;

                default:
                    Console.WriteLine("Nepoznata rola korisnika.");
                    break;
            }
        }
    }
}


