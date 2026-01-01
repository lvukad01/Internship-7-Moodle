using Moodle.Application.UseCases.Courses;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Entities;
using Moodle.Domain.Enums;

namespace Moodle.Presentation.Menus
{
    public class CourseMenu
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly User _currentUser;
        private readonly bool _isManagementMode;

        public CourseMenu(ICourseService courseService, IUserService userService, User currentUser, bool isManagementMode = false)
        {
            _courseService = courseService;
            _userService = userService;
            _currentUser = currentUser;
            _isManagementMode = isManagementMode;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_isManagementMode ? "=== Upravljanje kolegijima ===" : "=== Moji kolegiji ===");

                List<Course> courses = _currentUser.Role == UserRole.Student
                    ? await _courseService.GetByStudentIdAsync(_currentUser.Id)
                    : await _courseService.GetByProfessorIdAsync(_currentUser.Id);

                if (courses == null || courses.Count == 0)
                {
                    Console.WriteLine("Nema kolegija za prikaz.");
                    Console.ReadKey();
                    return;
                }

                foreach (var c in courses)
                {
                    c.Professor ??= new User { Email = "Nepoznati profesor" };
                    c.Enrollments ??= new List<Enrollment>();
                    c.Announcements ??= new List<Announcement>();
                    c.Materials ??= new List<Material>();
                }

                for (int i = 0; i < courses.Count; i++)
                    Console.WriteLine($"{i + 1}. {courses[i].Name} ({courses[i].Professor.Email})");

                Console.WriteLine("0. Povratak");
                Console.Write("Odabir kolegija: ");
                var input = Console.ReadLine();
                if (input == "0") return;
                if (!int.TryParse(input, out int selectedIndex) || selectedIndex < 1 || selectedIndex > courses.Count)
                {
                    Console.WriteLine("Nevažeći odabir.");
                    Console.ReadKey();
                    continue;
                }

                var selectedCourse = courses[selectedIndex - 1];
                await ShowCourseAsync(selectedCourse);
            }
        }

        private async Task ShowCourseAsync(Course course)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== {course.Name} ===");
                Console.WriteLine("1. Obavijesti");
                Console.WriteLine("2. Materijali");

                if (_currentUser.Role == UserRole.Professor)
                    Console.WriteLine("3. Pregled studenata");

                Console.WriteLine("0. Povratak");
                Console.Write("Odabir: ");
                var choice = Console.ReadLine();

                if (choice == "0") return;

                switch (choice)
                {
                    case "1":
                        ShowAnnouncements(course);
                        break;
                    case "2":
                        ShowMaterials(course);
                        break;
                    case "3":
                        if (_currentUser.Role == UserRole.Professor)
                            ShowStudents(course);
                        else
                            InvalidOption();
                        break;
                    default:
                        InvalidOption();
                        break;
                }
            }
        }

        private void ShowAnnouncements(Course course)
        {
            Console.Clear();
            Console.WriteLine($"=== Obavijesti za {course.Name} ===");

            var announcements = course.Announcements ?? new List<Announcement>();
            if (!announcements.Any())
                Console.WriteLine("Nema obavijesti.");
            else
            {
                foreach (var ann in announcements.OrderByDescending(a => a.CreatedAt))
                    Console.WriteLine($"{ann.CreatedAt:dd.MM.yyyy HH:mm} - {ann.Title}: {ann.Content}");
            }

            Console.WriteLine("Pritisnite tipku za povratak...");
            Console.ReadKey();
        }

        private void ShowMaterials(Course course)
        {
            Console.Clear();
            Console.WriteLine($"=== Materijali za {course.Name} ===");

            var materials = course.Materials ?? new List<Material>();
            if (!materials.Any())
                Console.WriteLine("Nema materijala.");
            else
            {
                foreach (var mat in materials.OrderByDescending(m => m.CreatedAt))
                    Console.WriteLine($"{mat.CreatedAt:dd.MM.yyyy HH:mm} - {mat.Name}: {mat.Url}");
            }

            Console.WriteLine("Pritisnite tipku za povratak...");
            Console.ReadKey();
        }

        private void ShowStudents(Course course)
        {
            Console.Clear();
            Console.WriteLine($"=== Studenti na {course.Name} ===");

            var students = course.Enrollments?.Select(e => e.User).Where(u => u != null).OrderBy(u => u.Email).ToList() ?? new List<User>();

            if (!students.Any())
                Console.WriteLine("Nema upisanih studenata.");
            else
            {
                for (int i = 0; i < students.Count; i++)
                    Console.WriteLine($"{i + 1}. {students[i].Email}");
            }

            Console.WriteLine("Pritisnite tipku za povratak...");
            Console.ReadKey();
        }

        private void InvalidOption()
        {
            Console.WriteLine("Nepoznata opcija.");
            Console.ReadKey();
        }
    }
}

