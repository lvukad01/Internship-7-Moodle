

namespace Moodle.Domain.Persistence // Definiramo metode koje cemo u infrastructure koristiti za implementaciju repozitorija
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);

        Task<List<User>> GetAllStudentsAsync();
        Task<List<User>> GetAllProfessorsAsync();

        Task AddAsync(User user);
        void Update(User user);
        Task DeleteAsync(User user);
    }
}
