using Moodle.Application.UseCases.Statistics;

public class StatisticsMenu
{
    private readonly IStatisticsService _stats;

    public StatisticsMenu(IStatisticsService stats)
    {
        _stats = stats;
    }

    public async Task ShowAsync()
    {
        Console.Clear();
        Console.WriteLine("=== STATISTIKE ===\n");

        var users = await _stats.GetUserCountByRoleAsync();
        Console.WriteLine("Korisnici:");
        foreach (var u in users)
            Console.WriteLine($"- {u.Key}: {u.Value}");

        Console.WriteLine($"\nBroj kolegija: {await _stats.GetCourseCountAsync()}");

        Console.WriteLine("\nTop 3 kolegija:");
        foreach (var c in await _stats.GetTopCoursesAsync(3))
            Console.WriteLine($"- {c.CourseName} ({c.StudentCount})");

        Console.WriteLine("\nTop 3 korisnika po porukama:");
        foreach (var u in await _stats.GetTopMessagersAsync(3))
            Console.WriteLine($"- {u.Email} ({u.MessageCount})");

        Console.ReadKey();
    }
}

