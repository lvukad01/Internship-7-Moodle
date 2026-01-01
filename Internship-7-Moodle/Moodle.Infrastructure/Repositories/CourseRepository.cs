using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence;
using Moodle.Infrastructure.Database;

namespace Moodle.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MoodleDbContext _context;

        public CourseRepository(MoodleDbContext context)
        {
            _context = context;
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Professor) // ovo je ključno
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.User)
                .Include(c => c.Announcements)
                .Include(c => c.Materials)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Course>> GetByProfessorIdAsync(int professorId)
        {
            return await _context.Courses
                .Where(c => c.ProfessorId == professorId)
                .Include(c => c.Professor)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.User)
                .Include(c => c.Announcements)
                .Include(c => c.Materials)
                .ToListAsync();
        }

        public async Task<List<Course>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Courses
                .Where(c => c.Enrollments.Any(e => e.UserId == studentId))
                .Include(c => c.Professor) // << ovo je ključno
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.User)
                .Include(c => c.Announcements)
                .Include(c => c.Materials)
                .ToListAsync();
        }


        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
