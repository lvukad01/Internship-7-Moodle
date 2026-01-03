using Moodle.Application.UseCases.Courses;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Entities;
using Moodle.Domain.Enums;
using Moodle.Presentation.Common;

namespace Moodle.Presentation.Menus
{
    public class CourseMenu
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly User _currentUser;

        public CourseMenu(ICourseService courseService, IUserService userService, User currentUser)
        {
            _courseService = courseService;
            _userService = userService;
            _currentUser = currentUser;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                List<Course> courses = _currentUser.Role == UserRole.Student
                    ? await _courseService.GetByStudentIdAsync(_currentUser.Id)
                    : await _courseService.GetByProfessorIdAsync(_currentUser.Id);

                if (!courses.Any())
                {
                    Console.WriteLine("Nema kolegija za prikaz.");
                    Console.ReadKey();
                    return;
                }

                var options = courses
                    .Select(c => $"{c.Name} ({c.Professor?.Email ?? "Nepoznati profesor"})")
                    .Append("Povratak")
                    .ToList();

                int choice = MenuNavigator.Show("MOJI KOLEGIJI", options);

                if (choice == -1 || choice == courses.Count)
                    return;

                await ShowCourseAsync(courses[choice]);
            }
        }

        private async Task ShowCourseAsync(Course course)
        {
            while (true)
            {
                var options = new List<string>
                {
                    "Obavijesti",
                    "Materijali"
                };

                if (_currentUser.Role == UserRole.Professor)
                    options.Add("Pregled studenata");

                options.Add("Povratak");

                int choice = MenuNavigator.Show($" {course.Name} ", options);

                if (choice == -1 || choice == options.Count - 1)
                    return;

                if (choice == 0)
                    ShowAnnouncements(course);

                if (choice == 1)
                    ShowMaterials(course);

                if (choice == 2 && _currentUser.Role == UserRole.Professor)
                    ShowStudents(course);
            }
        }

        private void ShowAnnouncements(Course course)
        {
            Console.Clear();
            Console.WriteLine($"Obavijesti za {course.Name} ");

            if (!course.Announcements.Any())
            {
                Console.WriteLine("Nema obavijesti.");
            }
            else
            {
                foreach (var ann in course.Announcements.OrderByDescending(a => a.CreatedAt))
                {
                    Console.WriteLine($"{ann.CreatedAt:dd.MM.yyyy HH:mm} - {ann.Title}: {ann.Content}");
                }
            }

            Console.WriteLine("\nPritisnite bilo koju tipku za povratak...");
            Console.ReadKey();
        }

        private void ShowMaterials(Course course)
        {
            Console.Clear();
            Console.WriteLine($" Materijali za {course.Name} ");

            if (!course.Materials.Any())
            {
                Console.WriteLine("Nema materijala.");
            }
            else
            {
                foreach (var mat in course.Materials.OrderByDescending(m => m.CreatedAt))
                {
                    Console.WriteLine($"{mat.CreatedAt:dd.MM.yyyy HH:mm} - {mat.Name}: {mat.Url}");
                }
            }

            Console.WriteLine("\nPritisnite bilo koju tipku za povratak...");
            Console.ReadKey();
        }

        private void ShowStudents(Course course)
        {
            Console.Clear();
            Console.WriteLine($"Studenti na {course.Name} ");

            var students = course.Enrollments
                .Select(e => e.User)
                .Where(u => u != null)
                .OrderBy(u => u.Email)
                .ToList();

            if (!students.Any())
            {
                Console.WriteLine("Nema upisanih studenata.");
            }
            else
            {
                foreach (var s in students)
                {
                    Console.WriteLine(s.Email);
                }
            }

            Console.WriteLine("\nPritisnite bilo koju tipku za povratak...");
            Console.ReadKey();
        }
    }
}


