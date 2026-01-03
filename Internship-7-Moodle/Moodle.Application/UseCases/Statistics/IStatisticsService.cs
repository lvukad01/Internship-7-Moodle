using Moodle.Domain.Enums;

namespace Moodle.Application.UseCases.Statistics
{
    public interface IStatisticsService
    {
        Task<int> GetUserCountAsync(UserRole role, DateTime? from = null, DateTime? to = null);
        Task<int> GetCourseCountAsync(DateTime? from = null, DateTime? to = null);
        Task<List<(string CourseName, int StudentCount)>> GetTopCoursesAsync(int top, DateTime? from = null, DateTime? to = null);
        Task<List<(string Email, int MessageCount)>> GetTopMessagersAsync(int top, DateTime? from = null, DateTime? to = null);
    }
}

