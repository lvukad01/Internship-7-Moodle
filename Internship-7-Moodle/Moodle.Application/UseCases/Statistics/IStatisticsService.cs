using Moodle.Domain.Enums;

namespace Moodle.Application.UseCases.Statistics
{
    public interface IStatisticsService
    {
        Task<int> GetUserCountAsync(UserRole role);
        Task<int> GetCourseCountAsync();

        Task<List<(string CourseName, int StudentCount)>> GetTopCoursesAsync(int top);
        Task<List<(string Email, int MessageCount)>> GetTopMessagersAsync(int top);
    }
}


