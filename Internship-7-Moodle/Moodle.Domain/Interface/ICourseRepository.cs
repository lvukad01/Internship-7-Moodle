
using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence
{
    public interface ICourseRepository
    {
        Task<Course?> GetByIdAsync(int id);
        Task<List<Course>> GetAllAsync();
        Task<List<Course>> GetByProfessorIdAsync(int professorId);
        Task AddAsync(Course course);
    }
}
