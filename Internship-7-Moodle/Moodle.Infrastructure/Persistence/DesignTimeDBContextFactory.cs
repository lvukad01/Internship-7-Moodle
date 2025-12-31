using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Moodle.Infrastructure.Database;

namespace Moodle.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MoodleDbContext>
    {
        public MoodleDbContext CreateDbContext(string[] args)
        {
            var presentationPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Moodle.Presentation");
            Console.WriteLine("Current directory: " + Directory.GetCurrentDirectory());
            Console.WriteLine("Looking for appsettings.json at: " + Path.Combine(presentationPath, "appsettings.json"));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(presentationPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("MoodleDbContext");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'MoodleDbContext' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<MoodleDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new MoodleDbContext(optionsBuilder.Options);
        }
    }
}


