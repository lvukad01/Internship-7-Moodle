

namespace Moodle.Domain.Entities
{
    public class Enrollment
    {
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        public int CourseId { get; private set; }
        public Course Course { get; private set; } = null!;

        public DateTime EnrolledAt { get; private set; }
    }
}
