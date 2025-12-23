
using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence
{
    public interface IEnrollmentRepository
    {
        Task<bool> ExistsAsync(int userId, int courseId);
        Task AddAsync(Enrollment enrollment);
        Task<List<Enrollment>> GetByUserIdAsync(int userId);
        Task<List<Enrollment>> GetByCourseIdAsync(int courseId);
        Task DeleteAsync(Enrollment enrollment);
    }
}
