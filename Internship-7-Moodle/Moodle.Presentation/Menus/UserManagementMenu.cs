using Moodle.Application.Exceptions;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Entities;
using Moodle.Domain.Enums;

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
                Console.Clear();
                Console.WriteLine("=== User management ===");
                Console.WriteLine("1. Upravljanje studentima");
                Console.WriteLine("2. Upravljanje profesorima");
                Console.WriteLine("0. Nazad");
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
            while (true)
            {
                var users = await _userService.GetUsersByRoleAsync(role);

                Console.Clear();
                Console.WriteLine($"=== Upravljanje {role} ===");

                if (!users.Any())
                {
                    Console.WriteLine($"Nema korisnika s rolom {role}.");
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < users.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {users[i].Email} (ID: {users[i].Id})");
                }

                Console.WriteLine("0. Nazad");
                Console.Write("Odaberite korisnika: ");

                if (!int.TryParse(Console.ReadLine(), out int selected) ||
                    selected < 0 || selected > users.Count)
                {
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    continue;
                }

                if (selected == 0)
                    return;

                var user = users[selected - 1];
                await UserActionMenuAsync(user);
                // ⬅️ nakon povratka se lista ponovo učita
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
                        return; 
                    case "2":
                        await ChangeEmailAsync(user);
                        break;
                    case "3":
                        await ChangeRoleAsync(user);
                        return; 
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
            Console.Write("Unesite novi email (za odustajanje upisite /exit): ");
            var newEmail = Console.ReadLine();
            if(newEmail=="/exit")
            {
                Console.WriteLine("Povratak u prethodni menu");
                Console.ReadKey();
            }
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
