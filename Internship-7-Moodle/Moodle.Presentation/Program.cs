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

services.AddScoped<AuthMenu>();
services.AddScoped<MainMenu>();
services.AddScoped<ChatMenu>();
services.AddScoped<CourseMenu>();
services.AddScoped<StatisticsMenu>();
var provider = services.BuildServiceProvider();

var authMenu = provider.GetRequiredService<AuthMenu>();
await authMenu.StartAsync();
