using Moodle.Application.Exceptions;
using Moodle.Application.UseCases.Courses;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Entities;

namespace Moodle.Presentation.Menus
{
    public class CourseManagementMenu
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly User _currentUser;

        public CourseManagementMenu(ICourseService courseService, IUserService userService, User currentUser)
        {
            _courseService = courseService;
            _userService = userService;
            _currentUser = currentUser;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Upravljanje kolegijima ===");

                // Dohvati kolegije profesora
                var courses = await _courseService.GetByProfessorIdAsync(_currentUser.Id);

                if (courses == null || !courses.Any())
                {
                    Console.WriteLine("Nemate dodijeljenih kolegija.");
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak...");
                    Console.ReadKey();
                    return;
                }

                // Ispiši kolegije
                for (int i = 0; i < courses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {courses[i].Name}");
                }

                Console.WriteLine("0. Povratak");
                Console.Write("Odabir kolegija: ");
                var input = Console.ReadLine();

                if (input == "0") return;

                if (!int.TryParse(input, out int selectedIndex) || selectedIndex < 1 || selectedIndex > courses.Count)
                {
                    Console.WriteLine("Nevažeći odabir. Pritisnite tipku za nastavak...");
                    Console.ReadKey();
                    continue;
                }

                var selectedCourse = courses[selectedIndex - 1];
                await ShowCourseManagementAsync(selectedCourse);
            }
        }

        private async Task ShowCourseManagementAsync(Course course)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Upravljanje kolegijem: {course.Name} ===");
                Console.WriteLine("1. Dodaj studenta");
                Console.WriteLine("2. Objavi obavijest");
                Console.WriteLine("3. Dodaj materijal");
                Console.WriteLine("0. Povratak");
                Console.Write("Odabir: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddStudentAsync(course);
                        break;
                    case "2":
                        await AddAnnouncementAsync(course);
                        break;
                    case "3":
                        await AddMaterialAsync(course);
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

        private async Task AddStudentAsync(Course course)
        {
            Console.Clear();
            Console.WriteLine($"=== Dodavanje studenta na {course.Name} ===");

            var students = await _userService.GetAllStudentsAsync();
            var availableStudents = students
                .Where(s => !course.Enrollments.Any(e => e.UserId == s.Id))
                .ToList();

            if (!availableStudents.Any())
            {
                Console.WriteLine("Nema dostupnih studenata za dodavanje.");
                Console.WriteLine("Pritisnite tipku za povratak...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < availableStudents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableStudents[i].Email}");
            }

            Console.WriteLine("0. Povratak");
            Console.Write("Odabir studenta: ");
            var input = Console.ReadLine();

            if (input == "0") return;

            if (!int.TryParse(input, out int selectedIndex) || selectedIndex < 1 || selectedIndex > availableStudents.Count)
            {
                Console.WriteLine("Nevažeći odabir.");
                Console.ReadKey();
                return;
            }

            var student = availableStudents[selectedIndex - 1];
            await _courseService.EnrollStudentAsync(course.Id, student.Id);
            Console.WriteLine($"Student {student.Email} uspješno dodan na kolegij {course.Name}.");
            Console.WriteLine("Pritisnite tipku za nastavak...");
            Console.ReadKey();
        }

        private async Task AddAnnouncementAsync(Course course)
        {
            Console.Clear();
            Console.WriteLine($"=== Objavi obavijest za {course.Name} ===");

            Console.Write("Naslov: ");
            var title = Console.ReadLine();

            Console.Write("Sadržaj: ");
            var content = Console.ReadLine();

            try
            {
                await _courseService.AddAnnouncementAsync(course.Id, title, content);
                Console.WriteLine("Obavijest uspješno objavljena.");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Greška prilikom objave obavijesti:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);
            }

            Console.WriteLine("Pritisnite tipku za nastavak...");
            Console.ReadKey();
        }

        private async Task AddMaterialAsync(Course course)
        {
            Console.Clear();
            Console.WriteLine($"=== Dodaj materijal za {course.Name} ===");

            Console.Write("Naziv materijala: ");
            var name = Console.ReadLine();

            Console.Write("URL materijala: ");
            var url = Console.ReadLine();

            try
            {
                await _courseService.AddMaterialAsync(course.Id, name, url);
                Console.WriteLine("Materijal uspješno dodan.");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Greška prilikom dodavanja materijala:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);
            }

            Console.WriteLine("Pritisnite tipku za nastavak...");
            Console.ReadKey();
        }
    }
}
