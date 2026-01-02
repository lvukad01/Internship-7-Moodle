using Moodle.Domain.Entities;
using Moodle.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Moodle.Infrastructure.Database.Seed
{
    public static class DatabaseSeed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "admin@moodle.com", Password = "admin123", Role = UserRole.Admin },
                new User { Id = 2, Email = "prof1@moodle.com", Password = "prof123", Role = UserRole.Professor },
                new User { Id = 3, Email = "prof2@moodle.com", Password = "prof123", Role = UserRole.Professor },
                new User { Id = 4, Email = "student1@moodle.com", Password = "student123", Role = UserRole.Student },
                new User { Id = 5, Email = "student2@moodle.com", Password = "student123", Role = UserRole.Student },
                new User { Id = 6, Email = "student3@moodle.com", Password = "student123", Role = UserRole.Student },
                new User { Id = 7, Email = "student4@moodle.com", Password = "student123", Role = UserRole.Student },
                new User { Id = 8, Email = "student5@moodle.com", Password = "student123", Role = UserRole.Student }
            );

            // Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Programiranje 1", ProfessorId = 2 },
                new Course { Id = 2, Name = "Objektno programiranje", ProfessorId = 2 },
                new Course { Id = 3, Name = "Baze podataka", ProfessorId = 3 },
                new Course { Id = 4, Name = "Web programiranje", ProfessorId = 3 }
            );

            // Enrollments
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { UserId = 4, CourseId = 1, EnrolledAt = DateTime.UtcNow },
                new Enrollment { UserId = 5, CourseId = 1, EnrolledAt = DateTime.UtcNow },
                new Enrollment { UserId = 6, CourseId = 2, EnrolledAt = DateTime.UtcNow },
                new Enrollment { UserId = 4, CourseId = 3, EnrolledAt = DateTime.UtcNow },
                new Enrollment { UserId = 7, CourseId = 4, EnrolledAt = DateTime.UtcNow },
                new Enrollment { UserId = 8, CourseId = 2, EnrolledAt = DateTime.UtcNow }
            );

            // Announcements
            modelBuilder.Entity<Announcement>().HasData(
                new Announcement { Id = 1, Title = "Dobrodošli", Content = "Dobrodošli na Programiranje 1!", CreatedAt = DateTime.UtcNow, CourseId = 1, ProfessorId = 2 },
                new Announcement { Id = 2, Title = "Prvi kolokvij", Content = "Prvi kolokvij održat će se sljedeći tjedan.", CreatedAt = DateTime.UtcNow, CourseId = 2, ProfessorId = 2 },
                new Announcement { Id = 3, Title = "Literatura", Content = "Dodana nova literatura za kolegij.", CreatedAt = DateTime.UtcNow, CourseId = 3, ProfessorId = 3 },
                new Announcement { Id = 4, Title = "Predavanje", Content = "Predavanje iz Web programiranja pomaknuto za utorak.", CreatedAt = DateTime.UtcNow, CourseId = 4, ProfessorId = 3 }
            );

            // Materials
            modelBuilder.Entity<Material>().HasData(
                new Material { Id = 1, Name = "Uvod u C#", Url = "https://example.com/csharp", CreatedAt = DateTime.UtcNow, CourseId = 1, ProfessorId = 2 },
                new Material { Id = 2, Name = "OOP principi", Url = "https://example.com/oop", CreatedAt = DateTime.UtcNow, CourseId = 2, ProfessorId = 2 },
                new Material { Id = 3, Name = "SQL osnove", Url = "https://example.com/sql", CreatedAt = DateTime.UtcNow, CourseId = 3, ProfessorId = 3 },
                new Material { Id = 4, Name = "HTML & CSS", Url = "https://example.com/html-css", CreatedAt = DateTime.UtcNow, CourseId = 4, ProfessorId = 3 },
                new Material { Id = 5, Name = "LINQ u C#", Url = "https://example.com/linq", CreatedAt = DateTime.UtcNow, CourseId = 2, ProfessorId = 2 }
            );

            // Messages
            modelBuilder.Entity<Message>().HasData(
                new Message { Id = 1, SenderId = 2, ReceiverId = 4, Content = "Dobrodošao na kolegij!", SentAt = DateTime.UtcNow },
                new Message { Id = 2, SenderId = 4, ReceiverId = 2, Content = "Hvala!", SentAt = DateTime.UtcNow },
                new Message { Id = 3, SenderId = 5, ReceiverId = 6, Content = "Jesi li riješio zadatak?", SentAt = DateTime.UtcNow },
                new Message { Id = 4, SenderId = 3, ReceiverId = 7, Content = "Molim te pošalji zadaću.", SentAt = DateTime.UtcNow },
                new Message { Id = 5, SenderId = 8, ReceiverId = 3, Content = "Poslao sam svoj projekt.", SentAt = DateTime.UtcNow }
            );
        }
    }
}

