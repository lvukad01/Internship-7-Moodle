using Moodle.Application.UseCases.Statistics;
using Moodle.Domain.Enums;
using Moodle.Presentation.Common;

namespace Moodle.Presentation.Menus
{
    public class StatisticsMenu
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsMenu(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                var options = new List<string>
                {
                    "Broj korisnika po rolama",
                    "Broj kolegija",
                    "Top 3 kolegija po broju studenata",
                    "Top 3 korisnika po broju poruka",
                    "Nazad"
                };

                int choice = MenuNavigator.Show("STATISTIKE", options);

                if (choice == -1 || choice == options.Count - 1)
                    return;

                switch (choice)
                {
                    case 0:
                        await ShowUserCountsAsync();
                        break;
                    case 1:
                        await ShowCourseCountAsync();
                        break;
                    case 2:
                        await ShowTopCoursesAsync();
                        break;
                    case 3:
                        await ShowTopMessagersAsync();
                        break;
                }
            }
        }

        private (DateTime? from, DateTime? to) ChooseTimeRange()
        {
            var options = new List<string>
            {
                "Danas",
                "Ovaj mjesec",
                "Ukupno"
            };

            int choice = MenuNavigator.Show("Odaberite vremenski raspon", options);
            var now = DateTime.UtcNow;

            return choice switch
            {
                0 => (
                    new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, DateTimeKind.Utc),
                    new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999, DateTimeKind.Utc)
                ),
                1 => (
                    new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc),
                    new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59, 999, DateTimeKind.Utc)
                ),
                _ => (null, null)
            };
        }

        private async Task ShowUserCountsAsync()
        {
            Console.Clear();
            Console.WriteLine(" BROJ KORISNIKA PO ROLAMA ");

            var (from, to) = ChooseTimeRange();

            Console.WriteLine($"Studenti  : {await _statisticsService.GetUserCountAsync(UserRole.Student, from, to)}");
            Console.WriteLine($"Profesori : {await _statisticsService.GetUserCountAsync(UserRole.Professor, from, to)}");
            Console.WriteLine($"Admini    : {await _statisticsService.GetUserCountAsync(UserRole.Admin, from, to)}");

            Console.ReadKey();
        }

        private async Task ShowCourseCountAsync()
        {
            Console.Clear();
            Console.WriteLine("BROJ KOLEGIJA ");

            var (from, to) = ChooseTimeRange();
            Console.WriteLine($"Ukupno kolegija: {await _statisticsService.GetCourseCountAsync(from, to)}");

            Console.ReadKey();
        }

        private async Task ShowTopCoursesAsync()
        {
            Console.Clear();
            Console.WriteLine(" TOP 3 KOLEGIJA PO BROJU STUDENATA ");

            var (from, to) = ChooseTimeRange();
            var courses = await _statisticsService.GetTopCoursesAsync(3, from, to);

            if (!courses.Any())
                Console.WriteLine("Nema podataka.");
            else
            {
                int rank = 1;
                foreach (var (name, count) in courses)
                    Console.WriteLine($"{rank++}. {name} – {count} studenata");
            }

            Console.ReadKey();
        }

        private async Task ShowTopMessagersAsync()
        {
            Console.Clear();
            Console.WriteLine(" TOP 3 KORISNIKA PO BROJU PORUKA ");

            var (from, to) = ChooseTimeRange();
            var users = await _statisticsService.GetTopMessagersAsync(3, from, to);

            if (!users.Any())
                Console.WriteLine("Nema poruka.");
            else
            {
                int rank = 1;
                foreach (var (email, count) in users)
                    Console.WriteLine($"{rank++}. {email} – {count} poruka");
            }

            Console.ReadKey();
        }
    }
}

