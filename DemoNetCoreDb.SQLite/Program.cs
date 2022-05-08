using DemoNetCoreDb.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine(args);

var services = new ServiceCollection();
services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Information);
    builder.AddConsole();
});
services.AddDbContext<DemoNetCoreDbContext>(option =>
{
    //option.UseInMemoryDatabase(databaseName: "DemoNetCoreDb");
    option.UseSqlite("Data Source=d:/Database/SQLite/DemoNetCoreDb.db;");
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
