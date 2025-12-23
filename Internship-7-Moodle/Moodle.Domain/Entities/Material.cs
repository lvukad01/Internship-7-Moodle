

using Moodle.Domain.Abstractions;

namespace Moodle.Domain.Entities
{
    public class Material : BaseEntity
    {

        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int ProfessorId { get; set; }
        public User Professor { get; set; } = null!;
    }

}
