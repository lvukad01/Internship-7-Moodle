using Moodle.Application.UseCases.Statistics;
using Moodle.Domain.Enums;

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
                Console.Clear();
                Console.WriteLine("=== Statistike ===");
                Console.WriteLine("1. Broj korisnika po rolama");
                Console.WriteLine("2. Broj kolegija");
                Console.WriteLine("3. Top 3 kolegija po broju studenata");
                Console.WriteLine("4. Top 3 korisnika po broju poruka");
                Console.WriteLine("0. Nazad");
                Console.Write("Odabir: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ShowUserCountsAsync();
                        break;
                    case "2":
                        await ShowCourseCountAsync();
                        break;
                    case "3":
                        await ShowTopCoursesAsync();
                        break;
                    case "4":
                        await ShowTopMessagersAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nepoznata opcija.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ShowUserCountsAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Broj korisnika po rolama ===");

            var students = await _statisticsService.GetUserCountAsync(UserRole.Student);
            var professors = await _statisticsService.GetUserCountAsync(UserRole.Professor);
            var admins = await _statisticsService.GetUserCountAsync(UserRole.Admin);

            Console.WriteLine($"Studenti  : {students}");
            Console.WriteLine($"Profesori : {professors}");
            Console.WriteLine($"Admini    : {admins}");

            Console.WriteLine("\nPritisnite tipku za nastavak...");
            Console.ReadKey();
        }

        private async Task ShowCourseCountAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Broj kolegija ===");

            var count = await _statisticsService.GetCourseCountAsync();
            Console.WriteLine($"Ukupno kolegija: {count}");

            Console.WriteLine("\nPritisnite tipku za nastavak...");
            Console.ReadKey();
        }

        private async Task ShowTopCoursesAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Top 3 kolegija po broju studenata ===");

            var courses = await _statisticsService.GetTopCoursesAsync(3);

            if (!courses.Any())
            {
                Console.WriteLine("Nema podataka.");
            }
            else
            {
                int rank = 1;
                foreach (var (name, count) in courses)
                {
                    Console.WriteLine($"{rank}. {name} – {count} studenata");
                    rank++;
                }
            }

            Console.WriteLine("\nPritisnite tipku za nastavak...");
            Console.ReadKey();
        }

        private async Task ShowTopMessagersAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Top 3 korisnika po broju poruka ===");

            var users = await _statisticsService.GetTopMessagersAsync(3);

            if (!users.Any())
            {
                Console.WriteLine("Nema poruka.");
            }
            else
            {
                int rank = 1;
                foreach (var (email, count) in users)
                {
                    Console.WriteLine($"{rank}. {email} – {count} poruka");
                    rank++;
                }
            }

            Console.WriteLine("\nPritisnite tipku za nastavak...");
            Console.ReadKey();
        }
    }
}
