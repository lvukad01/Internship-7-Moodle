using Moodle.Domain.Abstractions;

namespace Moodle.Domain.Entities
{
    public class Course: BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 


        public int ProfessorId { get; set; }
        public User Professor { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();

        public ICollection<Announcement> Announcements { get; set; }
            = new List<Announcement>();

        public ICollection<Material> Materials { get; set; }
            = new List<Material>();
    }

}
