

namespace Moodle.Domain.Entities
{
    public class Course
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        // Professor
        public int ProfessorId { get; private set; }
        public User Professor { get; private set; } = null!;

        // Students
        public ICollection<Enrollment> Enrollments { get; private set; }
            = new List<Enrollment>();

        // Content
        public ICollection<Announcement> Announcements { get; private set; }
            = new List<Announcement>();

        public ICollection<Material> Materials { get; private set; }
            = new List<Material>();
    }

}
