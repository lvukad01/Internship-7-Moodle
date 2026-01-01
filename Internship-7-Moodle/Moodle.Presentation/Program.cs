using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moodle.Infrastructure.DI;
using Moodle.Presentation.Menus;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var services = new ServiceCollection();

services.AddInfrastructure(configuration);

// Menus ostaju u Presentation
services.AddScoped<AuthMenu>();
services.AddScoped<MainMenu>();
services.AddScoped<ChatMenu>();
services.AddScoped<CourseMenu>();

var provider = services.BuildServiceProvider();

var authMenu = provider.GetRequiredService<AuthMenu>();
await authMenu.StartAsync();
