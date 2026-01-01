using Moodle.Application.Exceptions;
using Moodle.Application.UseCases.Auth;
using Moodle.Application.UseCases.Courses;
using Moodle.Application.UseCases.Messages;
using Moodle.Application.UseCases.Users;

namespace Moodle.Presentation.Menus
{
    public class AuthMenu
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly ICourseService _courseService;

        public AuthMenu(IAuthService authService, IUserService userService, IMessageService messageService, ICourseService courseService)
        {
            _authService = authService;
            _userService = userService;
            _messageService = messageService;
            _courseService = courseService;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Moodle ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Registracija");
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
                        Console.WriteLine("Izlaz iz aplikacije. Doviđenja!");
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

                Console.WriteLine($"Uspješno prijavljeni: {user.Email} ({user.Role})");
                Console.ReadKey();

                var mainMenu = new MainMenu(_userService, _messageService, _courseService, user);
                await mainMenu.StartAsync();
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Login neuspješan:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);

                Console.WriteLine("Pritisnite tipku za nastavak...");
                Console.ReadKey();
            }
        }

        private async Task RegisterAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Registracija ===");
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Nova lozinka: ");
            var password = Console.ReadLine();
            Console.Write("Potvrda lozinke: ");
            var confirmPassword = Console.ReadLine();

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
                    Console.WriteLine("- " + err.Message);
            }

            Console.WriteLine("Pritisnite tipku za nastavak...");
            Console.ReadKey();
        }
    }
}
