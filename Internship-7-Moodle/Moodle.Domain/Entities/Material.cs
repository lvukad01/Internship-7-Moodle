

namespace Moodle.Domain.Entities
{
    public class Material
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;
        public string Url { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        public int CourseId { get; private set; }
        public Course Course { get; private set; } = null!;

        public int ProfessorId { get; private set; }
        public User Professor { get; private set; } = null!;
    }

}
