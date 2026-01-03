using Moodle.Application.Exceptions;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Enums;
using Moodle.Presentation.Common;

namespace Moodle.Presentation.Menus
{
    public class UserManagementMenu
    {
        private readonly IUserService _userService;

        public UserManagementMenu(IUserService userService)
        {
            _userService = userService;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                var options = new List<string>
                {
                    "Upravljanje studentima",
                    "Upravljanje profesorima",
                    "Nazad"
                };

                int choice = MenuNavigator.Show("USER MANAGEMENT", options);

                if (choice == -1 || choice == options.Count - 1)
                    return;

                var role = choice == 0 ? UserRole.Student : UserRole.Professor;
                await ManageUsersAsync(role);
            }
        }

        private async Task ManageUsersAsync(UserRole role)
        {
            while (true)
            {
                var users = await _userService.GetUsersByRoleAsync(role);

                if (!users.Any())
                {
                    Console.Clear();
                    Console.WriteLine($"Nema korisnika s rolom {role}.");
                    Console.ReadKey();
                    return;
                }

                var options = users
                    .Select(u => $"{u.Email} (ID: {u.Id})")
                    .ToList();

                options.Add("Nazad");

                int choice = MenuNavigator.Show($"UPRAVLJANJE {role} ", options);

                if (choice == -1 || choice == options.Count - 1)
                    return;

                await UserActionMenuAsync(users[choice]);
            }
        }

        private async Task UserActionMenuAsync(User user)
        {
            while (true)
            {
                var options = new List<string>
                {
                    "Obriši korisnika",
                    "Promijeni email",
                    "Promijeni rolu",
                    "Nazad"
                };

                int choice = MenuNavigator.Show(
                    $"AKCIJE ZA {user.Email} (ID: {user.Id}) ",
                    options
                );

                if (choice == -1 || choice == options.Count - 1)
                    return;

                switch (choice)
                {
                    case 0:
                        await DeleteUserAsync(user);
                        return;

                    case 1:
                        await ChangeEmailAsync(user);
                        break;

                    case 2:
                        await ChangeRoleAsync(user);
                        return;
                }
            }
        }

        private async Task DeleteUserAsync(User user)
        {
            Console.Clear();
            Console.Write($"Jeste li sigurni da želite obrisati korisnika {user.Email}? (y/n): ");
            var confirm = Console.ReadLine();

            if (confirm?.Trim().ToLower() != "y")
            {
                Console.WriteLine("Brisanje otkazano.");
                Console.ReadKey();
                return;
            }

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
            Console.Clear();
            Console.Write("Unesite novi email (za odustajanje upišite /exit): ");
            var newEmail = Console.ReadLine();

            if (newEmail == "/exit")
                return;

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
            Console.Clear();
            Console.WriteLine($"Trenutna rola: {user.Role}");

            UserRole newRole = user.Role == UserRole.Student
                ? UserRole.Professor
                : UserRole.Student;

            Console.Write($"Jeste li sigurni da želite promijeniti rolu u {newRole}? (y/n): ");
            var input = Console.ReadLine();

            if (input?.Trim().ToLower() != "y")
            {
                Console.WriteLine("Promjena role otkazana.");
                Console.ReadKey();
                return;
            }

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
