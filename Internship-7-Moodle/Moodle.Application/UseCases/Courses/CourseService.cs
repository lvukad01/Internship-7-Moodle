using Moodle.Application.Exceptions;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence;

namespace Moodle.Application.UseCases.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<Course?> GetByIdAsync(int courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new ValidationException(new[] { ValidationItems.Course.CourseNotFound });

            return course;
        }

        public async Task<List<Course>> GetByProfessorIdAsync(int professorId)
        {
            var courses = await _courseRepository.GetByProfessorIdAsync(professorId);
            return courses ?? new List<Course>();
        }

        public async Task<List<Course>> GetByStudentIdAsync(int studentId)
        {
            var courses = await _courseRepository.GetByStudentIdAsync(studentId);
            return courses ?? new List<Course>();
        }

        public async Task EnrollStudentAsync(int courseId, int studentId)
        {
            var course = await GetByIdAsync(courseId);
            var student = await _userRepository.GetByIdAsync(studentId);

            if (student == null)
                throw new ValidationException(new[] { ValidationItems.User.UserNotFound });

            if (!course.Enrollments.Any(e => e.UserId == studentId))
            {
                course.Enrollments.Add(new Enrollment
                {
                    CourseId = courseId,
                    UserId = studentId
                });

                await _courseRepository.SaveChangesAsync();
            }
        }

        public async Task AddAnnouncementAsync(int courseId, string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ValidationException(new[] { ValidationItems.Announcement.TitleRequired });

            if (string.IsNullOrWhiteSpace(content))
                throw new ValidationException(new[] { ValidationItems.Announcement.ContentRequired });

            var course = await GetByIdAsync(courseId);
            course.Announcements.Add(new Announcement
            {
                CourseId = courseId,
                ProfessorId = course.ProfessorId,  
                Title = title,
                Content = content,
                CreatedAt = DateTime.UtcNow
            });

            await _courseRepository.SaveChangesAsync();
        }

        public async Task AddMaterialAsync(int courseId, string name, string url)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException(new[] { ValidationItems.Material.TitleRequired });

            if (string.IsNullOrWhiteSpace(url))
                throw new ValidationException(new[] { ValidationItems.Material.UrlRequired });

            var course = await GetByIdAsync(courseId);
            course.Materials.Add(new Material
            {
                CourseId = courseId,
                Name = name,
                Url = url,
                ProfessorId = course.ProfessorId,
                CreatedAt = DateTime.UtcNow
            });

            await _courseRepository.SaveChangesAsync();
        }

    }
}

