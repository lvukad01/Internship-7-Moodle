

namespace Moodle.Infrastructure.Persistence.Seed
{
    using Microsoft.EntityFrameworkCore;
    using Moodle.Domain.Entities;
    using Moodle.Domain.Enums;

    public static class DatabaseSeed //seed za bazu, koristimo u moodledbcontext
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@moodle.com",
                    Password = "HASH",
                    Role = UserRole.Admin
                },
                new User
                {
                    Id = 2,
                    Email = "prof@moodle.com",
                    Password = "HASH",
                    Role = UserRole.Professor
                },
                new User
                {
                    Id = 3,
                    Email = "student@moodle.com",
                    Password = "HASH",
                    Role = UserRole.Student
                }
            );

            modelBuilder.Entity<Course>().HasData(
              new Course
              {
                  Id = 1,
                  Name = "Programiranje 1",
                  ProfessorId = 2
              }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    UserId = 3,
                    CourseId = 1,
                    EnrolledAt = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Announcement>().HasData(
                new Announcement
                {
                    Id = 1,
                    Title = "Dobrodošli",
                    Content = "Dobrodošli na kolegij",
                    CreatedAt = DateTime.UtcNow,
                    CourseId = 1,
                    ProfessorId = 2
                }
            );

            modelBuilder.Entity<Material>().HasData(
                new Material
                {
                    Id = 1,
                    Name = "Uvod u C#",
                    Url = "https://example.com/csharp",
                    CreatedAt = DateTime.UtcNow,
                    CourseId = 1,
                    ProfessorId = 2
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new Message
                {
                    Id = 1,
                    SenderId = 2,
                    ReceiverId = 3,
                    Content = "Dobrodošao!",
                    SentAt = DateTime.UtcNow
                }
            );

        }

    }

}
