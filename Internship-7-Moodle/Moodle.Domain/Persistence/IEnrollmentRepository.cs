
using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment?> GetByUserAndCourseAsync(int userId, int courseId);
        Task<List<Enrollment>> GetEnrollmentsByUserIdAsync(int userId);
        Task<List<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task AddAsync(Enrollment enrollment);
        void Update(Enrollment enrollment);
        Task DeleteAsync(Enrollment enrollment);
    }
}
