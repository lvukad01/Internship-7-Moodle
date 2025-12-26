

using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Enums;
using Moodle.Domain.Persistence;
using Moodle.Infrastructure.Persistence;

namespace Moodle.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository //Koirstimo LINQ
    {
        private readonly MoodleDbContext _context;

        public UserRepository(MoodleDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async  Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllProfessorsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Professor)
                .ToListAsync();
        }

        public async Task<List<User>> GetAllStudentsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Student)
                .ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
