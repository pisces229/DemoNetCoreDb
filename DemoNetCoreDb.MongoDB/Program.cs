using DemoNetCoreDb.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

Console.WriteLine(args);

var services = new ServiceCollection();
services.AddLogging(builder => 
{
    builder.SetMinimumLevel(LogLevel.Trace);
    builder.AddConsole();
});
services.AddSingleton(provider =>
{
    var mongoClientSettings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017/");
    mongoClientSettings.MinConnectionPoolSize = 10;
    mongoClientSettings.MaxConnectionPoolSize = 100;
    mongoClientSettings.DirectConnection = true;
    mongoClientSettings.ClusterConfigurator = builder =>
    {
        builder.Subscribe<CommandStartedEvent>(handler => {
            var logger = provider.GetRequiredService<ILogger<DefaultMongoClient>>();
            logger.LogInformation($"{handler.CommandName}");
            logger.LogInformation($"{handler.Command.ToJson()}");
        });
    };
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