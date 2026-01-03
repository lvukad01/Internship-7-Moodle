using Moodle.Application.Exceptions;
using Moodle.Application.UseCases.Courses;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Entities;
using Moodle.Presentation.Common;

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
                var courses = await _courseService.GetByProfessorIdAsync(_currentUser.Id);
                if (!courses.Any())
                {
                    Console.WriteLine("Nemate dodijeljenih kolegija.");
                    Console.ReadKey();
                    return;
                }

                var options = courses.Select(c => c.Name).Append("Povratak").ToList();
                var choice = MenuNavigator.Show("UPRAVLJANJE KOLEGIJIMA", options);

                if (choice == -1 || choice == courses.Count)
                    return;

                await ShowCourseManagementAsync(courses[choice]);
            }
        }

        private async Task ShowCourseManagementAsync(Course course)
        {
            while (true)
            {
                var choice = MenuNavigator.Show(
                    $" UPRAVLJANJE KOLEGIJEM: {course.Name} ",
                    new List<string>
                    {
                        "Dodaj studenta",
                        "Objavi obavijest",
                        "Dodaj materijal",
                        "Povratak"
                    });

                if (choice == -1 || choice == 3)
                    return;

                if (choice == 0)
                    await AddStudentAsync(course);

                if (choice == 1)
                    await AddAnnouncementAsync(course);

                if (choice == 2)
                    await AddMaterialAsync(course);
            }
        }

        private async Task AddStudentAsync(Course course)
        {
            var students = await _userService.GetAllStudentsAsync();
            var availableStudents = students
                .Where(s => !course.Enrollments.Any(e => e.UserId == s.Id))
                .ToList();

            if (!availableStudents.Any())
            {
                Console.WriteLine("Nema dostupnih studenata.");
                Console.ReadKey();
                return;
            }

            var options = availableStudents
                .Select(s => s.Email)
                .Append("Povratak")
                .ToList();

            var choice = MenuNavigator.Show($" Dodaj studenta na {course.Name} ", options);

            if (choice == -1 || choice == availableStudents.Count)
                return;

            var student = availableStudents[choice];
            await _courseService.EnrollStudentAsync(course.Id, student.Id);

            Console.WriteLine($"Student {student.Email} dodan na kolegij.");
            Console.ReadKey();
        }

        private async Task AddAnnouncementAsync(Course course)
        {
            Console.Clear();
            Console.WriteLine($"Objavi obavijest ({course.Name}) ");

            Console.Write("Naslov: ");
            var title = Console.ReadLine();

            Console.Write("Sadržaj: ");
            var content = Console.ReadLine();

            try
            {
                await _courseService.AddAnnouncementAsync(course.Id, title, content);
                Console.WriteLine("Obavijest objavljena.");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Greška:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);
            }

            Console.ReadKey();
        }

        private async Task AddMaterialAsync(Course course)
        {
            Console.Clear();
            Console.WriteLine($"Dodaj materijal ({course.Name}) ");

            Console.Write("Naziv materijala: ");
            var name = Console.ReadLine();

            Console.Write("URL materijala: ");
            var url = Console.ReadLine();

            try
            {
                await _courseService.AddMaterialAsync(course.Id, name, url);
                Console.WriteLine("Materijal dodan.");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Greška:");
                foreach (var err in ex.Errors)
                    Console.WriteLine("- " + err.Message);
            }

            Console.ReadKey();
        }
    }
}
