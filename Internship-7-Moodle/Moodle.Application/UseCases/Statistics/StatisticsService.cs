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

        public async Task<int> GetUserCountAsync(UserRole role)
        {
            return role switch
            {
                UserRole.Student => (await _userRepository.GetAllStudentsAsync()).Count,
                UserRole.Professor => (await _userRepository.GetAllProfessorsAsync()).Count,
                UserRole.Admin => (await _userRepository.GetAllAdminsAsync()).Count,
                _ => 0
            };
        }

        public async Task<int> GetCourseCountAsync()
        {
            return await _courseRepository.CountAsync();
        }

        public async Task<List<(string CourseName, int StudentCount)>> GetTopCoursesAsync(int top)
        {
            var courses = await _courseRepository.GetAllAsync();

            return courses
                .Select(c => (c.Name, c.Enrollments.Count))
                .OrderByDescending(x => x.Item2)
                .Take(top)
                .ToList();
        }

        public async Task<List<(string Email, int MessageCount)>> GetTopMessagersAsync(int top)
        {
            var messages = await _messageRepository.GetAllAsync();

            return messages
                .GroupBy(m => m.Sender.Email)
                .Select(g => (g.Key, g.Count()))
                .OrderByDescending(x => x.Item2)
                .Take(top)
                .ToList();
        }
    }
}

