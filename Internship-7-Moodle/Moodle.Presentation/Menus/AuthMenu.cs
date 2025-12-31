using Moodle.Application.Exceptions;
using Moodle.Application.UseCases.Auth;
using Moodle.Application.UseCases.Messages;
using Moodle.Application.UseCases.Users;

namespace Moodle.Presentation.Menus
{
    public class AuthMenu
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public AuthMenu(IAuthService authService, IUserService userService, IMessageService messageService)
        {
            _authService = authService;
            _userService = userService;
            _messageService = messageService;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Moodle ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("0. Exit");
                Console.Write("Odabir: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await LoginAsync();
                        break;
                    case "2":
                        await RegisterAsync();
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

        private async Task LoginAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Login ===");
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            try
            {
                var user = await _authService.LoginAsync(email, password);
                var loggedInUser = await _authService.LoginAsync(email, password);
                Console.WriteLine($"Uspješno prijavljeni: {loggedInUser.Email} ({loggedInUser.Role})\nPritisnite tipku za nastavak");
                Console.ReadKey();

                // Prosljeđujemo ulogiranog korisnika u MainMenu
                var mainMenu = new MainMenu(_userService, _messageService, user);
                await mainMenu.StartAsync();

            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Login neuspješan:");
                foreach (var err in ex.Errors)
                {
                    Console.WriteLine("- " + err.Message);
                }
            }

            Console.WriteLine("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }

        public async Task RegisterAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Registracija ===");
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Nova lozinka: ");
            var password = Console.ReadLine();
            Console.Write("Potvrda lozinke: ");
            var confirmPassword = Console.ReadLine();

            // Generiranje jednostavnog captcha koda
            var captcha = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            Console.WriteLine($"Captcha: {captcha}");
            Console.Write("Unesite captcha: ");
            var captchaInput = Console.ReadLine();

            try
            {
                await _authService.RegisterAsync(email, password, confirmPassword, captchaInput, captcha);
                Console.WriteLine("Registracija uspješna!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Registracija neuspješna:");
                foreach (var err in ex.Errors)
                {
                    Console.WriteLine("- " + err.Message);
                }
            }

            Console.WriteLine("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
    }
}
