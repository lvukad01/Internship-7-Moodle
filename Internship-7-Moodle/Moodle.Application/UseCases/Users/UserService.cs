using Moodle.Application.Exceptions;
using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Entities;
using Moodle.Domain.Enums;
using Moodle.Domain.Persistence;
using System.Text.RegularExpressions;

namespace Moodle.Application.UseCases.Users
{
    public class UserService : IUserService //koristi repository za pristup podacima, implementira poslovnu logiku vezanu uz korisnike koja ce se koristiti u program.cs
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsersByRoleAsync(UserRole role)
        {
            return role switch
            {
                UserRole.Student => await _userRepository.GetAllStudentsAsync(),
                UserRole.Professor => await _userRepository.GetAllProfessorsAsync(),
                _ => throw new ValidationException(new ValidationItem
                {
                    Code = "User9",
                    Message = "Nepodržana rola.",
                    Severity = ValidationSeverity.Error,
                    Type = ValidationType.BusinessRule
                })
            };
        }

        public async Task ChangeEmailAsync(int userId, string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ValidationException(ValidationItems.User.EmailRequired);

            if (!Regex.IsMatch(newEmail, @"^.{1,}@.{2,}\..{3,}$"))
                throw new ValidationException(ValidationItems.User.EmailValid);

            if (await _userRepository.EmailExistsAsync(newEmail))
                throw new ValidationException(ValidationItems.User.EmailAlreadyExists);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ValidationException(ValidationItems.User.UserNotFound);

            user.Email = newEmail;
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeRoleAsync(int userId, UserRole newRole)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ValidationException(ValidationItems.User.UserNotFound);

            if (user.Role == newRole)
                return; // nema promjene

            user.Role = newRole;
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ValidationException(ValidationItems.User.UserNotFound);

            await _userRepository.DeleteAsync(user);
        }
    }
}