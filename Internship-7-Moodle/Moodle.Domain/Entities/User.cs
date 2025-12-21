
using Moodle.Domain.Entities;
using Moodle.Domain.Enums;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; }


    //Navigations
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public ICollection<Course> CoursesTaught { get; set; } = new List<Course>();

    public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    public ICollection<Material> Materials { get; set; } = new List<Material>();

    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}