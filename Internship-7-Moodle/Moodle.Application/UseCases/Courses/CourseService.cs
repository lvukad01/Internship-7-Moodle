using Moodle.Domain.Entities;
using Moodle.Domain.Persistence;

namespace Moodle.Application.UseCases.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course?> GetByIdAsync(int courseId)
        {
            return await _courseRepository.GetByIdAsync(courseId);
        }

        public async Task<List<Course>> GetCoursesByProfessorAsync(int professorId)
        {
            return await _courseRepository.GetByProfessorIdAsync(professorId);
        }

        public async Task<List<Course>> GetCoursesByStudentAsync(int studentId)
        {
            return await _courseRepository.GetByStudentIdAsync(studentId);
        }
    }
}
