using DemoNetCoreDb.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

Console.WriteLine(args);

var services = new ServiceCollection();
services.AddLogging(builder => 
{
    builder.SetMinimumLevel(LogLevel.Information);
    builder.AddConsole();
});
services.AddSingleton(c =>
{
    var mongoClientSettings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017/");
    mongoClientSettings.MinConnectionPoolSize = 10;
    mongoClientSettings.MaxConnectionPoolSize = 100;
    mongoClientSettings.DirectConnection = true;
    return new DefaultMongoClient(mongoClientSettings);
});
services.AddScoped(p => p.GetService<DefaultMongoClient>()!.StartSession());
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