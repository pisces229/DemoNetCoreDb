using DemoNetCoreDb.SqlServer;
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
    option.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=DemoNetCoreDb;User ID=sa;Password=1qaz@WSX;");
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
