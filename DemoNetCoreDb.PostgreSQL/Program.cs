using DemoNetCoreDb.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine(args);

var services = new ServiceCollection();
services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Trace);
    builder.AddConsole();
});
services.AddDbContext<DemoNetCoreDbContext>(option =>
{
    //option.UseInMemoryDatabase(databaseName: "DemoNetCoreDb");
    option.UseNpgsql("Host=localhost;Database=DemoNetCoreDb;Username=postgres;Password=1234;");
    option.EnableSensitiveDataLogging();
    option.EnableDetailedErrors();
});
services.AddSingleton<Runner>();

var serviceProvider = services.BuildServiceProvider();
try
{
    serviceProvider.GetRequiredService<Runner>().Run();
}
finally
{
    serviceProvider.Dispose();
}
