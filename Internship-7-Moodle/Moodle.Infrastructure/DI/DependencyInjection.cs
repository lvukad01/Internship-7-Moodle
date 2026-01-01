using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moodle.Application.UseCases.Auth;
using Moodle.Application.UseCases.Courses;
using Moodle.Application.UseCases.Messages;
using Moodle.Application.UseCases.Users;
using Moodle.Domain.Persistence;
using Moodle.Infrastructure.Database;
using Moodle.Infrastructure.Repositories;

namespace Moodle.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<MoodleDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("MoodleDbContext")));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            // Application services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ICourseService, CourseService>();



            return services;
        }
    }
}

