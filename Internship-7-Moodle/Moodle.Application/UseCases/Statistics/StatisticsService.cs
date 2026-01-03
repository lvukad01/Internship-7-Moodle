using Moodle.Domain.Enums;
using Moodle.Domain.Persistence;

namespace Moodle.Application.UseCases.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMessageRepository _messageRepository;

        public StatisticsService(
            IUserRepository userRepository,
            ICourseRepository courseRepository,
            IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _messageRepository = messageRepository;
        }

        public async Task<int> GetUserCountAsync(UserRole role, DateTime? from = null, DateTime? to = null)
        {
            return await _userRepository.CountByRoleAsync(role, from, to);
        }

        public async Task<int> GetCourseCountAsync(DateTime? from = null, DateTime? to = null)
        {
            return await _courseRepository.CountAsync(from, to);
        }

        public async Task<List<(string CourseName, int StudentCount)>> GetTopCoursesAsync(int top, DateTime? from = null, DateTime? to = null)
        {
            var courses = await _courseRepository.GetAllAsync(from, to);

            return courses
                .Select(c => (
                    CourseName: c.Name,
                    StudentCount: c.Enrollments.Count(e => (!from.HasValue || e.EnrolledAt >= from.Value)
                                                      && (!to.HasValue || e.EnrolledAt <= to.Value))
                ))
                .OrderByDescending(x => x.StudentCount)
                .Take(top)
                .ToList();
        }

        public async Task<List<(string Email, int MessageCount)>> GetTopMessagersAsync(int top, DateTime? from = null, DateTime? to = null)
        {
            var messages = await _messageRepository.GetAllAsync(from, to);

            return messages
                .GroupBy(m => m.Sender.Email)
                .Select(g => (Email: g.Key, Count: g.Count()))
                .OrderByDescending(x => x.Count)
                .Take(top)
                .ToList();
        }
    }
}

