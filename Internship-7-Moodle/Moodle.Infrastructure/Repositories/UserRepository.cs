using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Enums;
using Moodle.Domain.Persistence;
using Moodle.Infrastructure.Database;

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

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
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
        public async Task<List<User>> GetAllAdminsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Admin)
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

        public IQueryable<User> Query()
        {
            return _context.Users.AsQueryable();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<int> CountByRoleAsync(UserRole role, DateTime? from = null, DateTime? to = null)
        {
            var query = _context.Users.Where(u => u.Role == role);

            if (from.HasValue)
                query = query.Where(u => u.CreatedAt >= from.Value);
            if (to.HasValue)
                query = query.Where(u => u.CreatedAt <= to.Value);

            return await query.CountAsync();
        }
        public async Task<List<User>> GetAllAsync(DateTime? from = null, DateTime? to = null)
        {
            var query = _context.Users.AsQueryable();

            if (from.HasValue)
                query = query.Where(u => u.CreatedAt >= from.Value);
            if (to.HasValue)
                query = query.Where(u => u.CreatedAt <= to.Value);

            return await query.ToListAsync();
        }



    }
}
