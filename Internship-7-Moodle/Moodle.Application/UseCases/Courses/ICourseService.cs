using Moodle.Domain.Entities;

namespace Moodle.Application.UseCases.Courses
{
    public interface ICourseService
    {
        Task<Course?> GetByIdAsync(int courseId);
        Task<List<Course>> GetByProfessorIdAsync(int professorId);
        Task<List<Course>> GetByStudentIdAsync(int studentId);
        Task EnrollStudentAsync(int courseId, int studentId);
        Task AddAnnouncementAsync(int courseId, string title, string content);
        Task AddMaterialAsync(int courseId, string name, string url);

    }
}
