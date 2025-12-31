using Moodle.Application.Exceptions;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Entities;
using Moodle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle.Presentation.Menus
{
    public class AdminMenu
    {
        private readonly IUserService _userService;
        private readonly User _loggedInUser;

        public AdminMenu(IUserService userService, User loggedInUser)
        {
            _userService = userService;
            _loggedInUser = loggedInUser;
        }

        public async Task StartAsync()
        {
            if (_loggedInUser.Role != UserRole.Admin)
            {
                Console.WriteLine("Nemate ovlasti za pristup ovom meniju.");
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Admin Menu ===");
                Console.WriteLine("1. Upravljanje studentima");
                Console.WriteLine("2. Upravljanje profesorima");
                Console.WriteLine("0. Odjava / Nazad");
                Console.Write("Odabir: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await ManageUsersAsync(UserRole.Student);
                        break;
                    case "2":
                        await ManageUsersAsync(UserRole.Professor);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nepoznata opcija.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ManageUsersAsync(UserRole role)
        {
            var users = await _userService.GetUsersByRoleAsync(role);
            if (!users.Any())
            {
                Console.WriteLine($"Nema korisnika s rolom {role}.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Upravljanje {role} ===");
                for (int i = 0; i < users.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {users[i].Email} (ID: {users[i].Id})");
                }
                Console.WriteLine("0. Nazad");
                Console.Write("Odaberite korisnika: ");

                if (!int.TryParse(Console.ReadLine(), out int selected) || selected < 0 || selected > users.Count)
                {
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    continue;
                }

                if (selected == 0)
                    return;

                var user = users[selected - 1];
                await UserActionMenuAsync(user);
            }
        }

        private async Task UserActionMenuAsync(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Akcije za {user.Email} (ID: {user.Id}) ===");
                Console.WriteLine("1. Obriši korisnika");
                Console.WriteLine("2. Promijeni email");
                Console.WriteLine("3. Promijeni rolu");
                Console.WriteLine("0. Nazad");
                Console.Write("Odabir: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await DeleteUserAsync(user);
                        return; // nakon brisanja vraća na listu korisnika
                    case "2":
                        await ChangeEmailAsync(user);
                        break;
                    case "3":
                        await ChangeRoleAsync(user);
                        return; // vraća na listu korisnika jer se rola promijenila
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nepoznata opcija.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task DeleteUserAsync(User user)
        {
            try
            {
                await _userService.DeleteUserAsync(user.Id);
                Console.WriteLine("Korisnik obrisan.");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Brisanje neuspješno:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);
            }
            Console.ReadKey();
        }

        private async Task ChangeEmailAsync(User user)
        {
            Console.Write("Unesite novi email: ");
            var newEmail = Console.ReadLine();

            try
            {
                await _userService.ChangeEmailAsync(user.Id, newEmail);
                Console.WriteLine("Email uspješno promijenjen.");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Promjena emaila neuspješna:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);
            }
            Console.ReadKey();
        }

        private async Task ChangeRoleAsync(User user)
        {
            Console.WriteLine($"Trenutna rola: {user.Role}");
            UserRole newRole = user.Role == UserRole.Student ? UserRole.Professor : UserRole.Student;
            Console.WriteLine($"Želite li promijeniti rolu u {newRole}? (y/n): ");
            var input = Console.ReadLine();
            if (input?.Trim().ToLower() == "y")
            {
                try
                {
                    await _userService.ChangeRoleAsync(user.Id, newRole);
                    Console.WriteLine("Rola promijenjena.");
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine("Promjena role neuspješna:");
                    foreach (var err in ex.Errors)
                        Console.WriteLine("- " + err.Message);
                }
                Console.ReadKey();
            }
        }
    }
}
