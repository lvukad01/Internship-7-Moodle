

using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Entities;
using Moodle.Infrastructure.Persistence.Seed;

namespace Moodle.Infrastructure.Persistence
{
    public class MoodleDbContext : DbContext //mapiranje entiteta na bazu podataka, sluzi u repository za linq upite
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Message> Messages => Set<Message>();

        public MoodleDbContext(DbContextOptions<MoodleDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoodleDbContext).Assembly);
            modelBuilder.HasDefaultSchema("public");
            DatabaseSeed.SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
