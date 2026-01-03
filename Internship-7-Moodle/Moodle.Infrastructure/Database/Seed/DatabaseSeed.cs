using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Entities;
using Moodle.Domain.Enums;

namespace Moodle.Infrastructure.Database.Seed
{
    public static class DatabaseSeed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "admin@moodle.com", Password = "admin123", Role = UserRole.Admin, CreatedAt = now.AddDays(-30) },
                new User { Id = 2, Email = "prof1@moodle.com", Password = "prof123", Role = UserRole.Professor, CreatedAt = now.AddDays(-28) },
                new User { Id = 3, Email = "lanavukadin@moodle.com", Password = "prof123", Role = UserRole.Professor, CreatedAt = now.AddDays(-28) },
                new User { Id = 4, Email = "student1@moodle.com", Password = "student123", Role = UserRole.Student, CreatedAt = now.AddDays(-25) },
                new User { Id = 5, Email = "student2@moodle.com", Password = "student123", Role = UserRole.Student, CreatedAt = now.AddDays(-24) },
                new User { Id = 6, Email = "student3@moodle.com", Password = "student123", Role = UserRole.Student, CreatedAt = now.AddDays(-23) },
                new User { Id = 7, Email = "student4@moodle.com", Password = "student123", Role = UserRole.Student, CreatedAt = now.AddDays(-22) },
                new User { Id = 8, Email = "student5@moodle.com", Password = "student123", Role = UserRole.Student, CreatedAt = now },
                new User { Id = 9, Email = "student6@moodle.com", Password = "student123", Role = UserRole.Student, CreatedAt = now.AddDays(-10) },
                new User { Id = 10, Email = "prof3@moodle.com", Password = "prof123", Role = UserRole.Professor, CreatedAt = now },
                new User { Id = 11, Email = "student7@moodle.com", Password = "student123", Role = UserRole.Student, CreatedAt = now.AddDays(-1) },
                new User { Id = 12, Email = "student8@moodle.com", Password = "student123", Role = UserRole.Student, CreatedAt = now.AddDays(-2) },
                new User { Id = 13, Email = "prof4@moodle.com", Password = "prof123", Role = UserRole.Professor, CreatedAt = now.AddDays(-5) }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Programiranje 1", ProfessorId = 2, CreatedAt = now.AddDays(-28) },
                new Course { Id = 2, Name = "Objektno programiranje", ProfessorId = 2, CreatedAt = now.AddDays(-27) },
                new Course { Id = 3, Name = "Baze podataka", ProfessorId = 3, CreatedAt = now.AddDays(-26) },
                new Course { Id = 4, Name = "Web programiranje", ProfessorId = 3, CreatedAt = now.AddDays(-25) },
                new Course { Id = 5, Name = "Napredno C#", ProfessorId = 10, CreatedAt = now },
                new Course { Id = 6, Name = "Data Science", ProfessorId = 13, CreatedAt = now.AddDays(-5) },
                new Course { Id = 7, Name = "Machine Learning", ProfessorId = 13, CreatedAt = now.AddDays(-3) }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { UserId = 4, CourseId = 1, EnrolledAt = now.AddDays(-25) },
                new Enrollment { UserId = 5, CourseId = 1, EnrolledAt = now.AddDays(-24) },
                new Enrollment { UserId = 6, CourseId = 2, EnrolledAt = now.AddDays(-23) },
                new Enrollment { UserId = 4, CourseId = 3, EnrolledAt = now },
                new Enrollment { UserId = 7, CourseId = 4, EnrolledAt = now.AddDays(-22) },
                new Enrollment { UserId = 8, CourseId = 2, EnrolledAt = now },
                new Enrollment { UserId = 9, CourseId = 5, EnrolledAt = now.AddDays(-10) },
                new Enrollment { UserId = 11, CourseId = 6, EnrolledAt = now.AddDays(-1) },
                new Enrollment { UserId = 12, CourseId = 6, EnrolledAt = now.AddDays(-2) },
                new Enrollment { UserId = 11, CourseId = 7, EnrolledAt = now.AddDays(-1) }
            );

            modelBuilder.Entity<Message>().HasData(
                new Message { Id = 1, SenderId = 2, ReceiverId = 4, Content = "Dobrodošao na kolegij!", SentAt = now.AddDays(-25) },
                new Message { Id = 2, SenderId = 4, ReceiverId = 2, Content = "Hvala!", SentAt = now.AddDays(-25) },
                new Message { Id = 3, SenderId = 5, ReceiverId = 6, Content = "Jesi li riješio zadatak?", SentAt = now.AddDays(-24) },
                new Message { Id = 4, SenderId = 3, ReceiverId = 7, Content = "Molim te pošalji zadaću.", SentAt = now.AddDays(-22) },
                new Message { Id = 5, SenderId = 8, ReceiverId = 3, Content = "Poslao sam svoj projekt.", SentAt = now },
                new Message { Id = 6, SenderId = 9, ReceiverId = 10, Content = "Pozdrav profesore!", SentAt = now.AddDays(-10) },
                new Message { Id = 7, SenderId = 10, ReceiverId = 9, Content = "Dobrodošao!", SentAt = now.AddDays(-10) },
                new Message { Id = 8, SenderId = 11, ReceiverId = 13, Content = "Pozdrav profesore!", SentAt = now.AddDays(-1) },
                new Message { Id = 9, SenderId = 12, ReceiverId = 13, Content = "Pošaljem zadatak danas.", SentAt = now.AddDays(-2) }
            );
        }
    }
}
