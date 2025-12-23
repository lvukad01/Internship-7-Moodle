

using Moodle.Domain.Abstractions;

namespace Moodle.Domain.Entities
{
    public class Course: BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        // Professor
        public int ProfessorId { get; set; }
        public User Professor { get; set; } = null!;

        // Students
        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();

        // Content
        public ICollection<Announcement> Announcements { get; set; }
            = new List<Announcement>();

        public ICollection<Material> Materials { get; set; }
            = new List<Material>();
    }

}
