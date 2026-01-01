using Moodle.Domain.Entities;

namespace Moodle.Application.UseCases.Courses
{
    public interface ICourseService
    {
        Task<Course?> GetByIdAsync(int courseId);
        Task<List<Course>> GetCoursesByProfessorAsync(int professorId);
        Task<List<Course>> GetCoursesByStudentAsync(int studentId);
    }
}
