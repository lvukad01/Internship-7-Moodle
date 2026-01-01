using System.Text.RegularExpressions;
using Moodle.Application.Exceptions;
using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Enums;
using Moodle.Domain.Persistence;

namespace Moodle.Application.UseCases.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ValidationException(ValidationItems.User.EmailRequired);

            if (string.IsNullOrWhiteSpace(password))
                throw new ValidationException(ValidationItems.User.PasswordRequired);

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || user.Password != password)
            {
                throw new ValidationException(ValidationItems.User.InvalidCredentials);
            }

            return user;
        }

        public async Task RegisterAsync(
            string email,
            string password,
            string confirmPassword,
            string captchaInput,
            string captchaExpected)
        {
            var errors = new List<ValidationItem>();

            // Email
            if (string.IsNullOrWhiteSpace(email))
                errors.Add(ValidationItems.User.EmailRequired);
            else if (!IsValidEmail(email))
                errors.Add(ValidationItems.User.EmailValid);
            else if (await _userRepository.EmailExistsAsync(email))
                errors.Add(ValidationItems.User.EmailAlreadyExists);

            // Password
            if (string.IsNullOrWhiteSpace(password))
                errors.Add(ValidationItems.User.PasswordRequired);
            else if (password != confirmPassword)
                errors.Add(ValidationItems.User.PasswordsDoNotMatch);

            // Captcha
            if (captchaInput != captchaExpected)
                errors.Add(ValidationItems.User.InvalidCaptcha);

            if (errors.Any())
                throw new ValidationException(errors);

            var user = new User
            {
                Email = email,
                Password = password, 
                Role = UserRole.Student
            };

            await _userRepository.AddAsync(user);
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^.{1,}@.{2,}\..{3,}$");
        }
    }
}
