using Moodle.Domain.Enums;

namespace Moodle.Application.UseCases.Users
{
    public interface IUserService
    {
        Task<List<User>> GetUsersByRoleAsync(UserRole role);

        Task ChangeEmailAsync(int userId, string newEmail);

        Task ChangeRoleAsync(int userId, UserRole newRole);

        Task DeleteUserAsync(int userId);
        Task<List<User>> GetAllStudentsAsync();
        Task<List<User>> GetAllProfessorsAsync();
    }
}