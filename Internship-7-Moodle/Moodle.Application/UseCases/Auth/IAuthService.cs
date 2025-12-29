namespace Moodle.Application.UseCases.Auth // Definiramo metode koje cemo implementirati u servisu za autentifikaciju
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string email, string password);
        Task RegisterAsync(
            string email,
            string password,
            string confirmPassword,
            string captchaInput,
            string captchaExpected);
    }
}
