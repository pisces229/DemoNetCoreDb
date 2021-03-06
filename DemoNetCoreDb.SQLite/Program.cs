using DemoNetCoreDb.SQLite;
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
    option.UseSqlite("Data Source=d:/Database/SQLite/DemoNetCoreDb.db;");
    option.EnableSensitiveDataLogging();
    option.EnableDetailedErrors();
});
services.AddSingleton<Runner>();

var serviceProvider = services.BuildServiceProvider();
try
{
    using (var dbcontext = serviceProvider.GetRequiredService<DemoNetCoreDbContext>())
    {
        Console.WriteLine($"Database EnsureCreated:[{dbcontext.Database.EnsureCreated()}]");
    }
    serviceProvider.GetRequiredService<Runner>().Run();
}
finally
{
    serviceProvider.Dispose();
}
