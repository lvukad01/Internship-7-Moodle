
using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence
{
    public interface ICourseRepository
    {
        Task<Course?> GetByIdAsync(int id);
        Task<List<Course>> GetAllAsync();
        Task AddAsync(Course course);
        void Update(Course course);
        Task DeleteAsync(Course course);
    }
}
