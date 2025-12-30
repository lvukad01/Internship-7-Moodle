

using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Entities;
using Moodle.Infrastructure.Database.Seed;

namespace Moodle.Infrastructure.Database
{
    public class MoodleDbContext : DbContext //mapiranje entiteta na bazu podataka, postavljanje seed, sluzi u repository za linq upite
    {
        public MoodleDbContext(DbContextOptions<MoodleDbContext> options) //konstruktor 
            : base(options)
        {
        }
        public DbSet<User> Users => Set<User>(); //DbSet za svaki entitet
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Message> Messages => Set<Message>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoodleDbContext).Assembly); //za configurations
            modelBuilder.HasDefaultSchema("public"); //postavljanje default sheme na public
            DatabaseSeed.SeedData(modelBuilder); //pozivanje seed metode za inicijalne podatke
            base.OnModelCreating(modelBuilder); //pozivanje bazne metode
        }
    }
}
